namespace Belin.Lcov

open System.Collections.Generic

/// Provides details for line coverage.
type LineData =
  {
    /// The data checksum.
    Checksum: string

    /// The execution count.
    ExecutionCount: int

    /// The line number.
    LineNumber: int
  }

  /// Creates a new instance with default values.
  static member Default =
    { Checksum = ""; ExecutionCount = 0; LineNumber = 0 }

  /// Returns a string representation of this object.
  override this.ToString() =
    let value = $"{Tokens.LineData}:{this.LineNumber},{this.ExecutionCount}";
    if this.Checksum.Length > 0 then $"{value},{this.Checksum}" else value

/// Provides the coverage data of lines.
type LineCoverage() =

  /// The coverage data.
  member val Data: IList<LineData> = ResizeArray() with get, set

  /// The number of lines found.
  member val Found = 0 with get, set

  /// The number of lines hit.
  member val Hit = 0 with get, set

  /// Returns a string representation of this object.
  override this.ToString() =
    let data = this.Data |> Seq.map string |> List.ofSeq
    data @ [$"{Tokens.LinesFound}:{this.Found}"; $"{Tokens.LinesHit}:{this.Hit}"] |> String.concat "\n"
