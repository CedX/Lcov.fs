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
    report.TestName |> shouldBe "Example"

    // It should contain three source files.
    report.SourceFiles.Count |> shouldBe 3
    report.SourceFiles[0].Path |> shouldBe "/home/CedX/Lcov.fs/Fixture.fs"
    report.SourceFiles[1].Path |> shouldBe "/home/CedX/Lcov.fs/Func1.fs"
    report.SourceFiles[2].Path |> shouldBe "/home/CedX/Lcov.fs/Func2.fs"

    // It should have detailed branch coverage.
    let branches = report.SourceFiles[1].Branches.Value
    branches.Found |> shouldBe 4
    branches.Hit |> shouldBe 4
    branches.Data.Count |> shouldBe 4
    branches.Data[0].LineNumber |> shouldBe 8

    // It should have detailed function coverage.
    let functions = report.SourceFiles[1].Functions.Value
    functions.Found |> shouldBe 1
    functions.Hit |> shouldBe 1
    functions.Data.Count |> shouldBe 1
    functions.Data[0].FunctionName |> shouldBe "func1"

    // It should have detailed line coverage.
    let lines = report.SourceFiles[1].Lines.Value
    lines.Found |> shouldBe 9
    lines.Hit |> shouldBe 9
    lines.Data.Count |> shouldBe 9
    lines.Data[0].Checksum |> shouldBe "5kX7OTfHFcjnS98fjeVqNA"

    match Report.parse "ZZ" with
    | Error (InvalidToken line) -> line |> shouldBe 1
    | _ -> Assert.Fail "It should return an `InvalidToken` error when the input is invalid."

    match Report.parse "TN:Example" with
    | Error EmptyCoverage -> ()
    | _ -> Assert.Fail "It should return an `EmptyCoverage` error when the report is empty."

  [<TestMethod>]
  member _.TestToString() =
    let sourceFile = SourceFile.withCoverage ""
    Report "" |> string |> shouldBeEmptyString
    Report("LcovTest", [sourceFile]) |> string |> shouldBe $"TN:LcovTest\n{sourceFile}"
