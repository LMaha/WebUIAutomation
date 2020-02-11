using NUnit.Framework;

namespace GoShipUI
{
	[TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
	public class GoShipUI_FTLVanTests : BaseTest
	{
		[Test]
        public void GOShipVanNoInsuranceTest()
        {
            var home = HomePage.LoginFTL("Van");
            var details = home.
            SignIn().
            EnterDetailsFTL();
            details.ProvideCommodityDetails(false);
            details.EnterRestOfDetails().
            PaymentDetails(5, false, false, false).
            GetConfirmationFTL("Van", true);
        }

        [Test]
        public void GOShipVanYesInsuranceFTLTest()
        {
            var home = HomePage.LoginFTL("Van");
            var details = home.
            SignIn().
            EnterDetailsFTL();
            details.ProvideCommodityDetails(false);
            details.EnterRestOfDetails().
            PaymentDetails(5, true, false, false).
            GetConfirmationFTL("Van", true);
        }

        [Test]
        public void ClearInputAfterSignInVanFTLTest()
        {
            var home = HomePage.LoginFTL("Van");
            var details = home.
            SignIn().
            EnterDetailsFTL(false, false, false);

            details.ClearInputDetailsPage();

            HomePage.VerifyClearInputHomePageFTL();

            HomePage.LoginFTL("Van");
            details.VerifyClearInputDetailsPageFTL();
        }
	}
}
