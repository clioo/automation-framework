using AllPoints.PageObjects.GenericWebPage.SharedElements.Modals.Enums;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base;
using CommonHelper;
using OpenQA.Selenium;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components
{
    public class ConfirmationModal : ModalBase
    {
        private DomElement CancelLink = new DomElement
        {
            locator = "span:nth-of-type(1)"
        };

        private DomElement DeleteLink = new DomElement
        {
            locator = "span:nth-of-type(2)"
        };

        private DomElement CloseButton = new DomElement
        {
            locator = "button.close"
        };

        public ConfirmationModal(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        protected void ClickAnyAction(ModalConfirmationActions action)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);

            DomElement modalFooterContainer = Container.GetElementWaitByCSS(ContainerFooter.locator);
            DomElement modalHeaderContainer = Container.GetElementWaitByCSS(ContainerHeader.locator);

            string locator = string.Empty;
            DomElement modalSelectedAction;

            switch (action)
            {
                case ModalConfirmationActions.Close:
                    DomElement modalCloseButton = modalHeaderContainer.GetElementWaitByCSS(CloseButton.locator);
                    modalCloseButton.webElement.Click();

                    break;

                case ModalConfirmationActions.Delete:
                    locator = DeleteLink.locator;

                    modalSelectedAction = modalFooterContainer.GetElementWaitByCSS(locator);

                    modalSelectedAction.webElement.Click();

                    break;

                case ModalConfirmationActions.Cancel:
                    locator = CancelLink.locator;

                    modalSelectedAction = modalFooterContainer.GetElementWaitByCSS(locator);

                    modalSelectedAction.webElement.Click();

                    break;
            }
        }
    }
}