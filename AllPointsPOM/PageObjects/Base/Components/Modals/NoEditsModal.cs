using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonHelper;
using AllPoints.PageObjects.Base.Components.Modals.Enums;

namespace AllPoints.PageObjects.Base.Components.Modals
{
    public class NoEditsModal : ModalBase
    {
        #region modal actions
        private DomElement CloseHeaderButton = new DomElement
        {
            locator = "button.close"
        };
        private DomElement CloseFooterButton = new DomElement
        {
            locator = "button.btn"
        };
        #endregion modal actions

        #region constructor
        public NoEditsModal(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }
        #endregion constructor

        protected void ClickOnAction(ModalNoEditsActions selectedAction)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);

            string locator = string.Empty;
            DomElement modalSelectedAction = new DomElement();

            switch (selectedAction)
            {
                case ModalNoEditsActions.CloseFromHeader:
                    modalSelectedAction = Container.GetElementWaitByCSS(ContainerHeader.locator)
                        .GetElementWaitByCSS(CloseHeaderButton.locator);
                    break;

                case ModalNoEditsActions.CloseOnFooter:
                    modalSelectedAction = Container.GetElementWaitByCSS(ContainerFooter.locator)
                        .GetElementWaitByCSS(CloseFooterButton.locator);
                    break;

                default: throw new ArgumentException("Invalid action");
            }

            modalSelectedAction.webElement.Click();
        }
    }
}
