namespace Belin.Lcov

open System.Collections.Generic

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
  override this.ToString () =
    [$"{Tokens.FunctionName}:{this.LineNumber},{this.FunctionName}"; $"{Tokens.FunctionData}:{this.ExecutionCount},{this.FunctionName}"] |> String.concat "\n"

/// Provides the coverage data of functions.
type FunctionCoverage () =

  /// The coverage data.
  member val Data: IList<FunctionData> = ResizeArray() with get, set

  /// The number of functions found.
  member val Found = 0 with get, set

  /// The number of functions hit.
  member val Hit = 0 with get, set

  /// Returns a string representation of this object.
  override this.ToString () =
    let data = this.Data |> Seq.map string |> List.ofSeq
    data @ [$"{Tokens.FunctionsFound}:{this.Found}"; $"{Tokens.FunctionsHit}:{this.Hit}"] |> String.concat "\n"
