using DemoECommerceFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace DemoECommerceFramework.PageObjects
{
     class RegisterPage : BaseClass
    {
        IWebDriver driver;
        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Name, Using = "name")]
        private IWebElement nameTxtBox;
        [FindsBy(How = How.XPath, Using = "//input[@data-qa='signup-email']")]
        private IWebElement signupEmailTxtBox;
        [FindsBy(How = How.XPath, Using = "//button[text()='Signup']")]
        private IWebElement signUpBtn;
        [FindsBy(How = How.Id, Using = "id_gender1")]
        private IWebElement mrRadioBtn;
        [FindsBy(How = How.Id, Using = "id_gender2")]
        private IWebElement mrsRadioBtn;
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordBtn;
        [FindsBy(How = How.Id, Using = "name")]
        private IWebElement nameNonEditableBox;
        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement emailNonEditableBox;
        [FindsBy(How = How.Name, Using = "days")]
        private IWebElement daysDropdown;
        [FindsBy(How = How.Id, Using = "months")]
        private IWebElement monthsDropdown;
        [FindsBy(How = How.Id, Using = "years")]
        private IWebElement yearsDropdown;
        [FindsBy(How = How.XPath, Using = "//select[@id='country']")]
        private IWebElement countryDropdown;
        [FindsBy(How = How.Id, Using = "first_name")]
        private IWebElement firstNameTxtBox;
        [FindsBy(How = How.Id, Using = "last_name")]
        private IWebElement lastNameTxtBox;
        [FindsBy(How = How.Id, Using = "company")]
        private IWebElement companyTxtBox;
        [FindsBy(How = How.Id, Using = "address1")]
        private IWebElement addressLineTxtBox1;
        [FindsBy(How = How.Id, Using = "state")]
        private IWebElement stateTxtBox;
        [FindsBy(How = How.Id, Using = "city")]
        private IWebElement cityTxtBox;
        [FindsBy(How = How.Id, Using = "zipcode")]
        private IWebElement zipCodeTxtBox;
        [FindsBy(How = How.Id, Using = "mobile_number")]
        private IWebElement mobileNumberTxtBox;
        [FindsBy(How = How.XPath, Using = "//button[text()='Create Account']")]
        private IWebElement createAccBtn;
        [FindsBy(How = How.XPath, Using = "//h2[@class='title text-center']")]
        private IWebElement accountCreatedMessage;
            

        public IWebElement GetNameTxtBox()
        {
            return nameTxtBox; 
        }
        public string PerformSignUp()
        {
            var firstName=Faker.Name.First();
            var lastName=Faker.Name.Last();
            var nameCheck= firstName + " " + lastName;
            var emailCheck=Faker.Internet.Email();
            nameTxtBox.SendKeys(nameCheck);
            signupEmailTxtBox.SendKeys(emailCheck);
            Console.WriteLine("Name: "+nameCheck);
            Console.WriteLine("Email: "+emailCheck);
            signUpBtn.Click();
            if ((nameCheck.Equals(nameNonEditableBox.GetAttribute("value")) && (emailCheck.Equals(emailNonEditableBox.GetAttribute("value")))))
            {
                mrRadioBtn.Click();
                var randomWords = Faker.Lorem.GetFirstWord();
                var randomName = Faker.Internet.UserName();
                passwordBtn.SendKeys(randomName + Faker.RandomNumber.Next(5) + randomWords);
                String dayValues=Faker.Identification.DateOfBirth().ToString("D");
                var dobValues=SplitNames(dayValues);
                for (int i = 0; i < dobValues.Length; i++)
                {
                    Console.WriteLine(dobValues[i]);
                }
                HandleDropdownByText(daysDropdown, dobValues[0]);
                HandleDropdownByText(monthsDropdown, dobValues[1]);
                HandleDropdownByText(yearsDropdown, dobValues[2]);
                IList<IWebElement> countries =countryDropdown.FindElements(By.TagName("option"));
                var country=Faker.Country.Name();
                SelectCountryDropdown(countries,country);
                firstNameTxtBox.SendKeys(firstName);
                lastNameTxtBox.SendKeys(lastName);
                addressLineTxtBox1.SendKeys(Faker.Address.StreetAddress());
                cityTxtBox.SendKeys(Faker.Address.City());
                companyTxtBox.SendKeys(Faker.Company.Name());
                zipCodeTxtBox.SendKeys(Faker.Address.ZipCode());
                mobileNumberTxtBox.SendKeys(Faker.Phone.Number());
                stateTxtBox.SendKeys(Faker.Address.UsState());
                createAccBtn.Click();
                return accountCreatedMessage.Text;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public string[] SplitNames(String date)
        {
            String[] dates=date.Split(" ");
            return dates;
        }
        public void HandleDropdownByText(IWebElement webElement,String value)
        {
            SelectElement selectDropdown = new SelectElement(webElement);
            selectDropdown.SelectByText(value);
        }

        public void SelectCountryDropdown(IList<IWebElement> countries, String country)
        {
            foreach (var currentCountry in countries)
            {
                if (country.Equals(currentCountry))
                {
                    HandleDropdownByText(countryDropdown, country);
                    Console.WriteLine(country + " is selected");
                }
                else
                {
                    HandleDropdownByText(countryDropdown, "India");
                    Console.WriteLine(country + " is not selected");
                }
            }
        }

    }
}