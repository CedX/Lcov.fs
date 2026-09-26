namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting
open System.ComponentModel

/// Tests the features of the `LineData` class.
[<TestClass>]
type LineDataTests() =

  [<TestMethod; DisplayName("ToString")>]
  member _.TestToString() =
    Assert.AreEqual("DA:0,0", string LineData.Default)
    Assert.AreEqual("DA:127,3,ed076287532e86365e841e92bfc50d8c", string { Checksum = "ed076287532e86365e841e92bfc50d8c"; ExecutionCount = 3; LineNumber = 127 })

/// Tests the features of the `LineCoverage` class.
[<TestClass>]
type LineCoverageTests() =

  [<TestMethod; DisplayName("ToString")>]
  member _.TestToString() =
    let data = { Checksum = ""; ExecutionCount = 3; LineNumber = 127 }
    Assert.AreEqual("LF:0\nLH:0", string LineCoverage.Default)
    Assert.AreEqual($"{data}\nLF:23\nLH:11", string { LineCoverage.Data = [data]; Found = 23; Hit = 11 })
