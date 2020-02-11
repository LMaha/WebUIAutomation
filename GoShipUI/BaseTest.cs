using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;
using System.Configuration;
using Protractor;
using Utilities;
using SharedUIDataEntities;
using System.Security.Principal;
using System.Security.AccessControl;

namespace GoShipUI
{
    public class BaseTest : IDisposable
    {
        public HomePage HomePage { get; private set; }
        public LoginPage LoginPage { get; private set; }
        public RandomHelper Random { get; private set; }
        //public static ExtentReports Reporter { get; private set; }
        //public static ExtentTest Test { get; private set; }
        public static ExtentTest SubTest { get; private set; }
        private IWebDriver _driver;
        public OrderSummaryPage OrderSummary { get; private set; }
        protected PLS20Entities GoShipDataContext = new PLS20Entities();
        public BaseTest() { }
        public BaseTest(ExtentTest reporter, string path)
        {
            Setup(reporter, path);
        }

        private string path;
        //private NgWebDriver ngDriver;
        private bool _testFinished;



        public static void SetFolderPermission(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            var directorySecurity = directoryInfo.GetAccessControl();
            var currentUserIdentity = WindowsIdentity.GetCurrent();
            var fileSystemRule = new FileSystemAccessRule(currentUserIdentity.Name,
                                                          FileSystemRights.FullControl,
                                                          InheritanceFlags.ObjectInherit |
                                                          InheritanceFlags.ContainerInherit,
                                                          PropagationFlags.None,
                                                          AccessControlType.Allow);

            directorySecurity.AddAccessRule(fileSystemRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }


        [OneTimeSetUp]
        public void SetupOneTime()
        {
            
             path = Path.Combine(Path.GetDirectoryName(new Uri(typeof(BaseTest).Assembly.CodeBase).LocalPath), "ExtentReports");
            //path = Path.Combine(Path.GetDirectoryName(new Uri(typeof(BaseTest).Assembly.CodeBase).LocalPath), TestContext.CurrentContext.Test.FullName);
            //var path = Path.Combine(Path.GetDirectoryName(new Uri(typeof(BaseTest).Assembly.CodeBase).LocalPath), TestContext.CurrentContext.Test.FullName, DateTime.Now.ToString("MM_dd_yyyy_HH.mm.ss_tt"));

            if (Directory.Exists(path))
            {
               Directory.Delete(path, true);
               Directory.CreateDirectory(path);
                SetFolderPermission(path);
            }
           
            //var htmlreporter = new ExtentHtmlReporter(Path.Combine(path, "extent.html"));
            //Reporter = new ExtentReports();
            //Reporter.AttachReporter(htmlreporter);
            //Test = Reporter.CreateTest(TestContext.CurrentContext.Test.FullName);

            ExtentTestManager.CreateParentTest(GetType().Name);

            //Setup(Test, path);
             _testFinished = false;
        }

        [SetUp]
        public void Setup()
        {
            //SubTest = Test.CreateNode(TestContext.CurrentContext.Test.FullName);
            //Setup(SubTest, path);
            SubTest=ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            Setup(SubTest, path);
            _testFinished = false;
        }

        
        public void Setup(ExtentTest reporter, string path)
        {
            string machineName = Environment.MachineName;
            string userName = Environment.UserName;
            //Test = reporter;    
                     
            var runOnLocal = Convert.ToBoolean(ConfigurationManager.AppSettings["RUN_ON_LOCAL"]);
            if (!runOnLocal)
            {
				var chromeOptions = new ChromeOptions();
				chromeOptions.AddUserProfilePreference("download.default_directory", ConfigurationManager.AppSettings["DownloadPath_Remote"]);
				chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
				ICapabilities capability = chromeOptions.ToCapabilities();
                
                _driver = new RemoteWebDriver(new Uri("http://10.160.10.6:4444/wd/hub"), capability);
                _driver.Manage().Window.Maximize();
                _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
            }
            else
            {                
                ChromeOptions options = new ChromeOptions();
                //options.AddArgument("no-sandbox");
                //options.AddArgument("proxy-server='direct://'");
                //options.AddArgument("proxy-bypass-list=*");
                _driver = new ChromeDriver(options);
                _driver.Manage().Window.Maximize();
                 //ngDriver = new NgWebDriver(_driver);          
                _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
             
            }
            //Initalize Driver class here
            var driver = new Driver(_driver, reporter, path);
            //For every page declared here new initalization
            HomePage = new HomePage(driver, new Assert(driver));
            OrderSummary = new OrderSummaryPage(driver, new Assert(driver));
            Random = new RandomHelper();

        }

       
        [TearDown]
        public void Dispose()
        {
            if (!_testFinished)
            {
                try
                {
                    var status = TestContext.CurrentContext.Result.Outcome.Status;
                    var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)?"" : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
                    Status logstatus;

                    switch (status)
                    {
                        case TestStatus.Failed:
                            logstatus = Status.Fail;
                            break;
                        case TestStatus.Inconclusive:
                            logstatus = Status.Warning;
                            break;
                        case TestStatus.Skipped:
                            logstatus = Status.Skip;
                            break;
                        default:
                            logstatus = Status.Pass;
                            break;
                    }

                    ExtentTestManager.GetTest().Log(logstatus, "Test ended with status: " + logstatus + stacktrace);
                            
                }
                catch { }
            }

            _testFinished = true;
            _driver.Quit();
            _driver.Dispose();
           
        }
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

            //Reporter.Flush();
            ExtentManager.Instance.Flush();
           
        }
        
    }
}