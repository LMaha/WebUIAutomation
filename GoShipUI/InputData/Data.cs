using System;
using System.Configuration;

namespace GoShipUI.InputData
{
	public static class Data
    {
        public static string strPckCompName = "PickUpCompName";
        public static string strPckAddressLine1 = "123 Pickup Drive";
        public static string strPckContactName = "Pickup Name";
        public static string strPckPhoneNo = "1234567890";
        public static string strFormPckPhoneNo = "(123) 456 7890";
        public static string strFrmPckPhoneNo = "(123) 456-7890";
        public static string strPckContactEmail = "pickupemail@test.com";
        public static string strPckInstructions = "Pick up packages on time!";
        public static string strDelCompName = "DeliveryCompName";
        public static string strDelContactName = "Delivery contact name";
        public static string strDelAddressLine1 = "345 Delivery Drive";
        public static string strDelPhoneNo= "1987654321";
        public static string strFormDelPhoneNo = "(198) 765 4321";
		public static string strFrmDelPhoneNo = "(198) 765-4321";
        public static string strDelEmail = "delEmail@test.com";
        public static string strDelInstructions = "Keep your items ready for pick up!";
        public static string strPckStartTime = "09:30AM";
        public static string strPckEndTime = "02:45PM";
		public static string strPckWindow = "Pickup Window: 09:30 AM - 02:45 PM";
        public static string strDelStartTime = "10:30AM";
        public static string strDelEndTime = "03:30PM";
		public static string strDelWindow = "Delivery Hours: 10:30 AM - 03:30 PM";
        public static string strItemDescription = "Fragile items in the boxes";
        public static string strCitySource = "SEWICKLEY, PA, 15143";
        public static string strFullCitySource = "SEWICKLEY, PENNSYLVANIA, 15143";
		public static string selPickupLocation = "Residence/Home Business";
        public static string selPickupCountry = "United States of America";
        public static string selDeliveryLocation = "Residence/Home Business";
        public static string selDeliveryCountry = "United States of America";
        public static string strCityDestination = "PITTSBURGH, PA, 15220";
        public static string strFullCityDestination = "PITTSBURGH, PENNSYLVANIA, 15220";
        public static string strInputQuantity = "1";
        public static string selPackageType = "Boxes";
        public static string strInputLength = "10";
        public static string strInputWidth = "10";
        public static string strInputHeight = "10";
        public static string selUOMDemen = "in";
        public static string strInputWeight = "100";
        public static string selUOMWeight = "lbs";
        public static string strItemValue = "100.00";
        public static string selCondition = "New Commercial Goods";
		public static string strLoadTemp = "-99.9";
        public static double Quote = 0.00;
        public static double QuoteNoLoad = 0.00;
        public static double QuoteNoInsCAN = 0.00;
        public static double QuoteInsCAN = 0.00;
        public static double QuoteFTLFlatbed = 0.00;
        public static double QuoteFTLVan = 0.00;
        public static double QuoteFTLReefer = 0.00;
		public static double InsuranceCost = 0.00;
		public static double FinalQuote = 0.00;
		public static double FinalQuoteNoInsCAN = 0.00;
		public static double FinalQuoteInsCAN = 0.00;
		public static double FinalQuoteFTLFlatbed = 0.00;
		public static double FinalQuoteFTLVan = 0.00;
		public static double FinalQuoteFTLReefer = 0.00;
        public static string CarrierName = "";
        public static string CarrierNameCAN = "";
        public static string CarrierNameFTL = "";
        public static string CommodityValue = "500.00";
        public static string NewUserEmailLTL = "";
        public static string NewUserPasswordLTL = "";
		public static string NewUserEmailFTL = "";
        public static string NewUserPasswordFTL = "";
		public static string DefaultPassword = "GoShip1#";
		public static string DefaultNewPassword = "NewG0Sh1p#";
		public static int numPickupDays = 1;
		public static int numDeliveryDays = 1;
		public static int numSavedPickupAddresses = 0;
		public static int numSavedDeliveryAddresses = 0;

