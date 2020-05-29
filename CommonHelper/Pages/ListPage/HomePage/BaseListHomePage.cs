using CommonHelper.BaseComponents;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace CommonHelper.Pages.ListPage.HomePage
{
    public abstract class BaseListHomePage : BasePOM
    {
        #region Constructor
        public BaseListHomePage(IWebDriver driver) : base(driver) { }
        #endregion

        #region Locators
        protected DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = ".body-content"
        };

        protected DomElement detailSection = new DomElement
        {
            locator = ".detail-section"
        };

        protected DomElement detaulfTitleList = new DomElement
        {
            locator = "h1"
        };

        protected DomElement detailCreate = new DomElement
        {
            locator = ".list.create"
        };

        protected DomElement detailCreateNewList = new DomElement
        {
            locator = ".create"
        };

        protected DomElement detailModal = new DomElement(By.CssSelector)
        {
            locator = ".modal-content"
        };

        protected DomElement detailModalBody = new DomElement
        {
            locator = ".modal-body"
        };

        protected DomElement detailModalFooter = new DomElement
        {
            locator = ".modal-footer"
        };

        protected DomElement clickDeleteDanger = new DomElement
        {
            locator = ".danger"
        };

        protected DomElement detailFormTextBox = new DomElement
        {
            locator = ".form-control"
        };

        protected DomElement detailFormActions = new DomElement
        {
            locator = ".form-actions"
        };

        protected DomElement buttonCreateList = new DomElement
        {
            locator = ".btn"
        };

        protected DomElement alertSuccessModal = new DomElement
        {
            //changed by Jesús. Before: .alert-success
            locator = ".modal-body-success"
        };

        protected DomElement alertDangerModal = new DomElement
        {
            locator = ".alert-danger"
        };

        protected DomElement modalHeaderDisplay = new DomElement
        {
            locator = ".modal-header"
        };

        protected DomElement cardContainer = new DomElement
        {
            locator = ".licard-container"
        };

        protected DomElement cardInformation = new DomElement
        {
            locator = ".licard-info"
        };

        protected DomElement cardActions = new DomElement
        {
            locator = ".licard-actions"
        };

        protected DomElement cardDeleteLink = new DomElement
        {
            locator = ".footer-actionlist"
        };

        protected DomElement deleteClick = new DomElement
        {
            locator = ".ng-binding"
        };

        protected DomElement nameList = new DomElement
        {
            locator = ".summary-title"
        };

        protected DomElement nameLink = new DomElement
        {
            locator = ".ng-binding"
        };

        protected DomElement imageList = new DomElement
        {
            locator = ".summary-image"
        };

        protected DomElement subTitle = new DomElement
        {
            locator = ".sub-title"
        };

        protected DomElement closeModal = new DomElement(By.XPath)
        {
            locator = "//div[@class='modal-content']//button[contains(@class,'btn-link')]"
        };

        protected DomElement listItem = new DomElement(By.XPath)
        {
            locator = "div.lists.view"
        };

        #endregion


        public virtual bool DefaultTitlePage()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement listDetailSection = listDetailSummary.GetElementWaitByCSS(detaulfTitleList.locator);
            return listDetailSection.webElement.Enabled;
        }

        public virtual void ClickCreateaNewList()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailcreatesection = listDetailSummary.GetElementWaitByCSS(detailCreate.locator);
            DomElement detailcreatelink = detailcreatesection.GetElementWaitByCSS(detailCreateNewList.locator);
            detailcreatelink.webElement.Click();

            this.detailModal.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        public virtual void SendListName(string listname)
        {
            DomElement detailSummaryModal = detailModal.GetElementWaitByCSS(detailModalBody.locator);
            DomElement detailTextbox = detailSummaryModal.GetElementWaitByCSS(detailFormTextBox.locator);
            detailTextbox.webElement.SendKeys(listname);
        }

        public virtual void ClickCreateListButton()
        {
            DomElement detailSummaryModal = detailModal.GetElementWaitByCSS(detailModalBody.locator);
            DomElement detailformAction = detailSummaryModal.GetElementWaitByCSS(detailFormActions.locator);
            DomElement detailCreateList = detailformAction.GetElementWaitByCSS(buttonCreateList.locator);
            detailCreateList.webElement.Click();
        }

        public virtual bool SuccessListCreated()
        {
            DomElement detailSummaryModal = detailModal.GetElementWaitByCSS(alertSuccessModal.locator);
            //DomElement successMessage = detailSummaryModal.GetElementWaitByCSS(modalHeaderDisplay.locator);
            return detailSummaryModal.webElement.Enabled;
        }

        public virtual bool DangerListnotCreated()
        {
            DomElement detailSummaryModal = detailModal.GetElementWaitByCSS(alertDangerModal.locator);
            DomElement successMessage = detailSummaryModal.GetElementWaitByCSS(modalHeaderDisplay.locator);
            return successMessage.webElement.Enabled;
        }

        public virtual void ClickOnFirstList()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailCardContainer = listDetailSummary.GetElementWaitByCSS(cardContainer.locator);
            DomElement detailCardInfo = detailCardContainer.GetElementWaitByCSS(cardInformation.locator);
            DomElement detailNameList = detailCardInfo.GetElementWaitByCSS(nameList.locator);
            DomElement detailNameLink = detailNameList.GetElementWaitByCSS(nameLink.locator);
            detailNameLink.webElement.Click();
        }

        public virtual bool CoverImageIsDisplayed()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailCardContainer = listDetailSummary.GetElementWaitByCSS(cardContainer.locator);
            DomElement detailCardInfo = detailCardContainer.GetElementWaitByCSS(cardInformation.locator);
            DomElement detailImage = detailCardInfo.GetElementWaitByCSS(imageList.locator);
            return detailImage.webElement.Enabled;
        }

        public virtual bool ListNameIsDisplayed()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailCardContainer = listDetailSummary.GetElementWaitByCSS(cardContainer.locator);
            DomElement detailCardInfo = detailCardContainer.GetElementWaitByCSS(cardInformation.locator);
            DomElement detailNameList = detailCardInfo.GetElementWaitByCSS(nameList.locator);
            return detailNameList.webElement.Enabled;
        }

        public virtual bool NumberOfItemsIsDisplayed()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailCardContainer = listDetailSummary.GetElementWaitByCSS(cardContainer.locator);
            DomElement detailCardInfo = detailCardContainer.GetElementWaitByCSS(cardInformation.locator);
            DomElement detailNumberofItemandCreationDate = detailCardInfo.GetElementWaitByCSS(subTitle.locator);
            return detailNumberofItemandCreationDate.webElement.Enabled;

        }

        public virtual bool ListPageisEmty()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailCardContainer = listDetailSummary.GetElementWaitByCSS(cardContainer.locator);
            return !detailCardContainer.IsElementPresent(cardInformation.locator);
        }

        public virtual void ClickDeleteLink()
        {
            DomElement listDetailSummary = detailSummary.GetElementWaitByCSS(detailSection.locator);
            DomElement detailCardContainer = listDetailSummary.GetElementWaitByCSS(cardContainer.locator);
            DomElement detailCardAction = detailCardContainer.GetElementWaitByCSS(cardActions.locator);
            DomElement detailActionList = detailCardAction.GetElementWaitByCSS(cardDeleteLink.locator);
            DomElement clickonDelete = detailActionList.GetElementWaitByCSS(deleteClick.locator);
            clickonDelete.webElement.Click();
            this.detailModal.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        public virtual void ClickDeleteOnModal()
        {
            DomElement listDetailSummary = detailModal.GetElementWaitByCSS(detailModalFooter.locator);
            DomElement clickDeleteModal = listDetailSummary.GetElementWaitByCSS(clickDeleteDanger.locator);
            clickDeleteModal.webElement.Click();
        }

        public virtual void CloseModal()
        {
            DomElement closeModal = detailSummary.GetElementWaitXpath(this.closeModal.locator);
            closeModal.webElement.Click();
        }

        public virtual int GetNumberOfLists()
        {
            List<DomElement> items = detailSummary.GetElementsWaitByCSS(this.listItem.locator);
            return items.Count;
        }

    }
}
