using NUnit.Framework;

namespace GoShipUI
{
	[TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
	public class GoShipUI_FTLReeferTests : BaseTest
	{
		[Test]
        public void GOShipReeferNoInsuranceTest()
        {
            var home = HomePage.LoginFTL("Reefer");
            var details = home.
            SignIn().
            EnterDetailsFTL();
            details.ProvideCommodityDetails(true);
            details.EnterRestOfDetails().
            PaymentDetails(6, false, false, false).
            GetConfirmationFTL("Reefer", true);
        }

        [Test]
        public void GOShipReeferYesInsuranceFTLTest()
        {
            var home = HomePage.LoginFTL("Reefer");
            var details = home.
            SignIn().
            EnterDetailsFTL();
            details.ProvideCommodityDetails(true);
            details.EnterRestOfDetails().
            PaymentDetails(6, true, false, false).
            GetConfirmationFTL("Reefer", true);
        }

        [Test]
        public void ClearInputAfterSignInReeferFTLTest()
        {
            var home = HomePage.LoginFTL("Reefer");
            var details = home.
            SignIn().
            EnterDetailsFTL(false, false, false);

            details.ClearInputDetailsPage();

            HomePage.VerifyClearInputHomePageFTL();

            HomePage.LoginFTL("Reefer");
            details.VerifyClearInputDetailsPageFTL();
        }
	}
}
