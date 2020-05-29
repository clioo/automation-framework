using CommonHelper;
using CommonHelper.BaseComponents;
using OpenQA.Selenium;

namespace AllPoints.Pages.Components
{
    public class AccountMenuLeft : BaseComponent
    {
        private DomElement AccountMenuContainer = new DomElement(By.CssSelector)
        {
            locator = "aside.nav-section"
        };
        private DomElement AccountDashboardSection = new DomElement
        {
            locator = "nav section:nth-of-type(1)"
        };
        private DomElement MenuLinkOption = new DomElement
        {
            locator = "ul li a"
        };
        private DomElement AccountSection = new DomElement
        {
            locator = "nav section:nth-of-type(5)"
        };
        private DomElement AccountNumber = new DomElement
        {
            locator = "p"
        };

        #region constructor
        public AccountMenuLeft(IWebDriver driver) : base(driver)
        {

        }
        #endregion constructor

        public string GetAccountNumber()
        {
            AccountMenuContainer.Init(Driver, SeleniumConstants.defaultWaitTime);
            DomElement accountContainer = AccountMenuContainer.GetElementWaitByCSS(AccountSection.locator);            
            return accountContainer.GetElementWaitByCSS(AccountNumber.locator).webElement.Text;
        }
    }
}