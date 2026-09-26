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
  override this.ToString() =
    let output = ResizeArray [$"{Tokens.SourceFile}:{this.Path}"]
    match this.Functions with None -> () | Some value -> output.Add(string value)
    match this.Branches with None -> () | Some value -> output.Add(string value)
    match this.Lines with None -> () | Some value -> output.Add(string value)
    output.Add Tokens.EndOfRecord
    output |> String.concat "\n"
