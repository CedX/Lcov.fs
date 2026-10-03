namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Contains assertions for working with unit tests.
[<AutoOpen>]
module Assertions =

  /// Tests whether the specified values are equal.
  let inline shouldBe (expected: 'T) (actual: 'T) =
    Assert.AreEqual<'T>(expected, actual)

  /// Tests whether the specified value is an empty string.
  let inline shouldBeEmptyString (actual: string) =
    Assert.AreEqual(0, actual.Length)

  /// Tests whether the specified collection has the expected count/length.
  let inline shouldHaveCount (expected: int) (actual: 'T seq) =
    Assert.HasCount<'T>(expected, actual)
