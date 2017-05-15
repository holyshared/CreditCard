namespace CreditCard.Tests

open System
open NUnit.Framework
open CreditCard

[<TestFixture>]
type Test() = 

  [<Test>]
  member x.TestVISAMatches() =
    Assert.IsTrue(VISA.matches("411111********11"))

  [<Test>]
  member x.TestMasterCardMatches() =
    Assert.IsTrue(MasterCard.matches("555555********44"))

  [<Test>]
  member x.TestAmericanExpressMatches() =
    Assert.IsTrue(AmericanExpress.matches("378282*******05"))
    Assert.IsTrue(AmericanExpress.matches("348282*******05"))

  [<Test>]
  member x.TestJCBMatches() = 
    Assert.IsTrue(JCB.matches("353011********00"))

  [<Test>]
  member x.TestDinersClubMatches() =
    Assert.IsTrue(DinersClub.matches("300693******04"))
    Assert.IsTrue(DinersClub.matches("305693******04"))
    Assert.IsTrue(DinersClub.matches("309593******04"))
    Assert.IsTrue(DinersClub.matches("365693******04"))
    Assert.IsTrue(DinersClub.matches("385693******04"))
    Assert.IsTrue(DinersClub.matches("395693******04"))
