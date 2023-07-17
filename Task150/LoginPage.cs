using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task150
{
    public class LoginPage
    {
        private static readonly string URL = "http://automationpractice.com/";
        private static readonly By SIGN_IN_BUTTON_MAIN_PAGE = By.ClassName("login");
        private static readonly By EMAIL_ADDRESS_NEW = By.Id("email_create");
        private static readonly By CREATE_AN_ACCOUNT_BUTTON = By.Id("SubmitCreate");
        private static readonly By FIRST_NAME_INPUT = By.Id("customer_firstname");
        private static readonly By LAST_NAME_INPUT = By.Id("customer_lastname");
        private static readonly By PASSWORD_INPUT = By.Id("passwd");
        private static readonly By ADDRESS_INPUT = By.Id("address1");
        private static readonly By CITY_INPUT = By.Id("city");
        private static readonly By STATE_DROPDOWN = By.Id("id_state");
        private static readonly By POSTAL_CODE_INPUT = By.Id("postcode");
        private static readonly By MOBILE_PHONE_INPUT = By.Id("phone_mobile");
        private static readonly By REGISTER_BUTTON = By.Id("submitAccount");
        private static readonly By REGISTERED_EMAIL_ADDRESS = By.Id("email");
        private static readonly By REGISTERED_PASSWORD_ADDRESS = By.Id("passwd");
        private static readonly By SIGN_IN_BUTTON = By.Id("SubmitLogin");


        protected IWebDriver _driver;

        public LoginPage(IWebDriver Driver)
        {
            _driver = Driver;
            Driver.Url = URL;
        }

        public void CreateAnAccount(string email)
        {
            IWebElement signInButtonMainPage = _driver.FindElement(SIGN_IN_BUTTON_MAIN_PAGE);
            signInButtonMainPage.Click();

            IWebElement emailAddressNew = _driver.FindElement(EMAIL_ADDRESS_NEW);
            emailAddressNew.SendKeys(email);

            IWebElement createAnAccountButton = _driver.FindElement(CREATE_AN_ACCOUNT_BUTTON);
            createAnAccountButton.Click();

            IWebElement firstName = _driver.FindElement(FIRST_NAME_INPUT);
            firstName.SendKeys("Cate");

            IWebElement lastName = _driver.FindElement(LAST_NAME_INPUT);
            lastName.SendKeys("Moss");

            IWebElement password = _driver.FindElement(PASSWORD_INPUT);
            password.SendKeys("12345");

            IWebElement address = _driver.FindElement(ADDRESS_INPUT);
            address.SendKeys("Glacier Highway");

            IWebElement city = _driver.FindElement(CITY_INPUT);
            city.SendKeys("Juneau");

            IWebElement state = _driver.FindElement(STATE_DROPDOWN);
            state.Click();
            SelectElement select = new SelectElement(state);
            select.SelectByText("Alaska");

            IWebElement postalCode = _driver.FindElement(POSTAL_CODE_INPUT);
            postalCode.SendKeys("99824");

            IWebElement mobilePhone = _driver.FindElement(MOBILE_PHONE_INPUT);
            mobilePhone.SendKeys("8889988");

            IWebElement registerButton = _driver.FindElement(REGISTER_BUTTON);
            registerButton.Click();
        }

        public void AlreadyRegisteredAccountEmail(string email)
        {
            IWebElement signInButton = _driver.FindElement(SIGN_IN_BUTTON_MAIN_PAGE);
            signInButton.Click();

            IWebElement registeredEmailAddress = _driver.FindElement(REGISTERED_EMAIL_ADDRESS);
            registeredEmailAddress.SendKeys(email);
        }

        public void AlreadyRegisteredAccountPassword(string password)
        {
            IWebElement registeredPassword = _driver.FindElement(REGISTERED_PASSWORD_ADDRESS);
            registeredPassword.SendKeys(password);

            IWebElement signInButton = _driver.FindElement(SIGN_IN_BUTTON);
            signInButton.Click();
        }
    }
}
