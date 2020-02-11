using OpenQA.Selenium;
using System;
using Utilities;

namespace GoShipUI
{
	public class UserProfilePage : BasePage
	{
		public UserProfilePage(Driver driver, Assert assert) : base(driver, assert) { }
		private UtilityMethods Util = new UtilityMethods();

		private By lnkShipmentTabLTL = By.XPath("//*[@href='#/homeFtl' and text()='FTL Shipments']");
		private By lnkShipmentTabFTL = By.XPath("//*[@href='#/home' and text()='LTL Shipments']");
		private By txtShipmentNumber = By.Id("trackShipmentNumber");
		private By btnFindBOL = By.XPath("//*[contains(@class,'trackShipment') and text()='Find BOL']");

		private By lblConfirmationNumber = By.XPath("//*[@class='col-xs-12 ng-binding' and contains(.,'BOL #:')]");
		private By lblEstDeliveryDate = By.XPath("//*[@class='ng-binding ng-scope' and contains(.,'Estimated Delivery:')]");

		private By lblPickupInformation = By.XPath("//*[@class='col-md-6 vert-line-right']/div[1]");
		private By lblPickupStartTime = By.XPath("//*[@class='col-md-6 vert-line-right']/div[1]/div[1]");
		private By lblPickupEndTime = By.XPath("//*[@class='col-md-6 vert-line-right']/div[1]/div[2]");
		private By lblDeliveryInformation = By.XPath("//*[@class='col-md-6 vert-line-right']/div[3]");
		private By lblDeliveryStartTime = By.XPath("//*[@class='col-md-6 vert-line-right']/div[3]/div[1]");
		private By lblDeliveryEndTime = By.XPath("//*[@class='col-md-6 vert-line-right']/div[3]/div[2]");

		private By lblCarrierName = By.XPath("//*[@class='col-xs-12 ng-scope']/a");
		private By btnChangePassword = By.XPath("//*[@id='customerData']/descendant::a[text()='Change password']");

		// Change password page
		private By txtCurrentPassword = By.Id("currentPassword");
		private By txtNewPassword = By.Id("newPassword");
		private By txtConfirmedNewPassword = By.Id("confirmedPassword");
		private By btnChangePass = By.XPath("//*[@id='displayContainer']/descendant::button[text()='CHANGE PASSWORD']");
		private By lblThankYou = By.XPath("//*[@class='login-title']/h2");
		private By lblConfirmResetMessage = By.XPath("//*[@id='displayContainer']/descendant::label[4]");

		private string confirmationMessage = "Your password has been changed.";

		public void LookupLoadLTL(long confNum, DateTime pickupDate, DateTime deliveryDate, bool isCanada = false)
		{
			Driver.WaitForCondition(d => Driver.FindElement(txtShipmentNumber).Displayed, "Wait for confirmation number field to be visible");
			Driver.SendKeys(txtShipmentNumber, confNum.ToString(), "Enter confirmation number");
			Driver.Click(btnFindBOL, "Click 'Find BOL'");
			WaitForLoadIndicator();

			VerifyLoadInformation(confNum, pickupDate, deliveryDate);

			if (isCanada == false)
			{
				Assert.AreEqual(InputData.Data.CarrierName, Driver.FindElement(lblCarrierName).Text, "Validate Carrier Information");
			}
			else
			{
				Assert.AreEqual(InputData.Data.CarrierNameCAN, Driver.FindElement(lblCarrierName).Text, "Validate Carrier Information");
			}
		}

		public void LookupLoadFTL(long confNum, DateTime pickupDate, DateTime deliveryDate)
		{
			Driver.WaitForCondition(d => Driver.FindElement(txtShipmentNumber).Displayed, "Wait for confirmation number field to be visible");
			Driver.SendKeys(txtShipmentNumber, confNum.ToString(), "Enter confirmation number");
			Driver.Click(btnFindBOL, "Click 'Find BOL'");
			WaitForLoadIndicator();

			VerifyLoadInformation(confNum, pickupDate, deliveryDate);
		}

