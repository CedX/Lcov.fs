namespace Belin.Lcov

open System
open System.Text.RegularExpressions

/// Represents a trace file, that is a coverage report.
type Report(testName: string, ?sourceFiles: SourceFile seq) =

  /// The regular expression used to split the lines.
  static let newLinePattern = Regex(@"\r?\n", RegexOptions.Compiled)

  /// The source file list.
  member val SourceFiles = ResizeArray<SourceFile>(defaultArg sourceFiles []) with get, set

  /// The test name.
  member val TestName: string = testName with get, set

  /// Returns a string representation of this object.
  override this.ToString() =
    let testName = if String.IsNullOrWhiteSpace this.TestName then [] else [$"{Tokens.TestName}:{this.TestName}"]
    let sourceFiles = this.SourceFiles |> Seq.map (fun value -> string value) |> List.ofSeq
    testName @ sourceFiles |> String.concat "\n"

  /// Parses the specified coverage data in LCOV format.
  /// Raises a `FormatException` when an error occurs.
  static member Parse (coverage: string) =
    let mutable offset = 0
    let report = Report ""
    let mutable sourceFile = SourceFile.withCoverage ""

    for line in newLinePattern.Split coverage do
      offset <- offset + 1
      if not (String.IsNullOrWhiteSpace line) then
        let parts = line.Trim().Split ':'
        if parts.Length < 2 && parts[0] <> Tokens.EndOfRecord then raise (FormatException $"Invalid token format at line #{offset}.")

        let data = (parts[1..] |> String.concat ":").Split ','
        match parts[0] with
        | Tokens.TestName -> if String.IsNullOrWhiteSpace report.TestName then report.TestName <- data[0]
        | Tokens.BranchData ->
          if data.Length < 4 then raise (FormatException $"Invalid branch data at line #{offset}.")
          sourceFile.Branches.Value.Data.Add {
            BlockNumber = int data[1]
            BranchNumber = int data[2]
            LineNumber = int data[0]
            Taken = if data[3] = "-" then 0 else int data[3]
          }
        | Tokens.BranchesFound -> sourceFile.Branches.Value.Found <- int data[0]
        | Tokens.BranchesHit -> sourceFile.Branches.Value.Hit <- int data[0]
        | Tokens.FunctionData ->
          if data.Length < 2 then raise (FormatException $"Invalid function data at line #{offset}.")
          let items = sourceFile.Functions.Value.Data
          for index = 0 to items.Count - 1 do
            if items[index].FunctionName = data[1] then items[index] <- { items[index] with ExecutionCount = int data[0] }
        | Tokens.FunctionName ->
          if data.Length < 2 then raise (FormatException $"Invalid function name at line #{offset}.")
          sourceFile.Functions.Value.Data.Add { ExecutionCount = 0; FunctionName = data[1]; LineNumber = int data[0] }
        | Tokens.FunctionsFound -> sourceFile.Functions.Value.Found <- int data[0]
        | Tokens.FunctionsHit -> sourceFile.Functions.Value.Hit <- int data[0]
        | Tokens.LineData ->
          if data.Length < 2 then raise (FormatException $"Invalid line data at line #{offset}.")
          sourceFile.Lines.Value.Data.Add { Checksum = (if data.Length > 2 then data[2] else ""); ExecutionCount = int data[1]; LineNumber = int data[0] }
        | Tokens.LinesFound -> sourceFile.Lines.Value.Found <- int data[0]
        | Tokens.LinesHit -> sourceFile.Lines.Value.Hit <- int data[0]
        | Tokens.SourceFile -> sourceFile <- SourceFile.withCoverage data[0]
        | Tokens.EndOfRecord -> report.SourceFiles.Add sourceFile
        | _ -> raise (FormatException $"Unknown token at line #{offset}.")

    if report.SourceFiles.Count > 0 then report else raise (FormatException "The coverage data is empty or invalid.")

/// Contains operations for working with LCOV reports.
module Report =

  /// The regular expression used to split the lines.
  let private newLinePattern = Regex(@"\r?\n", RegexOptions.Compiled)

  /// Parses the specified coverage data in LCOV format.
  let parse (coverage: string): Result<Report, FormatException> =
    try Ok (Report.Parse coverage)
    with :? FormatException as ex -> Error ex

  /// Parses the specified coverage data in LCOV format.
  let parse2 (coverage: string) =
    let mutable offset = 0
    let report = Report ""
    let mutable error: exn option = None
    let mutable sourceFile = SourceFile.withCoverage ""

    for line in newLinePattern.Split coverage do
      offset <- offset + 1
      if not (String.IsNullOrWhiteSpace line) then
        let parts = line.Trim().Split ':'
        if parts.Length < 2 && parts[0] <> Tokens.EndOfRecord then raise (FormatException $"Invalid token format at line #{offset}.")

        let data = (parts[1..] |> String.concat ":").Split ','
        match parts[0] with
        | Tokens.TestName -> if String.IsNullOrWhiteSpace report.TestName then report.TestName <- data[0]
        | Tokens.BranchData ->
          if data.Length < 4 then raise (FormatException $"Invalid branch data at line #{offset}.")
          sourceFile.Branches.Value.Data.Add {
            BlockNumber = int data[1]
            BranchNumber = int data[2]
            LineNumber = int data[0]
            Taken = if data[3] = "-" then 0 else int data[3]
          }
        | Tokens.BranchesFound -> sourceFile.Branches.Value.Found <- int data[0]
        | Tokens.BranchesHit -> sourceFile.Branches.Value.Hit <- int data[0]
        | Tokens.FunctionData ->
          if data.Length < 2 then raise (FormatException $"Invalid function data at line #{offset}.")
          let items = sourceFile.Functions.Value.Data
          for index = 0 to items.Count - 1 do
            if items[index].FunctionName = data[1] then items[index] <- { items[index] with ExecutionCount = int data[0] }
        | Tokens.FunctionName ->
          if data.Length < 2 then raise (FormatException $"Invalid function name at line #{offset}.")
          sourceFile.Functions.Value.Data.Add { ExecutionCount = 0; FunctionName = data[1]; LineNumber = int data[0] }
        | Tokens.FunctionsFound -> sourceFile.Functions.Value.Found <- int data[0]
        | Tokens.FunctionsHit -> sourceFile.Functions.Value.Hit <- int data[0]
        | Tokens.LineData ->
          if data.Length < 2 then raise (FormatException $"Invalid line data at line #{offset}.")
          sourceFile.Lines.Value.Data.Add { Checksum = (if data.Length > 2 then data[2] else ""); ExecutionCount = int data[1]; LineNumber = int data[0] }
        | Tokens.LinesFound -> sourceFile.Lines.Value.Found <- int data[0]
        | Tokens.LinesHit -> sourceFile.Lines.Value.Hit <- int data[0]
        | Tokens.SourceFile -> sourceFile <- SourceFile.withCoverage data[0]
        | Tokens.EndOfRecord -> report.SourceFiles.Add sourceFile
        | _ -> raise (FormatException $"Unknown token at line #{offset}.")

    if report.SourceFiles.Count > 0 then report else raise (FormatException "The coverage data is empty or invalid.")
