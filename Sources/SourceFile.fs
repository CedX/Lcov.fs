namespace Belin.Lcov

/// Provides the coverage data of a source file.
type SourceFile =
  {
    /// The branch coverage.
    Branches: BranchCoverage option

    /// The function coverage.
    Functions: FunctionCoverage option

    /// The line coverage.
    Lines: LineCoverage option

    /// The path to the source file.
    Path: string
  }

  /// Creates a new instance with default values.
  static member Default =
    { Branches = None; Functions = None; Lines = None; Path = "" }

  /// Returns a string representation of this object.
  override this.ToString() =
    let output = ResizeArray [$"{Tokens.SourceFile}:{this.Path}"]
    match this.Functions with None -> () | Some value -> output.Add(string value)
    match this.Branches with None -> () | Some value -> output.Add(string value)
    match this.Lines with None -> () | Some value -> output.Add(string value)
    output.Add Tokens.EndOfRecord
    output |> String.concat "\n"
