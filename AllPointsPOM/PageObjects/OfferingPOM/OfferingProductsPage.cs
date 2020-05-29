using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllPoints.PageObjects.MyAccountPOM.DashboardPOM;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AllPoints.Pages;
using System.Threading;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;

namespace AllPoints.PageObjects.OfferingPOM
{
    public class OfferingProductsPage : AllPointsBaseWebPage
    {
        public AddListModal AddListModal;

        private int amountQuantityCounter;

        private GenericPage _helper;

        public OfferingProductsPage(IWebDriver driver) : base(driver)
        {
            _helper = new GenericPage(driver);
            offeringSummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }


        #region Offering Summary
        private DomElement offeringSummary = new DomElement(By.CssSelector)
        {
            locator = ".offering-detail"
        };

        #region Image Section
        private DomElement imageSection = new DomElement
        {
            locator = ".image-section"
        };

        #endregion //image section

        #region Product Section
        private DomElement productSection = new DomElement
        {
            locator = ".product-section"
        };

        private DomElement numericPrice = new DomElement
        {
            locator = "span.price"
        };

        private DomElement detailProductSignInandPriceContainer = new DomElement
        {
            locator = ".price-container"
        };

        private DomElement detailListPriceLabel = new DomElement
        {
            locator = ".price-label.anon"
        };

        private DomElement detailYourPriceLabel = new DomElement
        {
            locator = ".price-label.auth"
        };

        private DomElement savingsmsglabel = new DomElement
        {
            locator = ".alert.alert-success"
        };

        private DomElement alertmsgWarning = new DomElement
        {
            locator = ".alert.alert-warning"
        };

        private DomElement detailaddbutton = new DomElement
        {
            locator = ".icon-cart-item-add"
        };

        private DomElement signinOffering = new DomElement
        {
            locator = ".reminder-link"
        };

        private DomElement detailPriceLogInReminder = new DomElement
        {
            locator = ".price-login-reminder"
        };

        private DomElement detailRemainderLinks = new DomElement
        {
            locator = ".reminder-links"
        };

        private DomElement detailRemainderLinkSignInLink = new DomElement
        {
            locator = ".reminder-link"
        };

        private DomElement detailActions = new DomElement
        {
            locator = ".actions"
        };

        private DomElement detailQuantityAndAddToCartbutton = new DomElement
        {
            locator = ".add-to-cart"
        };

        private DomElement detailFormControlQuantity = new DomElement
        {
            locator = ".quantity"
        };

        private DomElement detailButtonAddToCartBUtton = new DomElement
        {
            locator = ".js-add-to-cart"
        };

        private DomElement detailshippingavailable = new DomElement
        {
            locator = ".shipping-available"
        };

        private DomElement detailAvailability = new DomElement
        {
            locator = ".availability-container"
        };

        private DomElement detailDescription = new DomElement
        {
            locator = ".description"
        };

        private DomElement detailSpecifications = new DomElement
        {
            locator = ".list-unstyled"
        };

        private DomElement amountQuantity = new DomElement
        {
            locator = ".amount.quantity .value"
        };

        private DomElement cartButton = new DomElement
        {
            locator = "//button[@class='btn btn-bare btn-icon-left minicart mini-cart-link']"
        };

        private DomElement addToListButton = new DomElement(By.CssSelector)
        {
            locator = "button.add-to-list"
        };

        private DomElement listNameSelect = new DomElement(By.CssSelector)
        {
            locator = "select.js-add-to-list-select"
        };

        private DomElement quantityInput = new DomElement(By.CssSelector)
        {
            locator = "#quantity"
        };

        #endregion //Product Section

        #region Replaces Section


        #endregion //Replaces Section

        #endregion //Offering Summary

        public bool ListPriceLabelforANON()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement listpriceLabeltextforANON = offeringProductSection.GetElementWaitByCSS(detailListPriceLabel.locator);
            return listpriceLabeltextforANON.webElement.Enabled;
        }

