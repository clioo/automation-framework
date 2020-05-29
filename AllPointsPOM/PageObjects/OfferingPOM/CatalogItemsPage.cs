using AllPoints.PageObjects.ListPOM.ListSummaryPOM;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllPoints.PageObjects.OfferingPOM
{
    public class CatalogItemsPage : AllPointsBaseWebPage
    {
        public AddListModal AddListModal;
        public CatalogItemsPage(IWebDriver driver) : base(driver)
        {
            this.catalogSummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        #region Catalog Summary
        private DomElement catalogSummary = new DomElement(By.CssSelector)
        {
            locator = ".detail-section.grid-items-section"
        };

        //Gets the first numeric price object when item is searched
        private DomElement firstPrice = new DomElement
        {
            locator = "(//span[@itemprop='price' and @class='price'])[1]"
        };

        //Gets the text "List price" from the page
        private DomElement firstListPriceText = new DomElement
        {
            locator = "(//div[@class='line-label section-label  anon'])[1]"
        };

        private DomElement yourPriceLabelText = new DomElement
        {
            locator = ".line-label.section-label.auth"
        };

        //First add to cart button in catalog
        private DomElement firstAddToCartButtonInCatalog = new DomElement
        {
            locator = "button.btn.btn-default.js-add-to-cart:first-of-type"
        };

        private DomElement displayedButtonItems = new DomElement
        {
            locator = ".btn.btn-default.js-add-to-cart"
        };

        private DomElement addToListButton = new DomElement
        {
            locator = "button.add-to-list"
        };

        //Success message after add an item
        private DomElement successMessage = new DomElement
        {
            locator = "//*[contains(text(), 'added to cart')]"
        };

        private DomElement listViewIcon = new DomElement
        {
            locator = ".grid-view-toggle.action.list-view"
        };

        private DomElement gridListView = new DomElement
        {
            locator = ".grid.list-view"
        };

        private DomElement pageListNumber = new DomElement
        {
            locator = "li.active a"
        };

        private DomElement nextPageArrow = new DomElement
        {
            locator = ".icon.icon-caret-right "
        };

        private DomElement lastPage = new DomElement
        {
            locator = "//ul[@class='pagination']/li[4]/a"
        };

        private DomElement paginationButtons = new DomElement
        {
            locator = "(//ul[@class='pagination'])[1]/li/a"
        };

        private DomElement miniCart = new DomElement(By.XPath)
        {
            locator = "//button[contains(@class,'mini-cart-link')]/following-sibling::div[contains(@class,'mini-cart-container')]//div[contains(@class,'alert')]"

        };

        #region Item Content
        private DomElement detailGridItemContent = new DomElement
        {
            locator = ".grid-item-content"
        };

        private DomElement detailItemImage = new DomElement
        {
            locator = ".image-section"
        };

        #region Add to Cart button
        private DomElement detailAddbtn = new DomElement
        {
            locator = ".btn"
        };


        #endregion

        #endregion //Item Content

        #endregion // Catalog Summary

        public OfferingProductsPage ClickOnFirstItemInCatalog()
        {
            DomElement catalogPageView = catalogSummary.GetElementWaitByCSS(detailGridItemContent.locator);
            DomElement itemImageView = catalogPageView.GetElementWaitByCSS(detailItemImage.locator);
            itemImageView.webElement.Click();
            return new OfferingProductsPage(Driver);
        }

        private DomElement GetLastPageElementButton()
        {
            List<DomElement> paginationButtonsElements = catalogSummary.GetElementsWaitByXpath(paginationButtons.locator);
            DomElement majorPageIndexElement = null;
            int majorIndex = 0;
            foreach (var element in paginationButtonsElements)
            {
                int number;
                int.TryParse(element.webElement.Text, out number);
                if (number > majorIndex)
                {
                    majorIndex = number;
                    majorPageIndexElement = element;
                }
            }
            return majorPageIndexElement;
        }

        public void AddtoCartbuttonInCatalog()
        {
            DomElement catalogPageView = catalogSummary.GetElementWaitByCSS(detailGridItemContent.locator);
            DomElement buttonAddtoCart = catalogPageView.GetElementWaitByCSS(detailAddbtn.locator);
            buttonAddtoCart.webElement.Click();
        }

        public bool IsListPriceDisplayed()
        {
            //TODO: make this return bool
            DomElement firstListPriceTextElement = this.catalogSummary.GetElementWaitXpath(firstListPriceText.locator);
            DomElement firstPriceElement = this.catalogSummary.GetElementWaitXpath(firstPrice.locator);
            bool result = firstListPriceTextElement.webElement.Text != null &&
                firstPriceElement.webElement.Text != null;
            return result;
        }

        public void AddToCartFirstItemInCatalog()
        {
            DomElement catalogPageView = catalogSummary.GetElementWaitByCSS(detailGridItemContent.locator);
            DomElement firstAddTocartButtonElement = catalogPageView.GetElementWaitByCSS(firstAddToCartButtonInCatalog.locator);
            try
            {
                firstAddTocartButtonElement.webElement.Click();
            }
            catch (Exception)
            {
                //If the element could not be found, you have to relocate it again
                firstAddTocartButtonElement = catalogPageView.GetElementWaitByCSS(firstAddToCartButtonInCatalog.locator);
                firstAddTocartButtonElement.webElement.Click();
            }
            base.WaitForAppBusy(8);
        }

        public bool CheckIfYourPriceIsDisplayed()
        {
            DomElement firstListPriceTextElement = this.catalogSummary.GetElementWaitByCSS(yourPriceLabelText.locator);
            DomElement firstPriceElement = this.catalogSummary.GetElementWaitXpath(firstPrice.locator);
            return !firstListPriceTextElement.webElement.Text.Equals(null) &&
                !firstPriceElement.webElement.Text.Equals(null);
        }

        public void ClickOnListViewIcon()
        {
            base.ScrollToTop();
            Thread.Sleep(1000);
            DomElement listViewIconElement = catalogSummary.GetElementWaitByCSS(listViewIcon.locator);
            try
            {
                listViewIconElement.webElement.Click();
            }
            catch (Exception)
            {
                listViewIconElement = catalogSummary.GetElementWaitByCSS(listViewIcon.locator);
                listViewIconElement.webElement.Click();
            }
        }

        public void AddToCartElementByIndex(int index)
        {
            List<DomElement> items = catalogSummary.GetElementsWaitByCSS(displayedButtonItems.locator);
            if (index > items.Count - 1 || index < 0) index = items.Count - 1;
            base.ScrollToTop();
            //this do while repeats the click if the page doesn't get selenium's click
            do
            {
                try
                {
                    Thread.Sleep(1000);
                    items[index].webElement.Click();
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    items = catalogSummary.GetElementsWaitByCSS(displayedButtonItems.locator);
                    items[index].webElement.Click();
                }
                DomElement miniCart = catalogSummary.GetElementWaitUntilByXpath(this.miniCart.locator, (el) => el.Displayed);
            } while (miniCart.webElement != null);
            //this line waits until mini cart is no longer visible
            catalogSummary.GetElementWaitUntilByXpath(this.miniCart.locator, (el) => el.Displayed == false);
            //*********
        }

        public bool ListViewDisplayed()
        {
            DomElement listViewElement = catalogSummary.GetElementWaitByCSS(listViewIcon.locator);
            return !listViewElement.webElement.Equals(null);
        }

        public CatalogItemsPage ClickOnNextPage()
        {
            base.ScrollToTop();
            DomElement nextPageArrowElement = catalogSummary.GetElementWaitByCSS(nextPageArrow.locator);
            try
            {
                nextPageArrowElement.webElement.Click();
            }
            catch (Exception)
            {
                nextPageArrowElement = catalogSummary.GetElementWaitByCSS(nextPageArrow.locator);
                nextPageArrowElement.webElement.Click();
            }
            int actualPage = int.Parse(GetActualPageListNumber());
            catalogSummary.GetElementWaitUntil(pageListNumber.locator,
                (el) => int.Parse(el.Text) > actualPage);
            return new CatalogItemsPage(Driver);
        }

        public string GetActualPageListNumber()
        {
            DomElement pageListNumberElement = catalogSummary.GetElementWaitByCSS(pageListNumber.locator);
            return pageListNumberElement.webElement.Text;
        }

        public CatalogItemsPage ClickOnLastPage()
        {
            DomElement lastPageElement = GetLastPageElementButton();
            try
            {
                lastPageElement.webElement.Click();
            }
            catch (Exception)
            {
                lastPageElement = GetLastPageElementButton();
                lastPageElement.webElement.Click();
            }
            return new CatalogItemsPage(Driver);
        }

        public string GetLastPageListNumber()
        {
            DomElement lastElementButton = GetLastPageElementButton();
            return lastElementButton.webElement.Text;
        }

        public CatalogItemsPage ClickOnSubCategory(string subCategoryName)
        {
            DomElement subCategoryElement = catalogSummary.GetElementWaitXpath("//div[contains(text(),'" + subCategoryName + "')]");
            subCategoryElement.webElement.Click();
            return new CatalogItemsPage(base.Driver);
        }

        public APListSummaryPage AddToListByIndex(int index, bool visitList = false, string list = "My List")
        {
            List<DomElement> items = catalogSummary.GetElementsWaitByCSS(addToListButton.locator);
            if (index > items.Count - 1 || index < 0) index = items.Count - 1;
            base.ScrollToTop();
            try
            {
                Thread.Sleep(5000);
                items[index].webElement.Click();
            }
            catch (Exception)
            {
                items = catalogSummary.GetElementsWaitByCSS(addToListButton.locator);
                items[index].webElement.Click();
            }
            AddListModal = new AddListModal(base.Driver);
            catalogSummary.GetElementWaitUntilByXpath(AddListModal.modal.locator,
                (el) => el.Displayed, 15);
            AddListModal.SelectList(list);
            AddListModal.ClickOnAddToListModal();
            AddListModal.IsSuccessMessageInModal();
            if (visitList)
            {
                AddListModal.ClickOnAddToListModal();
                return new APListSummaryPage(Driver);
            }
            else
            {
                AddListModal.CloseAddToListModal();
            }
            base.WaitForAppBusy(3);
            return null;
        }




    }
}
