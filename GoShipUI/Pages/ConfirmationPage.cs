using OpenQA.Selenium;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Utilities;
using SharedUIDataEntities;
namespace GoShipUI
{
	public class ConfirmationPage : BasePage
	{
		public ConfirmationPage(Driver driver, Assert assert) : base(driver, assert) { }
		private By lblThankYou = By.XPath("//*[text()='Thanks for shipping with GoShip!']");
		private By lblConfirmationId = By.Id("confirmation-number-id");
		private By lblPckAddress = By.Id("formatAddress-origin-zip-id");
		private By lblPckContactName = By.Id("origin-contactName-id");
		private By lblPckPhoneNo = By.Id("origin-phone-id");
		private By lblDelAddress = By.Id("formatAddress-destination-zip-id");
		private By lblDelContactName = By.Id("destination-contactName-id");
		private By lblDelPhoneNo = By.Id("destinationPhone-id");
		private By lblPckStartWindow = By.Id("origin-startTime-id");
		private By lblPckEndWindow = By.Id("origin-endTime-id");
		private By lblDelStartWindow = By.Id("destination-startTime-id");
		private By lblDelEndWindow = By.Id("destination-endTime-id");
		private By lblCarrierId = By.Id("quote-carrier-name-id");
		private By lblDescriptionHeader = By.Id("item-description-id");
		private By lblItemWeight = By.Id("item-weight-dimWight-id");
		private By lblItemDimension = By.Id("item-length-width-height-dimSize-id");
		private By lblItemQuantity = By.Id("item-quantity-id");
		private By lblItemCondition = By.Id("itemCondition-id");
		private By lblPackageType = By.Id("item-productPackage-packageType-id");
		private By lblItemValue = By.Id("item-value-currency-id");
		private By lblItemDescription = By.Id("item-description-id");
		private By lblWeight = By.Id("weight-id");
		private By lblValue = By.Id("declared-value-id");
		private By lblInsuredCost = By.Id("insured-id");
		private By lblShippingCost = By.Id("shipping-cost-id");
		private By lblTotalCost = By.Id("currency-cost-id");
		private By lblInsuranceCost = By.Id("insured-coverage-id");
		private By lblPremiumInsuranceCost = By.Id("insurance-premium-cost-id");
		private By lblEstimatedDelivery = By.Id("quote-deliveryDate-id");
		
		// Angular implementation for download links
		private By lnkINSDownload = By.XPath("//*[contains(text(),'Certificate of Insurance')]/ancestor::div[1]/descendant::i");
		private By lnkBOLDownloadLTL = By.XPath("//*[contains(text(),'Bill of Lading')]/ancestor::div[1]/descendant::i");
		private By lnkNAFTADownload = By.XPath("//*[contains(text(),'NAFTA Document')]/ancestor::div[1]/descendant::i[1]");
		private By lnkSLIDownload = By.XPath("//*[contains(text(),'Shippers Letter of Instruction')]/ancestor::div[1]/descendant::i[1]");
		private By lnkPLDownload = By.XPath("//*[contains(text(),'Packing List')]/ancestor::div[1]/descendant::i[1]");
		private By lnkCCIDownload = By.XPath("//*[contains(text(),'Canadian Customs Invoice')]/ancestor::div[1]/descendant::i[1]");
		private By lnkCIDownload = By.XPath("//*[contains(text(),'Commercial Invoice')]/ancestor::div[1]/descendant::i[1]");
		private By lnkNAFTADownloadBlank = By.XPath("//*[contains(text(),'NAFTA Document')]/ancestor::div[1]/descendant::i[2]");
		private By lnkSLIDownloadBlank = By.XPath("//*[contains(text(),'Shippers Letter of Instruction')]/ancestor::div[1]/descendant::i[2]");
		private By lnkPLDownloadBlank = By.XPath("//*[contains(text(),'Packing List')]/ancestor::div[1]/descendant::i[2]");
		private By lnkCCIDownloadBlank = By.XPath("//*[contains(text(),'Canadian Customs Invoice')]/ancestor::div[1]/descendant::i[2]");
		private By lnkCIDownloadBlank = By.XPath("//*[contains(text(),'Commercial Invoice')]/ancestor::div[1]/descendant::i[2]");
		private By lnkBOLDownloadFTL = By.XPath("//*[contains(text(),'Bill of Lading')]/ancestor::div[1]/descendant::i");

