using System;
using System.Collections.Generic;
using System.Configuration;
using OpenQA.Selenium;
using Utilities;

namespace GoShipUI
{
    public class HomePage : BasePage
    {
        public HomePage(Driver driver, Assert assert) : base(driver, assert) { }

        private By txtUsername = By.XPath("//*[@id= 'userName']");
        private By txtPassword = By.XPath("//*[@id= 'userPassword']");
        private By btnLogin = By.XPath("//*[@id='loginBtn']");
        //private By Ltlheading = By.XPath("//*[text()='LTL Shipment']");
        private By Ltlheading = By.Id("lessThan-truckLoad-id");
        private By FtlHeading = By.Id("full-truckLoad-id");
        private By btnGetQuote = By.Id("get-quote-header");
        private By selPickUpLocation = By.Id("pickupLocationType");
        private By selPickupCountry = By.Id("country-source");
        private By txtCitySource = By.Id("city-source");
        //private By txtCitySource = By.Id("address-autocomplete");
        private By txtPickUpDate = By.Id("pickupDate");
        private By chkPickUp = By.Id("IPU");
        private By selDeliveryLocation = By.Id("deliveryLocationType");
        private By selDeliveryCountry = By.Id("country-destination");
        private By txtCityDestination = By.Id("city-destination");
        private By chkLFT = By.Id("LFT");
        private By chkNDR = By.Id("NDR");
        private By chkIDL = By.Id("IDL");
        private By txtInputQuantity = By.Id("input-quantity");
        private By selPackageType = By.Id("select-package-type");
        private By txtInputLength = By.Id("input-length");
        private By txtInputWidth = By.Id("input-width");
        private By txtInputHeight = By.Id("input-height");
        private By selUOMDimension = By.Id("select-dimension-type");
        private By txtInputWeight = By.Id("inputWeight0");
        private By txtInputPieces = By.Id("input-pieces");
        private By selUOMWeight = By.Id("select-weight-type-uom");
        private By txtItemValue = By.Id("itemValue0");
        private By selCondition = By.Id("select-weight-type-condition");
        private By chkStackable = By.Id("stackable0");
        private By chkHazardous = By.Id("hazardous0");
        private By chkRQ = By.Id("hazmatItem-rq-id");
        private By btnContinueToQuote = By.Id("to-quotes");
        private By btnCreateShipment = By.Id("create-shipment");
        private By btnGoshipQuote = By.XPath("//*[@class='col-md-8']/descendant::button[1]");
        private By lblAmount = By.XPath("//*[contains(@class,'fullCost ng-binding')]");
        private By lblFTLDate = By.XPath("//*[contains(@class,'calendar-date ng-binding')]");
        private By lblAmountChange = By.XPath("//*[contains(@class,'change ng-binding')]");
        private By lblOrderSummary = By.XPath("//*[text()='Order Summary']");
        private By lblCarrierName = By.XPath("//*[contains(@class,'ng-binding carrierName')]");

        private By lblFirstQuoteAmount = By.XPath("//*[@class='carrier ng-scope']/descendant::span[5]");
        private By lblFirstQuoteAmountChange = By.XPath("//*[@class='carrier ng-scope']/descendant::span[6]");
        private By lblFirstQuoteCarrierName = By.XPath("//*[@class='carrier ng-scope']/descendant::h3[1]");
        private By btnFirstQuoteButton = By.XPath("//*[@class='carrier ng-scope']/descendant::button[1]");

