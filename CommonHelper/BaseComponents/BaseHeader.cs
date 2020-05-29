using OpenQA.Selenium;

namespace CommonHelper.BaseComponents
{
    public abstract class BaseHeader : BaseComponent
    {
        protected DomElement Container = new DomElement(By.CssSelector)
        {
            locator = "header"
        };
        //utility menu
        readonly UtilityMenu _utilityMenu;        
        protected readonly DomElement UtilityMenuContainer = new DomElement
        {
            locator = "section.tnav"
        };
        //top header
        //categories list
        //In checkout this list does not exist

        public BaseHeader(IWebDriver driver): base(driver)
        {
            Container.Init(driver, SeleniumConstants.defaultWaitTime);
            var utilityMenuContainer = Container.GetElementWaitByCSS(UtilityMenuContainer.locator);

            //initialize subcomponents
            _utilityMenu = new UtilityMenu(driver, utilityMenuContainer);
        }

        protected virtual void SelectSignInOption()
        {
            _utilityMenu.SignIn();
        }

        protected virtual void SelectQuickOrder()
        {
            _utilityMenu.QuickOrder();
        }

        protected virtual void SelectMyAccountMenuItem(string menuItemText)
        {
            _utilityMenu.SelectMyAccountOption(menuItemText);
        }

        protected virtual void SelectCart()
        {
            _utilityMenu.Cart();
        }

        protected virtual void HoverOnCart()
        {
            _utilityMenu.OpenMiniCart();
        }

        protected virtual void SelectCategory(string categoryLabel) { }
    }
}
