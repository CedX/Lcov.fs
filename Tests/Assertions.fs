namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

/// Contains assertions for working with unit tests.
[<AutoOpen>]
module Assertions =

  /// Tests whether the specified values are equal.
  let inline shouldBe (expected: 'T) (actual: 'T) =
    Assert.AreEqual<'T>(expected, actual)

  /// Tests whether the specified collection is empty.
  let inline shouldBeEmpty (collection: 'T seq) =
    Assert.IsEmpty collection

  /// Tests whether the specified string is empty.
  let inline shouldBeEmptyString (actual: string) =
    Assert.AreEqual(0, actual.Length)

  /// Tests whether the specified condition is false.
  let inline shouldBeFalse (condition: bool) =
    Assert.IsFalse condition

  /// Tests whether the specified object is `null`.
  let inline shouldBeNull (value: objnull) =
    Assert.IsNull value

  /// Tests whether two sequences contain equal elements in the same order.
  let inline shouldBeSequence (expected: 'T seq) (actual: 'T seq) =
    Assert.AreSequenceEqual(expected, actual)

  /// Tests whether the specified condition is true.
  let inline shouldBeTrue (condition: bool) =
    Assert.IsTrue condition

  /// Tests whether the specified string ends with the given suffix.
  let inline shouldEndWith (expectedSuffix: string) (value: string) =
    Assert.EndsWith(expectedSuffix, value)

  /// Tests whether the specified values are unequal.
  let inline shouldNotBe (notExpected: 'T) (actual: 'T) =
    Assert.AreNotEqual<'T>(notExpected, actual)

  /// Tests whether the specified object is not `null`.
  let inline shouldNotBeNull (value: objnull) =
    Assert.IsNotNull value

  /// Tests whether the specified string begins with the given prefix.
  let inline shouldStartWith (expectedPrefix: string) (value: string) =
    Assert.StartsWith(expectedPrefix, value)
