#r "nuget: Belin.Lcov.FSharp"
open Belin.Lcov
open System.IO
open System.Text.Json

// Parses a LCOV report to coverage data.
let coverage = Report.parse (File.ReadAllText "/path/to/lcov.info")
match coverage with
| Error ex ->
  eprintfn "%s" ex.Message
| Ok report ->
  printfn "The coverage report contains %d source files:" report.SourceFiles.Count
  printfn "%s" (JsonSerializer.Serialize(report, JsonSerializerOptions(WriteIndented = true)))
