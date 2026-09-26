namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `SourceFile` class.
[<TestClass>]
type SourceFileTests() =

  [<TestMethod>]
  member _.TestToString() =
    let sourceFile = SourceFile(
      "/home/CedX/Lcov.fs/program.fs",
      Branches = Some (BranchCoverage()),
      Functions = Some (FunctionCoverage()),
      Lines = Some (LineCoverage())
    )

    Assert.AreEqual("SF:\nend_of_record", string (SourceFile ""))
    Assert.AreEqual($"SF:/home/CedX/Lcov.fs/program.fs\n{sourceFile.Functions.Value}\n{sourceFile.Branches.Value}\n{sourceFile.Lines.Value}\nend_of_record", string sourceFile)
