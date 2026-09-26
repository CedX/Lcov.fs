namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `BranchData` class.
[<TestClass>]
type BranchDataTests() =

  [<TestMethod>]
  member _.TestToString() =
    Assert.AreEqual("BRDA:0,0,0,-", string BranchData.Default)
    Assert.AreEqual("BRDA:127,3,2,1", string { BlockNumber = 3; BranchNumber = 2; LineNumber = 127; Taken = 1 })

/// Tests the features of the `BranchCoverage` class.
[<TestClass>]
type BranchCoverageTests() =

  [<TestMethod>]
  member _.TestToString() =
    let data = ResizeArray [{ BlockNumber = 3; BranchNumber = 2; LineNumber = 127; Taken = 1 }]
    Assert.AreEqual("BRF:0\nBRH:0", string (BranchCoverage()))
    Assert.AreEqual($"{data[0]}\nBRF:23\nBRH:11", string (BranchCoverage(Data = data, Found = 23, Hit = 11)))
