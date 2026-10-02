namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `LineData` class.
[<TestClass>]
type LineDataTests () =

  [<TestMethod>]
  member _.TestToString () =
    Assert.AreEqual ("DA:0,0", string LineData.Default)
    Assert.AreEqual ("DA:127,3,ed076287532e86365e841e92bfc50d8c", string {
      Checksum = "ed076287532e86365e841e92bfc50d8c"; ExecutionCount = 3; LineNumber = 127
    })

/// Tests the features of the `LineCoverage` class.
[<TestClass>]
type LineCoverageTests () =

  [<TestMethod>]
  member _.TestToString () =
    let data = ResizeArray [{ Checksum = ""; ExecutionCount = 3; LineNumber = 127 }]
    Assert.AreEqual ("LF:0\nLH:0", string (LineCoverage()))
    Assert.AreEqual ($"{data[0]}\nLF:23\nLH:11", string (LineCoverage(Data = data, Found = 23, Hit = 11)))
