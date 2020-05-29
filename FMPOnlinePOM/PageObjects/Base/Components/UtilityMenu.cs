using CommonHelper;
using CommonHelper.BaseComponents;
using FMPOnlinePOM.PageObjects.Cart;
using OpenQA.Selenium;

namespace FMPOnlinePOM.PageObjects.Base.Components
{
    public class UtilityMenu : BaseComponent
    {
        #region locators
        private DomElement Container = new DomElement(By.CssSelector)
        {
            locator = "section.tnav"
        };
        private DomElement ContainerContact = new DomElement
        {
            locator = "nav.contact"
        };
        private DomElement ContactPhoneNumber = new DomElement
        {
            locator = ".phone a span"
        };
        private DomElement CartButton = new DomElement
        {
            locator = "button.minicart"
        };
        #endregion locators

        public MiniCart MiniCart;

        //TODO: add menu items as fmp needs them

        #region constructor
        public UtilityMenu(IWebDriver driver) : base(driver)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);
            MiniCart = new MiniCart(Driver);
        }
        #endregion constructor

        public void ShowMiniCart()
        {
            DomElement cartButton = Container.GetElementWaitByCSS(CartButton.locator);
            HoverWebElement(cartButton);
        }

        public CartPage ClickOnCart()
        {
            DomElement cartButton = Container.GetElementWaitByCSS(CartButton.locator);
            cartButton.webElement.Click();
            return new CartPage(Driver);
        }

        public string GetPhoneNumber()
        {
            DomElement contactSection = Container.GetElementWaitByCSS(ContainerContact.locator);
            DomElement phoneNumber = contactSection.GetElementWaitByCSS(ContactPhoneNumber.locator);
            return phoneNumber.webElement.Text;
        }
    }
}