        private By lblHighlightedTextPickupLTL = By.XPath("//*[@class='highlight' and text()='SEWICKLEY, PA, 15143']");
        private By lblHighlightedTextPickupCANLTL = By.XPath("//*[@class='highlight' and text()='TORONTO, ON, M3C 0J1']");
        private By lblHighlightedTextDeliveryLTL = By.XPath("//*[@class='highlight' and text()='PITTSBURGH, PA, 15220']");
        //private By lblHighlightedTextPickupFTL = By.XPath("//*[@class='item-metastat' and text()='SEWICKLEY, PENNSYLVANIA, ']");
        //private By lblHighlightedTextDeliveryFTL = By.XPath("//*[@class='item-metastat' and text()='PITTSBURGH, PENNSYLVANIA, ']");
        private static string XPathFullStatePickupFTL = "//*[@class='item-metastat' and text()='SEWICKLEY, PENNSYLVANIA, ']";
        private static string XPathFullStateDeliveryFTL = "//*[@class='item-metastat' and text()='PITTSBURGH, PENNSYLVANIA, ']";
        private static string XPathAbbrevStatePickupFTL = "//*[@class='item-metastat' and text()='SEWICKLEY, PA, ']";
        private static string XPathAbbrevStateDeliveryFTL = "//*[@class='item-metastat' and text()='PITTSBURGH, PA, ']";
        private static string XPathFullStateSpacePickupFTL = "//*[@class='item-metastat' and text()='SEWICKLEY,  PENNSYLVANIA,  ']";
        private static string XPathFullStateSpaceDeliveryFTL = "//*[@class='item-metastat' and text()='PITTSBURGH,  PENNSYLVANIA,  ']";
        private static string XPathAbbrevStateSpacePickupFTL = "//*[@class='item-metastat' and text()='SEWICKLEY,  PA,  ']";
        private static string XPathAbbrevStateSpaceDeliveryFTL = "//*[@class='item-metastat' and text()='PITTSBURGH,  PA,  ']";
        private static string[] XPathPickupFTL = { XPathAbbrevStatePickupFTL, XPathFullStatePickupFTL, XPathFullStateSpacePickupFTL, XPathAbbrevStateSpacePickupFTL };
        private static string[] XPathDeliveryFTL = { XPathAbbrevStateDeliveryFTL, XPathFullStateDeliveryFTL, XPathFullStateSpaceDeliveryFTL, XPathAbbrevStateSpaceDeliveryFTL };
        private By lblFlatbed = By.Id("select-flatbed-id");
        private By lblVan = By.Id("select-van-id");
        private By lblReefer = By.Id("select-reefer-id");
        private By txtHazmatUN = By.Id("hazmatItem-unNumber-id");
        private By txtHazmatPckGrp = By.Id("hazmatItem-packingGroup-id");
        private By selHazmatClass = By.Id("hazmatItem-hazmatClass-id");
        private By txtHazmatEmergencyRes = By.Id("hazmatItem-emergencyCompany-id");
        private By txtHazmatContractId = By.Id("hazmatItem-emergencyContact-id");
        private By txtHazmatPhoneNo = By.Id("hazmatItem-emergencyPhone-id");
        private By txtHazmatInstructions = By.Id("hazmatItem-instruction-id");

        private WorkingDayCalculator WC = new WorkingDayCalculator(2100, 2019);

        private By lblPickUpHeaderFTL = By.XPath("//*[@class='quote-booking-item-header' and text()=' Pickup ']");
        private By lblPickUpLocationFTL = By.Id("origin-address");
        private By lblPickUpDateFTL = By.Id("origin-pickup-date");

        private By lblDeliveryHeaderFTL = By.XPath("//*[@class='quote-booking-item-header' and text()=' Dropoff ']");
        private By lblDeliveryLocationFTL = By.Id("destination-address");
        private By lblDeliveryDateFTL = By.Id("destination-delivery-date");
        private By btnSearch = By.XPath("//*[@class ='search-icon']");
        private By lblCostFTL = By.XPath("//*[contains(@class,'calendar-cost ng-binding')]");
        private By lblCostFTLFirstDay = By.XPath("//*[@class='calendar-day ng-scope'][1]/descendant::div[@class='calendar-cost ng-binding']");
        private By lblLocationTypeErrorMessageLTL = By.XPath("//*[@class='alert alert-danger warning-header ng-scope']/descendant::h4");
        private By btnCancel = By.Id("cancelDialogBtn");
        private By btnClear = By.XPath("//*[@class='btn btn-border' and text()='Clear']");
        private By lblConfirmClear = By.XPath("//*[@class='cancelConfirm-header']");

