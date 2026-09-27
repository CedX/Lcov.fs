namespace Belin.Lcov

open System
open System.Collections.Generic
open System.Text.RegularExpressions

/// Describes an error that occurs while parsing a LCOV report.
type ParseError =
  | EmptyCoverage
  | InvalidBranchData of line: int
  | InvalidFunctionData of line: int
  | InvalidFunctionName of line: int
  | InvalidLineData of line: int
  | InvalidToken of line: int
  | UnknownToken of line: int * token: string

/// Represents a trace file, that is a coverage report.
type Report(testName: string, ?sourceFiles: SourceFile seq) = // fsharplint:disable-line TypePrefixing

  /// The source file list.
  member val SourceFiles: IList<SourceFile> = ResizeArray(defaultArg sourceFiles []) with get, set

  /// The test name.
  member val TestName: string = testName with get, set

  /// Returns a string representation of this object.
  override this.ToString() =
    let testName = if String.IsNullOrWhiteSpace this.TestName then [] else [$"{Tokens.TestName}:{this.TestName}"]
    let sourceFiles = this.SourceFiles |> Seq.map string |> List.ofSeq
    testName @ sourceFiles |> String.concat "\n"

/// Contains operations for working with LCOV reports.
module Report =

  /// The regular expression used to split the lines.
  let private newLinePattern = Regex @"\r?\n"

  /// Parses the specified coverage data in LCOV format.
  let parse (coverage: string): Result<Report, ParseError> =
    let report = Report ""
    let mutable sourceFile = SourceFile.withCoverage ""

    let mutable error: ParseError option = None
    let mutable offset = 0

    let lines = newLinePattern.Split coverage
    while error.IsNone && offset < lines.Length do
      let line = lines[offset]
      offset <- offset + 1

      if not (String.IsNullOrWhiteSpace line) then
        let parts = line.Trim().Split ':'
        if parts.Length < 2 && parts[0] <> Tokens.EndOfRecord then
          error <- Some (InvalidToken offset)
        else
          let data = (parts[1..] |> String.concat ":").Split ','
          let token = parts[0]

          match token with
          | Tokens.TestName -> if String.IsNullOrWhiteSpace report.TestName then report.TestName <- data[0]
          | Tokens.BranchData ->
            if data.Length < 4 then error <- Some (InvalidBranchData offset)
            else sourceFile.Branches.Value.Data.Add {
              BlockNumber = int data[1]
              BranchNumber = int data[2]
              LineNumber = int data[0]
              Taken = if data[3] = "-" then 0 else int data[3]
            }
          | Tokens.BranchesFound -> sourceFile.Branches.Value.Found <- int data[0]
          | Tokens.BranchesHit -> sourceFile.Branches.Value.Hit <- int data[0]
          | Tokens.FunctionData ->
            if data.Length < 2 then error <- Some (InvalidFunctionData offset)
            else
              let items = sourceFile.Functions.Value.Data
              for index = 0 to items.Count - 1 do
                if items[index].FunctionName = data[1] then items[index] <- { items[index] with ExecutionCount = int data[0] }
          | Tokens.FunctionName ->
            if data.Length < 2 then error <- Some (InvalidFunctionName offset)
            else sourceFile.Functions.Value.Data.Add { ExecutionCount = 0; FunctionName = data[1]; LineNumber = int data[0] }
          | Tokens.FunctionsFound -> sourceFile.Functions.Value.Found <- int data[0]
          | Tokens.FunctionsHit -> sourceFile.Functions.Value.Hit <- int data[0]
          | Tokens.LineData ->
            if data.Length < 2 then error <- Some (InvalidLineData offset)
            else
              let checksum = if data.Length > 2 then data[2] else ""
              sourceFile.Lines.Value.Data.Add { Checksum = checksum; ExecutionCount = int data[1]; LineNumber = int data[0] }
          | Tokens.LinesFound -> sourceFile.Lines.Value.Found <- int data[0]
          | Tokens.LinesHit -> sourceFile.Lines.Value.Hit <- int data[0]
          | Tokens.SourceFile -> sourceFile <- SourceFile.withCoverage data[0]
          | Tokens.EndOfRecord -> report.SourceFiles.Add sourceFile
          | _ -> error <- Some (UnknownToken (offset, token))

    match error with
    | Some parseError -> Error parseError
    | None -> if report.SourceFiles.Count > 0 then Ok report else Error EmptyCoverage
