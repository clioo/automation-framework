using CommonHelper;
using OpenQA.Selenium;
using System.Linq;

namespace AllPoints.Pages.Components
{
    public class EquipmentManualsWidget
    {
        private DomElement Container = new DomElement(By.CssSelector) { locator = "header" };
        private DomElement DropdownOption = new DomElement() { locator = "ul li a" };

        #region Equipment Manual button
        private DomElement EquipmentManualWidgetButton = new DomElement() { locator = "button.davisware-show-ctls" };
        #endregion

        #region containers
        private DomElement EquipmentManualWidgetContainer = new DomElement() { locator = "div .davisware-ctls" };

        private DomElement ManufacturerDropdownContainer = new DomElement() { locator = "div.davisware-mfg div.dropdown-menu.open" };

        private DomElement ModelDropdownContainer = new DomElement() { locator = "div.davisware-mdl div.dropdown-menu.open" };

        private DomElement DocTypeDropdownContainer = new DomElement() { locator = "div.davisware-doc div.dropdown-menu.open" };
        #endregion

        #region equipment manual submenus buttons
        private DomElement ManufacturerDropdownButton = new DomElement()
        {
            locator = "button[data-id='davisware-mfg']"
        };

        private DomElement ModelDropdownButton = new DomElement
        {
            locator = "button[data-id='davisware-mdl']"
        };

        private DomElement DocTypeDropdownButton = new DomElement
        {
            locator = "button[data-id='davisware-doc']"
        };
        #endregion

        public EquipmentManualsWidget(IWebDriver driver)
        {
            Container.Init(driver, SeleniumConstants.defaultWaitTime);
        }
        public void clickWidget()
        {
            var equipmentManualOpener = Container.GetElementWaitByCSS(EquipmentManualWidgetButton.locator);
            //click on the equipment manual button for sub menu display
            equipmentManualOpener.webElement.Click();
        }

        #region Search submenu slects
        public void SelectManufacturer(string manufacturer)
        {
            var equipmentManualWidgetContainer = Container.GetElementWaitByCSS(EquipmentManualWidgetContainer.locator);
            var dropdownContainer = equipmentManualWidgetContainer.GetElementWaitByCSS(ManufacturerDropdownContainer.locator);
            var dropdownOpener = equipmentManualWidgetContainer.GetElementWaitByCSS(ManufacturerDropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.FirstOrDefault(item => item.webElement.Text.Equals(manufacturer));

            if (optionItem == null) throw new NotFoundException($"{manufacturer} is not found in the dropdown");

            optionItem.webElement.Click();


        }

        public void SelectManufacturer(int manufacturer)
        {
            var equipmentManualWidgetContainer = Container.GetElementWaitByCSS(EquipmentManualWidgetContainer.locator);
            var dropdownContainer = equipmentManualWidgetContainer.GetElementWaitByCSS(ManufacturerDropdownContainer.locator);
            var dropdownOpener = equipmentManualWidgetContainer.GetElementWaitByCSS(ManufacturerDropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.ElementAtOrDefault(manufacturer);

            if (optionItem == null) throw new NotFoundException($"{manufacturer} is not found in the dropdown");

            optionItem.webElement.Click();
        }

        public void SelectModel(string model)
        {
            var equipmentManualWidgetContainer = Container.GetElementWaitByCSS(EquipmentManualWidgetContainer.locator);
            var dropdownContainer = equipmentManualWidgetContainer.GetElementWaitByCSS(ModelDropdownContainer.locator);
            var dropdownOpener = equipmentManualWidgetContainer.GetElementWaitByCSS(ModelDropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            // dropdownContainer.GetElementWaitUntil("", d => d.FindElement(ModelDropdownContainer.));

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.FirstOrDefault(item => item.webElement.Text.Equals(model));

            if (optionItem == null) throw new NotFoundException($"{model} is not found in the dropdown");

            optionItem.webElement.Click();
        }

        public void SelectModel(int model)
        {
            var equipmentManualWidgetContainer = Container.GetElementWaitByCSS(EquipmentManualWidgetContainer.locator);
            var dropdownContainer = equipmentManualWidgetContainer.GetElementWaitByCSS(ModelDropdownContainer.locator);
            var dropdownOpener = equipmentManualWidgetContainer.GetElementWaitByCSS(ModelDropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.ElementAtOrDefault(model);

            if (optionItem == null) throw new NotFoundException($"{model} is not found in the dropdown");

            optionItem.webElement.Click();
        }

        public void SelectDocType(string docType)
        {
            var equipmentManualWidgetContainer = Container.GetElementWaitByCSS(EquipmentManualWidgetContainer.locator);
            var dropdownContainer = equipmentManualWidgetContainer.GetElementWaitByCSS(DocTypeDropdownContainer.locator);
            var dropdownOpener = equipmentManualWidgetContainer.GetElementWaitByCSS(DocTypeDropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.FirstOrDefault(item => item.webElement.Text.Equals(docType));

            if (optionItem == null) throw new NotFoundException($"{docType} is not found in the dropdown");

            optionItem.webElement.Click();
        }

        public void SelectDocType(int docType)
        {
            var equipmentManualWidgetContainer = Container.GetElementWaitByCSS(EquipmentManualWidgetContainer.locator);
            var dropdownContainer = equipmentManualWidgetContainer.GetElementWaitByCSS(ModelDropdownContainer.locator);
            var dropdownOpener = equipmentManualWidgetContainer.GetElementWaitByCSS(ModelDropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.ElementAtOrDefault(docType);

            if (optionItem == null) throw new NotFoundException($"{docType} is not found in the dropdown");

            optionItem.webElement.Click();
        }
        #endregion
    }
}