        private By optPickupLocationTypeDefault = By.XPath("//*[@id='pickupLocationType']/descendant::option");
        private By optPickupCountryDefault = By.XPath("//*[@id='country-source']/descendant::option");
        private By optDeliveryLocationTypeDefault = By.XPath("//*[@id='deliveryLocationType']/descendant::option");
        private By optDeliveryCountryDefault = By.XPath("//*[@id='country-destination']/descendant::option");
        private By optPackageTypeDefault = By.XPath("//*[@id='select-package-type']/descendant::option");
        private By optHazmatClassDefault = By.XPath("//*[@id='hazmatItem-hazmatClass-id']/descendant::option");

        private By btnFlatbed = By.Id("flatbed-id");
        private By btnVan = By.Id("van-id");
        private By btnReefer = By.Id("reefer-id");

        private By lblOverDimsErrorMessageLTL = By.XPath("//*[@class='alert alert-danger warning-item']");
        private By lblOverDimsErrorMessageFTL = By.XPath("//*[contains(@class,'col-md-12 ng-scope')]");
        private By lblQuoteHeaderFTL = By.XPath("//*[@class='quote-booking-header ng-binding']");

        private By btnFirstQuoteFTL = By.XPath("//*[@class='calendar-border']/div[1]/div[2]/div[1]");
        private By btnFirstQuoteLTL = By.XPath("//*[@class='carrier ng-scope']/descendant::button[1]");

        private By btnSignIn = By.Id("sign-in-header");

        private By icnLearnMoreLTL_FTL = By.Id("lessThan-truckLoad-learn-more-id");
        private By drpDwnElementsLTL = By.XPath("//*[@id='lessThan-truckLoad-info-id']/ul/li/span");
        private By drpDwnElementsFTL = By.XPath("//*[@id='full-truckLoad-info-id']/ul/li/span");

        private static int QuoteMaxWait = Convert.ToInt32(ConfigurationManager.AppSettings["QUOTE_WAIT"]);

