

using CommonHelper.BaseComponents;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;

namespace CommonHelper.Pages.ListPage.ListSummaryPage
{
    public abstract class BaseListSummaryPage : BasePOM
    {
        #region Constructor
        public BaseListSummaryPage(IWebDriver driver) : base(driver) { }

        #endregion

        #region Locators

        protected DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = ".main.body-content"
        };

        protected DomElement detailSection = new DomElement
        {
            locator = ".detail-section"
        };

        protected DomElement detailSectionAccountItems = new DomElement
        {
            locator = ".section-accountitems"
        };

        protected DomElement detailShoppingList = new DomElement
        {
            locator = ".shopping-list-items-section"
        };

        protected DomElement detailNumberOfItems = new DomElement
        {
            locator = ".header"
        };

        protected DomElement detailGridItems = new DomElement
        {
            locator = ".grid-items"
        };

        protected DomElement detailItemContent = new DomElement
        {
            locator = ".item-content"
        };

        protected DomElement detailSKUContent = new DomElement
        {
            locator = ".sku-section"
        };

        protected DomElement detailIconContainer = new DomElement
        {
            locator = ".icon-container"
        };

        protected DomElement detailRename = new DomElement
        {
            locator = ".dropdown-menu"
        };

        protected DomElement detailModal = new DomElement(By.CssSelector)
        {
            locator = ".modal-content"
        };

        protected DomElement detailModalBody = new DomElement
        {
            locator = ".modal-body"
        };

        protected DomElement detailFormTextBox = new DomElement
        {
            locator = ".form-control"
        };

        protected DomElement detailFormActions = new DomElement
        {
            locator = ".form-actions"
        };

        protected DomElement buttonUpateList = new DomElement
        {
            locator = ".btn"
        };

        protected DomElement itemInList = new DomElement(By.CssSelector)
        {
            locator = "div.item-content"
        };

        protected DomElement checkboxInput = new DomElement(By.CssSelector)
        {
            locator = "div.grid-item div.checkbox input[type='checkbox']"
        };

        protected DomElement removeSelected = new DomElement(By.CssSelector)
        {
            locator = "div.list button.action-remove"
        };

        protected DomElement removeIndividual = new DomElement(By.CssSelector)
        {
            locator = "div.actions button.action-remove"
        };

        protected DomElement addToCartButton = new DomElement(By.CssSelector)
        {
            locator = "button.js-add-to-cart"
        };

        protected DomElement listsBreadcrumb = new DomElement(By.XPath)
        {
            locator = "//ul[contains(@class,'breadcrumb')]//a[contains(text(),'Lists')]"
        };

        protected DomElement itemNameLink = new DomElement(By.XPath)
        {
            locator = "//h3[@class='title ng-binding']"
        };

        protected DomElement selectedItems = new DomElement(By.CssSelector)
        {
            locator = "span.selected"
        };
        #endregion

        public virtual  void ClickOnIcon()
        {
            DomElement detailSummaryList = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement icondetail = detailSummaryList.GetElementWaitByCSS(detailIconContainer.locator);
            icondetail.webElement.Click();
            Thread.Sleep(2000);
        }

