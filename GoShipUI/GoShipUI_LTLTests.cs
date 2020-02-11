using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace GoShipUI
{
	[TestFixture]
    //[Parallelizable(ParallelScope.All)]
   [Parallelizable(ParallelScope.Fixtures)]
    public class GoShipUI_LTLTests : BaseTest
    {

        [Test]
        //GS-246-TC_01
        public void GOShipNoInsuranceLTLTest()
        {
			var loginPage = HomePage.LoginLTL();
            var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            loginPage.
            SignIn().
            EnterDetailsLTL().
            PaymentDetails(1).
            GetConfirmationLTL(false, true);                                     
        }

        [Test]
        public void GOShipYesInsuranceLTLTest()
        {
			var loginPage = HomePage.LoginLTL();
            var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            loginPage.
            SignIn().
            EnterDetailsLTL().
            PaymentDetails(1, true, false, true).
            GetConfirmationLTL(false, true);
        }

        [Test]
        public void GOShipHazmatLTLTest()
        {
			var loginPage = HomePage.LoginLTL(false);
            var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            loginPage.
            SignIn().
            EnterDetailsLTL().
            PaymentDetails(1, false, true).
            GetConfirmationLTL(false, true,false, false, true);
        }


 
        public void GetQuoteInfo(GetQuote getQuote)
        {
            InputData.Data.CarrierName = getQuote.GetCarrierName;
            var QuoteValue = getQuote.GetQuotes.Replace("$", "");
            QuoteValue = getQuote.GetQuotes.Replace("USD", "");
            InputData.Data.Quote = Convert.ToDouble(QuoteValue);
        }
    }
}