		public void VerifyLoadInformation(long confNum, DateTime pickupDate, DateTime deliveryDate)
		{
			long ConfirmationId = Util.FindLongInString(Driver.FindElement(lblConfirmationNumber).Text);
			Assert.AreEqual(confNum, ConfirmationId, "Verify that correct confirmation number is displayed");

			Assert.AreEqual("Estimated Delivery: " + deliveryDate.ToString("MM/dd/yyyy"), Driver.FindElement(lblEstDeliveryDate).Text, "Verify estimated delivery date");

			//pick up window
            var dat = "Start: " + pickupDate.ToString("MMM d, yyyy ") + "9:30 AM";
            Assert.AreEqual(dat, Driver.FindElement(lblPickupStartTime).Text, "Validate Pick up start window");
            dat = "End: " + pickupDate.ToString("MMM d, yyyy ") + "2:45 PM";
            Assert.AreEqual(dat, Driver.FindElement(lblPickupEndTime).Text, "Validate Pick up end window");

			//delivery window
            dat = "Start: " + deliveryDate.ToString("MMM d, yyyy ") + "10:30 AM";
            Assert.AreEqual(dat, Driver.FindElement(lblDeliveryStartTime).Text, "Validate Delivery start window");
            dat = "End: " + deliveryDate.ToString("MMM d, yyyy ") + "3:30 PM";
            Assert.AreEqual(dat, Driver.FindElement(lblDeliveryEndTime).Text, "Validate Delivery end window");

			/*
            //pick up location
            Assert.IsTrue(InputData.Data.strPckAddressLine1+" "+InputData.Data.strCitySource, Driver.FindElement(lblPckAddress).Text, "Validating Pick Up address.");
            Assert.AreEqual(InputData.Data.strPckContactName, Driver.FindElement(lblPckContactName).Text, "Validate Pick up Contact Name");
            Assert.AreEqual(InputData.Data.strFormPckPhoneNo, Driver.FindElement(lblPckPhoneNo).Text, "Validate Pick up Phone no");

            //delivery location
            Assert.AreEqual(InputData.Data.strDelAddressLine1+", "+InputData.Data.strCityDestination, Driver.FindElement(lblDelAddress).Text, "Validate Delivery address");
            Assert.AreEqual(InputData.Data.strDelContactName, Driver.FindElement(lblDelContactName).Text, "Validate Delivery Contact Name");
            Assert.AreEqual(InputData.Data.strFormDelPhoneNo, Driver.FindElement(lblDelPhoneNo).Text, "Validate Delivery Phone no");
			*/
		}

		public void ChangePassword(bool isFTL)
		{
			Driver.WaitForCondition(d => Driver.FindElement(btnChangePassword).Displayed, "Wait for change password button to be visible");
			Driver.Click(btnChangePassword, "Click on change password button");
			string oldPassword;
			if (isFTL == true)
			{
				oldPassword = InputData.Data.NewUserPasswordFTL;
				InputData.Data.NewUserPasswordFTL = InputData.Data.DefaultNewPassword;
			}
			else
			{
				oldPassword = InputData.Data.NewUserPasswordLTL;
				InputData.Data.NewUserPasswordLTL = InputData.Data.DefaultNewPassword;
			}
			Driver.SendKeys(txtCurrentPassword, oldPassword, "Enter old password: " + oldPassword);
			Driver.SendKeys(txtNewPassword, InputData.Data.DefaultNewPassword, "Enter old password: " + InputData.Data.DefaultNewPassword);
			Driver.SendKeys(txtConfirmedNewPassword, InputData.Data.DefaultNewPassword, "Enter old password: " + InputData.Data.DefaultNewPassword);
			Driver.WaitForCondition(d => Driver.FindElement(btnChangePass).Enabled, "Wait for change password button to be enabled");
			Driver.Click(btnChangePass, "Click change password");

			Driver.WaitForCondition(d => Driver.FindElement(lblConfirmResetMessage).Displayed, "Wait for confirmation message to be visible");
			Assert.AreEqual(confirmationMessage, Driver.FindElement(lblConfirmResetMessage).Text.ToString(), "Verify confirmation message: Expecting - " + confirmationMessage + " Observed - " + Driver.FindElement(lblConfirmResetMessage).Text.ToString());
		}
	}
}