        public void test(string url)
        {
            Driver.GoToUrl(url);
        }
        public LoginPage LoginLTL(bool stackable = true, bool continueToLogin = true, bool isCanada = false)
        {
            GoToGetQuoteLTL();

            Driver.SelectFromDropdown(selPickUpLocation, InputData.Data.selPickupLocation, "Select 'Pickup Location'");
            if (isCanada == false)
            {
                Driver.SelectFromDropdown(selPickupCountry, InputData.Data.selPickupCountry, "Select 'Pickup Country' ");

                Driver.Click(txtCitySource, "Validate 'Pick Up Country'");
                Driver.SendKeys(txtCitySource, InputData.Data.strCitySource, "Enter city, state and zipcode");
                Driver.Click(lblHighlightedTextPickupLTL, "Select highlighted text");
            }
            else
            {
                Driver.SelectFromDropdown(selPickupCountry, InputData.Data.selPickupCountryCAN, "Select Canada for 'Pickup Country' ");

                Driver.Click(txtCitySource, "Validate 'Pick Up Country'");
                Driver.SendKeys(txtCitySource, InputData.Data.strCitySourceCAN, "Enter Canadian city, state and zipcode");
                Driver.Click(lblHighlightedTextPickupCANLTL, "Select highlighted text");
            }

            //var obj = Driver.EnterByJavascript("city-destination", "SEWICKLEY, PA, 15143");
            // Driver.Click(txtPickUpDate, "Click Date field");
            Driver.EnterByJavascript("pickupDate", DateTime.Today.AddDays(1).ToShortDateString());
            // Driver.SendKeys(txtPickUpDate, DateTime.Today.AddDays(1).ToShortDateString(), "Enter Pick Up date");
            Driver.SelectFromDropdown(selDeliveryLocation, InputData.Data.selDeliveryLocation, "Select delivery location");
            Driver.SelectFromDropdown(selDeliveryCountry, InputData.Data.selDeliveryCountry, "Select 'Delivery Country'");

            Driver.Click(txtCityDestination, "Validate 'Destination Country'");
            Driver.SendKeys(txtCityDestination, InputData.Data.strCityDestination, "Enter city, state and zipcode");
            Driver.Click(lblHighlightedTextDeliveryLTL, "Select highlighted text");

            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SelectFromDropdown(selPackageType, InputData.Data.selPackageType, "Select Package type");
            Driver.SendKeys(txtInputLength, InputData.Data.strInputLength, "Enter length");
            Driver.SendKeys(txtInputWidth, InputData.Data.strInputWidth, "Enter width");
            Driver.SendKeys(txtInputHeight, InputData.Data.strInputHeight, "Enter height");
            Driver.SelectFromDropdown(selUOMDimension, InputData.Data.selUOMDemen, "Select measurement of dimension");
            Driver.SendKeys(txtInputWeight, InputData.Data.strInputWeight, "Enter input weight");
            Driver.SelectFromDropdown(selUOMWeight, InputData.Data.selUOMWeight, "Select measurement of weight");
            Driver.SendKeys(txtItemValue, InputData.Data.strItemValue, "Enter item value");
            Driver.SelectFromDropdown(selCondition, InputData.Data.selCondition, "Select weight type");
            if (stackable == true)
            {
                Driver.Click(chkStackable, "Click 'Stackable' option");
            }
            else
            {
                if (Driver.FindElement(chkHazardous).GetAttribute("class").Contains("ng-empty"))
                {
                    Driver.Click(chkHazardous, "Click 'Hazardous' option");
                    Driver.Click(chkRQ, "Click reportable quantity ");
                }
                Driver.SendKeys(txtHazmatUN, "UN3456", "Enter Hazmat UN No");
                Driver.SelectFromDropdown(txtHazmatPckGrp, "II.", "Select Hazmat Pak. Group");
                Driver.SelectFromDropdown(selHazmatClass, "1. Explosives", "Select Hazmat class");
                Driver.SendKeys(txtHazmatEmergencyRes, "Hazmat Company", "Enter Hazmat Comapny");
                Driver.SendKeys(txtHazmatContractId, "12345678", "Enter Hazmat contract id");
                Driver.SendKeys(txtHazmatPhoneNo, "1234567890", "Enter Hazmat Phone no");
                Driver.SendKeys(txtHazmatInstructions, "Explosives, handle with care!", "Enter Hazmat instructions");
            }
            if (continueToLogin == true)
            {
                Driver.Click(btnContinueToQuote, "Click 'Continue'");
            }
            //Driver.WaitForCondition(d => Driver.IsDisplayed(lblOrderSummary, "Verify 'Order Summary'"), "Wait for Quotes to appear");
            return new LoginPage(Driver, Assert);
        }

