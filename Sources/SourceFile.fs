namespace Belin.Lcov

/// Provides the coverage data of a source file.
type SourceFile(path: string) =

  /// The branch coverage.
  member val Branches: BranchCoverage option = None with get, set

  /// The function coverage.
  member val Functions: FunctionCoverage option = None with get, set

  /// The line coverage.
  member val Lines: LineCoverage option = None with get, set

  /// The path to the source file.
  member val Path: string = path with get, set

  /// Returns a string representation of this object.
  override this.ToString () =
    let output = ResizeArray [$"{Tokens.SourceFile}:{this.Path}"]
    match this.Functions with None -> () | Some functions -> output.Add(string functions)
    match this.Branches with None -> () | Some branches -> output.Add(string branches)
    match this.Lines with None -> () | Some lines -> output.Add(string lines)
    output.Add Tokens.EndOfRecord
    output |> String.concat "\n"

/// Contains operations for working with source files.
module SourceFile =

  /// Creates a new instance with default coverage values.
  let withCoverage (path: string) = SourceFile(
    path,
    Branches = Some (BranchCoverage()),
    Functions = Some (FunctionCoverage()),
    Lines = Some (LineCoverage())
  )