		private By lnkProfilePage = By.XPath("//*[@id='site-navigation']/ul/li[6]/ul/li[2]/a");
		private By lblProfile = By.XPath("//*[@id='site-navigation']/ul/li[6]/a");
		private WorkingDayCalculator WC = new WorkingDayCalculator(2100, 2019);
		private PLS20Entities DataContext = new PLS20Entities();
		private PLS30HFEntities DataContext30 = new PLS30HFEntities();
		private UtilityMethods Util = new UtilityMethods();

		private string downloadPath_LocalUser = Environment.GetEnvironmentVariable("USERPROFILE") + Convert.ToString(ConfigurationManager.AppSettings["DownloadPath_Local"]);
		private string downloadPath_Remote = Convert.ToString(ConfigurationManager.AppSettings["Jenkins_Local"]);
		private bool RunLocal = Convert.ToBoolean(ConfigurationManager.AppSettings["RUN_ON_LOCAL"]);

		public void GetConfirmationLTL(bool checkAllCanadaDocuments = false, bool verifyLoadinShipmentHistory = false, bool isCanada = false, bool hasIns = false, bool isHazMat=false)
		{
			WaitForLoadIndicator();
			Driver.WaitForCondition(d => Driver.IsDisplayed(lblThankYou, "Validate Thank you text"), "Wait for Thank you text to load");
			Driver.WaitForCondition(d => Driver.IsDisplayed(lblConfirmationId, "Check if confirmation number is displayed"), "Wait for confirmation number to load");
			//BOL info
			long ConfirmationId = Convert.ToInt64(Driver.FindElement(lblConfirmationId).Text);
			var bolId = (from ld in DataContext.loadPLS20 where ld.load_id == ConfirmationId select ld).SingleOrDefault();
			Assert.AreEqual(ConfirmationId.ToString(), bolId.bol, "Validate BOL in database");

			int pckDays;
			int delDays;
			DateTime pickupDate;
			DateTime deliveryDate;

			if (isCanada == false)
			{
				pckDays = WC.GetWorkingDay(DateTime.Today, InputData.Data.numPickupDays);
				pickupDate = DateTime.Today.AddDays(pckDays);
				delDays = WC.GetWorkingDeliveryDay(pickupDate, InputData.Data.numDeliveryDays);
				deliveryDate = pickupDate.AddDays(delDays);

				Assert.AreEqual(InputData.Data.CarrierName, Driver.FindElement(lblCarrierId).Text, "Validate Carrier Information");
			}
			else
			{
				pckDays = WC.GetWorkingDay(DateTime.Today, InputData.Data.numPickupDaysCAN, isCanada);
				pickupDate = DateTime.Today.AddDays(pckDays);
				delDays = WC.GetWorkingDeliveryDay(pickupDate, InputData.Data.numDeliveryDaysCAN, isCanada);
				deliveryDate = pickupDate.AddDays(delDays);

				Assert.AreEqual(InputData.Data.CarrierNameCAN, Driver.FindElement(lblCarrierId).Text, "Validate Carrier Information");
			}

			VerifyLoadDetails(pickupDate, deliveryDate, isCanada);
			VerifyLoadCostLTL(isCanada, hasIns);

			// Item condition / Package Type
			Assert.AreEqual(InputData.Data.selCondition, Driver.FindElement(lblItemCondition).Text, "Validate item condition");
			Assert.AreEqual(InputData.Data.selPackageType, Driver.FindElement(lblPackageType).Text, "Validate item package type");

			VerifyEmailContent(ConfirmationId, pickupDate, isCanada, hasIns);

			string downloadPath;
			if (RunLocal == false)
			{
				downloadPath = downloadPath_Remote;
			}
			else
			{
				downloadPath = downloadPath_LocalUser;
			}

			CheckBOLDocumentLTL(ConfirmationId, downloadPath, isCanada,isHazMat);

            // Currently only handles new user entered vendor and purchaser addresses with a user entered Customs Broker
            if (checkAllCanadaDocuments == true)
            {
                CheckNAFTADocument(ConfirmationId, downloadPath);
                CheckSLIDocument(ConfirmationId, downloadPath);
                CheckPLDocument(ConfirmationId, downloadPath);
                CheckCCIDocument(ConfirmationId, downloadPath);
                CheckCIDocument(ConfirmationId, downloadPath);
            }

            if (verifyLoadinShipmentHistory)
            {
                Driver.Click(lblProfile, "Click profile dropdown");
                Driver.Click(lnkProfilePage, "Click 'Profile' to navigate to user profile");
                UserProfilePage profile = new UserProfilePage(Driver, Assert);
                profile.LookupLoadLTL(ConfirmationId, pickupDate, deliveryDate, isCanada);
            }
        }

