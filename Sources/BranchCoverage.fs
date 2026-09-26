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

  /// Returns a string representation of this object.
  override this.ToString() =
    let value = $"{Tokens.BranchData}:{this.LineNumber},{this.BlockNumber},{this.BranchNumber}"
    if this.Taken > 0 then $"{value},{this.Taken}" else $"{value},-"

/// Provides the coverage data of branches.
type BranchCoverage =
  {
    /// The coverage data.
    Data: BranchData list

    /// The number of branches found.
    Found: int

    /// The number of branches hit.
    Hit: int
  }

  /// Returns a string representation of this object.
  override this.ToString() =
    let data = this.Data |> List.map (fun value -> string value)
    data @ [$"{Tokens.BranchesFound}:{this.Found}"; $"{Tokens.BranchesHit}:{this.Hit}"] |> String.concat "\n"
