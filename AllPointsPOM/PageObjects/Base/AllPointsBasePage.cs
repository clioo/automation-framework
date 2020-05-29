using AllPoints.Pages.Components;
using CommonHelper;
using CommonHelper.BaseComponents;
using OpenQA.Selenium;

namespace AllPoints.PageObjects.NewFolder1
{
    public class AllPointsBaseWebPage : BasePOM
    {
        public Header Header;
        public AccountMenuLeft AccountMenuLeft;

        // deprecate this helper
        protected GenericPage Helper;

        public AllPointsBaseWebPage(IWebDriver driver) : base(driver)
        {
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);

            //deprecated
            Helper = new GenericPage(driver);
        }        

        public string GetAccountNumber()
        {
            return AccountMenuLeft.GetAccountNumber();
        }
    }
}