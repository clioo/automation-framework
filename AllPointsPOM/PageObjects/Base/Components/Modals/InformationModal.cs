using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base;
using CommonHelper;
using OpenQA.Selenium;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components
{
    public class InformationModal : ModalBase
    {
        private DomElement CloseButton = new DomElement
        {
            locator = "a.close"
        };

        #region constructor
        public InformationModal(IWebDriver driver) : base(driver)
        {
            
        }
        #endregion

        protected void Close()
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);

            DomElement modalHeader = Container.GetElementWaitByCSS(ContainerHeader.locator);            
            modalHeader.GetElementWaitByCSS(CloseButton.locator).webElement.Click();
        }
    }
}