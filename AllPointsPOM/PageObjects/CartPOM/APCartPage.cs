using AllPoints.PageObjects.ListPOM.HomePagePOM;
using AllPoints.Pages.Components;
using CommonHelper;
using CommonHelper.Pages.CartPage;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AllPoints.PageObjects.CartPOM
{
    public class APCartPage : BaseCartPage
    {
        public Header Header;

        public AccountMenuLeft AccountMenuLeft;

        public APCartPage(IWebDriver driver) : base(driver)
        {
            //wait until loading text is gone
            WaitUntilLoadingDone();
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
            this.rightNavMenu.Init(driver, SeleniumConstants.defaultWaitTime);
            this.rightNavigationSection.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        public APCheckoutPage CheckoutAsGuest()
        {
            DomElement checkoutAsGuestButtonElement = detailSummary.GetElementWaitXpath(checkoutAsGuestButton.locator);
            checkoutAsGuestButtonElement.webElement.Click();
            return new APCheckoutPage(base.Driver);
        }

        public new APCartPage MovetoSFL()
        {
            base.MovetoSFL();
            return new APCartPage(Driver);
        }

        public new APCartPage SeeSFLItems()
        {
            base.SeeSFLItems();
            return new APCartPage(Driver);
        }

        public new APCheckoutPage ProceedToCheckOut()
        {
            base.ProceedToCheckOut();
            if (Driver.Url.Contains("SignIn"))
            {
                return null;
            }
            return new APCheckoutPage(Driver);
        }

        public new APListHomePage VisitListButton()
        {
            base.VisitListButton();
            return new APListHomePage(Driver);
        }
        
        public virtual void RemoveIndividualItemByIndex(int index)
        {
            // Remove an item from cart by index
            base.ScrollToTop();
            List<DomElement> listItems = detailSummary.GetElementsWaitByCSS(this.removeIndividualItemButton.locator);
            //Validate the index exists.
            if (index > listItems.Count - 1 || index < 0) index = listItems.Count - 1;
            int itemsInCartQuantityBeforeRemove = Header.GetMiniCartQuantity();
            int newItemsInCartQuantity = 0;
            do
            {
                try
                {
                    listItems[index].webElement.Click();
                }
                catch (Exception)
                {
                    listItems = detailSummary.GetElementsWaitByCSS(this.removeIndividualItemButton.locator);
                    listItems[index].webElement.Click();
                }
                newItemsInCartQuantity = Header.GetMiniCartQuantity();
                base.WaitForAppBusy();
            } while (itemsInCartQuantityBeforeRemove <= newItemsInCartQuantity);

        }

        public string GetShippingServiceLevelsTDB()
        {
            DomElement detailrightnavMenu = rightNavMenu.GetElementWaitByCSS(rightNavShippingSection.locator);
            DomElement detailShippingSection = detailrightnavMenu.GetElementWaitByCSS(rightNavShippingRates.locator);
            DomElement detailShippingRates = detailShippingSection.GetElementWaitByCSS(rightNavShippingRate.locator);
            return detailShippingRates.webElement.Text;
        }
        // method that returns if all the shipping options are priced or not
        public bool ShippingOptionsPriced()
        {
            DomElement detailrightnavMenu = rightNavMenu.GetElementWaitByCSS(rightNavShippingSection.locator);
            DomElement detailShippingSection = detailrightnavMenu.GetElementWaitByCSS(rightNavShippingRates.locator);
            List<DomElement> detailShippingRatesPrice = detailShippingSection.GetElementsWaitByCSS(rightNavShippingRate.locator);

            foreach (DomElement item in detailShippingRatesPrice)
            {
                double value = -1;
                double.TryParse(item.webElement.Text.Replace("$", ""), out value);
                if (value == -1)
                {
                    return false;
                }
            }
            return true;
        }

        // option to select shipping method based off the name
        public void SelectShipping(int index)
        {

        }

        public string SelectShipping(string shipMethod)
        {
            DomElement detailrightnavMenu = rightNavMenu.GetElementWaitByCSS(rightNavShippingSection.locator);
            DomElement detailShippingSection = detailrightnavMenu.GetElementWaitByCSS(rightNavShippingRates.locator);
            List<DomElement> detailShippingRates = detailShippingSection.GetElementsWaitByCSS("div.line");

            foreach (DomElement item in detailShippingRates)
            {

                if (item.GetElementWaitByCSS("input + span").webElement.Text.Equals(shipMethod))
                {
                    // TEMPORARY fix for clicks to work on shipping inputs
                    IJavaScriptExecutor jse2 = (IJavaScriptExecutor)Driver;
                    jse2.ExecuteScript("arguments[0].click();", item.GetElementWaitByCSS("input").webElement);

                    //item.GetElementWaitByCSS("input").webElement.Click();

                    return item.GetElementWaitByCSS("div.value span").webElement.Text;
                }
            }

            return "";
        }

        public string GetShippingTotal()
        {
            DomElement orderSummaryContainer = rightNavMenu.GetElementWaitByCSS(orderSummarytotals.locator);
            DomElement shippingTotal = orderSummaryContainer.GetElementWaitByCSS(shippingTotalValue.locator);

            return shippingTotal.webElement.Text;
        }


    }
}