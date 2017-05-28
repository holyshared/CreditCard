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
    let range = Range (1, 4)
    Assert.IsTrue(RangeOfDigits (range, 1) "4")
    Assert.IsTrue(RangeOfDigits (range, 1) "1")
    Assert.IsFalse(RangeOfDigits (range, 1) "0")
    Assert.IsFalse(RangeOfDigits (range, 1) "5")

  [<Test>]
  member x.TestDigits() =
    let digits = Digits [4; 4; 4; 4]
    Assert.AreEqual(16, digits.Length)
    Assert.AreEqual("4111-****-****-****", digits.Format("4111************"))

  [<Test>]
  member x.TestMatchAll() =
    let matcher = MatchAll [StartsWith "4"]
    Assert.IsTrue(matcher "4")
    Assert.IsFalse(matcher "5")