		public static string strCountryAbbreviated = "USA";
		public static string strFullCitySourceFTL = "SEWICKLEY, PENNSYLVANIA 15143";
		public static string strFullCityDestinationFTL = "PITTSBURGH, PENNSYLVANIA 15220";
		public static string strAbbrevCitySourceFTL = "SEWICKLEY, PA 15143";
		public static string strAbbrevCityDestinationFTL = "PITTSBURGH, PA 15220";

		public static string strFoodGroceryErrorMessage = "At this time, GoShip.com does not support shipments involving food/grocery warehouse locations. This is due to limitations on participating LTL carriers, special accessorial requirements, as well as restricted appointment times & procedures that must be followed. This service may be offered by GoShip.com in the future.";
		public static string strTradeShowsErrorMessage = "GoShip.com does not support shipments involving trade shows, convention centers, or temporary advance warehouses which hold freight for these time sensitive items. This service may be offered by GoShip.com in the future.";

		public static string strOverDimsErrorMessage = "Due to equipment limitations and safety precautions, GoShip.com does not provide lift gate services for LTL shipments where the linear dimension per handling unit exceeds 84 inches/213 centimeters, the vertical dimension exceeds 60 inches/152 centimeters or the side dimension exceeds 48 inches/122 centimeters as would be positioned on the tailgate.";
		public static string strInputLengthOverInch = "85";
		public static string strInputWidthOverInch = "49";
		public static string strInputHeightOverInch = "61";
		public static string strInputLengthOverCm = "214";
		public static string strInputWidthOverCm = "123";
		public static string strInputHeightOverCm = "152";
		public static string selUOMcm = "cm";

		public static string strDiscountAmount = "";

		// Static variables for customs page
		public static string selUSA = "United States of America";
		public static string selCAN = "Canada";

		// ClearIt Customs Brokerage Details (PLS Referred)
		public static string strClearItCompanyName = "ClearIt Customs Brokerage";
		public static string strClearItPhoneNumber = "1-844-999-9777";
		public static string strClearItEmail = "tyler@clearit.ca";
		public static string strClearItFax = "1-844-999-9111";

		// Custom Broker Address Details
		public static string strCustomsBrokerCompanyName = "Customs Company Name";
		public static string strCustomsBrokerAddress1 = "123 Broker Drive";
		public static string strCustomsBrokerAddress2 = "";
		public static string strCustomsBrokerEmail = "broker@email.com";
		public static string strCustomsBrokerCityStateZip = "PITTSBURGH, PA, 15122";
		public static string strCustomsBrokerPhoneNumber = "1234512345";
		public static string strCustomsBrokerFormPhoneNumber = "(123) 451-2345";

		// CAN Address Details
		public static string strCANCompanyName = "Vendor Company name";
		public static string strCANContactName = "Vendor 123";
		public static string strCANAddress1 = "Someplace Drive";
		public static string strCANAddress2 = "";
		public static string strCANCityStateZip = "OTTAWA, ON, K1A 0A1";
		public static string strCANFullCityStateZip = "OTTAWA, ONTARIO, K1A 0A1";
		public static string strCANPhoneNumber = "1234567890";
		public static string strCANFormPhoneNumber = "(123) 456-7890";
		public static string strCANFaxNumber = "";
		public static string strCANEmail = "CANaddress@test.com";

		// USA Address Details
		public static string strUSACompanyName = "Purchaser Company Name";
		public static string strUSAContactName = "Purchaser 123";
		public static string strUSAAddress1 = "Some other place";
		public static string strUSAAddress2 = "";
		public static string strUSACityStateZip = "PITTSBURGH, PA, 15220";
		public static string strUSAFullCityStateZip = "PITTSBURGH, PENNSYLVANIA, 15220";
		public static string strUSAPhoneNumber = "7131238977";
		public static string strUSAFormPhoneNumber = "(713) 123-8977";
		public static string strUSAFaxNumber = "";
		public static string strUSAEmail = "USAaddress@test.com";

