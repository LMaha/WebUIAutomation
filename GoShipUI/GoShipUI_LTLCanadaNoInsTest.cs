using System;
using NUnit.Framework;

namespace GoShipUI
{
	[TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    class GoShipUI_LTLCanadaNoInsTest : BaseTest
    {
        [Test]
        public void NoInsCanadaPLSBrokerHaveCustomsInvoiceLTLTest()
        {
            // Use PLS referred customs broker
            // Have customs invoice
            GOShipNoInsuranceCanadaLTL(false, true, false);
        }

        [Test]
        public void NoInsCanadaUserBrokerNewPickupNewDeliveryLTLTest()
        {
            // Have customs broker
            // Create customs invoice
            // Use new pickup address for Vendor
            // Use new delivery address for Purchaser
            GOShipNoInsuranceCanadaLTL(true, false, true, 3, 4);
        }

        [Test]
        public void NoInsCanadaUserBrokerVendorPickupPurchaserDeliveryCheckLTLTest()
        {
            // Have customs broker
            // Create customs invoice
            // Use pickup address for Vendor
            // Use delivery address for Purchaser
            GOShipNoInsuranceCanadaLTL(true, false, false, 1, 2);
        }
  
		public void GetQuoteInfo(GetQuote getQuote)
        {
            InputData.Data.CarrierNameCAN = getQuote.GetCarrierName;
            var QuoteValue = getQuote.GetQuotes.Replace("$", "");
            QuoteValue = getQuote.GetQuotes.Replace("USD", "");
            InputData.Data.QuoteNoInsCAN = Convert.ToDouble(QuoteValue);
        }
		/*
		 * Assumes that pickup will always be Canadian address
		 * Assumes that delivery will always be USA address
		 * Scenario IDs
		 * 1 - Existing Pickup Address
		 * 2 - Existing Delivery Address
		 * 3 - New Canadian Address
		 * 4 - New USA Address
		 * Cannot select same IDs for both pickup and delivery
		 */
        public void GOShipNoInsuranceCanadaLTL(bool haveCustomsBroker, bool haveCustomsInvoice, bool checkAllDocs, int pickupOption = 0, int deliveryOption = 0)
        {
            var loginPage = HomePage.LoginLTL(true, true, true);
            var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            loginPage.
            SignIn().
            EnterDetailsCanadaLTL().
            EnterCustomsDetailsLTL(haveCustomsBroker, haveCustomsInvoice, pickupOption, deliveryOption).
            PaymentDetails(2).
            GetConfirmationLTL(checkAllDocs, true, true);
        }
    }
}
