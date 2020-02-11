using System.Linq;
using OpenQA.Selenium;
using System.Configuration;
using SharedUIDataEntities;

namespace GoShipUI
{
	public class LoginPage : BasePage
    {
        public LoginPage (Driver driver, Assert assert) : base(driver, assert) { }
        private By txtEmailId = By.Id("username");
        private By txtPassword = By.Id("password");
        private By btnSignIn = By.Id("sign-in-login");
        private By btnSignUp = By.Id("sign-up-login");
        private By txtCompany = By.Id("company");
        private By txtFirstName = By.Id("firstName");
        private By txtLastName = By.Id("lastName");
        private By txtEmail = By.Id("email");
        private By txtPhone = By.Id("phoneNumber");
        private By btnCreateAccount = By.XPath("//*[text()='CREATE ACCOUNT']");
        private By lblVerifyAccount = By.Id("verifying-account-id");
        private By lblVerifyAccountText = By.Id("verify-user-text-id");
        private By btnCreateNewActivationEmail = By.XPath("//*[text()='CREATE NEW ACTIVATION EMAIL']");
        private By btnSignIn1 = By.Id("sign-in-header");

		private By lnkProfilePage = By.XPath("//*[@id='site-navigation']/ul/li[6]/ul/li[2]/a");
		private By lblProfile = By.XPath("//*[@id='site-navigation']/ul/li[6]/a");
		private By lnkLogOut = By.XPath("//*[@id='site-navigation']/ul/li[6]/ul/li[3]/a");

        public DetailsPage SignIn()
        {
			Driver.WaitForCondition(d => Driver.IsDisplayed(txtEmailId, "Wait for login to load"),"Wait for login to load");
			Driver.SendKeys(txtEmailId, ConfigurationManager.AppSettings["UserName"], "Enter email address");
            Driver.SendKeys(txtPassword, ConfigurationManager.AppSettings["Password"], "Enter password");
            Driver.WaitForCondition(d => Driver.IsEnabled(btnSignIn, "Wait for 'Sign In' to be enabled"),"Wait for 'SIGN IN' to be enabled");
			Driver.Sleep();
            Driver.Click(btnSignIn, "Click 'SIGN IN' button");
            return new DetailsPage(Driver, Assert);
        }

        public DetailsPage SignUp(bool isFTL)
        {
			WaitForLoadIndicator();
			string strRandom = Random.RandomString(4);
            string strEmail = strRandom + Random.RandomNumber(3)+"@Test.com";
			if (isFTL == true)
			{
				InputData.Data.NewUserEmailFTL = strEmail;
				InputData.Data.NewUserPasswordFTL = InputData.Data.DefaultPassword;
			}
			else
			{
				InputData.Data.NewUserEmailLTL = strEmail;
				InputData.Data.NewUserPasswordLTL = InputData.Data.DefaultPassword;
			}

			CreateEmailAccount(strRandom, strEmail);

            using (var db = new PLS20Entities())
            {
                var result = db.users.SingleOrDefault(u => u.email_address == strEmail);
                if (result != null)
                {
                    result.status = "A";
                    db.SaveChanges();
                }
            }

			SignInNewAccount(isFTL);
			GoToChangePassword(isFTL);
			SignOut();
			SignInNewAccount(isFTL);
			try
			{
				SignOut();
				Assert.Pass("Successfully signed in after password reset.");
			}
			catch
			{
				Assert.Fail("Failed to sign in after password reset.");
			}
            return new DetailsPage(Driver, Assert);
        }

		public void CreateEmailAccount(string strRandom, string strEmail)
		{
            Driver.WaitForCondition(d => Driver.IsDisplayed(btnSignUp, "Verify 'Sign Up'"), "Verify 'Sign Up'");
			Driver.Sleep();
            Driver.Click(btnSignUp, "Click 'SIGN UP' button");
            Driver.SendKeys(txtCompany, strRandom + " Company", "Enter Company name");
            Driver.SendKeys(txtFirstName, "FirstName" + strRandom, "Enter First name");
            Driver.SendKeys(txtLastName, "LastName" + strRandom, "Enter Last Name");
            Driver.SendKeys(txtEmail, strEmail, "Enter Email Address");         
            Driver.SendKeys(txtPhone, "1234567890", "Enter Phone number");
            Driver.SendKeys(txtPassword, InputData.Data.DefaultPassword, "Enter Password");
			Driver.WaitForCondition(d => Driver.IsEnabled(btnCreateAccount, "Wait for 'Create Account' to be enabled"), "Wait for 'Create Account' to be enabled");
			Driver.Sleep();
            Driver.Click(btnCreateAccount, "Click 'Submit' button");
			Driver.WaitForCondition(d => Driver.IsDisplayed(lblVerifyAccount, "Validate Verify Account"), "Wait for verify account screen to load");
            Assert.IsTrue(Driver.IsDisplayed(lblVerifyAccount, "Validate Verify Account"), "Validate Verify Account");
            Assert.AreEqual("A verification link has been sent to the email you provided. Please open link once received and the next steps in the booking process will be enabled. PLEASE DO NOT CLOSE THIS BROWSER TO PREVENT LOSS OF QUOTE DATA (link expires after 30 minutes)", Driver.FindElement(lblVerifyAccountText).Text.ToString(),"");
            Assert.IsTrue(Driver.IsDisplayed(btnCreateNewActivationEmail, "Validate 'Create New Activation Email' button"), "Validate 'Create New Activation Email' button");
		}

		public void SignInNewAccount(bool isFTL)
		{
			Driver.Click(btnSignIn1, "Click 'Sign In' button");
			if (isFTL == true)
			{
				Driver.SendKeys(txtEmailId, InputData.Data.NewUserEmailFTL, "Enter email address (FTL route): " + InputData.Data.NewUserEmailFTL);
				Driver.SendKeys(txtPassword, InputData.Data.NewUserPasswordFTL, "Enter password (FTL route): " + InputData.Data.NewUserPasswordFTL);
			}
			else
			{
				Driver.SendKeys(txtEmailId, InputData.Data.NewUserEmailLTL, "Enter email address (LTL route): " + InputData.Data.NewUserEmailLTL);
				Driver.SendKeys(txtPassword, InputData.Data.NewUserPasswordLTL, "Enter password (LTL route): " + InputData.Data.NewUserPasswordLTL);
			}
            Driver.WaitForCondition(d => Driver.IsEnabled(btnSignIn, "Wait for 'Sign In' to be enabled"), "Wait for 'SIGN IN' to be enabled");
            Driver.Click(btnSignIn, "Click 'SIGN IN' button");

			WaitForLoadIndicator();
		}

		public void GoToChangePassword(bool isFTL)
		{
			Driver.Click(lblProfile, "Click profile dropdown");
			Driver.Click(lnkProfilePage, "Click 'Profile' to navigate to user profile");
			UserProfilePage profile = new UserProfilePage(Driver, Assert);
			profile.ChangePassword(isFTL);
		}

		public void SignOut()
		{
			Driver.Click(lblProfile, "Click profile dropdown");
			Driver.Click(lnkLogOut, "Click 'Logout'");
		}
    }
}
