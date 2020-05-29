using AllPoints.Features.BaseTest.Constants;
using BrowserStack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace AllPoints.Tests.BaseTest
{
    public abstract class BaseBrowserstackTest
    {
        protected IWebDriver Driver;
        protected DesiredCapabilities Capabilities;
        protected readonly string Url = EnvironmentConstants.AllPointsWebAppUrl;
        Local BrowserStackLocal;
        NameValueCollection CapabilitiesValues;
        readonly string RemoteServerUri = ConfigurationManager.AppSettings.Get("BsServer");
        readonly string BrowserStackUser = ConfigurationManager.AppSettings.Get("BsUser");
        readonly string BrowserStackKey = ConfigurationManager.AppSettings.Get("BsKey");
        readonly string IsLocal = ConfigurationManager.AppSettings.Get("BstackLocal") ?? "false";

        public TestContext TestContext { get; set; }

        public BaseBrowserstackTest()
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

        protected IWebDriver GetDriverInstance(string device)
        {
            SetEnvironmentSettings(device);
            if (IsLocal == "true")
            {
                BsLocalInit();
            }
            Driver = new RemoteWebDriver(new Uri("http://" + RemoteServerUri), Capabilities);
            return Driver;
        }

        private void SetEnvironmentSettings(string env)
        {
            CapabilitiesValues = ConfigurationManager.GetSection("environments/" + env) as NameValueCollection;

            if (CapabilitiesValues == null) throw new ArgumentException($"Device ({env}) not found on config file");

            foreach (var key in CapabilitiesValues.AllKeys)
            {
                Capabilities.SetCapability(key, CapabilitiesValues[key]);
            }

            Capabilities.SetCapability("browserstack.local", IsLocal);
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
