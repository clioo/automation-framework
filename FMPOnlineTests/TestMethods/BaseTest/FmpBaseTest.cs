using FMPOnlineTests.Constants;
using FMPOnlineTests.Enums;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.IO;

namespace FMPOnlineTests.TestMethods.BaseTest
{
    [TestClass]
    public abstract class FmpBaseTest
    {
        protected string Url;
        protected IWebDriver Driver;
        protected IAutomationDataFactory DataFactory;
        public TestContext TestContext
        {
            get => _testContext;
            set => _testContext = value;
        }
        static TestContext _testContext { get; set; }

        public FmpBaseTest()
        {
            DataFactoryConfiguration configuration = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = EnvironmentConstants.IntegrationsApiUrl,
                ShippingServiceApiUrl = EnvironmentConstants.ShippingServiceApiUrl,
                TenantExternalIdentifier = EnvironmentConstants.PlatformExternalId,
                TenantInternalIdentifier = EnvironmentConstants.PlatformId
            };

            Url = EnvironmentConstants.WebAppUrl;
            DataFactory = new AutomationDataFactory(configuration);
        }

        [TestInitialize]
        public virtual void InitTest()
        {
            //chrome is selected as default
            Driver = GetDriverInstance(BrowsersEnum.Chrome);
            Driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public virtual void EndTest()
        {
            if (Driver != null)
                Driver.Quit();
        }

        //define the web driver instance [chrome, iexplorer, firefox]
        protected virtual IWebDriver GetDriverInstance(BrowsersEnum browser)
        {
            string dir = Directory.GetCurrentDirectory();

            switch (browser)
            {
                case BrowsersEnum.Chrome:
                    if (IsBrowserHeadless())
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--headless");
                        options.AddArgument("-no-sandbox");
                        return new ChromeDriver(dir, options);
                    }
                    return new ChromeDriver(dir);

                case BrowsersEnum.IExplorer:
                    return new InternetExplorerDriver(dir);

                //TODO
                //add firefox driver
                //case Firefox:

                default:
                    throw new ArgumentException("Browser not supported yet --> " + browser);
            }
        }

        //determine if headless is set
        protected bool IsBrowserHeadless()
        {
            //headless is true by default
            string headlessString = EnvironmentConstants.IsBrowserHeadless ?? bool.TrueString;

            bool headless = (headlessString.ToLower() == bool.TrueString.ToLower());

            return headless;
        }
    }
}
