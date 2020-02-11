using NUnit.Framework;

namespace GoShipUI
{
	[TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class GoShipUI_FTLFlatbedTests : BaseTest
    {
        [Test]
        public void GOShipFlatbedNoInsuranceTest()
        {
            var home = HomePage.LoginFTL("Flatbed");
            var details = home.
            SignIn().
            EnterDetailsFTL();
            details.ProvideCommodityDetails(false);
            details.EnterRestOfDetails().
            PaymentDetails(4, false, false, false).
            GetConfirmationFTL("Flatbed", true);
        }

        [Test]
        public void GOShipFlatbedYesInsuranceFTLTest()
        {
            var home = HomePage.LoginFTL("Flatbed");
            var details = home.
            SignIn().
            EnterDetailsFTL();
            details.ProvideCommodityDetails(false);
            details.EnterRestOfDetails().
            PaymentDetails(4, true, false, false).
            GetConfirmationFTL("Flatbed", true);
        }

        [Test]
        //GS-261-TC_01
        public void SignUpNewUser()
        {
            var home = HomePage.LoginFTL("Flatbed");
            home.SignUp(true);
        }

        [Test]
        //GS-383-TC_03
        public void ClearInputBeforeSignInFTLTest()
        {
            HomePage.ProvideDetailsFTL();
            HomePage.ClearInputHomePage();
            HomePage.VerifyClearInputHomePageFTL();
        }

        [Test]
        //GS-491-TC_03
        public void ClearInputAfterSignInFlatbedFTLTest()
        {
            var home = HomePage.LoginFTL("Flatbed");
            var details = home.
            SignIn().
            EnterDetailsFTL(false, false, false);

            details.ClearInputDetailsPage();

            HomePage.VerifyClearInputHomePageFTL();
			
            HomePage.LoginFTL("Flatbed");
            details.VerifyClearInputDetailsPageFTL();
        }
    }
}