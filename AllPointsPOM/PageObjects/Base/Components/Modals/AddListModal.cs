using AllPoints.PageObjects.GenericWebPage.SharedElements.Modals.Enums;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base;
using CommonHelper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components
{
    public class AddListModal : ModalBase
    {
        #region listModal
        public DomElement modal = new DomElement(By.XPath)
        {
            locator = "//div[@class='modal-dialog']"
        };

        private DomElement addToListModal = new DomElement(By.XPath)
        {
            locator = "//div[@class='modal-dialog']"
        };

        private DomElement listSelect = new DomElement(By.XPath)
        {
            locator = "//button[contains(@class,'selectpicker')]"
        };

        private DomElement addTolistButtonModal = new DomElement(By.CssSelector)
        {
            locator = "//div[@class='modal-content']//*[contains(@class,'btn-primary')]"
        };

        private DomElement cancelButtonModal = new DomElement(By.XPath)
        {
            locator = "//div[@class='modal-content']//button[contains(@class,'btn-link')]"
        };

        private DomElement innerSelectOptionsModal = new DomElement(By.XPath)
        {
            locator = "//select[contains(@class,'selectpicker')]/option"
        };

        private DomElement successMessageModal = new DomElement(By.XPath)
        {
            locator = "//div[@class='modal-body']/div[@class='alert alert-success']"
        };

        private DomElement buttonSelectPicker = new DomElement(By.CssSelector)
        {
            locator = "//button[@class='btn dropdown-toggle selectpicker']"
        };

        private DomElement listsComboboxContainer = new DomElement(By.XPath)
        {
            locator = "//div[@class='modal-content']//ul[@class='dropdown-menu inner ']"
        };
        #endregion //Modal section

        #region constructor
        public AddListModal(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);
        }
        #endregion


        protected void ClickOnAction(ModalContentActions action)
        {
            string locator = string.Empty;

            switch (action)
            {
                case ModalContentActions.Cancel:
                    locator = cancelButtonModal.locator;
                    break;

                case ModalContentActions.MakeDefault:
                    locator = addTolistButtonModal.locator;
                    break;

                default: throw new ArgumentException("Invalid action");
            }

            DomElement actionLink = Container.GetElementWaitXpath(locator);
            actionLink.webElement.Click();
        }

        public List<DomElement> GetOptionsFromChooseListModal()
        {
            List<DomElement> options = Container.GetElementsWaitByXpath(innerSelectOptionsModal.locator);
            return options;
        }

        public void CloseAddToListModal() => this.ClickOnAction(ModalContentActions.Cancel);

        public void ClickOnAddToListModal() => this.ClickOnAction(ModalContentActions.MakeDefault);

        public bool IsSuccessMessageInModal()
        {
            DomElement successMessageInModal = Container.GetElementWaitXpath(successMessageModal.locator);
            if (successMessageInModal.webElement == null)
            {
                return false;
            }
            return true;
        }

        public void SelectList(string list)
        {
            DomElement buttonSelectPicker = Container.GetElementWaitXpath(this.buttonSelectPicker.locator);
            buttonSelectPicker.webElement.Click();
            //TODO: ELIMINAR
            Thread.Sleep(1000);
            DomElement comboBox = Container.GetElementWaitUntilByXpath(listsComboboxContainer.locator,
                (el) => el.Displayed);
            DomElement option = Container.GetElementWaitXpath($"{this.listsComboboxContainer.locator}//*[contains(text(),'{list}')]");
            option.webElement.Click();
            
        }

    }
}
