# LCOV Parsing
The `Report.parse` function parses a [LCOV](https://github.com/linux-test-project/lcov) coverage report provided as string,
and creates a `Report` instance giving detailed information about this coverage report:

```fsharp
open Belin.Lcov
open System.IO
open System.Text.Json

let result = Report.parse (File.ReadAllText "/path/to/lcov.info")
match result with
| Error parseError ->
  eprintfn "%A" parseError
| Ok report ->
  printfn "The coverage report contains %d source files:" report.SourceFiles.Count
  printfn "%s" (JsonSerializer.Serialize(report, JsonSerializerOptions(WriteIndented = true)))
```

> [!NOTE]
> A `ParseError` is returned if any error occurred while parsing the coverage report.  

Converting the `Report` instance to [JSON](https://www.json.org) format will return a structure like this:

```json
{
  "TestName": "Example",
  "SourceFiles": [
    {
      "Path": "/home/CedX/Lcov.fs/Fixture.fs",
      "Branches": {
        "Found": 0,
        "Hit": 0,
        "Data": []
      },
      "Functions": {
        "Found": 1,
        "Hit": 1,
        "Data": [
          {"FunctionName": "main", "LineNumber": 4, "ExecutionCount": 2}
        ]
      },
      "Lines": {
        "Found": 2,
        "Hit": 2,
        "Data": [
          {"LineNumber": 6, "ExecutionCount": 2, "Checksum": "PF4Rz2r7RTliO9u6bZ7h6g"},
          {"LineNumber": 9, "ExecutionCount": 2, "Checksum": "y7GE3Y4FyXCeXcrtqgSVzw"}
        ]
      }
    }
  ]
}
```
