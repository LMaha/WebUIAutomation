using NUnit.Framework;
using System;

namespace GoShipUI
{
	[TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
	public class GoShipUI_LTLCanadaInsTest : BaseTest
	{
		[Test]
        public void YesInsCanadaUserBrokerNewPickupNewDeliveryCheckAllDocsLTLTest()
        {
            // Have customs broker
            // Create customs invoice
            // Use new pickup address for Vendor
            // Use new delivery address for Purchaser
            // Verify all documents
            GOShipYesInsuranceCanadaLTL(true, false, true, 3, 4);
        }

		[Test]
        public void YesInsCanadaUserBrokerNewPickupPurchaserDeliveryLTLTest()
        {
            // Have customs broker
            // Create customs invoice
            // Use new pickup address for Vendor
            // Use delivery address for Purchaser
            GOShipYesInsuranceCanadaLTL(true, false, false, 3, 2);
        }

		[Test]
        public void YesInsCanadaUserBrokerVendorPickupNewDeliveryLTLTest()
        {
            // Have customs broker
            // Create customs invoice
            // Use pickup address for Vendor
            // Use new delivery address for Purchaser
            GOShipYesInsuranceCanadaLTL(true, false, false, 1, 4);
        }

		public void GetQuoteInfo(GetQuote getQuote)
        {
            InputData.Data.CarrierNameCAN = getQuote.GetCarrierName;
            var QuoteValue = getQuote.GetQuotes.Replace("$", "");
            QuoteValue = getQuote.GetQuotes.Replace("USD", "");
            InputData.Data.QuoteInsCAN = Convert.ToDouble(QuoteValue);
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
        public void GOShipYesInsuranceCanadaLTL(bool haveCustomsBroker, bool haveCustomsInvoice, bool checkAllDocs, int pickupOption = 0, int deliveryOption = 0)
        {
            var loginPage = HomePage.LoginLTL(true, true, true);
            var getQuote = HomePage.GetQuoteInfo();
            GetQuoteInfo(getQuote);
            loginPage.
            SignIn().
            EnterDetailsCanadaLTL().
            EnterCustomsDetailsLTL(haveCustomsBroker, haveCustomsInvoice, pickupOption, deliveryOption).
            PaymentDetails(3, true).
            GetConfirmationLTL(checkAllDocs, true, true, true);
        }
	}
}
