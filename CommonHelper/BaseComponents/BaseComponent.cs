using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CommonHelper.BaseComponents
{
    public class BaseComponent
    {
        protected IWebDriver Driver;

        #region constructor
        public BaseComponent(IWebDriver driver)
        {
            Driver = driver;
        }
        #endregion

        protected void HoverWebElement(DomElement element)
        {
            Actions action = new Actions(Driver);

            action.MoveToElement(element.webElement).Perform();
        }

        protected DomElement GetElementByHover(DomElement element, DomElement child)
        {
            HoverWebElement(element);

            return element.GetElementWaitByCSS(child.locator);
        }
    }
}