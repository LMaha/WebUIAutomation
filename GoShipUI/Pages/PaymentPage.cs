using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using System.Configuration;
using Utilities;
using System.Threading;
using SharedUIDataEntities;
using OpenQA.Selenium.Support.UI;

namespace GoShipUI
{
	public class PaymentPage : BasePage
    {
        public PaymentPage(Driver driver, Assert assert) : base(driver, assert) { }
        private By rdNoInsurance = By.Id("noNeed-insurance-id");
        private By rdInsurance = By.Id("need-insurance-id");
        private By txtExpMM = By.Name("exptime1");
        private By txtExpYY = By.Name("exptime2");
        private By txtCardNo = By.Id("NewCard_CardNumber");
        private By txtCVVCode = By.Id("NewCard_SecurityCode");
        private By txtCardHolderName = By.Id("NewCard_CardHolderFirstName");
        private By txtCardHolderLastName = By.Id("NewCard_CardHolderLastName");
        private By selCountryCode = By.Id("NewBillAddresses_CountryCode");
        private By txtBillingAddress = By.Id("NewBillAddresses_AddressLine1");
        private By txtCityCode = By.Id("NewBillAddresses_CityCode");
        private By txtStateCode = By.Id("NewBillAddresses_StateCode");
        private By txtZipCode = By.Id("NewBillAddresses_ZipCode");
        private By btnProcessTransaction = By.Id("btn_Process");
        private By txtCommodityValue = By.Id("insurance-amountInsured-id");
        private By txtPromoCode = By.Id("promo-code-id");
        private By btnApply = By.Id("applyPromoCode-id");
        private By lblDiscount = By.Id("promoCode-discount-goShipCurrency-id");
        private By lblInsurancePremium = By.Id("insurance-premium-goShipCurrency-id");
        private By lblTotalSum = By.Id("//*[@class='total-sum ng-binding']");
        private By lblNoHazmatIns = By.XPath("//*[text()= 'Additional insurance not available for hazmat.']");
        private By lblInsuranceId = By.Id("insurance-coverage-id");
        private By btnAgreeFalvey = By.Id("agree-falvey");
		private By btnApplyFalvey = By.Id("getInsuranceQuote-id");
        private PLS20Entities DataContext = new PLS20Entities();
		private UtilityMethods Util = new UtilityMethods();

        public ConfirmationPage PaymentDetails(int scenario, bool needIns = false, bool hazmat = false, bool IsPromo = true)
        {
            WaitForLoadIndicator();
            List<IWebElement> frames = new List<IWebElement>(Driver.FindElements(By.TagName("iframe")));
            NeedInsurance(scenario, needIns,hazmat,IsPromo);
            Driver.WaitForCondition(d => Driver.IsDisplayed(Driver.FindElementByPropertyEquals("id", "iframe"), "Validate iFrame"), "Wait for iframe to load");
            Driver.SwitchToFrame("iframe");
			Driver.WaitForCondition(d => Driver.IsDisplayed(txtCardNo, "Validate Card Number Field"), "Wait for Card Number field to load");
			Thread.Sleep(3000);
			Driver.SendKeys(txtCardNo, "4111111111111111", "Enter card number");
            Driver.SendKeys(txtExpMM, "12", "Enter expiry months");
            Driver.SendKeys(txtExpYY, "25", "Enter expiry years");
            Driver.SendKeys(txtCVVCode, "123", "Enter cvv code");
            Driver.SendKeys(txtCardHolderName, "Jane", "Enter First Name");
            Driver.SendKeys(txtCardHolderLastName, "Doe", "Enter Last Name");
            Driver.SendKeys(txtBillingAddress, "Some address", "Enter card address");
            Driver.SendKeys(txtCityCode, "Pittsburgh", "Enter city name");
            Driver.SelectFromDropdown(txtStateCode, "Pennsylvania", "Enter state name");
			SelectElement selItem = new SelectElement(Driver.FindElement(txtStateCode));
			string selectedItem = selItem.SelectedOption.Text.ToString();
			while (selectedItem != "Pennsylvania")
			{
				Driver.SelectFromDropdown(txtStateCode, "Pennsylvania", "Enter state name");
				selItem = new SelectElement(Driver.FindElement(txtStateCode));
				selectedItem = selItem.SelectedOption.Text.ToString();
			}
            Driver.SendKeys(txtZipCode, "15220", "Enter zip code");
            Driver.Click(btnProcessTransaction, "Click 'Process Transaction' ");

            return new ConfirmationPage(Driver, Assert);
        }