		// Payment Terms
		public static string selPaymentTerms = "Collect";

		// Customs Invoice Details
		public static string strTariffNumber = "111222333444";
		public static string strFormTariffNumber = "1112.22.3334";
		public static string selCountryOfOrigin= "Columbia";
		public static string strCountryOfOriginAbbrev = "COL";

		public static string selPickupCountryCAN = "Canada";
        public static string strCitySourceCAN = "TORONTO, ON, M3C 0J1";
        public static string strFullCitySourceCAN = "TORONTO, ONTARIO, M3C 0J1";
        public static int numPickupDaysCAN = 1;
        public static int numDeliveryDaysCAN = 2;

		public static Entities.LTL_BillOfLading bolLTL = new Entities.LTL_BillOfLading()
		{
			PckCompanyName = strPckCompName,
			PckContactName = strPckContactName,
			PckAddress = strPckAddressLine1,
			PckPhoneNo = strFrmPckPhoneNo,
			PckContactEmail = strPckContactEmail,
			PckInstructions = strPckInstructions,
			DelCompanyName = strDelCompName,
			DelContactName = strDelContactName,
			DelAddress = strDelAddressLine1,
			DelFullCityStateZip = strFullCityDestination,
			DelPhoneNo = strFrmDelPhoneNo,
			DelContactEmail = strDelEmail,
			DelInstructions = strDelInstructions,
			CommodityDescription = strItemDescription,
			PckWindow = strPckWindow,
			DelWindow = strDelWindow
		};

		public static Entities.LTL_NAFTA naftaLTL = new Entities.LTL_NAFTA()
		{
			PckCompanyName = strPckCompName,
			PckContactName = strPckContactName,
			PckAddress = strPckAddressLine1,
			PckCityStateZip = strCitySourceCAN.ToString().Replace(",", ""),
			PckContactEmail = strPckContactEmail,
			DelContactName = strDelContactName,
			DelAddress = strDelAddressLine1,
			DelCityStateZip = strCityDestination.ToString().Replace(",", ""),
			DelContactEmail = strDelEmail,
			CommodityDescription = strItemDescription,
			TariffNo = strFormTariffNumber,
			CountryOfOriginAbbrev = strCountryOfOriginAbbrev
		};

		public static Entities.LTL_ShipperInstructions sliLTL = new Entities.LTL_ShipperInstructions()
		{
			PckCompanyName = strPckCompName,
			PckContactName = strPckContactName,
			PckAddress = strPckAddressLine1,
			PckCityStateZip = strCitySourceCAN.ToString().Replace(",", ""),
			PckPhoneNo = strFrmPckPhoneNo,
			PckInstructions = strPckInstructions,
			DelCompanyName = strDelCompName,
			DelContactName = strDelContactName,
			DelAddress = strDelAddressLine1,
			DelCityStateZip = strCityDestination.ToString().Replace(",", ""),
			DelPhoneNo = strFrmDelPhoneNo,
			DelContactEmail = strDelEmail,
			DelInstructions = strDelInstructions,
			CarrierName = CarrierNameCAN,
			VendorCompanyName = strCANCompanyName,
			VendorContactName = strCANContactName,
			VendorAddress = strCANAddress1,
			VendorCityStateZip = strCANCityStateZip.ToString().Replace(",", ""),
			VendorPhoneNo = strCANFormPhoneNumber,
			VendorEmail = strCANEmail
		};