        public LoginPage LoginFTL(string truckType, bool continueToLogin = true)
        {
            GoToGetQuoteFTL();

            Driver.Click(txtCitySource, "Validate 'Pick Up Country'");
            Driver.SendKeys(txtCitySource, "15143", "Enter city, state and zipcode");
            SelectHighlightedText(XPathPickupFTL);

            Driver.Click(txtCityDestination, "Validate 'Destination Country'");
            Driver.SendKeys(txtCityDestination, "15220", "Enter city, state and zipcode");
            SelectHighlightedText(XPathDeliveryFTL);

            Driver.Click(btnSearch, "Click Search button");

            bool clicked = false;
            int counter = 0;
            while (clicked == false && counter < QuoteMaxWait)
            {
                try
                {
                    Driver.Sleep();
                    Driver.WaitForAngular();
                    var amt = Convert.ToDouble(Driver.GetRepeaterInfo("quote in quotes", 0, lblCostFTL).Text.TrimStart('$'));
                    SaveGlobalQuoteVarByTruckType(truckType, amt);
                    int calendarDays = Driver.GetRepeaterCount("quote in quotes");
                    Assert.AreEqual(14, calendarDays, "Verify 14 days are shown in FTL date pick up");
                    Driver.GetRepeaterButton("quote in quotes", 0, lblFTLDate);
                    clicked = true;
                }
                catch
                {
                    if (truckType == "Flatbed")
                    {
                        Driver.Click(btnFlatbed, "Click 'Flatbed'");
                    }
                    else if (truckType == "Van")
                    {
                        Driver.Click(btnVan, "Click 'Van'");
                    }
                    else if (truckType == "Reefer")
                    {
                        Driver.Click(btnReefer, "Click 'Reefer'");
                    }
                    else
                    {
                        Assert.Fail("Invalid equipment type: " + truckType);
                    }
                    Driver.Sleep();
                    counter++;
                }
            }

            if (clicked == false)
            {
                Assert.Fail("Unable to select FTL quote - Took too long for quotes to load");
            }

            Driver.WaitForCondition(d => Driver.IsDisplayed(lblPickUpLocationFTL, "Verify Pick up location"), "Wait for origin address to load");
            var quote = GetGlobalQuoteVarByTruckType(truckType);
            Assert.AreEqual("Your Quote is $" + String.Format("{0:0.00}", quote) + " USD", Driver.FindElement(lblQuoteHeaderFTL).Text.ToString(), "Verify quote header text");
            Assert.AreEqual(InputData.Data.strAbbrevCitySourceFTL + ", " + InputData.Data.strCountryAbbreviated, Driver.FindElement(lblPickUpLocationFTL).Text.ToString(), "Verify Pick up location");
            Assert.AreEqual(InputData.Data.strAbbrevCityDestinationFTL + ", " + InputData.Data.strCountryAbbreviated, Driver.FindElement(lblDeliveryLocationFTL).Text.ToString(), "Verify Delivery location");

            int pckDays = WC.HolidayMonth(DateTime.Today, InputData.Data.numPickupDays);
            DateTime pickupDate = DateTime.Today.AddDays(pckDays);
            int delDays = WC.HolidayMonth(pickupDate, InputData.Data.numDeliveryDays);
            DateTime deliveryDate = pickupDate.AddDays(delDays);
            var dat = pickupDate.ToString("dddd, MMMM d, yyyy");
            Assert.AreEqual(dat, Driver.FindElement(lblPickUpDateFTL).Text.ToString(), "Verify Pick up date");
            dat = deliveryDate.ToString("dddd, MMMM d, yyyy");
            Assert.AreEqual(dat, Driver.FindElement(lblDeliveryDateFTL).Text.ToString(), "Verify Delivery date");

            Assert.AreEqual("Create shipment for $" + String.Format("{0:0.00}", quote) + " USD", Driver.FindElement(btnCreateShipment).Text.ToString(), "Verify Create Shipment button text");

            if (continueToLogin == true)
            {
                Driver.Click(btnCreateShipment, "Click Create Shipment");
            }

            return new LoginPage(Driver, Assert);
        }

        public GetQuote GetQuoteInfo()
        {
            GetQuote getQuote = new GetQuote();

            Driver.WaitForAngular();

            bool clicked = false;
            int counter = 0;
            while (clicked == false && counter < QuoteMaxWait)
            {
                try
                {
                    IWebElement eleAmount = Driver.GetRepeaterInfo("quote in quotes", 0, lblAmount);
                    IWebElement eleAmountChange = Driver.GetRepeaterInfo("quote in quotes", 0, lblAmountChange);
                    IWebElement eleCarrierName = Driver.GetRepeaterInfo("quote in quotes", 0, lblCarrierName);

                    getQuote.GetQuotes = eleAmount.Text + eleAmountChange.Text;
                    getQuote.GetCarrierName = eleCarrierName.Text;

                    eleAmount = Driver.GetRepeaterInfo("quote in quotes", 0, lblAmount);
                    Driver.GetRepeaterButton("quote in quotes", 0, btnGoshipQuote);
                    clicked = true;
                }
                catch
                {
                    Driver.Sleep();
                    counter++;
                }
            }
            if (clicked == false)
            {
                Assert.Fail("Unable to select LTL quote - Took too long for quotes to load");
            }

            return getQuote;
        }

