namespace Belin.Lcov

/// Provides details for function coverage.
type FunctionData =
  {
    /// The execution count.
    ExecutionCount: int

    /// The function name.
    FunctionName: string

    /// The line number of the function start.
    LineNumber: int
  }

  /// Creates a new instance with default values.
  static member Default =
    { ExecutionCount = 0; FunctionName = ""; LineNumber = 0 }

  /// Returns a string representation of this object.
  override this.ToString() =
    [$"{Tokens.FunctionName}:{this.LineNumber},{this.FunctionName}"; $"{Tokens.FunctionData}:{this.ExecutionCount},{this.FunctionName}"] |> String.concat "\n"

/// Provides the coverage data of functions.
type FunctionCoverage =
  {
    /// The coverage data.
    Data: FunctionData list

    /// The number of functions found.
    Found: int

    /// The number of functions hit.
    Hit: int
  }

  /// Creates a new instance with default values.
  static member Default =
    { Data = []; Found = 0; Hit = 0 }

  /// Returns a string representation of this object.
  override this.ToString() =
    let data = this.Data |> List.map (fun value -> string value)
    data @ [$"{Tokens.FunctionsFound}:{this.Found}"; $"{Tokens.FunctionsHit}:{this.Hit}"] |> String.concat "\n"
