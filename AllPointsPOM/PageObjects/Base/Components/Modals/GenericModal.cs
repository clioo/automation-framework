using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base;
using System.Collections.Generic;
using OpenQA.Selenium;
using CommonHelper;
using AllPoints.PageObjects.Base.Components.Modals.Constants;
using System;

namespace AllPoints.PageObjects.Base.Components.Modals
{
    public class GenericModal : ModalBase
    {
        #region generic actions
        private DomElement CloseHeaderButton = new DomElement
        {
            locator = "button.close"
        };
        #endregion generic actions

        #region confirmation specific actions
        private DomElement CancelConfirmationButton = new DomElement
        {
            locator = "span:nth-of-type(1)"
        };
        private DomElement DeleteConfirmationButton = new DomElement
        {
            locator = "span:nth-of-type(2)"
        };
        #endregion confirmation

        #region no edits specific
        private DomElement CloseActionButton = new DomElement { locator = "button.btn" };
        #endregion no edits

        #region content specific
        private DomElement MakeDefaulContentButton = new DomElement
        {
            locator = "span:nth-of-type(1) button"
        };

        private DomElement EditContentButton = new DomElement
        {
            locator = "span:nth-of-type(2) button"
        };

        private DomElement CancelContentButton = new DomElement
        {
            locator = "span:nth-of-type(3) button"
        };

        private DomElement DeleteContentButton = new DomElement
        {
            locator = "span:nth-of-type(4)"
        };
        #endregion content

        private Dictionary<string, DomElement> ModalActions;

        #region constructor
        public GenericModal(IWebDriver driver) : base(driver)
        {
            ModalActions = new Dictionary<string, DomElement>
            {
                {
                    ModalActionsConstants.CloseHeaderAction,
                    CloseHeaderButton
                },
                {
                    ModalActionsConstants.CloseAction,
                    CloseActionButton
                },
                {
                    ModalActionsConstants.CancelConfirmAction,
                    CancelConfirmationButton
                },
                {
                    ModalActionsConstants.DeleteConfirmAction,
                    DeleteConfirmationButton
                },
                {
                    ModalActionsConstants.MakeDefaultContentAction,
                    MakeDefaulContentButton
                },
                {
                    ModalActionsConstants.EditContentAction,
                    EditContentButton
                },
                {
                    ModalActionsConstants.CancelContentAction,
                    CancelContentButton
                },
                {
                    ModalActionsConstants.DeleteContentAction,
                    DeleteContentButton
                }
            };
        }
        #endregion constructor

        public void ClickOnAction(string modalAction)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);

            //TODO:
            //manage the container from search the action to perform
            DomElement modalFooter = Container.GetElementWaitByCSS(ContainerFooter.locator);
            DomElement selectedAction;

            string locator = string.Empty;

            bool actionExist = ModalActions.ContainsKey(modalAction);

            if (actionExist)
            {
                locator = ModalActions[modalAction].locator;
                
                selectedAction = modalFooter.GetElementWaitByCSS(locator);
                selectedAction.webElement.Click();
            }

            throw new ArgumentException("Action not found or invalid");
        }
    }
}
