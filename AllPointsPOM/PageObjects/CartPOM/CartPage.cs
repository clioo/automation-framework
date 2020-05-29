using AllPoints.PageObjects.ListPOM.HomePagePOM;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AllPoints.PageObjects.CartPOM
{
    public class CartPage : AllPointsBaseWebPage
    {
        public CartPage(IWebDriver driver) : base(driver)
        {
            //wait until loading text is gone
            WaitUntilLoadingDone();

            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
            this.rightNavMenu.Init(driver, SeleniumConstants.defaultWaitTime);
            this.rightNavigationSection.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        #region Cart Detail Card
        private DomElement removeLink = new DomElement
        {
            locator = ".action-remove"
        };

        private DomElement saveforlaterLink = new DomElement
        {
            locator = ".action-save"
        };

        private DomElement navigationtabs = new DomElement
        {
            locator = ".nav-tabs"
        };

        private DomElement saveforlaterSection = new DomElement
        {
            locator = ".icon.icon-save"
        };

        private DomElement infoHeadUptitle = new DomElement
        {
            locator = ".title"
        };

        private DomElement ContentHeadsUp = new DomElement
        {
            locator = ".content"
        };

        private DomElement detailFooter = new DomElement
        {
            locator = ".footer"
        };

        private DomElement contentSaveHeadUpInfo = new DomElement
        {
            locator = ".content"
        };

        private DomElement objlistLink = new DomElement
        {
            locator = ".action-list"
        };

        private DomElement cartItemsSec = new DomElement
        {
            locator = ".cart-items-section"
        };

        private DomElement items = new DomElement
        {
            locator = ".item-content"
        };

        private DomElement totalItemPrices = new DomElement(By.XPath)
        {
            locator = "//div[@class='line total-price-section']/div/span[@class='price ng-binding']"
        };

        private DomElement itemsTotals = new DomElement(By.XPath)
        {
            locator = "//div[@class='line-item-total line']/div[@class='value ng-binding']"
        };

        #endregion Card Detal Card

        public void RemoveLinkClick()
        {
            DomElement waitforElement = detailSummary.GetElementWaitUntil("h2", (el) => !el.Displayed);
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement removeLinkdetail = cartItemsSection.GetElementWaitByCSS(removeLink.locator);
            removeLinkdetail.webElement.Click();
        }
        
        public CartPage MovetoSFL()
        {
            DomElement waitforElement = detailSummary.GetElementWaitUntil("h2", (el) => !el.Displayed);
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement saveLink = cartItemsSection.GetElementWaitByCSS(saveforlaterLink.locator);
            saveLink.webElement.Click();
            return new CartPage(Driver);
        }

        public CartPage SeeSFLItems()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(navigationtabs.locator);
            DomElement saveforlatertab = cartItemsSection.GetElementWaitByCSS(saveforlaterSection.locator);
            saveforlatertab.webElement.Click();
            return new CartPage(Driver);
        }

        public void ClickAddtoListLink()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement listLink = cartItemsSection.GetElementWaitByCSS(objlistLink.locator);
            listLink.webElement.Click();
            Thread.Sleep(2000);
            this.detailModal.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        public void ClickAddtoListButton()
        {

            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailContent = modalSection.GetElementWaitByCSS(modalFooter.locator);
            DomElement detailmodalButton = detailmodailContent.GetElementWaitByCSS(addToListbutton.locator);
            detailmodalButton.webElement.Click();
            
        }

        public bool SuccessMessageAdded()
        {
            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailContent = modalSection.GetElementWaitByCSS(modalBody.locator);
            DomElement detailAlertSuccess = detailmodailContent.GetElementWaitByCSS(successLabel.locator);
            return detailAlertSuccess.webElement.Enabled;
        }

        public ListHomePage VisitListButton()
        {
            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailContent = modalSection.GetElementWaitByCSS(modalBody.locator);
            DomElement detailvisitlistButton = detailmodailContent.GetElementWaitByCSS(visitlistButton.locator);
            detailvisitlistButton.webElement.Click();
            return new ListHomePage(Driver);
        }

        public void ClickCloseModalLink()
        {
            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailBody = modalSection.GetElementWaitByCSS(modalBody.locator);
            DomElement detailmodalClose = detailmodailBody.GetElementWaitByCSS(closeModal.locator);
            detailmodalClose.webElement.Click();
        }

        public bool totalsInformationDislayed()
        {
            DomElement detailNavRight = rightNavigationSection.GetElementWaitByCSS(rightNavProceedToCheckOutButton.locator);
            DomElement detailCheckoutSummary = detailNavRight.GetElementWaitByCSS(checkoutSummary.locator);
            DomElement detailOrderSummary = detailCheckoutSummary.GetElementWaitByCSS(orderSummarytotals.locator);
            return detailOrderSummary.webElement.Enabled;
        }

        public bool HeadsUpCartInfo()
        {
            DomElement infoHeadupInformationtitle = detailSummary.GetElementWaitByCSS(infoHeadUptitle.locator);
            return infoHeadupInformationtitle.webElement.Enabled;
        }

        public bool ContentHeadsUpInfo()
        {
            DomElement headsupInfotitle = detailSummary.GetElementWaitByCSS(cartItemsSec.locator);
            DomElement footerSec = headsupInfotitle.GetElementWaitByCSS(detailFooter.locator);
            DomElement headsupContentInformation = footerSec.GetElementWaitByCSS(ContentHeadsUp.locator);
            return headsupContentInformation.webElement.Enabled;
        }

        public bool MovetoCartLinkEnable()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement saveLink = cartItemsSection.GetElementWaitByCSS(saveforlaterLink.locator);
            return saveLink.webElement.Enabled;
        }

        public bool FindCongraMessage()
        {
            DomElement detailNavRight = rightNavigationSection.GetElementWaitByCSS(rightNavProceedToCheckOutButton.locator);
            DomElement detailCheckoutSummary = detailNavRight.GetElementWaitByCSS(checkoutSummary.locator);
            DomElement detailContent = detailCheckoutSummary.GetElementWaitByCSS(contentDetail.locator);
            DomElement detailSuccess = detailContent.GetElementWaitByCSS(successMessageDetail.locator);
            return detailSuccess.webElement.Enabled;
        }

        public bool proximtyMessageDisplayed()
        {
            DomElement detailNavRight = rightNavigationSection.GetElementWaitByCSS(rightNavProceedToCheckOutButton.locator);
            DomElement detailCheckoutSummary = detailNavRight.GetElementWaitByCSS(checkoutSummary.locator);
            DomElement detailInfoProximity = detailCheckoutSummary.GetElementWaitByCSS(proxText.locator);
            return detailInfoProximity.webElement.Enabled;
        }

        public string GetShippingServiceLevelsTDB()
        {
            DomElement detailrightnavMenu = rightNavMenu.GetElementWaitByCSS(rightNavShippingSection.locator);
            DomElement detailShippingSection = detailrightnavMenu.GetElementWaitByCSS(rightNavShippingRates.locator);
            DomElement detailShippingRates = detailShippingSection.GetElementWaitByCSS(rightNavShippingRate.locator);
            return detailShippingRates.webElement.Text;
        }            

        #region Details Summary

        private DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = ".cart .detail-section"
        };

        DomElement DetailSectionLoadingSubtitle = new DomElement
        {
            locator = ".detail-section h2"
        };

        private DomElement detailModal = new DomElement(By.CssSelector)
        {
            locator = ".add-to-list-modal"
        };

        private List<DomElement> GetSelectedItems()
        {
            return detailSummary.GetElementsWaitByCSS(selectedSpan.locator);
        }

        private DomElement modalContent = new DomElement
        {
            locator = ".modal-content"
        };

        private DomElement modalFooter = new DomElement
        {
            locator = ".modal-footer"
        };

        private DomElement modalBody = new DomElement
        {
            locator = ".modal-body"
        };

        private DomElement successLabel = new DomElement
        {
            locator = ".alert-success"
        };

        private DomElement addToListbutton = new DomElement
        {
            locator = ".btn-primary"
        };

        private DomElement visitlistButton = new DomElement
        {
            locator = ".btn-primary"
        };

        private DomElement closeModal = new DomElement
        {
            locator = ".btn-link"
        };

        private DomElement loadingIndicator = new DomElement
        {
            locator = "h1"
        };

        private DomElement headsupSection = new DomElement
        {
            locator = ".cart-items-section"
        };

        private DomElement detailCartItems = new DomElement
        {
            locator = ".cart-items"
        };

        private DomElement detailCartItem = new DomElement
        {
            locator = ".cart-item"
        };

        private DomElement detailCartItemContent = new DomElement
        {
            locator = ".item-content"
        };

        private DomElement detailImageSection = new DomElement
        {
            locator = ".image-section"
        };

        private DomElement detailCartItemContentProduct = new DomElement
        {
            locator = ".product-section"
        };

        private DomElement detailProducTitleSection = new DomElement
        {
            locator = ".title-section"
        };

        private DomElement detailCartItemContentProductSKU = new DomElement
        {
            locator = ".sku-section .sku"
        };

        private DomElement detailCartItemContentLineItem = new DomElement
        {
            locator = ".line-item-section"
        };

        private DomElement detailCartItemContentLineItemQty = new DomElement
        {
            locator = ".quantity-section"
        };

        private DomElement detailCartItemContentLineItemQtyInput = new DomElement
        {
            locator = ".quantity input"
        };

        private DomElement detailCartItemAvailability = new DomElement
        {
            locator = ".sku-availability-section"
        };

        private DomElement detailCartItemAvailabilityTag = new DomElement
        {
            locator = ".availability-content"
        };

        private DomElement detailActionContent = new DomElement
        {
            locator = ".action-content"
        };

        private DomElement detailListLink = new DomElement
        {
            locator = ".action-list"
        };

        private DomElement checkoutAsGuestButton = new DomElement
        {
            locator = "//a[contains(text(),'Checkout')]"
        };

        private DomElement quantityInput = new DomElement(By.XPath)
        {
            locator = "//input[@ng-model='item.Quantity']"
        };

        private DomElement selectAllButton = new DomElement(By.CssSelector)
        {
            locator = "input[ng-model='$ctrl.isAllSelected']"
        };

        private DomElement selectedSpan = new DomElement(By.CssSelector)
        {
            locator = "span.selected"
        };

        private DomElement inputSelectItem = new DomElement(By.CssSelector)
        {
            locator = "input[ng-model='item.Selected']"
        };

        private DomElement moveSelToSaveLaterButton = new DomElement(By.XPath)
        {
            locator = "//button[contains(text(),'Move selected to Save for later')]"
        };

        private DomElement savedForLaterTab = new DomElement(By.XPath)
        {
            locator = "//a[contains(text(),'Saved for later')]"
        };

        private DomElement moveSelectedToCart = new DomElement(By.CssSelector)
        {
            locator = "div.multiselect-actions.list button.action-add-selected-to-cart"
        };

        private DomElement individualAddToCartButton = new DomElement(By.XPath)
        {
            locator = "//button[@ng-click='onClickMoveToCart(item)']"
        };

        private DomElement cartTab = new DomElement(By.CssSelector)
        {
            locator = "a[ng-click=\"onTabClick('cart.cartItemsView')\"]"
        };

        private DomElement availabilityDiv = new DomElement(By.CssSelector)
        {
            locator = "div.product-availability-section div.line.sku-availability-section.ng-scope.ng-isolate-scope"
        };

        private DomElement dropDownActionsContainer = new DomElement(By.CssSelector)
        {
            locator = "div.multiselect-actions.dropdown.btn-group"
        };

        private DomElement dropdownActionsButton = new DomElement(By.CssSelector)
        {
            locator = "button.dropdown-toggle"
        };

        private DomElement dropDwonActionsItem = new DomElement(By.CssSelector)
        {

        };

        private DomElement removeSelectedFromCartButton = new DomElement(By.CssSelector)
        {
            locator = "div.multiselect-actions.list button.action-remove"
        };

        private DomElement removeIndividualItemButton = new DomElement(By.CssSelector)
        {
            locator = "div.actions button.action-remove"
        };

        private DomElement inCartItemsContainer = new DomElement(By.CssSelector)
        {
            locator = "div.section.cart-items-section"
        };

        private DomElement checkboxInput = new DomElement(By.CssSelector)
        {
            locator = "div.grid-item div.checkbox input[type='checkbox']"
        };
        #endregion Details Summary

        #region Right Navigation Section
        private DomElement rightNavMenu = new DomElement(By.CssSelector)
        {
            locator = ".cart .nav-section"
        };

        private DomElement rightNavigationSection = new DomElement(By.CssSelector)
        {
            locator = ".nav-right"
        };

        private DomElement rightNavProceedToCheckOutButton = new DomElement
        {
            locator = ".nav-section"
        };

        private DomElement checkoutSummary = new DomElement
        {
            locator = ".checkout-summary"
        };

        private DomElement orderSummarytotals = new DomElement
        {
            locator = ".order-summary"
        };

        private DomElement contentDetail = new DomElement
        {
            locator = ".content"
        };

        private DomElement proxText = new DomElement()
        {
            locator = ".info"
        };

        private DomElement successMessageDetail = new DomElement
        {
            locator = ".success"
        };

        private DomElement proceedtoCheckoutbtn = new DomElement
        {
            locator = ".btn"
        };

        #endregion Right Naigation Section

        #region Shipping Options

        private DomElement rightNavShippingSection = new DomElement
        {
            locator = ".shipment-options"
        };

        private DomElement rightNavShippingRates = new DomElement
        {
            locator = ".content .shipment-rates"
        };

        private DomElement rightNavShippingRate = new DomElement
        {
            locator = ".value"
        };

        private DomElement rightNavShippingRateInput = new DomElement
        {
            locator = "input"
        };

        #endregion Shipping Options

        #region Additional Shipping Options

        private DomElement rightNavAdditionalShippingSection = new DomElement
        {
            locator = "shipment-blind-shipping"
        };

        private DomElement rightNavAdditionalShippingBlind = new DomElement
        {
            locator = ".content .select"
        };

        private DomElement rightNavAdditionalShippingBlindCheckBox = new DomElement
        {
            locator = ".checkbox-container input"
        };

        #endregion Additional Shipping Options

        //Component Base Code Functions
        public int GetNumberOfSelectedItems()
        {
            return GetSelectedItems().Count;
        }

        public bool IsLoading(int time = 30)
        {
            var findsLoading = By.CssSelector(detailSummary.locator + " " + loadingIndicator.locator);
            var modalBusy = Helper.FindByCondition(findsLoading, (el) => !el.Displayed, time);
            if (modalBusy == null)
            {
                return true;
            }
            return false;
        }

        public string AllComponentsExist()
        {
            StringBuilder sb = new StringBuilder();
            if (detailSummary.webElement == null)
                sb.Append(nameof(detailSummary));
            if (rightNavMenu.webElement == null)
                sb.Append(nameof(rightNavMenu));
            return sb.ToString();
        }

        public IDictionary<string, string> AvailabiltyTagGet()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            List<DomElement> cartItems = cartItemsSection.GetElementsWaitByCSS(detailCartItem.locator);
            foreach (var item in cartItems)
            {
                //Get Cart Item Sku
                DomElement itemContent = item.GetElementWaitByCSS(detailCartItemContent.locator);
                DomElement itemProduct = itemContent.GetElementWaitByCSS(detailCartItemContentProduct.locator);
                DomElement itemSku = itemProduct.GetElementWaitByCSS(detailCartItemContentProductSKU.locator);

                //Get Cart Item Availabilty Tag
                DomElement itemAvailabilty = item.GetElementWaitByCSS(detailCartItemAvailability.locator);
                DomElement itemAvailabilyTag = itemAvailabilty.GetElementWaitByCSS(detailCartItemAvailabilityTag.locator);
                dict.Add(itemSku.webElement.Text, itemAvailabilyTag.webElement.Text);
            }

            return dict;
        }

        public CheckoutPage ProceedToCheckOut()
        {
            
            DomElement proceedToCheckOutButton = rightNavMenu.GetElementWaitUntil(proceedtoCheckoutbtn.locator,
                                                    (el)=>el.Displayed);
            //DomElement proceedToCheckOutButton = navRighePanelinCart.GetElementWaitByCSS(proceedtoCheckoutbtn.locator);
            proceedToCheckOutButton.webElement.Click();
            if (Driver.Url.Contains("SignIn"))
            {
                return null;
            }
            return new CheckoutPage(Driver);
        }

        public bool ImageSectionInCart()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement imagesection = itemcontent.GetElementWaitByCSS(detailImageSection.locator);
            return imagesection.webElement.Enabled;
        }

        public bool TitleSectionIsDisplayed()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement productsection = itemcontent.GetElementWaitByCSS(detailCartItemContentProduct.locator);
            DomElement titleSection = productsection.GetElementWaitByCSS(detailProducTitleSection.locator);
            return titleSection.webElement.Enabled;
        }

        public bool SKUSectionIsDisplayed()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement productsection = itemcontent.GetElementWaitByCSS(detailCartItemContentProduct.locator);
            DomElement skuSection = productsection.GetElementWaitByCSS(detailCartItemContentProductSKU.locator);
            return skuSection.webElement.Enabled;
        }

        public bool LineItemSectionIsDisplayed()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement lineitemsection = itemcontent.GetElementWaitByCSS(detailCartItemContentLineItem.locator);
            return lineitemsection.webElement.Enabled;
        }

        public bool AddToListLink()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement actionContent = cartitem.GetElementWaitByCSS(detailActionContent.locator);
            DomElement listLink = actionContent.GetElementWaitByCSS(detailListLink.locator);
            return listLink.webElement.Enabled;
        }

        private void WaitUntilLoadingDone()
        {
            BodyContainer.GetElementWaitUntil(DetailSectionLoadingSubtitle.locator, el => 
            {
                return !el.Displayed;
            });
        }

        public int NumberOfItemsInCart()
        {
            List<DomElement> itemsInCart = detailSummary.GetElementsWaitByCSS(detailCartItem.locator);
            return itemsInCart.Count;
        }

        public int GetQuantityInput()
        {
            DomElement quantityInputElement = detailSummary.GetElementWaitByCSS(detailCartItemContentLineItemQtyInput.locator);
            return int.Parse(quantityInputElement.webElement.GetAttribute("value"));
        }

        public CheckoutPage CheckoutAsGuest()
        {
            DomElement checkoutAsGuestButtonElement = detailSummary.GetElementWaitXpath(checkoutAsGuestButton.locator);
            checkoutAsGuestButtonElement.webElement.Click();
            return new CheckoutPage(base.Driver);
        }

        private List<DomElement> GetItemsInCart()
        {
            var itemsel = detailSummary.GetElementsWaitByCSS(items.locator);
            return detailSummary.GetElementsWaitByCSS(items.locator);
        }
        public int GetNumberOfItemsInCart()
        {
            return GetItemsInCart().Count;
        }

        public bool TotalAmountIsCorrect()
        {
            //The Total amount reflects the sum of the items total
            List<DomElement> totalItemPricesElements = detailSummary.GetElementsWaitByXpath(totalItemPrices.locator);
            double sum = 0;
            foreach (DomElement item in totalItemPricesElements)
            {
                double value = 0;
                double.TryParse(item.webElement.Text, out value);
                sum += value;
            }
            DomElement itemTotalsElement = detailSummary.GetElementWaitXpath(itemsTotals.locator);
            double itemsTotalsAmount = double.Parse(itemTotalsElement.webElement.Text.Remove(0, 1));
            sum = Math.Round(sum, 2);
            return sum == itemsTotalsAmount;
        }

        public void SelectAllItems()
        {
            DomElement selectAllButtonElement = detailSummary.GetElementWaitByCSS(selectAllButton.locator);
            selectAllButtonElement.webElement.Click();
            detailSummary.GetElementWaitUntil(selectedSpan.locator,(el)=> el.Displayed);
        }

        public void MoveSelectedToSaveLater()
        {
            DomElement moveSelToSaveLaterElement = detailSummary.GetElementWaitXpath(moveSelToSaveLaterButton.locator);
            try
            {
                moveSelToSaveLaterElement.webElement.Click();
            }
            catch (Exception)
            {
                moveSelToSaveLaterElement = detailSummary.GetElementWaitXpath(moveSelToSaveLaterButton.locator);
                moveSelToSaveLaterElement.webElement.Click();
            }
            base.WaitForAppBusy(8);
            detailSummary.GetElementWaitUntil(inCartItemsContainer.locator, (el) => el.Displayed);
        }

        public void ClickOnSavedForLater()
        {
            DomElement savedForLaterTabElement = detailSummary.GetElementWaitXpath(savedForLaterTab.locator);
            savedForLaterTabElement.webElement.Click();
            detailSummary.GetElementWaitUntil(moveSelectedToCart.locator, (element) => element.Displayed);
        }

        public void SelectItemsByIndex(int index)
        {
            base.ScrollToTop();
            int selectedItemsBeforeSelect = GetSelectedItems().Count;
            int selectedItemsAfterSelect = 0;
            List<DomElement> checkboxInputs = detailSummary.GetElementsWaitByCSS(this.checkboxInput.locator);
            if (index > checkboxInputs.Count - 1 || index < 0) index = checkboxInputs.Count - 1;
            do
            {
                try
                {
                    checkboxInputs[index].webElement.Click();
                }
                catch (System.Exception)
                {
                    checkboxInputs = detailSummary.GetElementsWaitByCSS(this.checkboxInput.locator);
                    checkboxInputs[index].webElement.Click();
                }
                Thread.Sleep(1000);
                selectedItemsAfterSelect = GetSelectedItems().Count;
            } while (selectedItemsAfterSelect <= selectedItemsBeforeSelect);

        }

        public void SelectItemsByNumber(int number)
        {
            //Deprecated
            //Selects an amount of elements.
            List<DomElement> itemsSavedForLaterElements = detailSummary.GetElementsWaitByCSS(inputSelectItem.locator);
            int topIndex = itemsSavedForLaterElements.Count - 1;
            //validate this method is used correctly
            if (number > topIndex || (number - 1) < 0) number = topIndex;
            //********
            for (int i = 0; i < number; i++)
            {
                //TODO: replace for explicit wait
                if (base.IsAppBusy())
                {
                    base.WaitForAppBusy(8);
                }
                DomElement element = itemsSavedForLaterElements[i];
                try
                {
                    element.webElement.Click();
                }
                catch (Exception)
                {
                    //TODO: replace for explicit wait
                    Thread.Sleep(2500);
                    itemsSavedForLaterElements = detailSummary.GetElementsWaitByCSS(inputSelectItem.locator);
                    element = itemsSavedForLaterElements[i];
                    element.webElement.Click();
                }
            }
        }

        public void MoveSelectedToCart()
        {
            base.ScrollToTop();
            try
            {
                DomElement moveSelectedToCartElement = detailSummary.GetElementWaitByCSS(moveSelectedToCart.locator);
                moveSelectedToCartElement.webElement.Click();
            }
            catch (Exception)
            {
                DomElement dropdownActionsContainer = detailSummary.GetElementWaitByCSS(this.dropDownActionsContainer.locator);
                dropdownActionsContainer.GetElementWaitByCSS(this.dropdownActionsButton.locator).webElement.Click();
                List<DomElement> items = dropdownActionsContainer.GetElementsWaitByCSS(this.dropDwonActionsItem.locator);
                items[2].webElement.Click();
            }

            base.WaitForAppBusy(8);
        }

        public void ClickIndividualMoveToCartByIndex(int index)
        {
            //Move to cart item by index in dom page
            base.ScrollToTop();
            List<DomElement> listIndividualAddToCartButton = detailSummary.GetElementsWaitByXpath(this.individualAddToCartButton.locator);
            //Validate the index exists.
            if (index > listIndividualAddToCartButton.Count - 1 || index < 0) index = listIndividualAddToCartButton.Count - 1;
            try
            {
                listIndividualAddToCartButton[index].webElement.Click();
            }
            catch (Exception)
            {
                Thread.Sleep(2500);
                listIndividualAddToCartButton = detailSummary.GetElementsWaitByXpath(this.individualAddToCartButton.locator);
                listIndividualAddToCartButton[index].webElement.Click();
            }
        }

        public void ClickOnCartTab()
        {
            base.WaitForAppBusy();
            base.ScrollToTop();
            DomElement cartTab = detailSummary.GetElementWaitByCSS(this.cartTab.locator);
            cartTab.webElement.Click();
        }

        public bool InventoryAvailabilityIsDisplayed()
        {
            List<DomElement> availabilityDivs = detailSummary.GetElementsWaitByCSS(this.availabilityDiv.locator);
            foreach (DomElement element in availabilityDivs)
            {
                if (element.webElement == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void ClickRemoveSelectedItems()
        {
            base.ScrollToTop();
            try
            {
                DomElement removeSelectedFromCartButton = detailSummary.GetElementWaitByCSS(this.removeSelectedFromCartButton.locator);
                removeSelectedFromCartButton.webElement.Click();
            }
            catch (Exception)
            {
                Thread.Sleep(2500);
                DomElement dropdownActionsContainer = detailSummary.GetElementWaitByCSS(this.dropDownActionsContainer.locator);
                dropdownActionsContainer.GetElementWaitByCSS(this.dropdownActionsButton.locator).webElement.Click();
                List<DomElement> items = dropdownActionsContainer.GetElementsWaitByCSS(this.dropDwonActionsItem.locator);
                items[2].webElement.Click();
            }

            base.WaitForAppBusy(8);
        }

        public void RemoveIndividualItemByIndex(int index)
        {
            // Remove an item from cart by index
            base.ScrollToTop();
            List<DomElement> listItems = detailSummary.GetElementsWaitByCSS(this.removeIndividualItemButton.locator);
            //Validate the index exists.
            if (index > listItems.Count - 1 || index < 0) index = listItems.Count - 1;
            int itemsInCartQuantityBeforeRemove = Header.GetCartQuantity();
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
                newItemsInCartQuantity = Header.GetCartQuantity();
                base.WaitForAppBusy();
            } while (itemsInCartQuantityBeforeRemove <= newItemsInCartQuantity);
            
        }

        public bool IsQuantityInCartItem(int quantity)
        {
            List<DomElement> quantityInputElements = detailSummary.GetElementsWaitByXpath(quantityInput.locator);
            for (int i = 1; i < quantityInputElements.Count + 1; i++)
            {
                DomElement inputElement = detailSummary.GetElementWaitXpath("(" + quantityInput.locator + ")[" + i.ToString() + "]");
                string value = inputElement.webElement.GetAttribute("value");
                if (value == quantity.ToString())
                {
                    return true;
                }
            }
            return false;
        }


    }
}