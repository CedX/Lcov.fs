namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting
open System
open System.IO

/// Tests the features of the `Report` class.
[<TestClass>]
type ReportTests() =

  /// The test fixture.
  let coverage = File.ReadAllText(Path.Join(AppContext.BaseDirectory, "../Resources/Lcov.info"))

  [<TestMethod>]
  member _.Parse() =
    let report = match Report.parse coverage with Ok value -> value | Error _ -> failwith "The test report could not be successfully parsed."
    Assert.AreEqual("Example", report.TestName)

    Assert.HasCount(3, report.SourceFiles)
    Assert.AreEqual("/home/CedX/Lcov.fs/Fixture.fs", report.SourceFiles[0].Path)
    Assert.AreEqual("/home/CedX/Lcov.fs/Func1.fs", report.SourceFiles[1].Path)
    Assert.AreEqual("/home/CedX/Lcov.fs/Func2.fs", report.SourceFiles[2].Path)

    let branches = match report.SourceFiles[1].Branches with Some value -> value | None -> failwith "The test report does not have branch coverage."
    Assert.AreEqual(4, branches.Found)
    Assert.AreEqual(4, branches.Hit)
    Assert.HasCount(4, branches.Data)
    Assert.AreEqual(8, branches.Data[0].LineNumber)

    let functions = match report.SourceFiles[1].Functions with Some value -> value | None -> failwith "The test report does not have branch coverage."
    Assert.AreEqual(1, functions.Found)
    Assert.AreEqual(1, functions.Hit)
    Assert.HasCount(1, functions.Data)
    Assert.AreEqual("func1", functions.Data[0].FunctionName)

    let lines = match report.SourceFiles[1].Lines with Some value -> value | None -> failwith "The test report does not have line coverage."
    Assert.AreEqual(9, lines.Found)
    Assert.AreEqual(9, lines.Hit)
    Assert.HasCount(9, lines.Data)
    Assert.AreEqual("5kX7OTfHFcjnS98fjeVqNA", lines.Data[0].Checksum)

    match Report.parse "ZZ" with Error _ -> () | Ok _ -> Assert.Fail "TODO"
    match Report.parse "TN:Example" with Error _ -> () | Ok _ -> Assert.Fail "TODO"

  [<TestMethod>]
  member _.TestToString() =
    let sourceFile = SourceFile.withCoverage ""
    Assert.AreEqual("", string (Report ""))
    Assert.AreEqual($"TN:LcovTest\n{sourceFile}", string (Report("LcovTest", [sourceFile])))
