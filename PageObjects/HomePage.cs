using DemoECommerceFramework.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoECommerceFramework.PageObjects
{
    class HomePage : BaseClass 
    {
        IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.LinkText, Using = "Home")]
        private IWebElement homeLink;

        [FindsBy(How = How.LinkText, Using = "Products")]
        private IWebElement productsLink;
        [FindsBy(How = How.PartialLinkText, Using = "Login")]
        private IWebElement loginLink;
        public IWebElement GetHomeLink()
        {
            return homeLink;
        }

        public IWebElement GetProductsLink()
        {
            return productsLink;
        }

        public void NavigateToProductsLink()
        {
            GetProductsLink().Click();
        }
        public IWebElement GetLoginLink()
        {
            return loginLink;
        }
        public RegisterPage NavigateToRegisterPage()
        {
            GetLoginLink().Click();
            return new RegisterPage(driver);
        }
    }
}