        public void DisplayFoodGroceryPickupErrorMessage()
        {
            GoToGetQuoteLTL();
            Driver.SelectFromDropdown(selPickUpLocation, "Food/Grocery Warehouse", "Select 'Food/Grocery Warehouse' for 'Pickup Location'");
            Driver.WaitForCondition(d => Driver.IsDisplayed(lblLocationTypeErrorMessageLTL, "Check if food/grocery warehouose message is displayed"), "Wait for food/grocery warehouse message to load");
            Assert.AreEqual(InputData.Data.strFoodGroceryErrorMessage, Driver.FindElement(lblLocationTypeErrorMessageLTL).Text.ToString(), "Verify Error Message");
        }

        public void DisplayTradeShowsPickupErrorMessage()
        {
            GoToGetQuoteLTL();
            Driver.SelectFromDropdown(selPickUpLocation, "Trade Show", "Select 'Trade Show' for 'Pickup Location'");
            Driver.WaitForCondition(d => Driver.IsDisplayed(lblLocationTypeErrorMessageLTL, "Check if trade show message is displayed"), "Wait for trade show message to load");
            Assert.AreEqual(InputData.Data.strTradeShowsErrorMessage, Driver.FindElement(lblLocationTypeErrorMessageLTL).Text.ToString(), "Verify Error Message");
        }

        public void ProvideDetailsFTL()
        {
            GoToGetQuoteFTL();

            Driver.Click(txtCitySource, "Validate 'Pick Up Country'");
            Driver.SendKeys(txtCitySource, "15143", "Enter city, state and zipcode");
            SelectHighlightedText(XPathPickupFTL);

            Driver.Click(txtCityDestination, "Validate 'Destination Country'");
            Driver.SendKeys(txtCityDestination, "15220", "Enter city, state and zipcode");
            SelectHighlightedText(XPathDeliveryFTL);

            Driver.Click(btnSearch, "Click Search button");
        }

        public void ClearInputHomePage()
        {
            Driver.WaitForCondition(d => Driver.FindElement(btnCancel).Displayed, "Wait for cancel button to be visible");
            Driver.Click(btnCancel, "Click 'Cancel' Button");
            Driver.WaitForCondition(d => Driver.IsDisplayed(btnClear, "Confirm Form Clear"), "Wait for 'Confirm Form Clear' to load");
            Driver.Click(btnClear, "Click 'Clear' Button");
        }

        public void VerifyClearInputHomePageFTL()
        {
            GoToGetQuoteFTL();

            Assert.IsEmpty(Driver.FindElement(txtCitySource).Text.ToString(), "Verify Pickup Location is Empty/Blank");
            Assert.IsEmpty(Driver.FindElement(txtCityDestination).Text.ToString(), "Verify Delivery Location is Empty/Blank");
        }

