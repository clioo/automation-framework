using CommonHelper.BaseComponents;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.Pages.CartPage
{
    public abstract class BaseCartPage : BasePOM
    {
        #region Details Summary

        protected DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = ".cart .detail-section"
        };

        DomElement DetailSectionLoadingSubtitle = new DomElement
        {
            locator = ".detail-section h2"
        };

        protected DomElement detailModal = new DomElement(By.CssSelector)
        {
            locator = ".add-to-list-modal"
        };

        protected DomElement modalContent = new DomElement
        {
            locator = ".modal-content"
        };

        protected DomElement modalFooter = new DomElement
        {
            locator = ".modal-footer"
        };

        protected DomElement modalBody = new DomElement
        {
            locator = ".modal-body"
        };

        protected DomElement successLabel = new DomElement
        {
            locator = ".alert-success"
        };

        protected DomElement addToListbutton = new DomElement
        {
            locator = ".btn-primary"
        };

        protected DomElement visitlistButton = new DomElement
        {
            locator = ".btn-primary"
        };

        protected DomElement closeModal = new DomElement
        {
            locator = ".btn-link"
        };

        protected DomElement loadingIndicator = new DomElement
        {
            locator = "h1"
        };

        protected DomElement headsupSection = new DomElement
        {
            locator = ".cart-items-section"
        };

        protected DomElement detailCartItems = new DomElement
        {
            locator = ".cart-items"
        };

        protected DomElement detailCartItem = new DomElement
        {
            locator = ".cart-item"
        };

        protected DomElement detailCartItemContent = new DomElement
        {
            locator = ".item-content"
        };

        protected DomElement detailImageSection = new DomElement
        {
            locator = ".image-section"
        };

        protected DomElement detailCartItemContentProduct = new DomElement
        {
            locator = ".product-section"
        };

        protected DomElement detailProducTitleSection = new DomElement
        {
            locator = ".title-section"
        };

        protected DomElement detailCartItemContentProductSKU = new DomElement
        {
            locator = ".sku-section .sku"
        };

        protected DomElement detailCartItemContentLineItem = new DomElement
        {
            locator = ".line-item-section"
        };

        protected DomElement detailCartItemContentLineItemQty = new DomElement
        {
            locator = ".quantity-section"
        };

        protected DomElement detailCartItemContentLineItemQtyInput = new DomElement
        {
            locator = ".quantity input"
        };

        protected DomElement detailCartItemAvailability = new DomElement
        {
            locator = ".sku-availability-section"
        };

        protected DomElement detailCartItemAvailabilityTag = new DomElement
        {
            locator = ".availability-content"
        };

        protected DomElement detailActionContent = new DomElement
        {
            locator = ".action-content"
        };

        protected DomElement detailListLink = new DomElement
        {
            locator = ".action-list"
        };

        protected DomElement checkoutAsGuestButton = new DomElement
        {
            locator = "//a[contains(text(),'Checkout')]"
        };

        protected DomElement quantityInput = new DomElement(By.XPath)
        {
            locator = "//input[@ng-model='item.Quantity']"
        };

        protected DomElement selectAllButton = new DomElement(By.CssSelector)
        {
            locator = "input[ng-model='$ctrl.isAllSelected']"
        };

        protected DomElement selectedSpan = new DomElement(By.CssSelector)
        {
            locator = "span.selected"
        };

        protected DomElement inputSelectItem = new DomElement(By.CssSelector)
        {
            locator = "input[ng-model='item.Selected']"
        };

        protected DomElement moveSelToSaveLaterButton = new DomElement(By.XPath)
        {
            locator = "//button[contains(text(),'Move selected to Save for later')]"
        };

        protected DomElement savedForLaterTab = new DomElement(By.XPath)
        {
            locator = "//a[contains(text(),'Saved for later')]"
        };

        protected DomElement moveSelectedToCart = new DomElement(By.CssSelector)
        {
            locator = "div.multiselect-actions.list button.action-add-selected-to-cart"
        };

        protected DomElement individualAddToCartButton = new DomElement(By.XPath)
        {
            locator = "//button[@ng-click='onClickMoveToCart(item)']"
        };

        protected DomElement cartTab = new DomElement(By.CssSelector)
        {
            locator = "a[ng-click=\"onTabClick('cart.cartItemsView')\"]"
        };

        protected DomElement availabilityDiv = new DomElement(By.CssSelector)
        {
            locator = "div.product-availability-section div.line.sku-availability-section.ng-scope.ng-isolate-scope"
        };

        protected DomElement dropDownActionsContainer = new DomElement(By.CssSelector)
        {
            locator = "div.multiselect-actions.dropdown.btn-group"
        };

        protected DomElement dropdownActionsButton = new DomElement(By.CssSelector)
        {
            locator = "button.dropdown-toggle"
        };

        protected DomElement dropDwonActionsItem = new DomElement(By.CssSelector)
        {

        };

        protected DomElement removeSelectedFromCartButton = new DomElement(By.CssSelector)
        {
            locator = "div.multiselect-actions.list button.action-remove"
        };

        protected DomElement removeIndividualItemButton = new DomElement(By.CssSelector)
        {
            locator = "div.actions button.action-remove"
        };

        protected DomElement inCartItemsContainer = new DomElement(By.CssSelector)
        {
            locator = "div.section.cart-items-section"
        };

        protected DomElement checkboxInput = new DomElement(By.CssSelector)
        {
            locator = "div.grid-item div.checkbox input[type='checkbox']"
        };
        #endregion Details Summary


        #region Cart Detail Card
        protected DomElement removeLink = new DomElement
        {
            locator = ".action-remove"
        };

        protected DomElement saveforlaterLink = new DomElement
        {
            locator = ".action-save"
        };

        protected DomElement navigationtabs = new DomElement
        {
            locator = ".nav-tabs"
        };

        protected DomElement saveforlaterSection = new DomElement
        {
            locator = ".icon.icon-save"
        };

        protected DomElement infoHeadUptitle = new DomElement
        {
            locator = ".title"
        };

        protected DomElement ContentHeadsUp = new DomElement
        {
            locator = ".content"
        };

        protected DomElement detailFooter = new DomElement
        {
            locator = ".footer"
        };

        protected DomElement contentSaveHeadUpInfo = new DomElement
        {
            locator = ".content"
        };

        protected DomElement objlistLink = new DomElement
        {
            locator = ".action-list"
        };

        protected DomElement cartItemsSec = new DomElement
        {
            locator = ".cart-items-section"
        };

        protected DomElement items = new DomElement
        {
            locator = ".item-content"
        };

        protected DomElement totalItemPrices = new DomElement(By.XPath)
        {
            locator = "//div[@class='line total-price-section']/div/span[@class='price ng-binding']"
        };

        protected DomElement itemsTotals = new DomElement(By.XPath)
        {
            locator = "//div[@class='line-item-total line']/div[@class='value ng-binding']"
        };

        #endregion Card Detal Card

        #region Right Navigation Section
        protected DomElement rightNavMenu = new DomElement(By.CssSelector)
        {
            locator = ".cart .nav-section"
        };

        protected DomElement rightNavigationSection = new DomElement(By.CssSelector)
        {
            locator = ".nav-right"
        };

        protected DomElement rightNavProceedToCheckOutButton = new DomElement
        {
            locator = ".nav-section"
        };

        protected DomElement checkoutSummary = new DomElement
        {
            locator = ".checkout-summary"
        };

        protected DomElement orderSummarytotals = new DomElement
        {
            locator = ".order-summary"
        };

        protected DomElement contentDetail = new DomElement
        {
            locator = ".content"
        };

        protected DomElement proxText = new DomElement()
        {
            locator = ".info"
        };

        protected DomElement successMessageDetail = new DomElement
        {
            locator = ".success"
        };

        protected DomElement proceedtoCheckoutbtn = new DomElement
        {
            locator = ".btn"
        };

        #endregion Right Naigation Section

        #region Shipping Options

        protected DomElement rightNavShippingSection = new DomElement
        {
            locator = ".shipment-options"
        };

        protected DomElement rightNavShippingRates = new DomElement
        {
            locator = ".content .shipment-rates"
        };

        protected DomElement rightNavShippingRate = new DomElement
        {
            locator = ".value"
        };

        protected DomElement rightNavShippingRateInput = new DomElement
        {
            locator = "input"
        };

        protected DomElement shippingTotalValue = new DomElement()
        {
            locator = ".shipping.line .ng-binding.ng-scope"
        };

        #endregion Shipping Options

        #region Additional Shipping Options

        protected DomElement rightNavAdditionalShippingSection = new DomElement
        {
            locator = "shipment-blind-shipping"
        };

        protected DomElement rightNavAdditionalShippingBlind = new DomElement
        {
            locator = ".content .select"
        };

        protected DomElement rightNavAdditionalShippingBlindCheckBox = new DomElement
        {
            locator = ".checkbox-container input"
        };

        #endregion Additional Shipping Options

        #region Constructor
        public BaseCartPage(IWebDriver driver) : base(driver) { }

        #endregion Constructor


        protected virtual List<DomElement> GetSelectedItems()
        {
            return detailSummary.GetElementsWaitByCSS(selectedSpan.locator);
        }
        public virtual void RemoveLinkClick()
        {
            DomElement waitforElement = detailSummary.GetElementWaitUntil("h2", (el) => !el.Displayed);
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement removeLinkdetail = cartItemsSection.GetElementWaitByCSS(removeLink.locator);
            removeLinkdetail.webElement.Click();
        }

        public virtual void MovetoSFL()
        {
            DomElement waitforElement = detailSummary.GetElementWaitUntil("h2", (el) => !el.Displayed);
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement saveLink = cartItemsSection.GetElementWaitByCSS(saveforlaterLink.locator);
            saveLink.webElement.Click();
        }

        public virtual void SeeSFLItems()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(navigationtabs.locator);
            DomElement saveforlatertab = cartItemsSection.GetElementWaitByCSS(saveforlaterSection.locator);
            saveforlatertab.webElement.Click();
        }

        public virtual void ClickAddtoListLink()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement listLink = cartItemsSection.GetElementWaitByCSS(objlistLink.locator);
            listLink.webElement.Click();
            Thread.Sleep(2000);
            this.detailModal.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        public virtual void ClickAddtoListButton()
        {

            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailContent = modalSection.GetElementWaitByCSS(modalFooter.locator);
            DomElement detailmodalButton = detailmodailContent.GetElementWaitByCSS(addToListbutton.locator);
            detailmodalButton.webElement.Click();

        }

        public virtual bool SuccessMessageAdded()
        {
            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailContent = modalSection.GetElementWaitByCSS(modalBody.locator);
            DomElement detailAlertSuccess = detailmodailContent.GetElementWaitByCSS(successLabel.locator);
            return detailAlertSuccess.webElement.Enabled;
        }

        public virtual void VisitListButton()
        {
            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailContent = modalSection.GetElementWaitByCSS(modalBody.locator);
            DomElement detailvisitlistButton = detailmodailContent.GetElementWaitByCSS(visitlistButton.locator);
            detailvisitlistButton.webElement.Click();
        }

        public virtual void ClickCloseModalLink()
        {
            DomElement modalSection = detailModal.GetElementWaitByCSS(modalContent.locator);
            DomElement detailmodailBody = modalSection.GetElementWaitByCSS(modalBody.locator);
            DomElement detailmodalClose = detailmodailBody.GetElementWaitByCSS(closeModal.locator);
            detailmodalClose.webElement.Click();
        }

        public virtual bool totalsInformationDislayed()
        {
            DomElement detailNavRight = rightNavigationSection.GetElementWaitByCSS(rightNavProceedToCheckOutButton.locator);
            DomElement detailCheckoutSummary = detailNavRight.GetElementWaitByCSS(checkoutSummary.locator);
            DomElement detailOrderSummary = detailCheckoutSummary.GetElementWaitByCSS(orderSummarytotals.locator);
            return detailOrderSummary.webElement.Enabled;
        }

        public virtual bool HeadsUpCartInfo()
        {
            DomElement infoHeadupInformationtitle = detailSummary.GetElementWaitByCSS(infoHeadUptitle.locator);
            return infoHeadupInformationtitle.webElement.Enabled;
        }

        public virtual bool ContentHeadsUpInfo()
        {
            DomElement headsupInfotitle = detailSummary.GetElementWaitByCSS(cartItemsSec.locator);
            DomElement footerSec = headsupInfotitle.GetElementWaitByCSS(detailFooter.locator);
            DomElement headsupContentInformation = footerSec.GetElementWaitByCSS(ContentHeadsUp.locator);
            return headsupContentInformation.webElement.Enabled;
        }

        public virtual bool MovetoCartLinkEnable()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement saveLink = cartItemsSection.GetElementWaitByCSS(saveforlaterLink.locator);
            return saveLink.webElement.Enabled;
        }

        public virtual bool FindCongraMessage()
        {
            DomElement detailNavRight = rightNavigationSection.GetElementWaitByCSS(rightNavProceedToCheckOutButton.locator);
            DomElement detailCheckoutSummary = detailNavRight.GetElementWaitByCSS(checkoutSummary.locator);
            DomElement detailContent = detailCheckoutSummary.GetElementWaitByCSS(contentDetail.locator);
            DomElement detailSuccess = detailContent.GetElementWaitByCSS(successMessageDetail.locator);
            return detailSuccess.webElement.Enabled;
        }

        public virtual bool proximtyMessageDisplayed()
        {
            DomElement detailNavRight = rightNavigationSection.GetElementWaitByCSS(rightNavProceedToCheckOutButton.locator);
            DomElement detailCheckoutSummary = detailNavRight.GetElementWaitByCSS(checkoutSummary.locator);
            DomElement detailInfoProximity = detailCheckoutSummary.GetElementWaitByCSS(proxText.locator);
            return detailInfoProximity.webElement.Enabled;
        }

        public virtual string GetShippingServiceLevelsTDB()
        {
            DomElement detailrightnavMenu = rightNavMenu.GetElementWaitByCSS(rightNavShippingSection.locator);
            DomElement detailShippingSection = detailrightnavMenu.GetElementWaitByCSS(rightNavShippingRates.locator);
            DomElement detailShippingRates = detailShippingSection.GetElementWaitByCSS(rightNavShippingRate.locator);
            return detailShippingRates.webElement.Text;
        }

        //Component Base Code Functions
        public virtual int GetNumberOfSelectedItems()
        {
            return GetSelectedItems().Count;
        }

        public virtual bool IsLoading(int time = 30)
        {
            var modalBusy = detailSummary.GetElementWaitUntil(loadingIndicator.locator, (el) => !el.Displayed, time);
            if (modalBusy == null)
            {
                return true;
            }
            return false;
        }

        public virtual string AllComponentsExist()
        {
            StringBuilder sb = new StringBuilder();
            if (detailSummary.webElement == null)
                sb.Append(nameof(detailSummary));
            if (rightNavMenu.webElement == null)
                sb.Append(nameof(rightNavMenu));
            return sb.ToString();
        }

        public virtual IDictionary<string, string> AvailabiltyTagGet()
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

        public virtual void ProceedToCheckOut()
        {

            DomElement proceedToCheckOutButton = rightNavMenu.GetElementWaitUntil(proceedtoCheckoutbtn.locator,
                                                    (el) => el.Displayed);
            //DomElement proceedToCheckOutButton = navRighePanelinCart.GetElementWaitByCSS(proceedtoCheckoutbtn.locator);
            proceedToCheckOutButton.webElement.Click();
        }

        public virtual bool ImageSectionInCart()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement imagesection = itemcontent.GetElementWaitByCSS(detailImageSection.locator);
            return imagesection.webElement.Enabled;
        }

        public virtual bool TitleSectionIsDisplayed()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement productsection = itemcontent.GetElementWaitByCSS(detailCartItemContentProduct.locator);
            DomElement titleSection = productsection.GetElementWaitByCSS(detailProducTitleSection.locator);
            return titleSection.webElement.Enabled;
        }

        public virtual bool SKUSectionIsDisplayed()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement productsection = itemcontent.GetElementWaitByCSS(detailCartItemContentProduct.locator);
            DomElement skuSection = productsection.GetElementWaitByCSS(detailCartItemContentProductSKU.locator);
            return skuSection.webElement.Enabled;
        }

        public virtual bool LineItemSectionIsDisplayed()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement itemcontent = cartitem.GetElementWaitByCSS(detailCartItemContent.locator);
            DomElement lineitemsection = itemcontent.GetElementWaitByCSS(detailCartItemContentLineItem.locator);
            return lineitemsection.webElement.Enabled;
        }

        public virtual bool AddToListLink()
        {
            DomElement cartItemsSection = detailSummary.GetElementWaitByCSS(detailCartItems.locator);
            DomElement cartitem = cartItemsSection.GetElementWaitByCSS(detailCartItem.locator);
            DomElement actionContent = cartitem.GetElementWaitByCSS(detailActionContent.locator);
            DomElement listLink = actionContent.GetElementWaitByCSS(detailListLink.locator);
            return listLink.webElement.Enabled;
        }

        protected virtual void WaitUntilLoadingDone()
        {
            BodyContainer.GetElementWaitUntil(DetailSectionLoadingSubtitle.locator, el =>
            {
                return !el.Displayed;
            });
        }

        public virtual int NumberOfItemsInCart()
        {
            List<DomElement> itemsInCart = detailSummary.GetElementsWaitByCSS(detailCartItem.locator);
            return itemsInCart.Count;
        }

        public virtual int GetQuantityInput()
        {
            DomElement quantityInputElement = detailSummary.GetElementWaitByCSS(detailCartItemContentLineItemQtyInput.locator);
            return int.Parse(quantityInputElement.webElement.GetAttribute("value"));
        }

        protected virtual List<DomElement> GetItemsInCart()
        {
            var itemsel = detailSummary.GetElementsWaitByCSS(items.locator);
            return detailSummary.GetElementsWaitByCSS(items.locator);
        }
        public virtual int GetNumberOfItemsInCart()
        {
            return GetItemsInCart().Count;
        }

        public virtual bool TotalAmountIsCorrect()
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

        public virtual void SelectAllItems()
        {
            DomElement selectAllButtonElement = detailSummary.GetElementWaitByCSS(selectAllButton.locator);
            selectAllButtonElement.webElement.Click();
            detailSummary.GetElementWaitUntil(selectedSpan.locator, (el) => el.Displayed);
        }

        public virtual void MoveSelectedToSaveLater()
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

        public virtual void ClickOnSavedForLater()
        {
            DomElement savedForLaterTabElement = detailSummary.GetElementWaitXpath(savedForLaterTab.locator);
            savedForLaterTabElement.webElement.Click();
            detailSummary.GetElementWaitUntil(moveSelectedToCart.locator, (element) => element.Displayed);
        }

        public virtual void SelectItemsByIndex(int index)
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

        public virtual void SelectItemsByNumber(int number)
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
                base.WaitForAppBusy(8);
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

        public virtual void MoveSelectedToCart()
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

        public virtual void ClickIndividualMoveToCartByIndex(int index)
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

        public virtual void ClickOnCartTab()
        {
            base.WaitForAppBusy();
            base.ScrollToTop();
            DomElement cartTab = detailSummary.GetElementWaitByCSS(this.cartTab.locator);
            cartTab.webElement.Click();
        }

        public virtual bool InventoryAvailabilityIsDisplayed()
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

        public virtual void ClickRemoveSelectedItems()
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

 
        public virtual bool IsQuantityInCartItem(int quantity)
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
