using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.Commons;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Support.Extensions;

namespace Task150
{
    [AllureNUnit]

    [TestFixture("Chrome", "latest", "Windows 10", "Chrome Windows 10")]
    [TestFixture("Firefox", "latest", "Windows 10", "Firefox Windows 10")]
    public class OnlineShopTests
    {
        private static readonly By MY_WISHLIST = By.ClassName("icon-heart");
        private static readonly By CREATED_WISHLIST = By.XPath("//td[@style='width:200px;']/a[1]");
        private static readonly By WISHLIST_PRODUCT = By.XPath("//p[@id='s_title' and normalize-space(text()) = 'Faded Short Sleeve T-shirts']");
        private static readonly By PRODUCTS_TABLE = By.XPath("//table[@id='cart_summary']//tbody/tr");
        private static readonly By DELETE_WISHLIST_BUTTON = By.XPath("//td[@class='wishlist_delete']/a");

        private SauceLabDetails details;
        private IWebDriver Driver;

        public OnlineShopTests(string browser, string version, string platform, string name)
        {
            details = new SauceLabDetails(browser, version, platform, name);
        }

        [SetUp]
        public void SetUp()
        {
            Driver = SwitchConfig.SwitchEnv(details);
        }

        [Test]
        [Description("In this test user logs in, Wishlist is created, Products are added to the cart")]
        [AllureOwner("Alina")]
        [AllureTag("Registered User")]
        [AllureSeverity(SeverityLevel.critical)]
        public void WishListAndCartOfTheRegistredUserTest()
        {

            var loginPage = new LoginPage(Driver);

            loginPage.AlreadyRegisteredAccountEmail("a@a.by");

            loginPage.AlreadyRegisteredAccountPassword("12345");

            var waitLogin = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(15));
            var elementLogin = waitLogin.Until(elementToBeDisplayed => elementToBeDisplayed.FindElement(MY_WISHLIST));

            var wishlist = new WishlistPage(Driver);
            wishlist.WishlistCreating("Wishlist 1");

            var wishlistName = "Wishlist 11";
            var wishlistElement = Driver.FindElement(CREATED_WISHLIST).Text;
            var wishlistMessage = "Wishlist name has wrong value";
            Assert.AreEqual(wishlistName, wishlistElement, wishlistMessage);

            wishlist.ProductAddingToWishlist();

            var productName = "Faded Short Sleeve T-shirts\r\nS, Orange";
            var wishlistProduct = Driver.FindElement(WISHLIST_PRODUCT).Text;
            var productMessage = "Product name has wrong value";
            Assert.AreEqual(productName, wishlistProduct, productMessage);

            var myCart = new CartPage(Driver);
            myCart.ThreeProductsAddingToCart();

            var productsIsValid = CountProductsInTheCart();
            Assert.IsTrue(productsIsValid);

            DeleteProductsInTheCart();
        }

        [Test]
        [Description("In this test user creates a new account and creates a new auto Wishlist")]
        [AllureOwner("Alina")]
        [AllureTag("New User")]
        public void NewAccountAndWishlistCreatingTest()
        {
            var loginPage = new LoginPage(Driver);

            loginPage.CreateAnAccount("new1@a.by");

            var waitLogin = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(15));
            var elementLogin = waitLogin.Until(elementToBeDisplayed => elementToBeDisplayed.FindElement(MY_WISHLIST));

            elementLogin.Click();

            var wishlist = new WishlistPage(Driver);
            wishlist.ProductAddingToWishlist();

            var productName = "Faded Short Sleeve T-shirts\r\nS, Orange";
            var wishlistProduct = Driver.FindElement(WISHLIST_PRODUCT).Text;
            var productMessage = "Product name has wrong value";
            Assert.AreEqual(productName, wishlistProduct, productMessage);

            DeleteWishlist();
        }

        [TearDown]
        public void CloseBrowser()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshotDriver = Driver as ITakesScreenshot;
                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                string dateTime = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
                string platform = Platform.CurrentPlatform.ToString();
                screenshot.SaveAsFile(dateTime + platform + ".jpg", ScreenshotImageFormat.Jpeg);
                

                AllureLifecycle.Instance.AddAttachment($"Screenshot[{ DateTime.Now:HH: mm: ss}]",
                "image/png", Driver.TakeScreenshot().AsByteArray);
            }
        }

        public bool CountProductsInTheCart()
        {
            var productsList = new List<ProductsInfo>();
            var products = Driver.FindElements(PRODUCTS_TABLE);
            var qty = products.Count;
            if (qty == 3)
            {
                foreach (var product in products)
                {
                    var newProd = new ProductsInfo(
                        product.FindElement(By.XPath(".//td[2]/p/a")).Text,
                        decimal.Parse(product.FindElement(By.XPath(".//td[4]/span/span")).Text, NumberStyles.Currency));
                    productsList.Add(newProd);
                }

                var totalSumm = decimal.Parse(Driver.FindElement(By.Id("total_product")).Text, NumberStyles.Currency);
                var summProducts = productsList.Sum(x => x.Total);

                Assert.AreEqual(totalSumm, summProducts);
                return true;
            }

            return false;
        }

        public void DeleteProductsInTheCart()
        {
            var products = Driver.FindElements(PRODUCTS_TABLE);

            foreach (var product in products)
            {
                var newProd = product.FindElement(By.XPath(".//td[7]//i")).Click;
            }
        }

        public void DeleteWishlist()
        {
            Driver.FindElement(DELETE_WISHLIST_BUTTON).Click();

            var alert = Driver.SwitchTo().Alert();
            alert.Accept();

        }
    }
}