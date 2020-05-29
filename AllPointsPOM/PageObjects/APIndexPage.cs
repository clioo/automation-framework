using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.MyAccountPOM;
using AllPoints.PageObjects.MyAccountPOM.DashboardPOM;
using AllPoints.PageObjects.NewFolder1;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages.Components;
using CommonHelper;
using CommonHelper.Pages.IndexPage;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AllPoints.Pages
{
    public class APIndexPage : BaseIndexPage
    {

        public Header Header;

        public AccountMenuLeft AccountMenuLeft;

        #region constructor
        public APIndexPage(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            this.mainBodySummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        public APIndexPage(IWebDriver driver, bool init) : base(driver, init)
        {
            Driver = driver;

            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            this.mainBodySummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        public APIndexPage(IWebDriver driver, string url) : base(driver, url)
        {
            Driver = driver;
            Driver.Navigate().GoToUrl(url);
            Header = new Header(Driver);
            AccountMenuLeft = new AccountMenuLeft(Driver);
            this.mainBodySummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }
        #endregion

        #region details
        private DomElement mainBodySummary = new DomElement(By.CssSelector)
        {
            locator = "#main"
        };

        DomElement productSpotlightDeals = new DomElement(By.CssSelector)
        {
            locator = "div.slick-active"
        };
        #endregion


        //Deprecated methods, please stop using it
        public void Init(string url)
        {
            Driver.Navigate().GoToUrl(url);

            Header = new Header(Driver);
            AccountMenuLeft = new AccountMenuLeft(Driver);
        }

        public OfferingProductsPage ClickOnProductSpotlightDealsByIndex(int index)
        {
            List<DomElement> productsSpotlightDeals = mainBodySummary.GetElementsWaitByCSS(this.productSpotlightDeals.locator);
            if (index > productsSpotlightDeals.Count - 1 || index < 0) index = productsSpotlightDeals.Count - 1;
            productsSpotlightDeals[index].webElement.Click();
            return new OfferingProductsPage(Driver);
        }
    }
}