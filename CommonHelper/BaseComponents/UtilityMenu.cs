using OpenQA.Selenium;
using System.Linq;

namespace CommonHelper.BaseComponents
{
    public class UtilityMenu : BaseComponent
    {
        protected readonly DomElement _container;
        MiniCart Minicart;
        protected DomElement MinicartContainer = new DomElement { locator = "article.mini-cart" };
        protected DomElement ContainerContact = new DomElement { locator = "nav.contact" };
        protected DomElement NavSection = new DomElement { locator = "nav:nth-of-type(2) ul" };

        protected DomElement ContactPhoneNumber = new DomElement { locator = ".phone a span" };
        protected DomElement CartButton = new DomElement { locator = "button.minicart" };
        protected DomElement SignInLocator = new DomElement { locator = "li.user a" };
        protected DomElement QuickOrderLocator = new DomElement { locator = ".quick-order" };

        //myAccount menu
        protected DomElement MyAccountDropdownLocator = new DomElement { locator = "li.dropdown.user" };
        protected DomElement Locator = new DomElement { locator = "li.dropdown.user" };

        public UtilityMenu(IWebDriver driver, DomElement componentContainer) : base (driver)
        {
            _container = componentContainer;
        }

        public virtual void OpenMiniCart()
        {
            var minicartContainer = _container.GetElementWaitByCSS(MinicartContainer.locator);
            HoverWebElement(minicartContainer);
            Minicart = new MiniCart(minicartContainer);
        }

        public virtual void Cart()
        {
            DomElement cartButton = _container.GetElementWaitByCSS(CartButton.locator);
            cartButton.webElement.Click();
        }

        public virtual void SignIn()
        {
            var navSection = _container.GetElementWaitByCSS(NavSection.locator);
            var signIn = navSection.GetElementWaitByCSS(SignInLocator.locator);
            signIn.webElement.Click();
        }

        public virtual void QuickOrder()
        {
            var navContainer = _container.GetElementWaitByCSS(NavSection.locator);
            var quickOrder = navContainer.GetElementWaitByCSS(QuickOrderLocator.locator);
            quickOrder.webElement.Click();
        }

        public virtual string GetPhoneNumber()
        {
            DomElement contactSection = _container.GetElementWaitByCSS(ContainerContact.locator);
            DomElement phoneNumber = contactSection.GetElementWaitByCSS(ContactPhoneNumber.locator);
            return phoneNumber.webElement.Text;
        }

        public void SelectMyAccountOption(string menuItemLabel)
        {
            var navSection = _container.GetElementWaitByCSS(NavSection.locator);
            var myAccountMenu = navSection.GetElementWaitByCSS(MyAccountDropdownLocator.locator);
            HoverWebElement(myAccountMenu);

            var children = myAccountMenu.GetElementsWaitByCSS("li:not(.divider)");
            var item = children.FirstOrDefault(i => i.webElement.Text.Contains(menuItemLabel));

            if (item != null)
            {
                item.webElement.Click();
            }
            else
            {
                throw new NotFoundException($"Menu item, '{menuItemLabel}' is not found");
            }
        }
    }
}
