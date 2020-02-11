using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoShipUI
{

    public class BasePage
    {
        public Driver Driver { get; private set; }
        public Assert Assert { get; private set; }
        public RandomHelper Random { get; private set; }
        private WebDriverWait _wait;
        private IJavaScriptExecutor _executor;
        private By spinner = By.Id("spinner");
  
        

        public BasePage(Driver driver, Assert assert)
        {
            Driver = driver;
            Assert = assert;
            Random = new RandomHelper();
           	//WaitForLoadIndicator();
            
        }
    
        public HomePage GoToUrl(string url)
        {
            Driver.GoToUrl(url);
            return new HomePage(Driver, Assert);
        }

        public void WaitForLoadIndicator(bool toAppear = false)
        {
            Driver.WaitForCondition(d => CheckForLoadIndicator() == toAppear, TimeSpan.FromSeconds(60), "Waiting for load indicator.");
        }

        public bool CheckForLoadIndicator()
        {
             return Driver.IsDisplayed(spinner, "Check for load indicator.");
        }
    }

}