using System;

namespace Task150
{
    public class SauceLabDetails
    {
        public string Browser { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string Name { get; set; }
        public string SauceUserName { get; set; }
        public string SauceAccessKey { get; set; }

        public SauceLabDetails(
            string browser, 
            string version, 
            string platform, 
            string name)
        {
            Browser = browser;
            Version = version;
            Platform = platform;
            Name = name;
            SauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            SauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
        }
    }
}