		public static Entities.LTL_PackingList plLTL = new Entities.LTL_PackingList()
		{
			PckContactName = strPckContactName,
			PckAddress = strPckAddressLine1,
			PckCityStateZip = strCitySourceCAN.ToString().Replace(",", ""),
			PckPhoneNo = strFrmPckPhoneNo,
			PckContactEmail = strPckContactEmail,
			PckInstructions = strPckInstructions,
			PaymentTerms = selPaymentTerms,
			VendorCompanyName = strCANCompanyName,
			VendorContactName = strCANContactName,
			VendorAddress = strCANAddress1,
			VendorCityStateZip = strCANCityStateZip.ToString().Replace(",", ""),
			VendorPhoneNo = strCANFormPhoneNumber
		};

		public static Entities.LTL_CustomsInvoice cciLTL = new Entities.LTL_CustomsInvoice()
		{
			DelCompName = strDelCompName,
			DelContactName = strDelContactName,
			DelAddress = strDelAddressLine1,
			DelCityStateZip = strCityDestination.Replace(",", ""),
			DelPhoneNo = strFrmDelPhoneNo,
			DelContactEmail = strDelEmail,
			CarrierName = CarrierNameCAN,
			VendorCompanyName = strCANCompanyName,
			VendorContactName = strCANContactName,
			VendorAddress = strCANAddress1,
			VendorCityStateZip = strCANCityStateZip.Replace(",", ""),
			VendorPhoneNo = strCANFormPhoneNumber,
			VendorEmail = strCANEmail,
			PurchaserCompanyName = strUSACompanyName,
			PurchaserContactName = strUSAContactName,
			PurchaserAddress = strUSAAddress1,
			PurchaserCityStateZip = strUSACityStateZip.Replace(",", ""),
			PurchaserPhoneNo = strUSAFormPhoneNumber,
			PurchaserEmail = strUSAEmail,
			CommodityDescription = strItemDescription,
			CustomsBrokerCompanyName = strCustomsBrokerCompanyName,
			CustomsBrokerPhoneNo = strCustomsBrokerFormPhoneNumber,
			CustomsBrokerEmail = strCustomsBrokerEmail
		};

		public static Entities.LTL_CommercialInvoice ciLTL = new Entities.LTL_CommercialInvoice()
		{
			PckCompanyName = strPckCompName,
			PckContactName = strPckContactName,
			PckAddress = strPckAddressLine1,
			PckCityStateZip = strCitySourceCAN.Replace(",", ""),
			PckPhoneNo = strFrmPckPhoneNo,
			PckContactEmail = strPckContactEmail,
			CarrierName = CarrierNameCAN,
			VendorCompanyName = strCANCompanyName,
			VendorContactName = strCANContactName,
			VendorAddress = strCANAddress1,
			VendorCityStateZip = strCANCityStateZip.Replace(",", ""),
			VendorPhoneNo = strCANFormPhoneNumber,
			VendorEmail = strCANEmail,
			PurchaserCompanyName = strUSACompanyName,
			PurchaserContactName = strUSAContactName,
			PurchaserAddress = strUSAAddress1,
			PurchaserCityStateZip = strUSACityStateZip.Replace(",", ""),
			PurchaserPhoneNo = strUSAFormPhoneNumber,
			PurchaserEmail = strUSAEmail,
			CommodityDescription = strItemDescription,
			CustomsBrokerCompanyName = strCustomsBrokerCompanyName,
			CustomsBrokerPhoneNo = strCustomsBrokerFormPhoneNumber,
			CustomsBrokerEmail = strCustomsBrokerEmail
		};

		public static Entities.FTL_BillOfLading bolFTL = new Entities.FTL_BillOfLading()
		{
			PckCompanyName = strPckCompName,
			PckContactName = strPckContactName,
			PckAddress = strPckAddressLine1,
			PckFullCityStateZip = strFullCitySource,
			PckPhoneNo = strFrmPckPhoneNo,
			DelCompanyName = strDelCompName,
			DelContactName = strDelContactName,
			DelAddress = strDelAddressLine1,
			DelFullCityStateZip = strFullCityDestination,
			DelPhoneNo = strFrmDelPhoneNo,
			CommodityDescription = strItemDescription
		};
    }
}
