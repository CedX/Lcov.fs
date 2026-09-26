namespace Belin.Lcov

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
type LineCoverage =
  {
    /// The coverage data.
    Data: LineData list

    /// The number of lines found.
    Found: int

    /// The number of lines hit.
    Hit: int
  }

  /// Creates a new instance with default values.
  static member Default =
    { Data = []; Found = 0; Hit = 0 }

  /// Returns a string representation of this object.
  override this.ToString() =
    let data = this.Data |> List.map (fun value -> string value)
    data @ [$"{Tokens.LinesFound}:{this.Found}"; $"{Tokens.LinesHit}:{this.Hit}"] |> String.concat "\n"
