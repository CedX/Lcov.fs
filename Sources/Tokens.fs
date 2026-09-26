namespace Belin.Lcov

/// Provides the list of tokens supported by the parser.
module Tokens =

  /// The coverage data of a branch.
  [<Literal>]
  let BranchData = "BRDA"

  /// The number of branches found.
  [<Literal>]
  let BranchesFound = "BRF"

  /// The number of branches hit.
  [<Literal>]
  let BranchesHit = "BRH"

  /// The end of a section.
  [<Literal>]
  let EndOfRecord = "end_of_record"

  /// The coverage data of a function.
  [<Literal>]
  let FunctionData = "FNDA"

  /// A function name.
  [<Literal>]
  let FunctionName = "FN"

  /// The number of functions found.
  [<Literal>]
  let FunctionsFound = "FNF"

  /// The number of functions hit.
  [<Literal>]
  let FunctionsHit = "FNH"

  /// The coverage data of a line.
  [<Literal>]
  let LineData = "DA"

  /// The number of lines found.
  [<Literal>]
  let LinesFound = "LF"

  /// The number of lines hit.
  [<Literal>]
  let LinesHit = "LH"

  /// The path to a source file.
  [<Literal>]
  let SourceFile = "SF"

  /// A test name.
  [<Literal>]
  let TestName = "TN"