		public void GetConfirmationFTL(string truckType, bool verifyLoadinShipmentHistory = false)
		{
			Driver.WaitForCondition(d => Driver.IsDisplayed(lblThankYou, "Validate Thank you text"), "Wait for Thank you text to load");
			long ConfirmationId = Convert.ToInt64(Driver.FindElement(lblConfirmationId).Text);
			var bolId = (from ft in DataContext30.loads where ft.load_id == ConfirmationId select ft.load_id).SingleOrDefault();
			Assert.AreEqual(ConfirmationId, bolId, "Validate BOL in database");

			int pckDays = WC.HolidayMonth(DateTime.Today, InputData.Data.numPickupDays);
			DateTime pickupDate = DateTime.Today.AddDays(pckDays);
			int delDays = WC.HolidayMonth(pickupDate, InputData.Data.numDeliveryDays);
			DateTime deliveryDate = pickupDate.AddDays(delDays);

			VerifyLoadDetails(pickupDate, deliveryDate);
			VerifyLoadCostFTL(truckType);

			VerifyEmailContent(ConfirmationId, pickupDate, false, false, true, truckType);

			string downloadPath;
			if (RunLocal == false)
			{
				downloadPath = downloadPath_Remote;
			}
			else
			{
				downloadPath = downloadPath_LocalUser;
			}

			CheckBOLDocumentFTL(Driver.FindElement(lblConfirmationId).Text, downloadPath);

			if (verifyLoadinShipmentHistory)
			{
				Driver.Click(lblProfile, "Click profile dropdown");
				Driver.Click(lnkProfilePage, "Click 'Profile' to navigate to user profile");
				UserProfilePage profile = new UserProfilePage(Driver, Assert);
				profile.LookupLoadFTL(ConfirmationId, pickupDate, deliveryDate);
			}
		}

