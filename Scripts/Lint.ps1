using module PSScriptAnalyzer
using module ./Cmdlets.psm1

"Performing the static analysis of source code..."
Invoke-FSharpLint Lcov.slnx -Configuration Configuration/FSharpLint.json
Invoke-ScriptAnalyzer $PSScriptRoot -Recurse
Test-ModuleManifest Lcov.psd1 | Out-Null
