namespace Belin.Lcov

/// Provides details for branch coverage.
type BranchData =
  {
    /// The block number.
    BlockNumber: int

    /// The branch number.
    BranchNumber: int

    /// The line number.
    LineNumber: int

    /// A number indicating how often this branch was taken.
    Taken: int
  }

  /// Creates a new instance with default values.
  static member Default =
    { BlockNumber = 0; BranchNumber = 0; LineNumber = 0; Taken = 0 }

  /// Returns a string representation of this object.
  override this.ToString() =
    let value = $"{Tokens.BranchData}:{this.LineNumber},{this.BlockNumber},{this.BranchNumber}"
    if this.Taken > 0 then $"{value},{this.Taken}" else $"{value},-"

/// Provides the coverage data of branches.
type BranchCoverage() =

  /// The coverage data.
  member val Data = ResizeArray<BranchData>() with get, set

  /// The number of branches found.
  member val Found = 0 with get, set

  /// The number of branches hit.
  member val Hit = 0 with get, set

  /// Returns a string representation of this object.
  override this.ToString() =
    let data = this.Data |> Seq.map (fun value -> string value) |> List.ofSeq
    data @ [$"{Tokens.BranchesFound}:{this.Found}"; $"{Tokens.BranchesHit}:{this.Hit}"] |> String.concat "\n"
