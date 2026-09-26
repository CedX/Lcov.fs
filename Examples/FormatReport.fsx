#r "nuget: Belin.Lcov.FSharp"
open Belin.Lcov

// Formats coverage data as LCOV report.
let functions = FunctionCoverage(Found = 1, Hit = 1)
let lines = LineCoverage(Found = 2, Hit = 2, Data = ResizeArray [
  { LineNumber = 6; ExecutionCount = 2; Checksum = "PF4Rz2r7RTliO9u6bZ7h6g" }
  { LineNumber = 7; ExecutionCount = 2; Checksum = "yGMB6FhEEAd8OyASe3Ni1w" }
])

let sourceFile = SourceFile("/home/CedX/Lcov.fs/Fixture.fs", Functions = Some functions, Lines = Some lines)
let report = Report("Example", [sourceFile])
printfn "%O" report
