using OpenQA.Selenium;
using Utilities;

namespace GoShipUI
{
	public class CustomsPage : BasePage
	{
		public CustomsPage(Driver driver, Assert assert) : base(driver, assert) { }

		private UtilityMethods Util = new UtilityMethods();

		private By CustomsPageTitleHeader = By.XPath("//*[@class='main-title' and text()='Customs Information']");

		private By btnContinueToPayment = By.XPath("//*[text()='CONTINUE TO PAYMENT']");
		private By btnReferredCustomsBroker = By.XPath("//*[contains(@class,'col-md-12 padding-top-10')]/descendant::input");
		private By btnUserCustomsBroker = By.XPath("//*[contains(@class,'col-md-12 padding-20 padding-top-14')]/descendant::input");
		private By chkAuthorizeCustomsBroker = By.XPath("//*[@class='item-new-label broker-info-header']/descendant::input");
		private By btnCustomsInvoice = By.XPath("//*[label='I have a Customs Invoice']/descendant::input");
		private By btnCreateCustomsInvoice = By.XPath("//*[label='I want to create a Customs Invoice']/descendant::input");

		// Customs Broker Information
		private By txtBrokerCompanyName = By.Id("broker-company");
		private By txtBrokerAddress1 = By.XPath("//*[@class='col-md-12 no-side-padding']/descendant::input[2][@placeholder='Address Line 1']");
		private By txtBrokerAddress2 = By.XPath("//*[@class='col-md-12 no-side-padding']/descendant::input[1][@placeholder='Address Line 2']");
		private By txtBrokerEmail = By.XPath("//*[@class='col-md-12 no-side-padding']/descendant::input[2][@placeholder='Contact Email']");
		private By selBrokerCountry = By.Id("country-destination");
		private By txtBrokerCityStateZip = By.XPath("//*[@id='broker-destination' and @placeholder='Delivery City or Zip/Postal Code']");
		private By lblBrokerHighlightedText = By.XPath("//*[@class='highlight' and text()='PITTSBURGH, PA, 15122']");
		private By txtBrokerPhoneNumber = By.XPath("//*[@class='col-md-12 no-side-padding']/descendant::input[1][@placeholder='Contact Phone Number']");
		private By selPaymentTerms = By.Id("paymentTerms");

		// Customs Invoice
		// Vendor Address Details
		private By chkUseVendorPickUpAddr = By.Id("Vendor-usePickUp");
		private By chkUseVendorDeliveryAddr = By.Id("Vendor-useDelivery");
		private By txtVendorCompanyName = By.Id("Vendor-company");
		private By txtVendorContactName = By.Id("Vendor-contact-name");
		private By txtVendorAddress1 = By.Id("Vendor-address-line1");
		private By txtVendorAddress2 = By.Id("Vendor-address-line2");
		private By selVendorCountry = By.Id("Vendor-country-destination");
		private By txtVendorCityStateZip = By.XPath("//*[@id='Vendor-destination' and @placeholder='Vendor City or Zip/Postal Code']");
		private By txtVendorPhoneNumber = By.Id("Vendor-phone");
		private By txtVendorFaxNumber = By.Id("Vendor-fax");
		private By txtVendorEmail = By.Id("Vendor-email");

		// Purchaser Address Details
		private By chkUsePurchaserPickUpAddr = By.Id("Purchaser-usePickUp");
		private By chkUsePurchaserDeliveryAddr = By.Id("Purchaser-useDelivery");
		private By txtPurchaserCompanyName = By.Id("Purchaser-company");
		private By txtPurchaserContactName = By.Id("Purchaser-contact-name");
		private By txtPurchaserAddress1 = By.Id("Purchaser-address-line1");
		private By txtPurchaserAddress2 = By.Id("Purchaser-address-line2");
		private By selPurchaserCountry = By.Id("Purchaser-country-destination");
		private By txtPurchaserCityStateZip = By.XPath("//*[@id='Purchaser-destination' and @placeholder='Purchaser City or Zip/Postal Code']");
		private By txtPurchaserPhoneNumber = By.Id("Purchaser-phone");
		private By txtPurchaserFaxNumber = By.Id("Purchaser-fax");
		private By txtPurchaserEmail = By.Id("Purchaser-email");

		private By lblCANHighlightedText = By.XPath("//*[@class='highlight' and text()='OTTAWA, ON, K1A 0A1']");
		private By lblUSAHighlightedText = By.XPath("//*[@class='highlight' and text()='PITTSBURGH, PA, 15220']");

