using module PSScriptAnalyzer
using module ./Cmdlets.psm1

"Performing the static analysis of source code..."
Invoke-ScriptAnalyzer $PSScriptRoot -Recurse
Invoke-FSharpLint Lcov.slnx -Configuration Configuration/FSharpLint.json
Test-ModuleManifest Lcov.psd1 | Out-Null
