using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;

namespace Utilities
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "ExtentReports");


            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
            var htmlReporter = new ExtentHtmlReporter(Path.Combine(path, "extent.html"));
            htmlReporter.LoadConfig(TestContext.CurrentContext.TestDirectory + "\\extent-config.xml");
            // htmlReporter.Configuration().ChartLocation = ChartLocation.Top;
            // htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            // htmlReporter.Configuration().DocumentTitle = "Extent Report";
            // htmlReporter.Configuration().ReportName = "Automation Extent Report";
            //  htmlReporter.Configuration().Theme = Theme.Standard;
            Instance.AttachReporter(htmlReporter);
        }

        private ExtentManager()
        {
        }
    }
}