        public void NeedInsurance(int scenario, bool needIns, bool hazmat, bool IsPromo)
        {
			if (hazmat == false)
			{
				if (needIns == true)
				{
					Driver.Click(rdInsurance, "Click Insurance option");
					Driver.SendKeys(txtCommodityValue, "500.00","Enter commodity value");
					Driver.WaitForCondition(d=> Driver.IsEnabled(btnApplyFalvey,"Verify Apply Falvey button is enabled"), "Wait for Apply Falvey button to be enabled");
					Driver.Click(btnApplyFalvey, "Click on 'Apply' button to trigger Falvey pop up");
                    Driver.WaitForCondition(d=> Driver.IsDisplayed(btnAgreeFalvey,"Verify Falvey insurance"), "Wait for Falvey insurance");
                    Driver.Click(btnAgreeFalvey, "Click Falvey insurance");
                    Driver.WaitForCondition(d => Driver.IsDisplayed(lblInsuranceId, "Verify insurance text"), "Wait for insurance text");
					InputData.Data.InsuranceCost = Math.Round(Util.FindDecimalInString(Driver.FindElement(lblInsuranceId).Text));
                    Assert.AreEqual("Insurance Coverage: $" + String.Format("{0:0.00}", InputData.Data.InsuranceCost) + " USD", Driver.FindElement(lblInsuranceId).Text, "Verify Added insurance amount");
				}
				else
				{
					Driver.Click(rdNoInsurance, "Click No insurance radio button");
				}
			}
			else
			{
				try 
				{
					Driver.FindElement(rdNoInsurance);
					Assert.Fail("Insurance is displayed to the user");
				}
				catch
				{
					Assert.Pass("Insurance is not displayed to the user.");
				}
			}

            if (IsPromo == true)
			{
				PromoCode(scenario);
				Driver.Click(btnApply, "Click button Apply");
				//need wait condition before comparing discount amount
				//Assert.AreEqual(" Discount: $" + InputData.Data.strDiscountAmount + " " , Driver.FindElement(lblDiscount).Text.ToString(), "Verify Discount Amount");
			}
        }
		/*
		 * Scenario IDs
		 * 1 - LTL
		 * 2 - LTL No Insurance Canada
		 * 3 - LTL With Insurance Canada
		 * 4 - FTL Flatbed
		 * 5 - FTL Van
		 * 6 - FTL Reefer
		 */
        public void PromoCode(int scenario)
        {
            int version = Convert.ToInt16(ConfigurationManager.AppSettings["PromoCodeVersion"]);
            var promoCodeDetails = (from pcode in DataContext.promo_code_ae where (pcode.status == "A" && pcode.terms_and_conditions_version == version) select pcode).FirstOrDefault();
            Driver.SendKeys(txtPromoCode, promoCodeDetails.promo_code, "Enter Promo code");
            var promocodePercent = promoCodeDetails.percentage / 100.00;
			if (scenario == 1)
			{
				var discountAmount = Math.Round(InputData.Data.Quote * Convert.ToDouble(promocodePercent));
				var calculatedQuote = InputData.Data.Quote - discountAmount;
				InputData.Data.strDiscountAmount = discountAmount.ToString();
				InputData.Data.Quote = Math.Round(calculatedQuote, 2);
			}
			else if (scenario == 2)
			{
				var discountAmount = Math.Round(InputData.Data.QuoteNoInsCAN * Convert.ToDouble(promocodePercent));
				var calculatedQuote = InputData.Data.QuoteNoInsCAN - discountAmount;
				InputData.Data.strDiscountAmount = discountAmount.ToString();
				InputData.Data.QuoteNoInsCAN = Math.Round(calculatedQuote, 2);
			}
			else if (scenario == 3)
			{
				var discountAmount = Math.Round(InputData.Data.QuoteInsCAN * Convert.ToDouble(promocodePercent));
				var calculatedQuote = InputData.Data.QuoteInsCAN - discountAmount;
				InputData.Data.strDiscountAmount = discountAmount.ToString();
				InputData.Data.QuoteInsCAN = Math.Round(calculatedQuote, 2);
			}
			else if (scenario == 4)
			{
				var discountAmount = Math.Round(InputData.Data.QuoteFTLFlatbed * Convert.ToDouble(promocodePercent));
				var calculatedQuote = InputData.Data.QuoteFTLFlatbed - discountAmount;
				InputData.Data.strDiscountAmount = discountAmount.ToString();
				InputData.Data.QuoteFTLFlatbed = Math.Round(calculatedQuote, 2);
			}
			else if (scenario == 5)
			{
				var discountAmount = Math.Round(InputData.Data.QuoteFTLVan * Convert.ToDouble(promocodePercent));
				var calculatedQuote = InputData.Data.QuoteFTLVan - discountAmount;
				InputData.Data.strDiscountAmount = discountAmount.ToString();
				InputData.Data.QuoteFTLVan = Math.Round(calculatedQuote, 2);
			}
			else if (scenario == 6)
			{
				var discountAmount = Math.Round(InputData.Data.QuoteFTLReefer * Convert.ToDouble(promocodePercent));
				var calculatedQuote = InputData.Data.QuoteFTLReefer - discountAmount;
				InputData.Data.strDiscountAmount = discountAmount.ToString();
				InputData.Data.QuoteFTLReefer = Math.Round(calculatedQuote, 2);
			}
			else
			{
				Assert.Fail("Invalid scenario selected for which variable the promo code is saved to");
			}
        }

    }
}
