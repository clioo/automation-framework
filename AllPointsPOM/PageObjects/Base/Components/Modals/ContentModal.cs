using AllPoints.PageObjects.GenericWebPage.SharedElements.Modals.Enums;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base;
using AllPoints.Pages;
using CommonHelper;
using OpenQA.Selenium;
using System;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components
{
    public class ContentModal : ModalBase
    {
        #region main elements
        private DomElement MakeDefaultLink = new DomElement
        {
            locator = "span:nth-of-type(1) button"
        };

        private DomElement EditLink = new DomElement
        {
            locator = "span:nth-of-type(2) button"
        };

        private DomElement CancelLink = new DomElement
        {
            locator = "span:nth-of-type(3) button"
        };

        private DomElement DeleteLink = new DomElement
        {
            locator = "span:nth-of-type(4)"
        };
        #endregion

        #region constructor
        public ContentModal(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }
        #endregion

        protected void ClickOnAction(ModalContentActions action)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);

            DomElement modalFooter = Container.GetElementWaitByCSS(ContainerFooter.locator);
            DomElement actionLink;

            string locator = string.Empty;

            switch (action)
            {
                case ModalContentActions.MakeDefault:
                    locator = MakeDefaultLink.locator;
                    break;

                case ModalContentActions.Edit:
                    locator = EditLink.locator;
                    break;

                case ModalContentActions.Delete:
                    locator = DeleteLink.locator;
                    break;

                case ModalContentActions.Cancel:
                    locator = CancelLink.locator;
                    break;

                default: throw new ArgumentException("Invalid action");
            }

            actionLink = modalFooter.GetElementWaitByCSS(locator);
            actionLink.webElement.Click();
        }        
    }
}