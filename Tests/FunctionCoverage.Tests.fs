namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting
open System.ComponentModel

/// Tests the features of the `FunctionData` class.
[<TestClass>]
type FunctionDataTests() =

  [<TestMethod; DisplayName("ToString")>]
  member _.TestToString() =
    Assert.AreEqual("FN:0,\nFNDA:0,", string FunctionData.Default)
    Assert.AreEqual("FN:127,main\nFNDA:3,main", string { ExecutionCount = 3; FunctionName = "main"; LineNumber = 127 })

/// Tests the features of the `FunctionCoverage` class.
[<TestClass>]
type FunctionCoverageTests() =

  [<TestMethod; DisplayName("ToString")>]
  member _.TestToString() =
    let data = { ExecutionCount = 3; FunctionName = "main"; LineNumber = 127 }
    Assert.AreEqual("FNF:0\nFNH:0", string FunctionCoverage.Default)
    Assert.AreEqual("FN:127,main\nFNDA:3,main\nFNF:23\nFNH:11", string { FunctionCoverage.Data = [data]; Found = 23; Hit = 11 })
