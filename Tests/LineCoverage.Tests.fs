namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `LineData` class.
[<TestClass>]
type LineDataTests() =

  [<TestMethod>]
  member _.TestToString() =
    LineData.Default |> string |> shouldBe "DA:0,0"
    { Checksum = "ed076287532e86365e841e92bfc50d8c"; ExecutionCount = 3; LineNumber = 127 } |> string |> shouldBe "DA:127,3,ed076287532e86365e841e92bfc50d8c"

/// Tests the features of the `LineCoverage` class.
[<TestClass>]
type LineCoverageTests() =

  [<TestMethod>]
  member _.TestToString() =
    let data = ResizeArray [{ Checksum = ""; ExecutionCount = 3; LineNumber = 127 }]
    LineCoverage() |> string |> shouldBe "LF:0\nLH:0"
    LineCoverage(Data = data, Found = 23, Hit = 11) |> string |> shouldBe $"{data[0]}\nLF:23\nLH:11"
