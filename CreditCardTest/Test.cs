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
            Assert.IsTrue(CreditCard.VISA.matches("4***************"));
		}

        [Test()]
		public void TestMasterCardMatches()
		{
            Assert.IsTrue(CreditCard.MasterCard.matches("5***************"));
		}
	}
}
