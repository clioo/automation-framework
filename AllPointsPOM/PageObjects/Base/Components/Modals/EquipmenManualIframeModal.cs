using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components.Base;
using CommonHelper;
using OpenQA.Selenium;

namespace AllPointsPOM.PageObjects.Base.Components.Modals
{
    public class EquipmenManualIframeModal : ModalBase
    {
        private DomElement CloseHeaderButton = new DomElement
        {
            locator = "button.btn.btn-default"
        };

        public EquipmenManualIframeModal(IWebDriver driver) : base(driver)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);

            //TODO:
            //manage the container from search the action to perform

        }

        public bool ModalMessage(string expectedContent)
        {

            string messageBody = Container.GetElementWaitByCSS(ContainerBody.locator).webElement.Text;
            //ContainerBody.webElement.Text;

            System.Console.WriteLine("Modal body content: " + messageBody);

            return messageBody.Contains(expectedContent);
        }

        public void closeModal()
        {
            DomElement modalFooter = Container.GetElementWaitByCSS(ContainerFooter.locator);
            modalFooter.webElement.FindElement(By.CssSelector(".btn.btn-default")).Click();

        }
    }
}