        public virtual  void ClickOnRenameList()
        {
            DomElement detailSummaryList = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement icondetail = detailSummaryList.GetElementWaitByCSS(detailIconContainer.locator);
            DomElement clickinRename = icondetail.GetElementWaitByCSS(detailRename.locator);
            clickinRename.webElement.Click();
            Thread.Sleep(2000);
            this.detailModal.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        public virtual  void SendNewListName(string newName)
        {
            DomElement detailSummaryModal = detailModal.GetElementWaitByCSS(detailModalBody.locator);
            DomElement detailTextbox = detailSummaryModal.GetElementWaitByCSS(detailFormTextBox.locator);
            detailTextbox.webElement.Clear();
            detailTextbox.webElement.SendKeys(newName);
        }

        public virtual  void ClickUpdatebutton()
        {
            DomElement detailSummaryModal = detailModal.GetElementWaitByCSS(detailModalBody.locator);
            DomElement detailformAction = detailSummaryModal.GetElementWaitByCSS(detailFormActions.locator);
            DomElement detailUpdateList = detailformAction.GetElementWaitByCSS(buttonUpateList.locator);
            detailUpdateList.webElement.Click();
        }

        public virtual  bool TotalOfItemsDisplayed()
        {
            DomElement detailSummaryList = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailAccountItems = detailSummaryList.GetElementWaitByCSS(detailSectionAccountItems.locator);
            DomElement detailShoppingListItem = detailAccountItems.GetElementWaitByCSS(detailShoppingList.locator);
            DomElement detailTotalOfItemsInList = detailShoppingListItem.GetElementWaitByCSS(detailNumberOfItems.locator);
            return detailTotalOfItemsInList.webElement.Enabled;
        }

        public virtual  bool ItemCardIsDisplayed()
        {
            DomElement detailSummaryList = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailAccountItems = detailSummaryList.GetElementWaitByCSS(detailSectionAccountItems.locator);
            DomElement detailShoppingListItem = detailAccountItems.GetElementWaitByCSS(detailShoppingList.locator);
            DomElement detailGridItemDisplayed = detailShoppingListItem.GetElementWaitByCSS(detailGridItems.locator);
            return detailGridItemDisplayed.webElement.Enabled;
        }

        public virtual  bool SKUisDiplayed()
        {
            DomElement detailSummaryList = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailAccountItems = detailSummaryList.GetElementWaitByCSS(detailSectionAccountItems.locator);
            DomElement detailShoppingListItem = detailAccountItems.GetElementWaitByCSS(detailShoppingList.locator);
            DomElement detailGridItemDisplayed = detailShoppingListItem.GetElementWaitByCSS(detailGridItems.locator);
            DomElement detailItemContentDisplayed = detailGridItemDisplayed.GetElementWaitByCSS(detailItemContent.locator);
            DomElement detailSKUDisplayed = detailItemContentDisplayed.GetElementWaitByCSS(detailSKUContent.locator);
            return detailSKUDisplayed.webElement.Enabled;
        }

        public virtual  int NumberOfItemsInList()
        {
            base.WaitForAppBusy(6);
            Thread.Sleep(2000);
            List<DomElement> listItems = detailSummary.GetElementsWaitByCSS(itemInList.locator);
            return listItems.Count;
        }

        public virtual  void SelectByIndex(int index)
        {
            base.ScrollToTop();
            int selectedItemsBeforeSelect = detailSummary.GetElementsWaitByCSS(this.selectedItems.locator).Count;
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
                selectedItemsAfterSelect = detailSummary.GetElementsWaitByCSS(this.selectedItems.locator).Count;
            } while (selectedItemsAfterSelect <= selectedItemsBeforeSelect);

        }

        public virtual  void ClickOnRemoveSelected()
        {
            base.ScrollToTop();
            DomElement removeSelected = detailSummary.GetElementWaitByCSS(this.removeSelected.locator);
            removeSelected.webElement.Click();
            base.WaitForAppBusy(8);
        }

        public virtual  void RemoveIndividualByIndex(int index)
        {
            List<DomElement> items = detailSummary.GetElementsWaitByCSS(this.removeIndividual.locator);
            if (index > items.Count - 1 || index < 0) index = items.Count - 1;
            base.ScrollToTop();
            try
            {
                items[index].webElement.Click();
            }
            catch (System.Exception)
            {
                items = detailSummary.GetElementsWaitByCSS(this.removeIndividual.locator);
                items[index].webElement.Click();
            }
            WaitForAppBusy(8);
        }


        protected int GetNotEqualIndexByName(List<DomElement> list, string name)
        {
            //This method, given a list and string, gets from list a different item
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].webElement.GetAttribute("innerHTML") != name)
                {
                    return i;
                }
            }
            //returning -1 means that there's not different item in list by the name given
            return -1;
        }

        public virtual  void ClickOnBreadCrumbLists()
        {
            base.ScrollToTop();
            base.WaitForAppBusy(8);
            Thread.Sleep(2000);
            DomElement breadcrumbLists = detailSummary.GetElementWaitXpath(this.listsBreadcrumb.locator);
            breadcrumbLists.webElement.Click();
        }

    }
}
