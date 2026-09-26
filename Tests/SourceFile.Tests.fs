namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Tests the features of the `SourceFile` class.
[<TestClass>]
type SourceFileTests() =

  [<TestMethod>]
  member _.TestToString() =
    let sourceFile = {
      Branches = Some BranchCoverage.Default
      Functions = Some FunctionCoverage.Default
      Lines = Some LineCoverage.Default
      Path = "/home/CedX/Lcov.fs/program.fs"
    }

    Assert.AreEqual("SF:\nend_of_record", string SourceFile.Default)
    Assert.AreEqual($"SF:/home/CedX/Lcov.fs/program.fs\n{sourceFile.Functions.Value}\n{sourceFile.Branches.Value}\n{sourceFile.Lines.Value}\nend_of_record", string sourceFile)
