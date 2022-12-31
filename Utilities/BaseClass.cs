using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;

namespace DemoECommerceFramework.Utilities
{
    class BaseClass
    {
        private ThreadLocal<IWebDriver> driver= new ThreadLocal<IWebDriver>();
        public ExtentReports extentReports;
        public ExtentTest test;
         String browser;
        ChromeOptions chromeOptions;

        [OneTimeSetUp]
        public void ReportSetup()
        {
            String workingDirectory=Environment.CurrentDirectory;
            String projectDirectory= Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath= projectDirectory + "//index.html";
            var htmlReporter=new ExtentHtmlReporter(reportPath);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
            extentReports.AddSystemInfo("Host", "LocalHost");
            extentReports.AddSystemInfo("Environment","QA");
            extentReports.AddSystemInfo("Username","Magesh");
        }

       
        public void InitializeBrowser()
        {
             browser=ConfigurationManager.AppSettings["browser"];
            switch (browser.ToLower())
            {
                case "chrome":
                    chromeOptions = new ChromeOptions();
                    chromeOptions.AddExtension(ConfigurationManager.AppSettings["adBlockerExtension"]);
                    driver.Value = new ChromeDriver(chromeOptions);
                    ImportantUtilities.TemporaryFixForAdBlocker(GetDriver());
                    break;
                case "firefox":
                    driver.Value = new FirefoxDriver();
                    break;
                case "edge":
                    driver.Value = new EdgeDriver();
                    break;
                default:
                    TestContext.Out.WriteLine("Please provide a different browser name. Available browsers are Chrome, Firefox, and Edge");
                    break;
            }
          //  return driver.Value;
        }

        [SetUp]
        public void Setup()
        {
            test=extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
            InitializeBrowser();
            driver.Value.Navigate().GoToUrl(ConfigurationManager.AppSettings["url"]);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }


        public IWebDriver GetDriver()
        {
            return driver.Value;
        }

        [TearDown]
        public void CloseBrowser()
        {
            var status=TestContext.CurrentContext.Result.Outcome.Status;
            var log=TestContext.CurrentContext.Result.StackTrace;
            DateTime date = DateTime.Now;
            String fileTitle = "Screenshot_" + date.ToString("mm:dd:yyyy")+".png";
            if (status==TestStatus.Failed)
            {
                test.Fail("Testcase is failed",ImportantUtilities.GetScreenshot(GetDriver(),fileTitle));
                test.Log(Status.Fail, "Testcase due to exception: "+log);
            }
            else if (status==TestStatus.Passed)
            {
                test.Pass("Testcase is passed");
            }
            extentReports.Flush();
            driver.Value.Quit();
        }

        /*public MediaEntityModelProvider GetScreenshot(IWebDriver driver,String screenshotName)
        {
            var screenshot=(ITakesScreenshot) driver;
            var takingScreenshot=screenshot.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(takingScreenshot, screenshotName).Build();
        }*/

/*        public void TemporaryFixForAdBlocker()
        {
            IList<String> allTabs = driver.Value.WindowHandles;
            driver.Value.SwitchTo().Window(allTabs[1]);
            driver.Value.Close();
            driver.Value.SwitchTo().Window(allTabs[0]);
        }*/

        
    }
}
