using CommonHelper;
using FMPOnlinePOM.Base;
using FMPOnlinePOM.PageObjects.SignInRegister;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FMPOnlinePOM.PageObjects.Cart
{
    public class CartPage : FmpBasePage
    {
        DomElement LoadingCartSubtitle = new DomElement
        {
            locator = ".detail-section h2"
        };
        DomElement Container = new DomElement
        {
            locator = "div.container.cart"
        };
        DomElement DetailSection = new DomElement
        {
            locator = ".detail-section"
        };
        DomElement CartItemsSection = new DomElement
        {
            locator = ".cart-items-section"
        };
        DomElement CartItemsSectionGridItems = new DomElement
        {
            locator = ".cart-items"
        };
        //header text
        //multiselect action content
        DomElement CartItem = new DomElement
        {
            locator = ".cart-item"
        };
        DomElement NavSection = new DomElement
        {
            locator = ".nav-section"
        };
        DomElement ActionsSectionButton = new DomElement
        {
            locator = ".actions-section button"
        };

        #region constructor
        public CartPage(IWebDriver driver) : base(driver)
        {
        }
        #endregion constructor

        public List<CartProductItem> GetProductItems()
        {
            List<CartProductItem> itemsNamesCollection = new List<CartProductItem>();
            DomElement cartContainer = BodyContainer.GetElementWaitByCSS(Container.locator);
            DomElement detailSection = cartContainer.GetElementWaitByCSS(DetailSection.locator);
            DomElement itemsContainer = detailSection.GetElementWaitByCSS(CartItemsSection.locator);
            List<DomElement> items = itemsContainer.GetElementsWaitByCSS(CartItem.locator);

            foreach (var item in items)
            {
                itemsNamesCollection.Add(new CartProductItem(Driver, item));
            }
            return itemsNamesCollection;
        }

        public void WaitUntilItemsAreDisplayed(int time = 30)
        {
            BodyContainer.GetElementWaitUntil(LoadingCartSubtitle.locator, (el) => 
            {
                return !(el.Displayed);
            }, time);
        }

        public SignInRegisterPage ClickOnProceedToCheckoutButtonAnonymous()
        {
            DomElement cartContainer = BodyContainer.GetElementWaitByCSS(Container.locator);
            DomElement navSection = cartContainer.GetElementWaitByCSS(NavSection.locator);
            DomElement checkoutButton = navSection.GetElementWaitByCSS(ActionsSectionButton.locator);
            checkoutButton.webElement.Click();

            return new SignInRegisterPage(Driver);
        }
    }
}