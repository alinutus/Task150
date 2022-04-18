using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Linq;
using System.Globalization;

namespace Task150
{
    public class OnlineShopTests
    {
        private static readonly By MY_WISHLIST = By.ClassName("icon-heart");
        private static readonly By CREATED_WISHLIST = By.XPath("//td[@style='width:200px;']/a[1]");
        private static readonly By WISHLIST_PRODUCT = By.XPath("//p[@id='s_title' and normalize-space(text()) = 'Faded Short Sleeve T-shirts']");

        private IWebDriver Driver;

        [Test]
        public void WishListAndCartOfTheRegistredUser()
        {
            Driver = new ChromeDriver();
            var loginPage = new LoginPage(Driver);

            loginPage.AlreadyRegisteredAccountEmail("a@a.by");

            loginPage.AlreadyRegisteredAccountPassword("12345");

            var waitLogin = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(15));
            var elementLogin = waitLogin.Until(elementToBeDisplayed => elementToBeDisplayed.FindElement(MY_WISHLIST));

            var wishlist = new WishlistPage(Driver);
            wishlist.WishlistCreating("Wishlist 1");

            var wishlistName = "Wishlist 1";
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
        }

        public bool CountProductsInTheCart()
        {
            var productsList = new List<ProductsInfo>();
            var products = Driver.FindElements(By.XPath("//table[@id='cart_summary']//tbody/tr"));
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

        [Test]
        public void NewAccountAndWishlistCreating()
        {
            Driver = new ChromeDriver();
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
        }
    }
}