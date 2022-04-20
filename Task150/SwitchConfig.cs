using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Task150
{
    public class SwitchConfig
    {
        private IWebDriver Driver;

        const Environment env = Environment.Local;
        public static IWebDriver SwitchEnv(SauceLabDetails sauceLabDetails)
        {
            switch (env)
            {
                case Environment.Local:
                    return LocalEnv(sauceLabDetails.Browser);
                case Environment.Remote:
                    return SelenoidEnv(sauceLabDetails.Browser);
                case Environment.SauceLabs:
                    return SauceLabEnv(sauceLabDetails);
            }
        }
        enum Environment
        {
            Local,
            Remote,
            SauceLabs
        }

        private static IWebDriver LocalEnv(string browser)
        {
            return GetBrowserDriver(browser);
            
        }

        private static IWebDriver SauceLabEnv(SauceLabDetails sauceLabDetails)
        {
            var browserOptions = GetBrowserOptions(sauceLabDetails.Browser);
            browserOptions.PlatformName = sauceLabDetails.Platform;
            browserOptions.BrowserVersion = sauceLabDetails.Version;

            var sauceOptions = new Dictionary<string, object>();
            sauceOptions.Add("name", sauceLabDetails.Name);
            sauceOptions.Add("username", sauceLabDetails.SauceUserName);
            sauceOptions.Add("accessKey", sauceLabDetails.SauceAccessKey);
            sauceOptions.Add("extendedDebugging", true);
            sauceOptions.Add("capturePerformance", true);

            browserOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);
            var driver = new RemoteWebDriver(new Uri("https://ondemand.eu-central-1.saucelabs.com/wd/hub"),
                                    browserOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
            Thread.Sleep(1000);

            return driver;
        }

        private static dynamic GetBrowserOptions(string browser)
        {
            if (browser == "Chrome")
                return new ChromeOptions();
            if (browser == "Firefox")
                return new FirefoxOptions();

            return new ChromeOptions();
        }

        private static IWebDriver GetBrowserDriver(string browser)
        {
            if (browser == "Chrome")
                return new ChromeDriver();
            if (browser == "Firefox")
                return new FirefoxDriver();

            return new ChromeDriver();
        }

        private static IWebDriver SelenoidEnv(string browser)
        {
            var browserOptions = GetBrowserOptions(browser);
            browserOptions.BrowserVersion = "99";

            var selenoidOptions = new Dictionary<string, object>();
            selenoidOptions.Add("enableVNC", true);
            selenoidOptions.Add("enableVideo", true);

            browserOptions.AddAdditionalCapability("selenoid:options", selenoidOptions);
            var driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), browserOptions);
            Thread.Sleep(1000);

            return driver;
        }
    }
}
