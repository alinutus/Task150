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
    public class WishlistPage
    {
        private static readonly By WISHLIST_NAME = By.Id("name");
        private static readonly By SAVE_BUTTON = By.Id("submitWishlist");
        private static readonly By PRODUCT_TSHIRT = By.XPath("//div[@id='best-sellers_block_right']//li[2]/a");
        private static readonly By ADD_TO_WISHLIST_BUTTON = By.Id("wishlist_button");
        private static readonly By CUSTOMER_ACCOUNT = By.XPath("//a[@title='View my customer account']");
        private static readonly By MY_WISHLIST = By.ClassName("icon-heart");
        private static readonly By CREATED_WISHLIST = By.XPath("//td[@style='width:200px;']/a[1]");
        private static readonly By CLOSE_BUTTON = By.ClassName("fancybox-item");

        protected IWebDriver _driver;
        

        public WishlistPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void WishlistCreating(string name)
        {
            IWebElement wishlist = _driver.FindElement(MY_WISHLIST);
            wishlist.Click();

            IWebElement wishlistName = _driver.FindElement(WISHLIST_NAME);
            wishlistName.SendKeys(name);

            IWebElement saveButton = _driver.FindElement(SAVE_BUTTON);
            saveButton.Click();
         }

        public void ProductAddingToWishlist()
        {
            IWebElement tshirt = _driver.FindElement(PRODUCT_TSHIRT);
            tshirt.Click();

            IWebElement addToWishlistButton = _driver.FindElement(ADD_TO_WISHLIST_BUTTON);
            addToWishlistButton.Click();

            Thread.Sleep(8000);

            IWebElement closeButton = _driver.FindElement(CLOSE_BUTTON);
            closeButton.Click();

            IWebElement customerAccount = _driver.FindElement(CUSTOMER_ACCOUNT);
            customerAccount.Click();

            IWebElement myWishlist = _driver.FindElement(MY_WISHLIST);
            myWishlist.Click();

            IWebElement createdWishlist = _driver.FindElement(CREATED_WISHLIST);
            createdWishlist.Click();

            Thread.Sleep(8000);
        }
    }
}
