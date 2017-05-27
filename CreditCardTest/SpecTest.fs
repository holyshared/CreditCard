namespace CreditCard.Tests

open NUnit.Framework
open CreditCard.Spec

[<TestFixture>]
type SpecTest() =

  [<Test>]
  member x.TestPrefixMatches() =
    let prefix = "4"
    Assert.IsTrue(StartsWith prefix "4")
    Assert.IsFalse(StartsWith prefix "1")

  [<Test>]
  member x.TestPrefixRangeMatches() =
    let range = NumberRange(1, 4)
    Assert.IsTrue(RangeOfDigits (range, 1) "4")
    Assert.IsTrue(RangeOfDigits (range, 1) "1")
    Assert.IsFalse(RangeOfDigits (range, 1) "0")
    Assert.IsFalse(RangeOfDigits (range, 1) "5")
