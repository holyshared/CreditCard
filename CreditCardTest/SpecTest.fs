namespace CreditCard.Tests

open NUnit.Framework
open CreditCard.Spec

[<TestFixture>]
type SpecTest() =

  [<Test>]
  member x.TestPrefixMatches() =
    let prefix = "4"
    Assert.IsTrue(Prefix.Matches prefix "4")
    Assert.IsFalse(Prefix.Matches prefix "1")

  [<Test>]
  member x.TestPrefixRangeMatches() =
    let range = NumberRange(1, 4)
    Assert.IsTrue(PrefixRange.Matches range 1 "4")
    Assert.IsTrue(PrefixRange.Matches range 1 "1")
    Assert.IsFalse(PrefixRange.Matches range 1 "0")
    Assert.IsFalse(PrefixRange.Matches range 1 "5")
