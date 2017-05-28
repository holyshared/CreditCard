namespace CreditCard.Tests

open NUnit.Framework
open CreditCard.Brand

[<TestFixture>]
type BrandTest() =

  [<Test>]
  member x.TestVISAMatches() =
    Assert.IsTrue(VISA.Matches "411111********11")
    Assert.IsFalse(VISA.Matches("555555********44"))

  [<Test>]
  member x.TestMasterCardMatches() =
    Assert.IsTrue(MasterCard.Matches("555555********44"))
    Assert.IsFalse(MasterCard.Matches("4111************"))

  [<Test>]
  member x.TestAmericanExpressMatches() =
    Assert.IsTrue(AmericanExpress.Matches("378282*******05"))
    Assert.IsTrue(AmericanExpress.Matches("348282*******05"))
    Assert.IsFalse(AmericanExpress.Matches("4111************"))

  [<Test>]
  member x.TestJCBMatches() = 
    Assert.IsTrue(JCB.Matches("353011********00"))
    Assert.IsFalse(JCB.Matches("4111************"))

  [<Test>]
  member x.TestDinersClubMatches() =
    Assert.IsTrue(DinersClub.Matches("300693******04"))
    Assert.IsTrue(DinersClub.Matches("305693******04"))
    Assert.IsTrue(DinersClub.Matches("309593******04"))
    Assert.IsTrue(DinersClub.Matches("365693******04"))
    Assert.IsTrue(DinersClub.Matches("385693******04"))
    Assert.IsTrue(DinersClub.Matches("395693******04"))
    Assert.IsFalse(DinersClub.Matches("4111************"))

  [<Test>]
  member x.TestDetectFrom() =
    Assert.AreEqual(Some VISA, DetectFrom [VISA] "411111********11")
    Assert.AreEqual(None, DetectFrom [VISA] "348282*******05")
