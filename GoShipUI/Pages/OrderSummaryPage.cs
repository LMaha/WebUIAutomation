using OpenQA.Selenium;

namespace GoShipUI
{
	public class OrderSummaryPage : BasePage
    {
        public OrderSummaryPage(Driver driver, Assert assert) : base(driver, assert) { }
        private By lblPckUpLocation = By.Id("pick-up-location-id");
        private By lblDeliLocation = By.Id("delivery-location-id");
        private By lblItemQuanity = By.Id("item-quantity-id");
        private By lblWeight = By.Id("item-weight-dimWeight-id");
        private By lblPackageType = By.Id("item-productPackage-packageType-id");
        private By lblDimensions = By.Id("item-length-width-height-dimSize-id");
        private By lblCondition = By.Id("itemCondition-id");
        private By lblCurrency = By.Id("item-value-currency-id");
        private By lblTotalWeight = By.Id("getTotalWeight-uomWeight-id");
        private By lblTotalCurrency = By.Id("totalItemsValue-currency-id");
        private By lblPckAccessorial = By.Id("liftgatePickup-id");
        private By lblDelAccessorial = By.Id("liftgateDelivery-id");

        public void OrderSummary()
        {
			WaitForLoadIndicator();
			string strState="";
            if (InputData.Data.selDeliveryCountry == "United States of America")
            {
                strState = "USA";
            }
			Driver.WaitForCondition(d => Driver.IsDisplayed(lblPckUpLocation, "Check if pickup location is displayed"), "Wait for pickup location to load");
            Assert.AreEqual(InputData.Data.strCitySource+" "+ strState, Driver.FindElement(lblPckUpLocation).Text.ToString(), "Verify Pick up location");
            Assert.AreEqual(InputData.Data.strCityDestination + " " + strState, Driver.FindElement(lblDeliLocation).Text.ToString(), "Verify Delivery location");
            Assert.AreEqual(InputData.Data.strInputQuantity, Driver.FindElement(lblItemQuanity).Text.ToString(), "Verify item quantiy");
            Assert.AreEqual(InputData.Data.strInputWeight +" "+ InputData.Data.selUOMWeight, Driver.FindElement(lblWeight).Text.ToString(), "Verify item weight");
            Assert.AreEqual(InputData.Data.selPackageType, Driver.FindElement(lblPackageType).Text.ToString(), "Verify package type");
            Assert.AreEqual(InputData.Data.strInputLength+"L x "+InputData.Data.strInputWidth+ "W x "+InputData.Data.strInputHeight + "D ("+InputData.Data.selUOMDemen + ")", Driver.FindElement(lblDimensions).Text.ToString(), "Verify dimensions");
            Assert.AreEqual(InputData.Data.selCondition, Driver.FindElement(lblCondition).Text.ToString(), "Verify item condition");
            Assert.AreEqual("$"+InputData.Data.strItemValue + " USD", Driver.FindElement(lblCurrency).Text.ToString(), "Verify currency value");
            Assert.AreEqual(InputData.Data.strInputWeight + " " + InputData.Data.selUOMWeight, Driver.FindElement(lblTotalWeight).Text.ToString(), "Verify total weight");
            Assert.AreEqual("$" + InputData.Data.strItemValue + " USD", Driver.FindElement(lblTotalCurrency).Text.ToString(), "Verify total value");
            Assert.AreEqual("Liftgate Service at Pickup", Driver.FindElement(lblPckAccessorial).Text.ToString(), "Verify pick up accessorials");
            Assert.AreEqual("Liftgate Service at Delivery", Driver.FindElement(lblDelAccessorial).Text.ToString(), "Verify delivery accesorials");
        }
    }
}
