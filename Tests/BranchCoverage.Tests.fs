namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `BranchData` class.
[<TestClass>]
type BranchDataTests() =

  [<TestMethod>]
  member _.TestToString() =
    BranchData.Default |> string |> Should.be "BRDA:0,0,0,-"
    { BlockNumber = 3; BranchNumber = 2; LineNumber = 127; Taken = 1 } |> string |> Should.be "BRDA:127,3,2,1"

/// Tests the features of the `BranchCoverage` class.
[<TestClass>]
type BranchCoverageTests() =

  [<TestMethod>]
  member _.TestToString() =
    let data = ResizeArray [{ BlockNumber = 3; BranchNumber = 2; LineNumber = 127; Taken = 1 }]
    BranchCoverage() |> string |> Should.be "BRF:0\nBRH:0"
    BranchCoverage(Data = data, Found = 23, Hit = 11) |> string |> Should.be $"{data[0]}\nBRF:23\nBRH:11"
