using NUnit.Framework;
using CreditCard;

namespace CreditCard.UnitTest
{
    [TestFixture()]
    public class Test
    {

		[Test()]
		public void TestVISAMatches()
		{
            Assert.IsTrue(CreditCard.VISA.matches("411111********11"));
        }

        [Test()]
		public void TestMasterCardMatches()
		{
            Assert.IsTrue(CreditCard.MasterCard.matches("555555********44"));
		}

		[Test()]
		public void TestAmericanExpressMatches()
		{
			Assert.IsTrue(CreditCard.AmericanExpress.matches("378282*******05"));
			Assert.IsTrue(CreditCard.AmericanExpress.matches("348282*******05"));
		}

		[Test()]
		public void TestJCBMatches()
		{
            Assert.IsTrue(CreditCard.JCB.matches("353011********00"));
		}

		[Test()]
		public void TestDinersClubMatches()
		{
			Assert.IsTrue(CreditCard.DinersClub.matches("300693******04"));
			Assert.IsTrue(CreditCard.DinersClub.matches("305693******04"));
			Assert.IsTrue(CreditCard.DinersClub.matches("309593******04"));
			Assert.IsTrue(CreditCard.DinersClub.matches("365693******04"));
			Assert.IsTrue(CreditCard.DinersClub.matches("385693******04"));
			Assert.IsTrue(CreditCard.DinersClub.matches("395693******04"));
        }
    }
}
