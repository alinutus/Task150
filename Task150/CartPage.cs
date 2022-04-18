using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace Task150
{
    public class CartPage
    {
        private static readonly By ALL_DRESSES_PAGE = By.XPath("//*[@id='block_top_menu']/ul/li[1]/a");
        private static readonly By FIRST_ADD_TO_CART_BUTTON = By.XPath("//a[@data-id-product='1']");
        private static readonly By CONTINUE_SHOPPING_BUTTON = By.XPath("//span[@title='Continue shopping']");
        private static readonly By SECOND_ADD_TO_CART_BUTTON = By.XPath("//a[@data-id-product='3']");
        private static readonly By THIRD_ADD_TO_CART_BUTTON = By.XPath("//a[@data-id-product='5']");
        private static readonly By PROCEED_TO_CHECKOUT = By.XPath("//a[@title='Proceed to checkout']");



        protected IWebDriver _driver;


        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ThreeProductsAddingToCart()
        {
            IWebElement allDressesPage = _driver.FindElement(ALL_DRESSES_PAGE);
            allDressesPage.Click();

            IWebElement firstProduct = _driver.FindElement(FIRST_ADD_TO_CART_BUTTON);
            firstProduct.Click();

            Thread.Sleep(7000);

            IWebElement continueShopping = _driver.FindElement(CONTINUE_SHOPPING_BUTTON);
            continueShopping.Click();

            Thread.Sleep(1000);

            IWebElement secondProduct = _driver.FindElement(SECOND_ADD_TO_CART_BUTTON);
            secondProduct.Click();

            Thread.Sleep(7000);

            continueShopping.Click();

            Thread.Sleep(5000);

            IWebElement thirdProduct = _driver.FindElement(THIRD_ADD_TO_CART_BUTTON);
            thirdProduct.Click();

            Thread.Sleep(3000);

            IWebElement proceedToCheckout = _driver.FindElement(PROCEED_TO_CHECKOUT);
            proceedToCheckout.Click();

            Thread.Sleep(1000);
        }
    }
}
