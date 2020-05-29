using AllPoints.PageObjects.ListPOM.HomePagePOM;
using AllPoints.Pages.Components;
using CommonHelper;
using CommonHelper.Pages.ListPage.ListSummaryPage;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AllPoints.PageObjects.ListPOM.ListSummaryPOM
{
    public class APListSummaryPage : BaseListSummaryPage
    {
        //deprecated
        //private GenericPage browserHelper { get { return Helper; } }
        public Header Header;
        public AccountMenuLeft AccountMenuLeft;
        #region Constructor
        public APListSummaryPage(IWebDriver driver) : base(driver)
        {
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }
        #endregion

        public new APListHomePage ClickOnBreadCrumbLists()
        {
            base.ClickOnBreadCrumbLists();
            return new APListHomePage(Driver);
        }

        public void ClickOnAddToCartByIndex(int index, bool differentInCart = false)
        {
            //TO:Do
            //Stop using Header class in order to move this method to the base class
            base.ScrollToTop();
            List<DomElement> addToCartElements = detailSummary.GetElementsWaitByCSS(this.addToCartButton.locator);
            List<DomElement> nameElements = detailSummary.GetElementsWaitByXpath(this.itemNameLink.locator);
            if (index > addToCartElements.Count - 1 || index < 0) index = addToCartElements.Count - 1;
            // TO:DO
            // replace calling a methods that opens other tab and gets all items in cart
            // in order to not just check on the last added item in cart
            string lastItemNameAddedToCart = Header.GetLastItemAddedToCart();
            if (differentInCart) index = GetNotEqualIndexByName(nameElements, lastItemNameAddedToCart);
            //******
            try
            {
                addToCartElements[index].webElement.Click();
            }
            catch (System.Exception)
            {
                addToCartElements = detailSummary.GetElementsWaitByCSS(this.addToCartButton.locator);
                addToCartElements[index].webElement.Click();
            }
            WaitForAppBusy(8);
        }



    }
}
