using CreditCard;
using NUnit.Framework;

namespace CreditCard.UnitTest
{
    [TestFixture()]
    public class Test
    {

        [Test()]
        public void TestVISAMatches()
        {
            Assert.IsTrue(VISA.matches("411111********11"));
        }

        [Test()]
        public void TestMasterCardMatches()
        {
            Assert.IsTrue(MasterCard.matches("555555********44"));
        }

        [Test()]
        public void TestAmericanExpressMatches()
        {
            Assert.IsTrue(AmericanExpress.matches("378282*******05"));
            Assert.IsTrue(AmericanExpress.matches("348282*******05"));
        }

        [Test()]
        public void TestJCBMatches()
        {
            Assert.IsTrue(JCB.matches("353011********00"));
        }

        [Test()]
        public void TestDinersClubMatches()
        {
            Assert.IsTrue(DinersClub.matches("300693******04"));
            Assert.IsTrue(DinersClub.matches("305693******04"));
            Assert.IsTrue(DinersClub.matches("309593******04"));
            Assert.IsTrue(DinersClub.matches("365693******04"));
            Assert.IsTrue(DinersClub.matches("385693******04"));
            Assert.IsTrue(DinersClub.matches("395693******04"));
        }
    }
}
