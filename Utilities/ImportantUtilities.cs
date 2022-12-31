using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoECommerceFramework.Utilities
{
    class ImportantUtilities : BaseClass
    {
        public static MediaEntityModelProvider GetScreenshot(IWebDriver driver, String screenshotName)
        {
            var screenshot = (ITakesScreenshot)driver;
            var takingScreenshot = screenshot.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(takingScreenshot, screenshotName).Build();
        }

        public static void TemporaryFixForAdBlocker(IWebDriver driver)
        {
            IList<String> allTabs = driver.WindowHandles;
            driver.SwitchTo().Window(allTabs[1]);
            driver.Close();
            driver.SwitchTo().Window(allTabs[0]);
        }
    }
}
