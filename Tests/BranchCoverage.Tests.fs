namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `BranchData` class.
[<TestClass>]
type BranchDataTests() =

  [<TestMethod>]
  member _.TestToString() =
    BranchData.Default |> string |> shouldBe "BRDA:0,0,0,-"
    { BlockNumber = 3; BranchNumber = 2; LineNumber = 127; Taken = 1 } |> string |> shouldBe "BRDA:127,3,2,1"

/// Tests the features of the `BranchCoverage` class.
[<TestClass>]
type BranchCoverageTests() =

  [<TestMethod>]
  member _.TestToString() =
    let data = ResizeArray [{ BlockNumber = 3; BranchNumber = 2; LineNumber = 127; Taken = 1 }]
    BranchCoverage() |> string |> shouldBe "BRF:0\nBRH:0"
    BranchCoverage(Data = data, Found = 23, Hit = 11) |> string |> shouldBe $"{data[0]}\nBRF:23\nBRH:11"
