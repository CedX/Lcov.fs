namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting
open System
open System.IO

/// Tests the features of the `Report` class.
[<TestClass>]
type ReportTests() =

  /// The test fixture.
  let coverage = Path.Join(AppContext.BaseDirectory, "../Resources/Lcov.info") |> File.ReadAllText

  [<TestMethod>]
  member _.Parse() =
    let report = match Report.parse coverage with Ok value -> value | Error _ -> failwith "The test report could not be successfully parsed."

    // It should have a test name.
    report.TestName |> Should.be "Example"

    // It should contain three source files.
    report.SourceFiles.Count |> Should.be 3
    report.SourceFiles[0].Path |> Should.be "/home/CedX/Lcov.fs/Fixture.fs"
    report.SourceFiles[1].Path |> Should.be "/home/CedX/Lcov.fs/Func1.fs"
    report.SourceFiles[2].Path |> Should.be "/home/CedX/Lcov.fs/Func2.fs"

    // It should have detailed branch coverage.
    let branches = report.SourceFiles[1].Branches.Value
    branches.Found |> Should.be 4
    branches.Hit |> Should.be 4
    branches.Data.Count |> Should.be 4
    branches.Data[0].LineNumber |> Should.be 8

    // It should have detailed function coverage.
    let functions = report.SourceFiles[1].Functions.Value
    functions.Found |> Should.be 1
    functions.Hit |> Should.be 1
    functions.Data.Count |> Should.be 1
    functions.Data[0].FunctionName |> Should.be "func1"

    // It should have detailed line coverage.
    let lines = report.SourceFiles[1].Lines.Value
    lines.Found |> Should.be 9
    lines.Hit |> Should.be 9
    lines.Data.Count |> Should.be 9
    lines.Data[0].Checksum |> Should.be "5kX7OTfHFcjnS98fjeVqNA"

    match Report.parse "ZZ" with
    | Error (InvalidToken line) -> line |> Should.be 1
    | _ -> Assert.Fail "It should return an `InvalidToken` error when the input is invalid."

    match Report.parse "TN:Example" with
    | Error EmptyCoverage -> ()
    | _ -> Assert.Fail "It should return an `EmptyCoverage` error when the report is empty."

  [<TestMethod>]
  member _.TestToString() =
    let sourceFile = SourceFile.withCoverage ""
    Report "" |> string |> Should.beEmpty
    Report("LcovTest", [sourceFile]) |> string |> Should.be $"TN:LcovTest\n{sourceFile}"
