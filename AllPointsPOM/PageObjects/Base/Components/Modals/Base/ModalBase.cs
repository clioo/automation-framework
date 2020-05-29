using CommonHelper;
using OpenQA.Selenium;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base
{
    public class ModalBase
    {
        protected IWebDriver Driver;

        public DomElement Container = new DomElement(By.CssSelector)
        {
            locator = ".modal-dialog .modal-content"
        };
        public DomElement ContainerHeader = new DomElement
        {
            locator = ".modal-header"
        };
        public DomElement ContainerBody = new DomElement
        {
            locator = ".modal-body"
        };
        public DomElement ContainerFooter = new DomElement
        {
            locator = ".modal-footer"
        };

        #region constructor
        public ModalBase(IWebDriver driver)
        {
            Driver = driver;
        }
        #endregion
    }
}