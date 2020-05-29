using CommonHelper;
using FMPOnlinePOM.Base;
using FMPOnlinePOM.PageObjects.SignInRegister;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPOnlinePOM.PageObjects.Checkout
{
    public class CheckoutPage : FmpBasePage
    {
        DomElement Container = new DomElement
        {
            locator = ".checkout-app"
        };
        DomElement DetailSection = new DomElement
        {
            locator = ".detail-section"
        };
        DomElement LoadingHeading = new DomElement
        {
            locator = "h2"
        };
        DomElement NavSection = new DomElement
        {
            locator = ".nav-section"
        };
        DomElement CheckoutItemsSection = new DomElement
        {
            locator = ".checkout-items-section"
        };
        //cart header container
        //header
        //count
        DomElement ItemsGridSection = new DomElement
        {
            locator = ".content .cart-items"
        };
        DomElement CheckoutProductItem = new DomElement
        {
            locator = ".cart-item"
        };

        #region constructor
        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }
        #endregion constructor

        public List<CheckoutProductItem> GetProductItems()
        {
            List<CheckoutProductItem> productItems = new List<CheckoutProductItem>();
            DomElement container = BodyContainer.GetElementWaitByCSS(Container.locator);
            DomElement checkoutItemsSection = container.GetElementWaitByCSS(CheckoutItemsSection.locator);
            DomElement itemsGridSection = checkoutItemsSection.GetElementWaitByCSS(ItemsGridSection.locator);
            List<DomElement> items = itemsGridSection.GetElementsWaitByCSS(CheckoutProductItem.locator);
            foreach(var item in items)
            {
                productItems.Add(new CheckoutProductItem(Driver, item));
            }

            return productItems;
        }

        public void WaitUntilItemsAreDisplayed(int time = 30)
        {
            BodyContainer.GetElementWaitUntil(LoadingHeading.locator, el => !(el.Displayed), time);
        }
    }
}
