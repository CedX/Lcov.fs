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
  printn $"The coverage report contains {report.SourceFiles.Count} source files:"
  printn (JsonSerializer.Serialize(report, JsonSerializerOptions(WriteIndented = true)))
