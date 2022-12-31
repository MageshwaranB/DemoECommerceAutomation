using DemoECommerceFramework.PageObjects;
using DemoECommerceFramework.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoECommerceFramework.Tests
{
    class RegistrationTest :BaseClass
    {
        [Test]
        public void RegisterAUserSuccessfullyTest()
        {
            HomePage homePage = new HomePage(GetDriver());
            String actValidationText = homePage.NavigateToRegisterPage().PerformSignUp();
            Assert.AreEqual("ACCOUNT CREATED!",actValidationText);
        }
    }
}
