using AllPoints.Pages.Components;
using CommonHelper;
using CommonHelper.Pages.ListPage.HomePage;
using OpenQA.Selenium;

namespace AllPoints.PageObjects.ListPOM.HomePagePOM
{
    public class APListHomePage : BaseListHomePage
    {
        //deprecated
        //private GenericPage browserHelper { get { return Helper; } }
        public Header Header;
        public AccountMenuLeft AccountMenuLeft;
        #region Constructor
        public APListHomePage(IWebDriver driver) : base(driver)
        {
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }
        #endregion

    }
}
