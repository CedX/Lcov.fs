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

    // It should have a test name.
    Assert.AreEqual("Example", report.TestName)

    // It should contain three source files.
    Assert.HasCount(3, report.SourceFiles)
    Assert.AreEqual("/home/CedX/Lcov.fs/Fixture.fs", report.SourceFiles[0].Path)
    Assert.AreEqual("/home/CedX/Lcov.fs/Func1.fs", report.SourceFiles[1].Path)
    Assert.AreEqual("/home/CedX/Lcov.fs/Func2.fs", report.SourceFiles[2].Path)

    // It should have detailed branch coverage.
    let branches = report.SourceFiles[1].Branches.Value
    Assert.AreEqual(4, branches.Found)
    Assert.AreEqual(4, branches.Hit)
    Assert.HasCount(4, branches.Data)
    Assert.AreEqual(8, branches.Data[0].LineNumber)

    // It should have detailed function coverage.
    let functions = report.SourceFiles[1].Functions.Value
    Assert.AreEqual(1, functions.Found)
    Assert.AreEqual(1, functions.Hit)
    Assert.HasCount(1, functions.Data)
    Assert.AreEqual("func1", functions.Data[0].FunctionName)

    // It should have detailed line coverage.
    let lines = report.SourceFiles[1].Lines.Value
    Assert.AreEqual(9, lines.Found)
    Assert.AreEqual(9, lines.Hit)
    Assert.HasCount(9, lines.Data)
    Assert.AreEqual("5kX7OTfHFcjnS98fjeVqNA", lines.Data[0].Checksum)

    match Report.parse "ZZ" with
    | Error (InvalidToken line) -> Assert.AreEqual(1, line)
    | _ -> Assert.Fail "It should return an `InvalidToken` error when the input is invalid."

    match Report.parse "TN:Example" with
    | Error EmptyCoverage -> ()
    | _ -> Assert.Fail "It should return an `EmptyCoverage` error when the report is empty."

  [<TestMethod>]
  member _.TestToString() =
    let sourceFile = SourceFile.withCoverage ""
    Assert.AreEqual("", string (Report ""))
    Assert.AreEqual($"TN:LcovTest\n{sourceFile}", string (Report("LcovTest", [sourceFile])))
