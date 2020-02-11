using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GoShipUI
{
	public class DetailsPage : BasePage
    {
        public DetailsPage(Driver driver, Assert assert) : base(driver, assert) { }
        private By txtCompanyName = By.Id("PickUpName");
        private By txtAddressLine1 = By.Id("PickUpAddress1");
        private By txtDeliveryName = By.Id("DeliveryName");
        private By txtDelAddressLine1 = By.Id("DeliveryAddress1");
        private By txtPickupContactName = By.Id("PickUpContactName");
        private By txtPickupPhoneNumber = By.Id("PickUpContactPhone");
        private By txtPickupContactEmail = By.Id("PickUpContactEmail");
        private By txtPickupInstructions = By.Id("PickUpInstructions");
        private By txtDelContactName = By.Id("DeliveryContactName");
        private By txtDelContactNumber = By.Id("DeliveryContactPhone");
        private By txtDelContactEmail = By.Id("DeliveryContactEmail");
        private By txtPickUpStartTime = By.Id("PickUpStartTime");
        private By txtPickUpEndTime = By.Id("PickUpEndTime");
        private By txtDelStartTime = By.Id("DeliveryStartTime");
        private By txtDelEndTime = By.Id("DeliveryEndTime");
        private By txtDelInstructions = By.Id("DeliveryInstructions");
        private By txtItemDescription = By.Id("detailed-item-description-id");
        private By txtItemDescriptionDetail = By.XPath("//*[@id='detailed-item-description-id']/input");
        private By btnContinueToPayment = By.Id("continue-to-payment-id");
        private By btnContinueToPaymentFTL = By.Id("to-quotes");
        private By btnProvideCommodityDetails = By.Id("provide-commodity-details-id");
		private By chkPickupSaveAddress = By.Id("PickUpSaveToAddressBook");
		private By chkDeliverySaveAddress = By.Id("DeliverySaveToAddressBook");
		private By selSavedPickupAddresses = By.Id("PickUpSaved");
		private By selSavedDeliveryAddresses = By.Id("DeliverySaved");
		private By btnCancel = By.Id("cancelOrder");
		private By btnClear = By.XPath("//*[@class='btn btn-border' and text()='Clear']");
		private By lblConfirmClear = By.XPath("//*[@class='cancelConfirm-header']");
        private By txtDeliveryAddress1 = By.Id("DeliveryAddress1");
        private By btnSignIn = By.Id("//*[text() ='Sign in']");
		private By btnContinueToCustomsLTL = By.XPath("//*[@class='btn btn-action aN_10 ng-scope']");
		private By txtInputQuantity = By.Id("input-quantity");
		private By txtInputPieces = By.Id("input-pieces");
		private By txtInputLength = By.Id("input-length");
        private By txtInputWidth = By.Id("input-width");
        private By txtInputHeight = By.Id("input-height");
        private By selUOMDimension = By.Id("select-dimension-type");
        private By txtInputWeight = By.Id("inputWeight0");
        private By selUOMWeight = By.Id("select-weight-type-uom");
        private By txtItemValue = By.Id("itemValue0");
		private By txtLoadTemp = By.XPath("//*[@id='load-temperature-id']/descendant::input");
        private By lblOverDimsGeneralErrorMessageFTL = By.XPath("//*[contains(@class,'col-md-12 ng-scope')]/div/h4[contains(text(),'request exceeds')]");
        private By lblOverDimsWeightErrorMessageFTL = By.XPath("//*[contains(@class,'col-md-12 ng-scope')]/div/h4[contains(text(),'weight exceeds')]");
        public void CheckErrorMessageLenghtFTL(string length)
        {
            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
            Driver.SendKeys(txtInputLength, length, "Entering invalid length");
            Driver.Click(txtInputWidth, "just click");
            Assert.IsTrue(Driver.IsDisplayed(lblOverDimsGeneralErrorMessageFTL, "error should pop up"), "Error message is not displayed");
        }

        public void CheckErrorMessageWidthFTL(string width)
        {
            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
            Driver.SendKeys(txtInputWidth, width, "Entering invalid width");
            Driver.Click(txtInputLength, "just click");
            Assert.IsTrue(Driver.IsDisplayed(lblOverDimsGeneralErrorMessageFTL, "dimension is exceeded error should pop up"), "Error message is not displayed");
        }

        public void CheckErrorMessageHeightFTL(string height)
        {
            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
            Driver.SendKeys(txtInputHeight, height, "Entering invalid height");
            Driver.Click(txtInputLength, "just click");
            Assert.IsTrue(Driver.IsDisplayed(lblOverDimsGeneralErrorMessageFTL, "dimension is exceeded error should pop up"), "Error message is not displayed");
        }

        public void CheckErrorMessageWeighttFTL_FlatBed(string weight)
        {
            var expectedError = "Total weight exceeds 47,500 lbs/21,546 kg allowable weight per shipment. Please correct and try again.";
            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
            Driver.SendKeys(txtInputWeight, weight, "Entering invalid weight");
            Driver.Click(txtInputLength, "just click");
            var actualErrror= Driver.GetElementText(lblOverDimsWeightErrorMessageFTL);
            var commonError = Driver.IsDisplayed(lblOverDimsGeneralErrorMessageFTL, "dimension is exceeded error should pop up");
            var weightError = actualErrror == expectedError;
            Assert.IsTrue(commonError && weightError,$"Error message is not displayed for {weight}lb");
        }

        public void CheckErrorMessageWeighttFTL_Van(string weight)
        {
            var expectedError = "Total weight exceeds 45,000 lbs/20,412 kg allowable weight per shipment. Please correct and try again.";
            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
            Driver.SendKeys(txtInputWeight, weight, "Entering invalid weight");
            Driver.Click(txtInputLength, "just click");
            var actualErrror = Driver.GetElementText(lblOverDimsWeightErrorMessageFTL);
            var commonError = Driver.IsDisplayed(lblOverDimsGeneralErrorMessageFTL, "dimension is exceeded error should pop up");
            var weightError = actualErrror == expectedError;
            Assert.IsTrue(commonError && weightError, $"Error message is not displayed for {weight}lb");
        }

        public void CheckErrorMessageWeighttFTL_Reefer(string weight)
        {
            var expectedError = "Total weight exceeds 43,500 lbs/19,731 kg allowable weight per shipment. Please correct and try again.";
            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
            Driver.SendKeys(txtInputWeight, weight, "Entering invalid weight");
            Driver.Click(txtInputLength, "just click");
            var actualErrror = Driver.GetElementText(lblOverDimsWeightErrorMessageFTL);
            var commonError = Driver.IsDisplayed(lblOverDimsGeneralErrorMessageFTL, "dimension is exceeded error should pop up");
            var weightError = actualErrror == expectedError;
            Assert.IsTrue(commonError && weightError, $"Error message is not displayed for {weight}lb");
        }
        public PaymentPage EnterDetailsLTL(bool savePickupAddress = false, bool saveDeliveryAddress = false, bool continueToPayment = true)
        {
			Driver.WaitForCondition(d => Driver.FindElement(txtCompanyName).Displayed, "Wait for Company Name to be visible");
			EnterPckDelDetails();
            Driver.SendKeys(txtItemDescription, InputData.Data.strItemDescription, "Enter detailed item description");

			//ClickToSaveAddress(savePickupAddress, saveDeliveryAddress);

			if (continueToPayment == true)
			{
				Driver.Click(btnContinueToPayment, "Click 'Continue To Payment'");
			}

            return new PaymentPage(Driver, Assert);

        }

		public CustomsPage EnterDetailsCanadaLTL(bool savePickupAddress = false, bool saveDeliveryAddress = false, bool continueToCustoms = true)
        {
			Driver.WaitForCondition(d => Driver.FindElement(txtCompanyName).Displayed, "Wait for Company Name to be visible");
			EnterPckDelDetails();
            Driver.SendKeys(txtItemDescription, InputData.Data.strItemDescription, "Enter detailed item description");

			//ClickToSaveAddress(savePickupAddress, saveDeliveryAddress);

			if (continueToCustoms == true)
			{
				 Driver.Click(btnContinueToCustomsLTL, "Click 'Continue To Customs'");
			}

            return new CustomsPage(Driver, Assert);

        }
        public DetailsPage EnterDetailsFTL(bool savePickupAddress = false, bool saveDeliveryAddress = false, bool continueToProvideDetails = true)
        {
			Driver.WaitForCondition(d => Driver.FindElement(txtCompanyName).Displayed, "Wait for Company Name to be visible");
			EnterPckDelDetails();

			//ClickToSaveAddress(savePickupAddress, saveDeliveryAddress);

			if (continueToProvideDetails == true)
			{
				Driver.Click(btnProvideCommodityDetails, "Click provide details button");
			}
            return this;

        }
        public PaymentPage EnterRestOfDetails()
        {
            Driver.Click(txtItemDescription, "Verify Item description");
            Driver.EnterByJavascript("detailed-item-description-id", "Enter detailed item description");
            Driver.SendKeys(txtItemDescriptionDetail, InputData.Data.strItemDescription, "Enter detailed item description");
			Driver.WaitForCondition(d => Driver.FindElement(btnContinueToPaymentFTL).Enabled, "Wait for 'Continue to Payment' button to be enabled");
            Driver.Click(btnContinueToPaymentFTL, "Click 'Continue To Payment'");

            return new PaymentPage(Driver, Assert);
        }

		public void ClearInputDetailsPage() 
		{
			Driver.WaitForCondition(d => Driver.FindElement(btnCancel).Displayed, "Wait for cancel button to be visible");
			Driver.Click(btnCancel, "Click 'Cancel' Button");
			Driver.WaitForCondition(d => Driver.IsDisplayed(lblConfirmClear, "Confirm Form Clear"),"Wait for 'Confirm Form Clear' to load");
			Driver.Click(btnClear, "Click 'Clear' Button");
		}

		public void VerifyClearInputDetailsPageLTL()
		{
			Driver.WaitForCondition(d => Driver.FindElement(txtCompanyName).Displayed, "Wait for Company Name to be visible");
			VerifyEmptyDetailsPageFields();
            Assert.IsEmpty(Driver.FindElement(txtItemDescription).Text.ToString(), "Verify detailed item description is blank/empty");
		}

		public void VerifyClearInputDetailsPageFTL()
		{
			Driver.WaitForCondition(d => Driver.FindElement(txtCompanyName).Displayed, "Wait for Company Name to be visible");
			VerifyEmptyDetailsPageFields();
		}

		public void VerifyEmptyDetailsPageFields()
		{
			Assert.IsEmpty(Driver.FindElement(txtCompanyName).Text.ToString(), "Verify Company Name is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtAddressLine1).Text.ToString(), "Verify Pick up Address is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtPickupContactName).Text.ToString(), "Verify Pick up Contact Name is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtPickupPhoneNumber).Text.ToString(), "Verify pick up phone number is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtPickupContactEmail).Text.ToString(), "Verify pick up email address is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtPickupInstructions).Text.ToString().Replace(" ", ""), "Verify pick up instructions is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtDeliveryName).Text.ToString(), "Verify delivery company name is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtDelContactName).Text.ToString(), "Verify Delivery contact name is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtDelAddressLine1).Text.ToString(), "Verify delivery address is blank/empty");          
            Assert.IsEmpty(Driver.FindElement(txtDelContactNumber).Text.ToString(), "Verify delivery phone number is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtDelContactEmail).Text.ToString(), "Verify delivery email address is blank/empty");
            Assert.IsEmpty(Driver.FindElement(txtDelInstructions).Text.ToString().Replace(" ", ""), "Verify delivery instructions is blank/empty");
            Assert.AreEqual("08:00", Driver.FindElement(txtPickUpStartTime).GetAttribute("value").ToString().Substring(0, 5), "Verify Pick up Start Time is 08:00");
            Assert.AreEqual("16:00", Driver.FindElement(txtPickUpEndTime).GetAttribute("value").ToString().Substring(0, 5), "Verify Pick up End Time is 16:00");
            Assert.AreEqual("08:00", Driver.FindElement(txtDelStartTime).GetAttribute("value").ToString().Substring(0, 5), "Verify Delivery Start Time is 08:00");
            Assert.AreEqual("16:00", Driver.FindElement(txtDelEndTime).GetAttribute("value").ToString().Substring(0, 5), "Verify Delivery End Time is 16:00");
		}

		public void EnterPckDelDetails()
		{
			Driver.SendKeys(txtCompanyName, InputData.Data.strPckCompName, "Enter pick up company name");
            Driver.SendKeys(txtAddressLine1, InputData.Data.strPckAddressLine1, "Enter pick up address");
            Driver.SendKeys(txtPickupContactName, InputData.Data.strPckContactName, "Enter Pick up Contact Name");
            Driver.SendKeys(txtPickupPhoneNumber, InputData.Data.strPckPhoneNo, "Enter pick up phone number");
            Driver.SendKeys(txtPickupContactEmail, InputData.Data.strPckContactEmail, "Enter pick up email address");
            Driver.SendKeys(txtPickupInstructions, InputData.Data.strPckInstructions, "Enter pick up instructions");
            Driver.SendKeys(txtDeliveryName, InputData.Data.strDelCompName, "Enter delivery company name");
            Driver.SendKeys(txtDelContactName, InputData.Data.strDelContactName, "Enter Delivery contact name");
            Driver.SendKeys(txtDelAddressLine1, InputData.Data.strDelAddressLine1, "Enter delivery address");
            Driver.SendKeys(txtDelContactNumber, InputData.Data.strDelPhoneNo, "Enter delivery phone number");
            Driver.SendKeys(txtDelContactEmail, InputData.Data.strDelEmail, "Enter delivery email address");
            Driver.SendKeys(txtDelInstructions, InputData.Data.strDelInstructions, "Enter delivery instructions");
            Driver.SendKeys(txtPickUpStartTime, InputData.Data.strPckStartTime, "Enter Pick up Start Time");
            Driver.SendKeys(txtPickUpEndTime, InputData.Data.strPckEndTime, "Enter Pick up End Time");
            Driver.SendKeys(txtDelStartTime, InputData.Data.strDelStartTime, "Enter Delivery Start Time");
            Driver.SendKeys(txtDelEndTime, InputData.Data.strDelEndTime, "Enter Delivery End Time");   
		}

		public void ProvideCommodityDetails(bool isReefer = false)
        {
            Driver.SendKeys(txtInputQuantity, InputData.Data.strInputQuantity, "Enter Quantity");
            Driver.SendKeys(txtInputPieces, "1", "Enter number of pieces");
            Driver.SendKeys(txtInputLength, InputData.Data.strInputLength, "Enter length");
            Driver.SendKeys(txtInputWidth, InputData.Data.strInputWidth, "Enter width");
            Driver.SendKeys(txtInputHeight, InputData.Data.strInputHeight, "Enter height");
            Driver.SelectFromDropdown(selUOMDimension, InputData.Data.selUOMDemen, "Select measurement of dimension");
            Driver.SendKeys(txtInputWeight, InputData.Data.strInputWeight, "Enter input weight");
            Driver.SelectFromDropdown(selUOMWeight, InputData.Data.selUOMWeight, "Select measurement of weight");
            Driver.SendKeys(txtItemValue, InputData.Data.strItemValue, "Enter item value");
            if (isReefer == true)
            {
                Driver.SendKeys(txtLoadTemp, InputData.Data.strLoadTemp, "Enter load temperature");
            }
        }
		/*
		// Not tested
		public void ClickToSaveAddress(bool savePickupAddress, bool saveDeliveryAddress)
		{
			if (savePickupAddress == true)
			{
				Driver.Click(chkPickupSaveAddress, "Click checkbox to save pickup address to address book");
				CheckSaveAddressToAddressBook(selSavedPickupAddresses, true);
			}
			if (saveDeliveryAddress == true)
			{
				Driver.Click(chkDeliverySaveAddress, "Click checkbox to save delivery address to address book");
				CheckSaveAddressToAddressBook(selSavedDeliveryAddresses, false);
			}
		}

		public void CheckSaveAddressToAddressBook(By locator, bool forPickup)
		{
			SelectElement sel = new SelectElement(Driver.FindElement(locator));
			IList<IWebElement> selPickup = sel.Options;

			if (forPickup == true)
			{
				if (InputData.Data.numSavedPickupAddresses == 0)
				{
					InputData.Data.numSavedPickupAddresses = selPickup.Count();
				}
				else
				{
					Assert.AreEqual(InputData.Data.numSavedPickupAddresses + 1, selPickup.Count(), "Verify if a new pickup address was added to the list");
				}
			}
			else
			{
				if (InputData.Data.numSavedDeliveryAddresses == 0)
				{
					InputData.Data.numSavedDeliveryAddresses = selPickup.Count();
				}
				else
				{
					Assert.AreEqual(InputData.Data.numSavedDeliveryAddresses + 1, selPickup.Count(), "Verify if a new delivery address was added to the list");
				}
			}
		}
		*/
    }
}
