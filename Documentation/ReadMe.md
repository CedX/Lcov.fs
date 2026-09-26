# LCOV Reports for F#
Parse and format [LCOV](https://github.com/linux-test-project/lcov) coverage reports,
in [F#](https://learn.microsoft.com/en-us/dotnet/fsharp).
	
## Quick start
Install the latest version of **LCOV Reports for F#** with [NuGet](https://www.nuget.org) package manager:

```powershell
dotnet package add Belin.Lcov.FSharp
```

For detailed instructions, see the [installation guide](Installation.md).

## Usage
This library provides a set of [F#](https://learn.microsoft.com/en-us/dotnet/fsharp) classes representing
a [LCOV](https://github.com/linux-test-project/lcov) coverage report and its data.  
The `Report` class, the main one, provides the parsing and formatting features.  

For more details, please refer to the following pages:

- [Parse coverage data from a LCOV file](LcovParsing.md)
- [Format coverage data to the LCOV format](LcovFormatting.md)
