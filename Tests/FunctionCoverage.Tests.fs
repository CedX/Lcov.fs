namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `FunctionData` class.
[<TestClass>]
type FunctionDataTests() =

  [<TestMethod>]
  member _.TestToString() =
    FunctionData.Default |> string |> shouldBe "FN:0,\nFNDA:0,"
    { ExecutionCount = 3; FunctionName = "main"; LineNumber = 127 } |> string |> shouldBe "FN:127,main\nFNDA:3,main"

/// Tests the features of the `FunctionCoverage` class.
[<TestClass>]
type FunctionCoverageTests() =

  [<TestMethod>]
  member _.TestToString() =
    let data = ResizeArray [{ ExecutionCount = 3; FunctionName = "main"; LineNumber = 127 }]
    FunctionCoverage() |> string |> shouldBe "FNF:0\nFNH:0"
    FunctionCoverage(Data = data, Found = 23, Hit = 11) |> string |> shouldBe "FN:127,main\nFNDA:3,main\nFNF:23\nFNH:11"
