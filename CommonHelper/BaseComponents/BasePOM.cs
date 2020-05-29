using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonHelper.BaseComponents
{
    public abstract class BasePOM
    {
        protected IWebDriver Driver;
        protected DomElement BodyContainer = new DomElement(By.CssSelector) { locator = "body" };
        protected DomElement BusyIndicator = new DomElement
        {
            locator = "app-busy-indicator div"
        };

        public BasePOM(IWebDriver driver)
        {
            Driver = driver;
            BodyContainer.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        protected List<DomElement> GetDropdownAutoCompleteOptions(DomElement dropdown)
        {
            dropdown.webElement.Click();

            List<DomElement> options = dropdown.GetElementsWaitByCSS(".ui-select-choices-row-inner");

            return options;
        }

        protected List<DomElement> GetDropdownOptions(DomElement dropdown)
        {
            dropdown.webElement.Click();

            return dropdown.GetElementsWaitByCSS("option");
        }

        protected void SelectDropDownOption(DomElement dropdown, string optionText)
        {
            List<DomElement> searchOptions = dropdown.GetElementsWaitByCSS("option");
            DomElement option = searchOptions.FirstOrDefault(o => o.webElement.Text.Contains(optionText));

            if (option == null)
            {
                throw new NotFoundException($"{optionText} is not found");
            }

            option.webElement.Click();
        }

        protected void SelectDropDownAutoCompleteOption(DomElement dropdown, string optionText)
        {
            List<DomElement> searchOptions = GetDropdownAutoCompleteOptions(dropdown);
            DomElement option = searchOptions.FirstOrDefault(o => o.webElement.Text.Contains(optionText));

            try
            {
                option.webElement.Click();
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException($"{optionText} not found in dropdown");
            }
        }

        public void WaitForAppBusy(int time = 30)
        {
            var busyAnimation = BodyContainer.GetElementWaitUntil(BusyIndicator.locator, webElement =>
            {
                //exit when app busy animation is not displayed
                return !webElement.Displayed;
            }, time);

            if (busyAnimation == null)
            {
                //throw new Exception($"Busy animation is taking too long. Try increasing the actual wait time: {time}, seconds");
            }
        }

        public void ScrollToTop()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }

        protected void SetInputField(DomElement input, string inputText)
        {
            input.webElement.Clear();
            input.webElement.SendKeys(inputText);
        }
    }
}
