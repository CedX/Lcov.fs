namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `SourceFile` class.
[<TestClass>]
type SourceFileTests() =

  [<TestMethod>]
  member _.TestToString() =
    SourceFile "" |> string |> shouldBe "SF:\nend_of_record"

    let path = "/home/CedX/Lcov.fs/program.fs"
    let sourceFile = SourceFile.withCoverage path
    sourceFile |> string |> shouldBe $"SF:{path}\n{sourceFile.Functions.Value}\n{sourceFile.Branches.Value}\n{sourceFile.Lines.Value}\nend_of_record"