		public void VerifyEmailContent(long ConfirmationId, DateTime pickupDate, bool isCanada = false, bool hasLTLIns = false, bool isFTL = false, string truckType = "")
		{
           // var initialEmailCount = 0;
            string userEmail = ConfigurationManager.AppSettings["UserName"];
			var email = (from eml in DataContext.email_history_load join ehist in DataContext.email_history on eml.email_history_id equals ehist.email_history_id where eml.load_id == ConfirmationId && ehist.send_to == userEmail orderby ehist.send_time descending select new { LoadID = eml.load_id, Email_Id = eml.email_history_id, Sent_To = ehist.send_to, Subject = ehist.subject, Email_Content = ehist.text, Send_Time = ehist.send_time }).ToList();
		    //initialEmailCount = email.Count;
			int count = 0;
			int waitTimeSec = 60;
            if (email.Count <= 0)
            {
                while (email.Count == 0 && count < waitTimeSec)
                {
                    Driver.Sleep();
                    count++;
                    email = (from eml in DataContext.email_history_load join ehist in DataContext.email_history on eml.email_history_id equals ehist.email_history_id where eml.load_id == ConfirmationId && ehist.send_to == userEmail orderby ehist.send_time descending select new { LoadID = eml.load_id, Email_Id = eml.email_history_id, Sent_To = ehist.send_to, Subject = ehist.subject, Email_Content = ehist.text, Send_Time = ehist.send_time }).ToList();
                   
                }

            }
			
			if (email.Count <= 0)
			{
				Assert.Fail("Email sent to " + ConfigurationManager.AppSettings["UserName"] + " could not be found for load " + ConfirmationId + " after waiting for " + count + " seconds");
			}
			else
			{
				var emailContent = email[0].Email_Content;
				Assert.Pass("Email content retrieved for load " + ConfirmationId + ", email sent to " + ConfigurationManager.AppSettings["UserName"]);
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, ConfirmationId.ToString()), "Check if confirmation email has confirmation number - Expecting " + ConfirmationId.ToString());
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strPckAddressLine1.ToString()), "Check if confirmation email has the following pick up address for load " + ConfirmationId + " - Expecting " + InputData.Data.strPckAddressLine1.ToString());
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strDelAddressLine1.ToString()), "Check if confirmation email has the following delivery address for load " + ConfirmationId + " - Expecting " + InputData.Data.strDelAddressLine1.ToString());
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strCityDestination.ToString()), "Check if confirmation email has delivery city state zip for load " + ConfirmationId + " - Expecting " + InputData.Data.strFullCityDestination.ToString());
				if (isFTL == true)
				{
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strCitySource.ToString()), "Check if confirmation email has pickup city state zip for load " + ConfirmationId + " - Expecting " + InputData.Data.strFullCitySource.ToString());
					var quote = GetGlobalFinalQuoteVarByTruckType(truckType);
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, "$" + String.Format("{0:0.00}", quote)), "Check if confirmation email has the final quote price for load " + ConfirmationId + " - Expecting " + "$" + String.Format("{0:0.00}", quote));
				}
				else
				{
					if (isCanada == false)
					{
						Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strCitySource.ToString()), "Check if confirmation email has pickup city state zip for load " + ConfirmationId + " - Expecting " + InputData.Data.strFullCitySource.ToString());
						Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, "$" + String.Format("{0:0.00}", InputData.Data.FinalQuote)), "Check if confirmation email has the final quote price for load " + ConfirmationId + " - Expecting " + "$" + String.Format("{0:0.00}", InputData.Data.FinalQuote));
						Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.CarrierName.ToString()), "Check if confirmation email has carrier name for load " + ConfirmationId + " - Expecting " + InputData.Data.CarrierName.ToString());
					}
					else
					{
						Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strCitySourceCAN.ToString()), "Check if confirmation email has Canadian pickup city state zip for load " + ConfirmationId + " - Expecting " + InputData.Data.strFullCitySourceCAN.ToString());
						var finalQuote = GetGlobalFinalQuoteVarByIns(hasLTLIns);
						Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, "$" + String.Format("{0:0.00}", finalQuote)), "Check if confirmation email has the final quote price for load " + ConfirmationId + " - Expecting " + "$" + String.Format("{0:0.00}", finalQuote));
						Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.CarrierNameCAN.ToString()), "Check if confirmation email has carrier name for load " + ConfirmationId + " - Expecting " + InputData.Data.CarrierNameCAN.ToString());
					}
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strFrmPckPhoneNo.ToString()), "Check if confirmation email has pickup phone number for load " + ConfirmationId + " - Expecting " + InputData.Data.strFrmPckPhoneNo.ToString());
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, InputData.Data.strFrmDelPhoneNo.ToString()), "Check if confirmation email has delivery phone number for load " + ConfirmationId + " - Expecting " + InputData.Data.strFrmDelPhoneNo.ToString());
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(emailContent, pickupDate.ToString("ddd MM/dd/yyyy")), "Check if confirmation email has the pickup date for load " + ConfirmationId + " - Expecting " + pickupDate.ToString("ddd MM/dd/yyyy"));
				}
			}
		}

		public void VerifyLoadDetails(DateTime pickupDate, DateTime deliveryDate, bool isCanada = false)
		{
			//Estimated delivery
			Assert.AreEqual(deliveryDate.ToString("dddd, MMMM dd"), Driver.FindElement(lblEstimatedDelivery).Text, "Verify estimated delivery date");
			//pick up location
			if (isCanada == false)
			{
				Assert.AreEqual(InputData.Data.strPckAddressLine1 + ", " + InputData.Data.strCitySource, Driver.FindElement(lblPckAddress).Text, "Validating Pick Up address.");
			}
			else
			{
				Assert.AreEqual(InputData.Data.strPckAddressLine1 + ", " + InputData.Data.strCitySourceCAN, Driver.FindElement(lblPckAddress).Text, "Validating Canadian Pick Up address.");
			}
			Assert.AreEqual(InputData.Data.strPckContactName, Driver.FindElement(lblPckContactName).Text, "Validate Pick up Contact Name");
			Assert.AreEqual(InputData.Data.strFormPckPhoneNo, Driver.FindElement(lblPckPhoneNo).Text, "Validate Pick up Phone no");
			//pick up window

			var dat = pickupDate.ToString("MMMM dd, yyyy ") + "9:30 AM";
			Assert.AreEqual(dat, Driver.FindElement(lblPckStartWindow).Text, "Validate Pick up start window");
			dat = pickupDate.ToString("MMMM dd, yyyy ") + "2:45 PM";
			Assert.AreEqual(dat, Driver.FindElement(lblPckEndWindow).Text, "Validate Pick up end window");
			//delivery location
			Assert.AreEqual(InputData.Data.strDelAddressLine1 + ", " + InputData.Data.strCityDestination, Driver.FindElement(lblDelAddress).Text, "Validate Delivery address");
			Assert.AreEqual(InputData.Data.strDelContactName, Driver.FindElement(lblDelContactName).Text, "Validate Delivery Contact Name");
			Assert.AreEqual(InputData.Data.strFormDelPhoneNo, Driver.FindElement(lblDelPhoneNo).Text, "Validate Delivery Phone no");

			//delivery window
			dat = deliveryDate.ToString("MMMM dd, yyyy ") + "10:30 AM";
			Assert.AreEqual(dat, Driver.FindElement(lblDelStartWindow).Text, "Validate Delivery start window");
			dat = deliveryDate.ToString("MMMM dd, yyyy ") + "3:30 PM";
			Assert.AreEqual(dat, Driver.FindElement(lblDelEndWindow).Text, "Validate Delivery end window");

			//items in the shipment
			Assert.AreEqual(InputData.Data.strItemDescription, Driver.FindElement(lblDescriptionHeader).Text.Replace("Item 1: ", ""), "Validate item description");
			Assert.AreEqual(InputData.Data.strInputWeight + " " + InputData.Data.selUOMWeight, Driver.FindElement(lblItemWeight).Text, "Validate item weight");
			Assert.AreEqual(InputData.Data.strInputLength + "L x " + InputData.Data.strInputWidth + "W x " + InputData.Data.strInputHeight + "D (" + InputData.Data.selUOMDemen + ")", Driver.FindElement(lblItemDimension).Text, "Validate item dimension");
			//Assert.AreEqual(InputData.Data.selCondition, Driver.FindElement(lblItemCondition).Text, "Validate item condition");
			//Assert.AreEqual(InputData.Data.selPackageType, Driver.FindElement(lblPackageType).Text, "Validate item package type");            
			Assert.AreEqual("$" + InputData.Data.strItemValue + " USD", Driver.FindElement(lblItemValue).Text, "Validate item value");
			Assert.AreEqual(InputData.Data.strInputQuantity, Driver.FindElement(lblItemQuantity).Text, "Validate items quantity");

			//total commodity details
			Assert.AreEqual(InputData.Data.strInputWeight + " " + InputData.Data.selUOMWeight, Driver.FindElement(lblWeight).Text, "Validate item weight");
			Assert.AreEqual("$" + InputData.Data.strItemValue + " USD", Driver.FindElement(lblValue).Text, "Validate commodity details declared value");
		}
		public void VerifyLoadCostLTL(bool isCanada = false, bool hasIns = false)
		{
			try
			{
				Assert.AreEqual("$" + InputData.Data.CommodityValue + " USD", Driver.FindElement(lblInsuredCost).Text, "Validate insured cost");
			}
			catch { }
			var shipCost = Util.FindDecimalInString(Driver.FindElement(lblShippingCost).Text);
			double quote;
			if (isCanada == false)
			{
				quote = InputData.Data.Quote;
			}
			else
			{
				quote = GetGlobalQuoteVarByIns(hasIns);
			}
			Assert.IsTrue(Math.Abs(Convert.ToDecimal(shipCost - quote)) < 1, "Verify Shipping Cost Calculation Difference is less than $1.00 - Expected: " + quote + ", Observed: " + shipCost);
			//Assert.AreEqual("$" + quote + " USD", Driver.FindElement(lblShippingCost).Text, "Validate Shipping Cost");
			try
			{
				//Assert.AreEqual("$" + quote + " USD", Driver.FindElement(lblTotalCost).Text, "Validate total cost");
				var totalCost = Util.FindDecimalInString(Driver.FindElement(lblTotalCost).Text);
				if (isCanada == false)
				{
					InputData.Data.FinalQuote = totalCost;
				}
				else
				{
					SaveGlobalFinalQuoteVarByIns(hasIns, totalCost);
				}
				Assert.IsTrue(Math.Abs(Convert.ToDecimal(totalCost - quote)) < 1, "Verify Total Cost Calculation Difference is less than $1.00 - Expected: " + quote + ", Observed: " + totalCost);
			}
			catch
			{
				Assert.AreEqual("$" + String.Format("{0:0.00}", InputData.Data.InsuranceCost) + " USD", Driver.FindElement(lblInsuranceCost).Text, "Validate insurance cost");
			}
			try
			{
				//Assert.AreEqual("$" + (quote + 10.00) + " USD", Driver.FindElement(lblPremiumInsuranceCost).Text, "Validate Total cost with Insurance");
				var finalCost = Util.FindDecimalInString(Driver.FindElement(lblPremiumInsuranceCost).Text);
				if (isCanada == false)
				{
					InputData.Data.FinalQuote = finalCost;
				}
				else
				{
					SaveGlobalFinalQuoteVarByIns(hasIns, finalCost);
				}
				Assert.IsTrue(Math.Abs(Convert.ToDecimal(finalCost - (quote + InputData.Data.InsuranceCost))) < 1, "Verify Total Cost with Insurance is less than $1.00 - Expected: " + (quote + InputData.Data.InsuranceCost) + ", Observed: " + finalCost);
			}
			catch
			{
			}
		}

		public void VerifyLoadCostFTL(string truckType)
		{
			try
			{
				Assert.AreEqual("$" + InputData.Data.CommodityValue + " USD", Driver.FindElement(lblInsuredCost).Text, "Validate insured cost");
			}
			catch { }
			var shipCost = Util.FindDecimalInString(Driver.FindElement(lblShippingCost).Text);
			var quote = GetGlobalQuoteVarByTruckType(truckType);
			Assert.IsTrue(Math.Abs(Convert.ToDecimal(shipCost - quote)) < 1, "Verify Shipping Cost Calculation Difference is less than $1.00 - Expected: " + quote + ", Observed: " + shipCost);
			//Assert.AreEqual("$" + quote + " USD", Driver.FindElement(lblShippingCost).Text, "Validate Shipping Cost");
			try
			{
				//Assert.AreEqual("$" + quote + " USD", Driver.FindElement(lblTotalCost).Text, "Validate total cost");
				var totalCost = Util.FindDecimalInString(Driver.FindElement(lblTotalCost).Text);
				SaveGlobalFinalQuoteVarByTruckType(truckType, totalCost);
				quote = GetGlobalQuoteVarByTruckType(truckType);
				Assert.IsTrue(Math.Abs(Convert.ToDecimal(totalCost - quote)) < 1, "Verify Total Cost Calculation Difference is less than $1.00 - Expected: " + quote + ", Observed: " + totalCost);
			}
			catch
			{
				Assert.AreEqual("$" + String.Format("{0:0.00}", InputData.Data.InsuranceCost) + " USD", Driver.FindElement(lblInsuranceCost).Text, "Validate insurance cost");
			}
			try
			{
				//Assert.AreEqual("$" + (quote + 10.00) + " USD", Driver.FindElement(lblPremiumInsuranceCost).Text, "Validate Total cost with Insurance");
				var finalCost = Util.FindDecimalInString(Driver.FindElement(lblPremiumInsuranceCost).Text);
				SaveGlobalFinalQuoteVarByTruckType(truckType, finalCost);
				quote = GetGlobalQuoteVarByTruckType(truckType);
				Assert.IsTrue(Math.Abs(Convert.ToDecimal(finalCost - (quote + InputData.Data.InsuranceCost))) < 1, "Verify Total Cost with Insurance is less than $1.00 - Expected: " + (quote + InputData.Data.InsuranceCost) + ", Observed: " + finalCost);
			}
			catch
			{
			}
		}

        public void CheckBOLDocumentLTL(long confirmNumber, string downloadPath, bool isCanada = false, bool isHazMat = false)
        {
            Driver.ScrollDown();
            Driver.Click(lnkBOLDownloadLTL, "Click to download 'Bill of Lading'");
            string filePath = downloadPath + confirmNumber + "_BillOfLading.pdf";
            if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
            {
                Assert.Fail("BOL could not be downloaded or found at: " + filePath);
            }
            else
            {
                string BOL = PDFExtractor.ExtractTextFromPDF(filePath);
                Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, confirmNumber.ToString()), "Check if BOL has confirmation number - Expecting: " + confirmNumber.ToString());
                if (isCanada == false)
                {
                    Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, InputData.Data.strFullCitySource.ToString()), "Check if BOL has pickup city state zip - Expecting " + InputData.Data.strFullCitySource.ToString());
                    Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, InputData.Data.CarrierName.ToString()), "Check if BOL has carrier name - Expecting " + InputData.Data.CarrierName.ToString());
                }
                else
                {
                    Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, InputData.Data.strFullCitySourceCAN.ToString()), "Check if BOL has Canadian pickup city state zip - Expecting " + InputData.Data.strFullCitySourceCAN.ToString());
                    Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, InputData.Data.CarrierNameCAN.ToString()), "Check if BOL has carrier name - Expecting " + InputData.Data.CarrierNameCAN.ToString());
                }
                if (isHazMat == true)
                {
                    Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, "PGII"), "Check if BOL has PGII");
                    Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, "RQ, UN3456"), "Check if BOL has RQ, UNXXXX");
                }
				foreach (var docField in InputData.Data.bolLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.bolLTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, fieldVal), "Check if BOL has " + docField.Name + " - Expecting: " + fieldVal);
				}
            }
		}

		public void CheckNAFTADocument(long confirmNumber, string downloadPath)
		{
			Driver.Click(lnkNAFTADownload, "Click to download 'NAFTA Document'");
			string filePath = downloadPath + confirmNumber + "_NAFTADocument.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("NAFTA could not be downloaded or found at: " + filePath);
			}
			else
			{
				string NAFTA = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				foreach (var docField in InputData.Data.naftaLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.naftaLTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(NAFTA, fieldVal), "Check if NAFTA has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			/*
			Driver.Click(lnkNAFTADownloadBlank, "Click to download blank 'NAFTA Document'");
			filePath = downloadPath + confirmNumber + "_NAFTADocument.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("Blank NAFTA could not be downloaded or found at: " + filePath);
			}
			else
			{
				string NAFTA = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				foreach (var docField in InputData.Data.naftaLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.naftaLTL).ToString();
					Assert.IsFalse(Util.ContainsStrCaseInsensitive(NAFTA, fieldVal), "Check if NAFTA has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			*/
		}

		public void CheckSLIDocument(long confirmNumber, string downloadPath)
		{
			Driver.Click(lnkSLIDownload, "Click to download 'Shippers Letter of Instruction'");
			string filePath = downloadPath + confirmNumber + "_ShippersLetterOfInstruction.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("SLI could not be downloaded or found at: " + filePath);
			}
			else
			{
				string SLI = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(SLI, confirmNumber.ToString()), "Check if SLI has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.sliLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.sliLTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(SLI, fieldVal), "Check if SLI has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			/*
			Driver.Click(lnkSLIDownloadBlank, "Click to download blank 'Shippers Letter of Instruction'");
			filePath = downloadPath + confirmNumber + "_PackingList.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("Blank SLI could not be downloaded or found at: " + filePath);
			}
			else
			{
				string SLI = PDFExtractor.ExtractTextFromPDF(filePath);
				Assert.IsFalse(Util.ContainsStrCaseInsensitive(SLI, confirmNumber.ToString()), "Check if SLI has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.sliLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.sliLTL).ToString();
					Assert.IsFalse(Util.ContainsStrCaseInsensitive(SLI, fieldVal), "Check if SLI has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			*/
		}

		public void CheckPLDocument(long confirmNumber, string downloadPath)
		{
			Driver.Click(lnkPLDownload, "Click to download 'Packing List'");
			string filePath = downloadPath + confirmNumber + "_PackingList.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("PL could not be downloaded or found at: " + filePath);
			}
			else
			{
				string PL = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(PL, confirmNumber.ToString()), "Check if PL has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.plLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.plLTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(PL, fieldVal), "Check if PL has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			/*
			Driver.Click(lnkPLDownloadBlank, "Click to download blank 'Packing List'");
			filePath = downloadPath + confirmNumber + "_PackingList.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("Blank PL could not be downloaded or found at: " + filePath);
			}
			else
			{
				string PL = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				Assert.IsFalse(Util.ContainsStrCaseInsensitive(PL, confirmNumber.ToString()), "Check if blank PL has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.plLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.plLTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(PL, fieldVal), "Check if PL has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			*/
		}

		public void CheckCCIDocument(long confirmNumber, string downloadPath)
		{
			Driver.Click(lnkCCIDownload, "Click to download 'Canadian Customs Invoice'");
			string filePath = downloadPath + confirmNumber + "_CanadianCustomsInvoice.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("CCI could not be downloaded or found at: " + filePath);
			}
			else
			{
				string CCI = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(CCI, confirmNumber.ToString()), "Check if CCI has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.cciLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.cciLTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(CCI, fieldVal), "Check if CCI has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			/*
			Driver.Click(lnkCCIDownloadBlank, "Click to download blank 'Canadian Customs Invoice'");
			filePath = downloadPath + confirmNumber + "_CanadianCustomsInvoice.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("Blank CCI could not be downloaded or found at: " + filePath);
			}
			else
			{
				string CCI = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				Assert.IsFalse(Util.ContainsStrCaseInsensitive(CCI, confirmNumber.ToString()), "Check if blank CCI has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.cciLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.cciLTL).ToString();
					Assert.IsFalse(Util.ContainsStrCaseInsensitive(CCI, fieldVal), "Check if CCI has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			*/
		}

		public void CheckCIDocument(long confirmNumber, string downloadPath)
		{
			Driver.Click(lnkCIDownload, "Click to download 'Commercial Invoice'");
			string filePath = downloadPath + confirmNumber + "_CommercialInvoice.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("CI could not be downloaded or found at: " + filePath);
			}
			else
			{
				string CI = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(CI, confirmNumber.ToString()), "Check if CI has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.ciLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.ciLTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(CI, fieldVal), "Check if CI has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			/*
			Driver.Click(lnkCIDownloadBlank, "Click to download blank 'Commercial Invoice'");
			filePath = downloadPath + confirmNumber + "_CommercialInvoice.pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("Blank CI could not be downloaded or found at: " + filePath);
			}
			else
			{
				string CI = PDFExtractor.ExtractTextFromPDF(filePath).Replace(",", "");
				Assert.IsFalse(Util.ContainsStrCaseInsensitive(CI, confirmNumber.ToString()), "Check if blank CI has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.ciLTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.ciLTL).ToString();
					Assert.IsFalse(Util.ContainsStrCaseInsensitive(CI, fieldVal), "Check if BOL has " + docField.Name + " - Expecting: " + fieldVal);
				}
			}
			*/
		}

		public void CheckBOLDocumentFTL(string confirmNumber, string downloadPath)
		{
            Driver.ScrollDown();
            Driver.Click(lnkBOLDownloadFTL, "Click to download 'Bill of Lading'");
			string filePath = downloadPath + "BOL_" + confirmNumber + ".pdf";
			if (PDFExtractor.WaitForFileDownloadKnownFileName(filePath) == false)
			{
				Assert.Fail("BOL could not be downloaded or found at: " + filePath);
			}
			else
			{
				string BOL = PDFExtractor.ExtractTextFromPDF(filePath);
				Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, confirmNumber.ToString()), "Check if BOL has confirmation number - Expecting: " + confirmNumber.ToString());
				foreach (var docField in InputData.Data.bolFTL.GetType().GetProperties())
				{
					string fieldVal = docField.GetValue(InputData.Data.bolFTL).ToString();
					Assert.IsTrue(Util.ContainsStrCaseInsensitive(BOL, fieldVal), "Check if BOL has " + docField.Name + " - Expecting: " + fieldVal);
				}
				// Add PNET, PNLT, DNET, DNLT, weight, dims
			}
		}

		public double GetGlobalFinalQuoteVarByTruckType(string truckType)
		{
			double quote = 0.00;
			if (truckType == "Flatbed")
			{
				quote = InputData.Data.FinalQuoteFTLFlatbed;
			}
			else if (truckType == "Van")
			{
				quote = InputData.Data.FinalQuoteFTLVan;
			}
			else if (truckType == "Reefer")
			{
				quote = InputData.Data.FinalQuoteFTLReefer;
			}
			else
			{
				Assert.Fail("Invalid equipment type: " + truckType);
			}
			return quote;
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

		public void SaveGlobalFinalQuoteVarByTruckType(string truckType, double quote)
		{
			if (truckType == "Flatbed")
			{
				InputData.Data.FinalQuoteFTLFlatbed = quote;
			}
			else if (truckType == "Van")
			{
				InputData.Data.FinalQuoteFTLVan = quote;
			}
			else if (truckType == "Reefer")
			{
				InputData.Data.FinalQuoteFTLReefer = quote;
			}
			else
			{
				Assert.Fail("Invalid equipment type: " + truckType);
			}
		}

		public double GetGlobalFinalQuoteVarByIns(bool hasIns)
		{
			double quote = 0.00;
			if (hasIns == true)
			{
				quote = InputData.Data.FinalQuoteInsCAN;
			}
			else
			{
				quote = InputData.Data.FinalQuoteNoInsCAN;
			}
			return quote;
		}

		public double GetGlobalQuoteVarByIns(bool hasIns)
		{
			double quote = 0.00;
			if (hasIns == true)
			{
				quote = InputData.Data.QuoteInsCAN;
			}
			else
			{
				quote = InputData.Data.QuoteNoInsCAN;
			}
			return quote;
		}

		public void SaveGlobalFinalQuoteVarByIns(bool hasIns, double quote)
		{
			if (hasIns == true)
			{
				InputData.Data.FinalQuoteInsCAN = quote;
			}
			else
			{
				InputData.Data.FinalQuoteNoInsCAN = quote;
			}
		}
	}
}