        public bool YourPriceLabelforAUTH()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement yourpriceLabeltextforAUTH = offeringProductSection.GetElementWaitByCSS(detailYourPriceLabel.locator);
            return yourpriceLabeltextforAUTH.webElement.Enabled;
        }

        public bool SavingsMsgDisplayed()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement savingmessagedisplayed = offeringProductSection.GetElementWaitByCSS(savingsmsglabel.locator);
            return savingmessagedisplayed.webElement.Enabled;
        }

        public void AddtoCartInOffering()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement buttonaddtocart = offeringProductSection.GetElementWaitByCSS(detailaddbutton.locator);
            buttonaddtocart.webElement.Click();
        }

        public bool ShippingInfoSectionAvailable()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement sectionShippingAvailable = offeringProductSection.GetElementWaitByCSS(detailshippingavailable.locator);
            return sectionShippingAvailable.webElement.Enabled;
        }

        public bool AvailabilityInfo()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement sectionShippingAvailable = offeringProductSection.GetElementWaitByCSS(detailshippingavailable.locator);
            DomElement availabilityInformation = sectionShippingAvailable.GetElementWaitByCSS(detailAvailability.locator);
            return availabilityInformation.webElement.Enabled;
        }

        public bool DescriptionAvailableSection()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement sectionShippingAvailable = offeringProductSection.GetElementWaitByCSS(detailDescription.locator);
            return sectionShippingAvailable.webElement.Enabled;
        }

        public bool SpecificationsSection()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement sectionShippingAvailable = offeringProductSection.GetElementWaitByCSS(detailSpecifications.locator);
            return sectionShippingAvailable.webElement.Enabled;
        }

        public APLoginPage ClickOnSignInOffering()
        {
            DomElement offeringProductSection = offeringSummary.GetElementWaitByCSS(productSection.locator);
            DomElement headsupmessagedisplayed = offeringProductSection.GetElementWaitByCSS(alertmsgWarning.locator);
            DomElement clicksingin = headsupmessagedisplayed.GetElementWaitByCSS(signinOffering.locator);
            clicksingin.webElement.Click();
            return new APLoginPage(Driver);
        }

        public bool NumericPriceDisplayed()
        {
            DomElement numericPriceElement = offeringSummary.GetElementWaitByCSS(numericPrice.locator);
            return numericPriceElement.webElement.Enabled;
        }

        public void UpdateAmountQuantity()
        {
            Thread.Sleep(3000);
            DomElement amountQuantityElement = offeringSummary.GetElementWaitXpath(
                "//article[@class='mini-cart']/section/div[@class='amount quantity']/span"
                );
            this.amountQuantityCounter = int.Parse(amountQuantityElement.webElement.GetAttribute("innerHTML"));
        }

        public int GetAmountQuantity()
        {
            return this.amountQuantityCounter;
        }

        public void AddToCartFirstProduct()
        {
            DomElement detailButtonAddToCartBUttonElement = offeringSummary.GetElementWaitByCSS(detailButtonAddToCartBUtton.locator);

            try
            {
                detailButtonAddToCartBUttonElement.webElement.Click();
            }
            catch (Exception)
            {
                //If the element could not be found, you have to relocate it again
                detailButtonAddToCartBUttonElement = offeringSummary.GetElementWaitByCSS(detailButtonAddToCartBUtton.locator);
                detailButtonAddToCartBUttonElement.webElement.Click();
            }
            base.WaitForAppBusy(10);
        }

        public bool AumontQuantityIncremented(int beforeAddProduct, int afterAddProduct)
        {
            return afterAddProduct > beforeAddProduct;
        }
        public APCartPage ClickOnCart()
        {
            DomElement cartButtonElement = offeringSummary.GetElementWaitXpath(cartButton.locator);
            try
            {
                cartButtonElement.webElement.Click();
            }
            catch (Exception)
            {

                cartButtonElement = offeringSummary.GetElementWaitXpath(cartButton.locator);
                cartButton.webElement.Click();
            }
            return new APCartPage(base.Driver);
        }

        public void ClickOnAddToList()
        {
            DomElement addToListButton = offeringSummary.GetElementWaitByCSS(this.addToListButton.locator);
            try
            {
                Thread.Sleep(5000);
                addToListButton.webElement.Click();
            }
            catch (Exception)
            {
                addToListButton = offeringSummary.GetElementWaitByCSS(this.addToListButton.locator);
                addToListButton.webElement.Click();
            }
            AddListModal = new AddListModal(base.Driver);
            offeringSummary.GetElementWaitUntilByXpath(AddListModal.modal.locator,
                (el) => el.Displayed, 15);
        }

        public void ChangeQuantity(int quantity)
        {
            DomElement quantityInput = offeringSummary.GetElementWaitByCSS(this.quantityInput.locator);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)base.Driver;
            jse.ExecuteScript("arguments[0].focus();arguments[0].value='" + quantity.ToString() + "';", quantityInput.webElement);
        }


    }
}
