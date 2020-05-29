using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CommonHelper
{
    public class GenericPage
    {
        private IWebDriver driver;
        private IJavaScriptExecutor javascript;
        private WebDriverWait wait;
        public const int defaultWaitTime = 20;

        public GenericPage(IWebDriver browser)
        {
            this.driver = browser;

            javascript = (IJavaScriptExecutor)browser;
            wait = new WebDriverWait(browser, TimeSpan.FromSeconds(defaultWaitTime));
        }

        public IWebElement GetElement(By by)
        {
            return driver.FindElement(by);
        }

        //get web element using explicit wait
        public IWebElement GetElementWait(By by)
        {
            IWebElement element = wait.Until<IWebElement>(el => el.FindElement(by));

            return element;
        }

        public IWebElement GetElementWait(By by, int time)
        {
            wait.Timeout = TimeSpan.FromSeconds(time);

            var element = wait.Until<IWebElement>(el => el.FindElement(by));

            return element;
        }

        public IReadOnlyCollection<IWebElement> GetElementsWait(By by)
        {
            return wait.Until<IReadOnlyCollection<IWebElement>>(el => el.FindElements(by));
        }

        public IWebElement GetElementByJs(string locator)
        {
            string script = string.Format("return {0}", locator);

            return wait.Until<IWebElement>(el => (IWebElement)javascript.ExecuteScript(script));
        }

        public IReadOnlyCollection<IWebElement> GetElementsByJs(string locator)
        {
            string script = string.Format("return {0}", locator);

            var collection = wait.Until<IReadOnlyCollection<IWebElement>>(el => (IReadOnlyCollection<IWebElement>)javascript.ExecuteScript(script));

            return collection;
        }

        public bool ElementExist(By by)
        {
            try
            {
                driver.FindElement(by);

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool ElementExist(By by, int time)
        {
            wait.Timeout = TimeSpan.FromSeconds(time);

            try
            {
                Thread.Sleep(time);

                driver.FindElement(by);

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void SelectMenuItemByHover(IWebElement menu, By optionItem)
        {
            Actions action = new Actions(driver);

            action.MoveToElement(menu).Perform();

            menu.FindElement(optionItem).Click();
        }

        public IWebElement FindByCondition(By by, Func<IWebElement, bool> expectedCondition)
        {
            try
            {
                return wait.Until<IWebElement>(d =>
                {
                    var element = d.FindElement(by);

                    if (expectedCondition(element)) return element;

                    return null;
                });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IWebElement FindByCondition(By by, Func<IWebElement, bool> expectedCondition, int time)
        {
            wait.Timeout = TimeSpan.FromSeconds(time);

            try
            {
                return wait.Until<IWebElement>(d =>
                {
                    var element = d.FindElement(by);

                    if (expectedCondition(element)) return element;

                    return null;
                });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void JavaScript(string query)
        {
            javascript.ExecuteScript(query);
        }

        public string GetJsProperty(string query)
        {
            return javascript.ExecuteScript(query).ToString();
        }
    }
}