        public void VerifyClearInputHomePageLTL(bool stackable)
        {
            GoToGetQuoteLTL();

            Assert.AreEqual("Pickup Location Type", Driver.FindElement(optPickupLocationTypeDefault).Text, "Verify Pickup Location Type is set back to default ('Pickup Location Type')");
            Assert.AreEqual("United States of America", Driver.FindElement(optPickupCountryDefault).Text.ToString(), "Verify Pickup Country is set back to default ('United States of America')");
            Assert.IsEmpty(Driver.FindElement(txtCitySource).Text.ToString(), "Verify Pickup City, State, Zip code is Blank/Empty");

            var pckDays = WC.GetWorkingDay(DateTime.Today, 1);
            var dat = DateTime.Today.AddDays(pckDays).ToString("MM/dd/yyyy");
            Assert.AreEqual(dat, Driver.FindElement(txtPickUpDate).GetAttribute("value"), "Verify Pickup Date is set back to default (next work day)");

            Assert.AreEqual("Delivery Location Type", Driver.FindElement(optDeliveryLocationTypeDefault).Text, "Verify Delivery Location Type is set back to default ('Delivery Location Type')");
            Assert.AreEqual("United States of America", Driver.FindElement(optDeliveryCountryDefault).Text.ToString(), "Verify Delivery Country is set back to default ('United States of America')");
            Assert.IsEmpty(Driver.FindElement(txtCityDestination).Text.ToString(), "Verify Delivery City, State, Zip code is Blank/Empty");
            Assert.AreEqual("1", Driver.FindElement(txtInputQuantity).GetAttribute("value"), "Verify item quantity is set back to default ('1')");
            Assert.IsEmpty(Driver.FindElement(optPackageTypeDefault).Text.ToString(), "Verify package type is Blank/Empty");
            Assert.IsEmpty(Driver.FindElement(txtInputLength).Text.ToString(), "Verify that length is Blank/Empty");
            Assert.IsEmpty(Driver.FindElement(txtInputWidth).Text.ToString(), "Verify that width is Blank/Empty");
            Assert.IsEmpty(Driver.FindElement(txtInputHeight).Text.ToString(), "Verify that height is Blank/Empty");
            Assert.IsEmpty(Driver.FindElement(txtInputWeight).Text.ToString(), "Verify that weight is Blank/Empty");
            Assert.IsEmpty(Driver.FindElement(txtItemValue).Text.ToString(), "Verify that item value is Blank/Empty");

            if (stackable == true)
            {
                Assert.IsTrue(Driver.FindElement(chkStackable).GetAttribute("class").Contains("ng-empty"), "Verify that 'Stackable' checkbox is unchecked");
            }
            else
            {
                if (Driver.FindElement(chkHazardous).GetAttribute("class").Contains("ng-empty"))
                {
                    Driver.Click(chkHazardous, "Click 'Hazardous' option");
                }
                Assert.IsEmpty(Driver.FindElement(txtHazmatUN).Text.ToString(), "Verify that Hazmat UN No is Blank/Empty");
                Assert.AreEqual("?", Driver.FindElement(txtHazmatPckGrp).GetAttribute("value").ToString(), "Verify that Hazmat Pak. Group is Blank/Empty");
                Assert.IsEmpty(Driver.FindElement(optHazmatClassDefault).Text.ToString(), "Verify that Hazmat Class is Blank/Empty");
                Assert.IsEmpty(Driver.FindElement(txtHazmatEmergencyRes).Text.ToString(), "Verify that Hazmat Company is Blank/Empty");
                Assert.IsEmpty(Driver.FindElement(txtHazmatContractId).Text.ToString(), "Verify that Hazmat contract id is Blank/Empty");
                Assert.IsEmpty(Driver.FindElement(txtHazmatPhoneNo).Text.ToString(), "Verify that Hazmat Phone no is Blank/Empty");
                Assert.IsTrue(Driver.FindElement(txtHazmatInstructions).GetAttribute("class").Contains("ng-empty"), "Verify that Hazmat instructions is Blank/Empty");
            }
        }

        public void ProvideItemDimsLTL()
        {
            GoToGetQuoteLTL();

            Driver.SendKeys(txtInputLength, InputData.Data.strInputLengthOverInch, "Enter length");
            Driver.SendKeys(txtInputWidth, "1", "Enter width");
            Driver.SendKeys(txtInputHeight, "1", "Enter height");
            Driver.SelectFromDropdown(selUOMDimension, InputData.Data.selUOMDemen, "Select inch for UOM");
            Driver.Click(chkStackable, "Click 'Stackable' option to trigger error message to display");
            Assert.AreEqual(InputData.Data.strOverDimsErrorMessage, Driver.FindElement(lblOverDimsErrorMessageLTL).Text.ToString(), "Verify Error Message");
        }

        public void GoToGetQuoteFTL()
        {

            Driver.Click(btnGetQuote, "Click 'Get Quote'");
            Driver.WaitForCondition(d => Driver.IsDisplayed(FtlHeading, "Verify FTL Shipment"), "Wait for 'FTL Shipment' to load");
            Driver.ScrollUp();
            Driver.Click(FtlHeading, "Click 'FTL Shipment'");
            Driver.WaitForCondition(d => Driver.IsDisplayed(txtCitySource, "Verify Pickup Location Type"), "Wait for 'Pickup Location Type' to load");
        }

