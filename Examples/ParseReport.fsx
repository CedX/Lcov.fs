#r "nuget: Belin.Lcov.FSharp"
open Belin.Lcov
open System.IO
open System.Text.Json

// Parses a LCOV report to coverage data.
let result = Report.parse (File.ReadAllText "/path/to/lcov.info")
match result with
| Error parseError ->
  eprintfn "%A" parseError
| Ok report ->
  printfn "The coverage report contains %d source files:" report.SourceFiles.Count
  printfn "%s" (JsonSerializer.Serialize(report, JsonSerializerOptions(WriteIndented = true)))
