using NUnit.Framework;
using System;

namespace GoShipUI
{
	[TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
	public class GoShipUI_LTLNoLoadsTests : BaseTest
	{
		[Test]
        //GS-282-TC_01
        public void OrderSummaryLTLTest()
        {
			var loginPage = HomePage.LoginLTL();           
            OrderSummary.OrderSummary();
            var getQuote = HomePage.GetQuoteInfo();
            var details = loginPage.SignIn();
            OrderSummary.OrderSummary();
            details.EnterDetailsLTL();
            OrderSummary.OrderSummary();
        }

        [Test]
        //GS-261-TC_01
        public void SignUpNewUser()
        {
			var loginPage = HomePage.LoginLTL();
            var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            loginPage.
            SignUp(false);

        }
        public void GetQuoteInfo(GetQuote getQuote)
        {
            InputData.Data.CarrierName = getQuote.GetCarrierName;
            var QuoteValue = getQuote.GetQuotes.Replace("$", "");
            QuoteValue = getQuote.GetQuotes.Replace("USD", "");
            InputData.Data.QuoteNoLoad = Convert.ToDouble(QuoteValue);
        }

        [Test]     
        //GS-385-TC_01
        public void CheckFoodGroceryPickupErrorMessageTest()
        {
			HomePage.DisplayFoodGroceryPickupErrorMessage();
        }

		[Test]     
        //GS-385-TC_02
        public void CheckTradeShowPickupErrorMessageTest()
        {
			HomePage.DisplayTradeShowsPickupErrorMessage();
        }

		[Test] 
		//GS-383-TC_01
		public void ClearInputStackableBeforeSignInLTLTest() 
		{
			HomePage.LoginLTL(true, false);
			HomePage.ClearInputHomePage();
			HomePage.VerifyClearInputHomePageLTL(true);
		}

		[Test] 
		//GS-383-TC_02
		public void ClearInputHazardousBeforeSignInLTLTest() 
		{
			HomePage.LoginLTL(false, false);
			HomePage.ClearInputHomePage();
			HomePage.VerifyClearInputHomePageLTL(false);
		}

		[Test] 
		//GS-491-TC_01
		public void ClearInputStackableAfterSignInLTLTest() 
		{
			var loginPage = HomePage.LoginLTL();
			var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            var details = loginPage.
            SignIn();
            details.EnterDetailsLTL(false, false, false);

            details.ClearInputDetailsPage();

            HomePage.VerifyClearInputHomePageLTL(true);

            HomePage.LoginLTL();
            getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            details.VerifyClearInputDetailsPageLTL();
        }

		[Test] 
		//GS-491-TC_02
		public void ClearInputHazardousAfterSignInLTLTest() 
		{
			var loginPage = HomePage.LoginLTL(false);
			var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            var details = loginPage.
            SignIn();
            details.EnterDetailsLTL(false, false, false);

            details.ClearInputDetailsPage();

            HomePage.VerifyClearInputHomePageLTL(false);

            HomePage.LoginLTL(false);
            getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            details.VerifyClearInputDetailsPageLTL();
        }

		[Test] 
		//GS-404-TC_01
		public void CheckOverDimsErrorMessageTest() 
		{
			HomePage.ProvideItemDimsLTL();
		}

        [Test]
        //GS-662
        public void GetAllDropDownLTL()
        {
            HomePage.getDropDownInfoLTL();
        }

        [Test]
        //GS-662
        public void GetAllDropdownFTL()
        {
            HomePage.getDropDownInfoFTL();
        }
    }
}