		// Customs Invoice Details
		private By selItemCondition = By.XPath("//*[@id='item-condition']/descendant::option[@selected='selected']");
		private By txtItemValue = By.Id("itemValue");
		private By txtItemDescription = By.Id("item-description");
		private By txtTariffNumber = By.Id("tariffNum");
		private By selOriginCountry = By.Id("item-origin");
		private By lblItemInformation = By.XPath("//*[@class='media-heading font-size-16 ng-binding']");

		public PaymentPage EnterCustomsDetailsLTL(bool haveCustomsBroker = true, bool haveCustomsInvoice = true, int pickupOption = 0, int deliveryOption = 0)
        {
			WaitForLoadIndicator();
			Driver.WaitForCondition(d => Driver.IsDisplayed(CustomsPageTitleHeader, "Customs Information"),"Wait for Customs page to load");
			if (haveCustomsBroker == false)
			{
				Driver.WaitForCondition(d => Driver.IsDisplayed(chkAuthorizeCustomsBroker, "Authorize ClearIt checkbox"),"Wait for authorize checkbox to load");
				Driver.Sleep();
				Driver.Click(chkAuthorizeCustomsBroker, "Click checkbox to authorize PLS referred customs broker to act on user's behalf");
			}
			else
			{
				Driver.WaitForCondition(d => Driver.IsDisplayed(btnUserCustomsBroker, "User has Customs Broker"),"Wait for 'I have a Customs Broker' radio button to load");
				Driver.Sleep();
				Driver.Click(btnUserCustomsBroker, "Click radio button 'I have a Customs Broker'");
				Driver.SendKeys(txtBrokerCompanyName, InputData.Data.strCustomsBrokerCompanyName, "Enter Customs Broker Company Name");
				Driver.SendKeys(txtBrokerAddress1, InputData.Data.strCustomsBrokerAddress1, "Enter Customs Broker Address Line 1");
				//Driver.SendKeys(txtBrokerAddress1, InputData.Data.strCustomsBrokerAddress1, "Enter Customs Broker Address Line 2");
				Driver.SendKeys(txtBrokerEmail, InputData.Data.strCustomsBrokerEmail, "Enter Customs Broker Email Address");
				Driver.SelectFromDropdown(selBrokerCountry, InputData.Data.selUSA, "Select Customs Broker Country from Dropdown");
				Driver.SendKeys(txtBrokerCityStateZip, InputData.Data.strCustomsBrokerCityStateZip, "Enter Customs Broker City/State/Zip");
				Driver.Click(lblBrokerHighlightedText, "Select highlighted broker location text");
				Driver.SendKeys(txtBrokerPhoneNumber, InputData.Data.strCustomsBrokerPhoneNumber, "Enter Customs Broker Phone Number");
			}
			if (haveCustomsInvoice == false)
			{
				Driver.WaitForCondition(d => Driver.IsDisplayed(btnCreateCustomsInvoice, "User has Customs Invoice"),"Wait for 'I want to create a Customs Invoice' radio button to load");
				Driver.Click(btnCreateCustomsInvoice, "Click radio button to create Customs Invoice");
				switch(pickupOption)
				{
					case 1:
						Driver.SendKeys(chkUseVendorPickUpAddr, "", "Focus on 'Use Pick Up Address as Vendor Address' checkbox");
						Driver.Click(chkUseVendorPickUpAddr, "Select 'Use Pick Up Address as Vendor Address'");
						/*
						InputData.Data.strCANCompanyName = InputData.Data.strPckCompName;
						InputData.Data.strCANContactName = InputData.Data.strPckContactName;
						InputData.Data.strCANAddress1 = InputData.Data.strPckAddressLine1;
						InputData.Data.strCANFullCityStateZip = InputData.Data.strFullCitySourceCAN;
						InputData.Data.strCANFormPhoneNumber = InputData.Data.strFrmPckPhoneNo;
						InputData.Data.strCANEmail = InputData.Data.strPckContactEmail;
						*/
						break;
					case 2:
						Driver.SendKeys(chkUseVendorDeliveryAddr, "", "Focus on 'Use Delivery Address as Vendor Address' checkbox");
						Driver.Click(chkUseVendorDeliveryAddr, "Select 'Use Delivery Address as Vendor Address'");
						/*
						InputData.Data.strUSACompanyName = InputData.Data.strPckCompName;
						InputData.Data.strUSAContactName = InputData.Data.strPckContactName;
						InputData.Data.strUSAAddress1 = InputData.Data.strPckAddressLine1;
						InputData.Data.strUSAFullCityStateZip = InputData.Data.strFullCitySourceCAN;
						InputData.Data.strUSAFormPhoneNumber = InputData.Data.strFrmPckPhoneNo;
						InputData.Data.strUSAEmail = InputData.Data.strPckContactEmail;
						*/
						break;
					case 3:
						// Vendor CAN Address Details
						Driver.SendKeys(txtVendorCompanyName, InputData.Data.strCANCompanyName, "Enter CAN Vendor Company Name");
						Driver.SendKeys(txtVendorContactName, InputData.Data.strCANContactName, "Enter CAN Vendor Contact Name");
						Driver.SendKeys(txtVendorAddress1, InputData.Data.strCANAddress1, "Enter CAN Vendor Address 1");
						//Driver.SendKeys(txtVendorAddress2, InputData.Data.strCANAddress2, "Enter CAN Vendor Address 2");
						Driver.SelectFromDropdown(selVendorCountry, InputData.Data.selCAN, "Select CAN Vendor Country");
						Driver.SendKeys(txtVendorCityStateZip, InputData.Data.strCANCityStateZip, "Enter CAN Vendor City/State/Zip");
						Driver.Click(lblCANHighlightedText, "Select highlighted CAN vendor location text");
						Driver.SendKeys(txtVendorPhoneNumber, InputData.Data.strCANPhoneNumber, "Enter CAN Vendor Phone Number");
						//Driver.SendKeys(txtVendorFaxNumber, InputData.Data.strCANFaxNumber, "Enter CAN Vendor Fax Number");
						Driver.SendKeys(txtVendorEmail, InputData.Data.strCANEmail, "Enter CAN Vendor Email");
						break;
					case 4:
						// Vendor USA Address Details
						Driver.SendKeys(txtVendorCompanyName, InputData.Data.strUSACompanyName, "Enter USA Vendor Company Name");
						Driver.SendKeys(txtVendorContactName, InputData.Data.strUSAContactName, "Enter USA Vendor Contact Name");
						Driver.SendKeys(txtVendorAddress1, InputData.Data.strUSAAddress1, "Enter USA Vendor Address 1");
						//Driver.SendKeys(txtVendorAddress2, InputData.Data.strUSAAddress2, "Enter USA Vendor Address 2");
						Driver.SelectFromDropdown(selVendorCountry, InputData.Data.selUSA, "Select USA Vendor Country");
						Driver.SendKeys(txtVendorCityStateZip, InputData.Data.strUSACityStateZip, "Enter USA Vendor City/State/Zip");
						Driver.Click(lblUSAHighlightedText, "Select highlighted USA vendor location text");
						Driver.SendKeys(txtVendorPhoneNumber, InputData.Data.strUSAPhoneNumber, "Enter USA Vendor Phone Number");
						//Driver.SendKeys(txtVendorFaxNumber, InputData.Data.strUSAFaxNumber, "Enter USA Vendor Fax Number");
						Driver.SendKeys(txtVendorEmail, InputData.Data.strUSAEmail, "Enter USA Vendor Email");
						break;
					default:
						Assert.Fail("Invalid pickup option");
						break;
				}
				switch(deliveryOption)
				{
					case 1:
						Driver.SendKeys(chkUsePurchaserPickUpAddr, "", "Focus on 'Use Pick Up Address as Vendor Address' checkbox");
						Driver.Click(chkUsePurchaserPickUpAddr, "Select 'Use Pick Up Address as Purchaser Address'");
						/*
						InputData.Data.strCANCompanyName = InputData.Data.strDelCompName;
						InputData.Data.strCANContactName = InputData.Data.strDelContactName;
						InputData.Data.strCANAddress1 = InputData.Data.strDelAddressLine1;
						InputData.Data.strCANFullCityStateZip = InputData.Data.strFullCityDestination;
						InputData.Data.strCANFormPhoneNumber = InputData.Data.strFrmDelPhoneNo;
						InputData.Data.strCANEmail = InputData.Data.strDelEmail;
						*/
						break;
					case 2:
						Driver.SendKeys(chkUsePurchaserDeliveryAddr, "", "Focus on 'Use Delivery Address as Vendor Address' checkbox");
						Driver.Click(chkUsePurchaserDeliveryAddr, "Select 'Use Delivery Address as Purchaser Address'");
						/*
						InputData.Data.strUSACompanyName = InputData.Data.strDelCompName;
						InputData.Data.strUSAContactName = InputData.Data.strDelContactName;
						InputData.Data.strUSAAddress1 = InputData.Data.strDelAddressLine1;
						InputData.Data.strUSAFullCityStateZip = InputData.Data.strFullCityDestination;
						InputData.Data.strUSAFormPhoneNumber = InputData.Data.strFrmDelPhoneNo;
						InputData.Data.strUSAEmail = InputData.Data.strDelEmail;
						*/
						break;
					case 3:
						// Purchaser Address Details
						Driver.SendKeys(txtPurchaserCompanyName, InputData.Data.strCANCompanyName, "Enter CAN Purchaser Company Name");
						Driver.SendKeys(txtPurchaserContactName, InputData.Data.strCANContactName, "Enter CAN Purchaser Contact Name");
						Driver.SendKeys(txtPurchaserAddress1, InputData.Data.strCANAddress1, "Enter CAN Purchaser Address 1");
						//Driver.SendKeys(txtPurchaserAddress2, InputData.Data.strCANAddress2, "Enter CAN Purchaser Address 2");
						Driver.SelectFromDropdown(selPurchaserCountry, InputData.Data.selCAN, "Select CAN Purchaser Country");
						Driver.SendKeys(txtPurchaserCityStateZip, InputData.Data.strCANCityStateZip, "Enter CAN Purchaser City/State/Zip");
						Driver.Click(lblCANHighlightedText, "Select highlighted CAN Purchaser location text");
						Driver.SendKeys(txtPurchaserPhoneNumber, InputData.Data.strCANPhoneNumber, "Enter CAN Purchaser Phone Number");
						//Driver.SendKeys(txtPurchaserFaxNumber, InputData.Data.strCANFaxNumber, "Enter CAN Purchaser Fax Number");
						Driver.SendKeys(txtPurchaserEmail, InputData.Data.strCANEmail, "Enter CAN Purchaser Email");
						break;
					case 4:
						// Purchaser Address Details
						Driver.SendKeys(txtPurchaserCompanyName, InputData.Data.strUSACompanyName, "Enter USA Purchaser Company Name");
						Driver.SendKeys(txtPurchaserContactName, InputData.Data.strUSAContactName, "Enter USA Purchaser Contact Name");
						Driver.SendKeys(txtPurchaserAddress1, InputData.Data.strUSAAddress1, "Enter USA Purchaser Address 1");
						//Driver.SendKeys(txtPurchaserAddress2, InputData.Data.strUSAAddress2, "Enter USA Purchaser Address 2");
						Driver.SelectFromDropdown(selPurchaserCountry, InputData.Data.selUSA, "Select USA Purchaser Country");
						Driver.SendKeys(txtPurchaserCityStateZip, InputData.Data.strUSACityStateZip, "Enter USA Purchaser City/State/Zip");
						Driver.Click(lblUSAHighlightedText, "Select highlighted USA purchaser location text");
						Driver.SendKeys(txtPurchaserPhoneNumber, InputData.Data.strUSAPhoneNumber, "Enter USA Purchaser Phone Number");
						//Driver.SendKeys(txtPurchaserFaxNumber, InputData.Data.strUSAFaxNumber, "Enter USA Purchaser Fax Number");
						Driver.SendKeys(txtPurchaserEmail, InputData.Data.strUSAEmail, "Enter USA Purchaser Email");
						break;
					default:
						Assert.Fail("Invalid delivery option");
						break;
				}

				Driver.SelectFromDropdown(selPaymentTerms, InputData.Data.selPaymentTerms, "Select Payment Terms");

				Assert.AreEqual(InputData.Data.selCondition, Driver.FindElement(selItemCondition).Text.ToString(), "Verify Item Condition on Customs Page");
				//Assert.AreEqual(InputData.Data.strItemValue, string.Format("{0:0.00}", Driver.FindElement(txtItemValue).Text.ToString()), "Verify Item Value on Customs Page");
				string itemInfo = Driver.FindElement(lblItemInformation).Text.ToString();
				int index = itemInfo.IndexOf("valued");
				double declaredValue = Util.FindDecimalInString(itemInfo.Substring(index));
				string declaredValueFormatted = string.Format("{0:0.00}", declaredValue);
				Assert.AreEqual(InputData.Data.strItemValue, declaredValueFormatted, "Verify Item Value on Customs Page");
				//Assert.AreEqual(InputData.Data.strItemDescription, Driver.FindElement(txtItemDescription).Text.ToString(), "Verify Item Description on Customs Page");
				Driver.SendKeys(txtTariffNumber, InputData.Data.strTariffNumber, "Enter Tariff Number");
				Driver.SelectFromDropdown(selOriginCountry, InputData.Data.selCountryOfOrigin, "Select Country of Origin from Dropdown");
			}
			Driver.WaitForCondition(d => Driver.IsEnabled(btnContinueToPayment, "Wait for 'Continue to Payment' to be enabled"), "Wait for 'Continue to Payment' to be enabled");
            Driver.Click(btnContinueToPayment, "Click 'Continue To Payment'");

            return new PaymentPage(Driver, Assert);
        }
	}
}
