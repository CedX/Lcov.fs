# LCOV Parsing
The `Report.Parse()` static method parses a [LCOV](https://github.com/linux-test-project/lcov) coverage report provided as string,
and creates a `Report` instance giving detailed information about this coverage report:

```fsharp
open Belin.Lcov
open System.IO
open System.Text.Json

let coverage = Report.parse (File.ReadAllText "/path/to/lcov.info")
match coverage with
| Error ex ->
  eprintfn "%s" ex.Message
| Ok report ->
  printfn "The coverage report contains %d source files:" report.SourceFiles.Count
  printfn "%s" (JsonSerializer.Serialize(report, JsonSerializerOptions(WriteIndented = true)))
```

> [!NOTE]
> A `FormatException` is thrown if any error occurred while parsing the coverage report.  
> You can also use the convenient `Report.TryParse()` method to avoid the exception handling.

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
