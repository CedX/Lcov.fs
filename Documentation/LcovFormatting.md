# LCOV Formatting
Each type provided by this library has a dedicated `ToString()` method returning
the corresponding data formatted as [LCOV](https://github.com/linux-test-project/lcov) string.
All you have to do is to create the adequate structure using these different types, and to export the final result:

```fsharp
open Belin.Lcov

let functions = FunctionCoverage(Found = 1, Hit = 1)
let lines = LineCoverage(Found = 2, Hit = 2, Data = ResizeArray [
  { LineNumber = 6; ExecutionCount = 2; Checksum = "PF4Rz2r7RTliO9u6bZ7h6g" }
  { LineNumber = 7; ExecutionCount = 2; Checksum = "yGMB6FhEEAd8OyASe3Ni1w" }
])

let sourceFile = SourceFile("/home/CedX/Lcov.fs/Fixture.fs", Functions = Some functions, Lines = Some lines)
let report = Report("Example", [sourceFile])
printfn "%O" report
```

The `Report.ToString()` method will return a [LCOV](https://github.com/linux-test-project/lcov) report formatted like this:

```lcov
TN:Example
SF:/home/CedX/Lcov.fs/Fixture.fs
FNF:1
FNH:1
DA:6,2,PF4Rz2r7RTliO9u6bZ7h6g
DA:7,2,yGMB6FhEEAd8OyASe3Ni1w
LF:2
LH:2
end_of_record
```

> [!TIP]
> See the [source code](https://github.com/CedX/Lcov.fs/tree/main/Sources) of this library
> for detailed information on the available types.
