using AllPoints.PageObjects.NewFolder1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonHelper;
using AllPoints.Pages;
using System.Threading;
using CommonHelper.Pages.CartPage.Enums;

namespace AllPoints.PageObjects.OderConfirmPOM
{
    public class OrderConfirmationPage : AllPointsBaseWebPage
    {
        public OrderConfirmationPage(IWebDriver driver) : base(driver)
        {
            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
            this.detailNavSection.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        //TODO:
        //ContinueShoppingClick
        private DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = ".detail-section"
        };

        private DomElement detailNavSection = new DomElement(By.CssSelector)
        {
            locator = ".nav-section"
        };

        private DomElement detailContinueShoppingBtn = new DomElement
        {
            locator = ".btn"
        };

        private DomElement detailSuccessAlertMsg = new DomElement
        {
            locator = ".success-alert"
        };

        private DomElement detailStepsSection = new DomElement
        {
            locator = ".steps-section"
        };

        private DomElement detailCustomer = new DomElement
        {
            locator = ".customer"
        };

        private DomElement detailShipping = new DomElement
        {
            locator = ".shipping"
        };

        private DomElement detailBilling = new DomElement
        {
            locator = ".billing"
        };

        private DomElement detailItemslistview = new DomElement
        {
            locator = ".list-view"
        };
        private DomElement continueShoppingButtons = new DomElement(By.XPath)
        {
            locator = "//div[contains(@class,'continue-shopping')]/a"
        };

        public APIndexPage ContinueShoppingClick()
        {
            //Due to have same class for both buttons, we need to create a List and select by name (label)
            List<DomElement> detailActionButtons = detailNavSection.GetElementsWaitByCSS(detailContinueShoppingBtn.locator);
            DomElement detailContinueBtn = detailActionButtons.FirstOrDefault(el => el.webElement.Text == "Continue Shopping");
            Thread.Sleep(5000);
            detailContinueBtn.webElement.Click();
            return new APIndexPage(Driver, true);
        }

        public bool SuccessMsgIsPresent()
        {
            DomElement mainSection = detailSummary.GetElementWaitByCSS(detailSuccessAlertMsg.locator);
            return mainSection.webElement.Enabled;
        }

        public bool CustomerInfoIsDisplayed()
        {
            DomElement mainSection = detailSummary.GetElementWaitByCSS(detailStepsSection.locator);
            DomElement detailCustomerInfo = mainSection.GetElementWaitByCSS(detailCustomer.locator);
            return detailCustomerInfo.webElement.Enabled;
        }

        public bool ShippingInfoIsDisplayed()
        {
            DomElement mainSection = detailSummary.GetElementWaitByCSS(detailStepsSection.locator);
            DomElement detailShip = mainSection.GetElementWaitByCSS(detailShipping.locator);
            return detailShip.webElement.Enabled;
        }

        public bool BillingInfoIsDisplayed()
        {
            DomElement mainSection = detailSummary.GetElementWaitByCSS(detailStepsSection.locator);
            DomElement detailShip = mainSection.GetElementWaitByCSS(detailBilling.locator);
            return detailShip.webElement.Enabled;
        }

        public bool listOfItemsDisplayed()
        {
            DomElement mainSection = detailSummary.GetElementWaitByCSS(detailItemslistview.locator);
            return mainSection.webElement.Enabled;
        }

        public void ClickOnContinueShoppingButton(ContinueShoppingButtons field)
        {
            base.WaitForAppBusy();
            detailSummary.GetElementWaitUntil(this.continueShoppingButtons.locator, (el) => el.Displayed);
            List<DomElement> continueShoppingButtons = detailSummary.GetElementsWaitByXpath(this.continueShoppingButtons.locator);
            switch (field)
            {
                case ContinueShoppingButtons.PrintOrderConfirmation:
                    continueShoppingButtons[0].webElement.Click();
                    break;
                case ContinueShoppingButtons.ContinueShopping:
                    continueShoppingButtons[1].webElement.Click();
                    break;
                default:
                    break;
            }
        }


    }
}
