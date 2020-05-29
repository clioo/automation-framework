using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CommonHelper
{
    public class DomElement
    {
        public By findsBy { get; set; }

        public string locator { get; set; }

        public IWebElement webElement { get; set; }
        private WebDriverWait Wait { get; set; }

        public delegate By ByFunction(string path);

        private ByFunction createBy { get; set; }

        public DomElement()
        {
        }

        public DomElement(ByFunction byFunction)
        {
            createBy = byFunction;
        }

        public void Init(IWebDriver browser, int seconds)
        {
            Wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            findsBy = createBy(locator);
            webElement = Wait.Until(el => el.FindElement(findsBy));
        }

        public List<DomElement> GetElementsWait(string childLocator)
        {
            List<DomElement> children = new List<DomElement>();
            string childrenLocator = locator + " " + childLocator;
            var webElements = Wait.Until<IReadOnlyCollection<IWebElement>>(el =>
               el.FindElements(createBy(childrenLocator)));
            foreach (var element in webElements)
            {
                var newElement = new DomElement
                {
                    locator = childrenLocator,
                    Wait = Wait,
                    webElement = element,
                    createBy = createBy,
                    findsBy = createBy(locator + childLocator)
                };
                children.Add(newElement);
            }
            return children;
        }

        public List<DomElement> GetElementsWaitByCSS(string childLocator)
        {
            List<DomElement> children = new List<DomElement>();
            string childrenLocator = locator + " " + childLocator;
            var webElements = Wait.Until<IReadOnlyCollection<IWebElement>>(el =>
               el.FindElements(createBy(childrenLocator)));
            int count = 0;
            foreach (var element in webElements)
            {
                count++;
                string elementLocator = (webElements.Count > 1) ? childrenLocator + $":nth-child({count})" : childrenLocator;
                var newElement = new DomElement
                {
                    locator = elementLocator,
                    Wait = Wait,
                    webElement = element,
                    createBy = By.CssSelector,
                    findsBy = By.CssSelector(locator + elementLocator)
                };
                children.Add(newElement);
            }
            return children;
        }

        public DomElement GetElementWaitByCSS(string childLocator)
        {
            string elementLocator = locator + " " + childLocator;
            var elementWebElement = Wait.Until(el => el.FindElement(createBy(elementLocator)));
            var newElement = new DomElement
            {
                locator = elementLocator,
                Wait = Wait,
                webElement = elementWebElement,
                createBy = By.CssSelector,
                findsBy = By.CssSelector(elementLocator)
            };
            return newElement;
        }

        public bool IsElementPresent(string childLocator)
        {
            IWebElement element = null;

            try
            {
                element = webElement.FindElement(By.CssSelector(childLocator));
            }
            catch (Exception)
            {
                return false;
            }

            return element.Displayed;
        }

        public DomElement GetElementWaitUntil(string locator, Func<IWebElement, bool> until, int time = 30)
        {
            Wait.Timeout = TimeSpan.FromSeconds(time);
            DomElement result = new DomElement
            {
                locator = locator
            };

            try
            {
                var waitedElement = Wait.Until<IWebElement>(d =>
                {
                    IWebElement element = webElement.FindElement(By.CssSelector(locator));

                    //given conditions returns true
                    if (until(element)) return element;

                    return null;
                });
                result.webElement = waitedElement;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DomElement GetElementWaitXpath(string elementLocator)
        {
            var elementWebElement = Wait.Until(el => el.FindElement(By.XPath(elementLocator)));
            var newElement = new DomElement
            {
                locator = elementLocator,
                Wait = Wait,
                webElement = elementWebElement,
                createBy = By.XPath,
                findsBy = By.XPath(elementLocator)
            };
            return newElement;
        }

        public List<DomElement> GetElementsWaitByXpath(string elementLocator)
        {
            var webElements = Wait.Until(el => el.FindElements(By.XPath(elementLocator)));
            List<DomElement> children = new List<DomElement>();
            foreach (var element in webElements)
            {
                var newElement = new DomElement
                {
                    locator = elementLocator,
                    Wait = Wait,
                    webElement = element,
                    createBy = By.XPath,
                    findsBy = By.XPath(elementLocator)
                };
                children.Add(newElement);
            }
            return children;

        }

        public DomElement GetElementWaitUntilByXpath(string locator, Func<IWebElement, bool> until, int time = 30)
        {
            Wait.Timeout = TimeSpan.FromSeconds(time);
            DomElement result = new DomElement
            {
                locator = locator
            };

            try
            {
                var waitedElement = Wait.Until<IWebElement>(d =>
                {
                    IWebElement element = webElement.FindElement(By.XPath(locator));

                    //given conditions returns true
                    if (until(element)) return element;

                    return null;
                });
                result.webElement = waitedElement;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DomElement GetElementWaitByInnerHTML(string text)
        {
            string elementLocator = $"//a[contains(text(),'{text}')]";
            var elementWebElement = Wait.Until(el => el.FindElement(By.XPath(elementLocator)));
            var newElement = new DomElement
            {
                locator = elementLocator,
                Wait = Wait,
                webElement = elementWebElement,
                createBy = By.XPath,
                findsBy = By.XPath(elementLocator)
            };
            return newElement;
        }


    }
}