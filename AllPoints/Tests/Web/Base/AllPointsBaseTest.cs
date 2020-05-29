using AllPoints.Base;
using AllPoints.Enums;
using AllPoints.Features.BaseTest.Constants;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AllPoints.AllPoints
{
    public abstract class AllPointsBaseTest : BaseTest
    {
        public static TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }
        static TestContext _testContext { get; set; }

        public AllPointsBaseTest()
        {
            DataFactoryConfiguration configuration = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = EnvironmentConstants.IntegrationsApiUrl,
                TenantExternalIdentifier = EnvironmentConstants.AllPointsPlatformExtId,
                TenantInternalIdentifier = EnvironmentConstants.AllPointsPlatformId,
                ShippingServiceApiUrl = EnvironmentConstants.ShippingServiceApiUrl
            };

            Url = EnvironmentConstants.AllPointsWebAppUrl;
            DataFactory = new AutomationDataFactory(configuration);
        }

        [ClassInitialize]
        public static void InitTestSuite(TestContext testContext)
        {
            TestContext = testContext;
        }

        [TestInitialize]
        public virtual void InitTest()
        {
            //default browser is Chrome
            BrowsersEnum browser = BrowsersEnum.Chrome;
            string browserString = EnvironmentConstants.Browser.ToLower();
            switch (browserString)
            {
                case "chrome":
                    browser = BrowsersEnum.Chrome;
                    break;
                case "ie":
                    browser = BrowsersEnum.IExplorer;
                    break;
            }
            Driver = GetDriverInstance(browser);
            Driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public virtual void EndTest()
        {
            if (Driver != null)
                Driver.Quit();            
        }
    }
}
