using BrowserStack;
using FMPOnlineTests.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace FMPOnlineTests.TestMethods.BaseTest
{
    public abstract class BaseBrowserstackFeature
    {
        protected IWebDriver Driver;
        private Local BrowserStackLocal;
        protected DesiredCapabilities Capabilities;
        protected readonly string Url = EnvironmentConstants.WebAppUrl;
        private readonly string RemoteServerUri = ConfigurationManager.AppSettings.Get("BsServer");
        private readonly string BrowserStackUser = ConfigurationManager.AppSettings.Get("BsUser");
        private readonly string BrowserStackKey = ConfigurationManager.AppSettings.Get("BsKey");

        public TestContext TestContext { get; set; }

        public BaseBrowserstackFeature()
        {
            Capabilities = new DesiredCapabilities();
            Capabilities.SetCapability("browserstack.user", BrowserStackUser);
            Capabilities.SetCapability("browserstack.key", BrowserStackKey);
        }

        [TestInitialize]
        public void TestInit()
        {
            //set the browserstack test name
            Capabilities.SetCapability("name", TestContext.TestName);
        }

        [TestCleanup]
        public void EndTest()
        {
            if (Driver != null) Driver.Quit();

            if (BrowserStackLocal != null) BrowserStackLocal.stop();
        }

        protected IWebDriver GetDriverInstance(string browser)
        {
            SetEnvironmentSettings(browser);
            BsLocalInit();
            Capabilities.SetCapability("browser", browser);
            Driver = new RemoteWebDriver(new Uri("http://" + RemoteServerUri), Capabilities);
            //TODO:
            //on mobile devices this method is not working
            Driver.Manage().Window.Maximize();

            return Driver;
        }

        private void SetEnvironmentSettings(string env)
        {
            NameValueCollection caps = ConfigurationManager.GetSection("environments/" + env) as NameValueCollection;
            string isLocal = ConfigurationManager.AppSettings.Get("BstackLocal");

            foreach (var key in caps.AllKeys)
            {
                Capabilities.SetCapability(key, caps[key]);
            }

            Capabilities.SetCapability("browserstack.local", isLocal);
        }

        private void BsLocalInit()
        {
            BrowserStackLocal = new Local();
            List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("key", BrowserStackKey)
            };

            BrowserStackLocal.start(bsLocalArgs);
        }
    }
}
