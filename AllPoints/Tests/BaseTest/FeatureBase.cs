using AllPoints.Features.BaseTest.Constants;
using CommonHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Configuration;
using System.IO;

namespace AllPoints.Features
{
    [DeploymentItem("ChromeDriver.exe")]
    [DeploymentItem("IEDriverServer.exe")]
    public abstract class FeatureBase
    {
        public static TestContext testContext
        {
            get { return privateContext; }
            set { privateContext = value; }
        }

        private static TestContext privateContext { get; set; }

        protected string url = EnvironmentConstants.HostUrl;
        protected string Browser = SeleniumConstants.Browser;

        protected IWebDriver driver;

        [ClassInitialize]
        public static void InitTestSuite(TestContext tstContext)
        {
            testContext = tstContext;
        }

        [TestInitialize]
        public virtual void TestInit()
        {            
            driver = SetTestBrowser();
            driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public virtual void EndTest()
        {
            if (driver != null)
                driver.Quit();
        }

        private IWebDriver SetTestBrowser()
        {
            IWebDriver driver = null;
            Browser = Browser.ToLower();
            bool isHeadless = ConfigurationManager.AppSettings["IsHeadless"] == "true";

            switch (Browser)
            {
                case BrowserConstants.Chrome:
                    if (isHeadless || ShouldBeHeadless())
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--headless");
                        options.AddArgument("-no-sandbox");
                        //under test flags
                        options.AddArgument("--start-maximized");
                        options.AddArgument("--window-size=1920x1080");
                        //****
                        var dir = Directory.GetCurrentDirectory();
                        driver = new ChromeDriver(dir, options);
                    }
                    else
                    {
                        driver = new ChromeDriver();
                    }

                    break;

                case BrowserConstants.IExplorer:
                    //headless mode not supported yet..
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.IgnoreZoomLevel = true;
                    ieOptions.ElementScrollBehavior = InternetExplorerElementScrollBehavior.Bottom;
                    driver = new InternetExplorerDriver(ieOptions);
                    break;

                default:
                    break;
            }

            return driver;
        }

        private bool ShouldBeHeadless()
        {
            if (url == EnvironmentConstants.QASofttek)
            {
                return false;
            }
            else if (url == EnvironmentConstants.DevSandbox)
            {
                return false;
            }
            else if (url == EnvironmentConstants.Chris)
            {
                return false;
            }

            return true;
        }
    }
}