        public void GoToGetQuoteLTL()
        {
            Driver.Click(btnGetQuote, "Click 'Get Quote'");
            Driver.WaitForCondition(d => Driver.IsDisplayed(Ltlheading, "Verify LTL Shipment"), "Wait for 'LTL Shipment' to load");
            Driver.ScrollUp();
            Driver.Click(Ltlheading, "Click 'LTL Shipment'");
            Driver.WaitForCondition(d => Driver.IsDisplayed(selPickUpLocation, "Verify Pickup Location Type"), "Wait for 'Pickup Location Type' to load");
        }

        public void SelectHighlightedText(string[] XPath)
        {
            bool XPathFound = false;
            int i = 0;
            while (XPathFound == false && i < XPath.Length)
            {
                try
                {
                    By lblHighlightedText = By.XPath(XPath[i]);
                    //Driver.FindElement(lblHighlightedText);
                    Driver.Click(lblHighlightedText, "Select highlighted text");
                    XPathFound = true;
                }
                catch
                {
                    i++;
                }
            }

            if (i == XPath.Length)
            {
                Assert.Fail("Highlighted text does not match any of the possible variations in the current list. Script will have to be updated with new variation.");
            }
        }

        public void SaveGlobalQuoteVarByTruckType(string truckType, double quote)
        {
            if (truckType == "Flatbed")
            {
                InputData.Data.QuoteFTLFlatbed = quote;
            }
            else if (truckType == "Van")
            {
                InputData.Data.QuoteFTLVan = quote;
            }
            else if (truckType == "Reefer")
            {
                InputData.Data.QuoteFTLReefer = quote;
            }
            else
            {
                Assert.Fail("Invalid equipment type: " + truckType);
            }
        }

        public double GetGlobalQuoteVarByTruckType(string truckType)
        {
            double quote = 0.00;
            if (truckType == "Flatbed")
            {
                quote = InputData.Data.QuoteFTLFlatbed;
            }
            else if (truckType == "Van")
            {
                quote = InputData.Data.QuoteFTLVan;
            }
            else if (truckType == "Reefer")
            {
                quote = InputData.Data.QuoteFTLReefer;
            }
            else
            {
                Assert.Fail("Invalid equipment type: " + truckType);
            }
            return quote;
        }

        public void getDropDownInfoLTL()
        {
            List<String> expectedResultLTL = new List<string> { "Shipment weighs between 150 - 19,999 lbs",
            "1-6 non-stackable (or up to 12 stackable) pallets","Does not require an entire 48 ft or 53 ft dry van trailer",
            "Shared space helps minimize cost when compared to full truckload"};
            Driver.Click(icnLearnMoreLTL_FTL, "moreicon");
            List<string> actualResultLTL = new List<string>();
            var actualElements = Driver.FindElements(drpDwnElementsLTL);
            foreach (var x in actualElements)
            {
                actualResultLTL.Add(x.Text);
            }
            Assert.AreEqual(expectedResultLTL, actualResultLTL, "Lists are not equal");
        }

        public void getDropDownInfoFTL()
        {
            List<String> expectedResultFTL = new List<string> { "Shipment weighs up to 48,000 lbs (depending on truck type)",
          "Can be pallets, crates, large parcels or other shipping units","Standard trailer truck, refrigerated truck or flatbed"};
            Driver.Click(icnLearnMoreLTL_FTL, "moreicon");
            List<string> actualResultFTL = new List<string>();
            var actualElements = Driver.FindElements(drpDwnElementsFTL);
            foreach (var x in actualElements)
            {
                actualResultFTL.Add(x.Text);
            }
            Assert.AreEqual(expectedResultFTL, actualResultFTL, "Lists are not equal");
        }

        //public void CheckErrorMessageLenghtFTL(string truckName,string length)
        //{
        //    var home = LoginFTL(truckName);
        //    home.SignIn().EnterDetailsFTL();
        //    Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
        //    Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
        //    Driver.SendKeys(txtInputLength, length, "Entering invalid length");
        //    Driver.Click(txtInputWidth, "just click");
        //    Assert.IsTrue(Driver.IsDisplayed(lblOverDimsErrorMessageFTL, "error should pop up"), "Error message is not displayed");
        //}
    }
}

