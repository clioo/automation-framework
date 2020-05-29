using AllPoints.PageObjects.Base.Components.Header;
using AllPoints.PageObjects.Base.Components.Header.Enums;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.GenericWebPage.SharedElements;
using AllPoints.PageObjects.MyAccountPOM;
using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.PageObjects.MyAccountPOM.DashboardPOM;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.PageObjects.ListPOM.HomePagePOM;
using AllPointsPOM.PageObjects.QuickOrderPOM;
using CommonHelper;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using CommonHelper.BaseComponents;
using System;
using System.Threading;

namespace AllPoints.Pages.Components
{
    public class Header : BaseHeader
    { 
        private int cartQuantity;

        #region locators
        //Gets the quantity in the cart
        private DomElement miniCartItemQuantity = new DomElement(By.XPath)
        {
            locator = "//button[@class='btn btn-bare btn-icon-left minicart mini-cart-link']//span[@class='badge badge-danger-minicart']"
        };

        //search section container
        DomElement SearchSectionContainer = new DomElement
        {
            locator = ".brand-promo .container"
        };
        DomElement LogoContainerItem = new DomElement
        {
            locator = ".logo a"
        };
        DomElement SearchSection = new DomElement
        {
            locator = ".product-search-container"
        };
        DomElement DropdownContainer = new DomElement
        {
            locator = ".product-search .dropdown.bootstrap-select"
        };
        DomElement DropdownButton = new DomElement
        {
            locator = "button.dropdown-toggle"
        };
        DomElement DropdownOption = new DomElement
        {
            locator = "ul li a"
        };
        DomElement TextField = new DomElement
        {
            locator = ".search-input"
        };
        DomElement SubmitButton = new DomElement
        {
            locator = ".search-button"
        };
        DomElement lastItemAdded = new DomElement(By.XPath)
        {
            locator = "//div/article//h2"
        };

        #endregion locators

        #region main elements
        //TODO
        //utility menu will be exposed on a different class apart
        private PrimaryMenu PrimaryMenu = new PrimaryMenu();

        private CategoriesMenu CategoriesMenu = new CategoriesMenu();

        private EquipmentManualsWidget equipmentManuals;
        private EquipmentManualIframe equipmentManualIframe;
        #endregion

        #region constructor
        public Header(IWebDriver driver) : base(driver)
        {
            Driver = driver;

            Container.Init(driver, SeleniumConstants.defaultWaitTime);
            equipmentManuals = new EquipmentManualsWidget(Driver);
        }
        #endregion

        #region my account menu public methods
        public APLoginPage ClickOnSignIn()
        {
            DomElement primaryMenuContainer = Container.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainer.locator);
            DomElement primaryMenuContainerNav = primaryMenuContainer.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNav.locator);
            DomElement primaryMenuContainerNavSignIn = primaryMenuContainerNav.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNavSignInButton.locator);

            primaryMenuContainerNavSignIn.webElement.Click();

            return new APLoginPage(Driver);
        }

        public ContactInfoHomePage ClickOnContactInfo()
        {
            ClickOnAnyMyAccountMenuOption(MyAccountMenuItems.ContactInfo);

            return new ContactInfoHomePage(Driver);
        }

        public AddressesHomePage ClickOnAddresses()
        {
            ClickOnAnyMyAccountMenuOption(MyAccountMenuItems.Addresses);

            return new AddressesHomePage(Driver);
        }

        public PaymentOptionsHomePage ClickOnPaymentOptions()
        {
            ClickOnAnyMyAccountMenuOption(MyAccountMenuItems.Payments);

            return new PaymentOptionsHomePage(Driver);
        }

        public DashboardHomePage ClickOnDashboard()
        {
            ClickOnAnyMyAccountMenuOption(MyAccountMenuItems.Dashboard);

            return new DashboardHomePage(Driver);
        }

        public OrdersHomePage ClickOnOrders()
        {
            ClickOnAnyMyAccountMenuOption(MyAccountMenuItems.Orders);
            return new OrdersHomePage(Driver);
        }

        public QuickOrdersHomePage ClickOnQuickOrder()
        {
            DomElement primaryMenuContainer = Container.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainer.locator);
            DomElement quickorderClick = primaryMenuContainer.GetElementWaitByCSS(PrimaryMenu.QuickOrderMenuItem.locator);
            quickorderClick.webElement.Click();
            return new QuickOrdersHomePage(Driver);
        }

        public APIndexPage ClickOnSignOut()
        {
            ClickOnAnyMyAccountMenuOption(MyAccountMenuItems.SignOut);
            return new APIndexPage(Driver);
        }

        public APListHomePage ClickOnLists()
        {
            ClickOnAnyMyAccountMenuOption(MyAccountMenuItems.Lists);
            return new APListHomePage(Driver);
        }

        public bool AccountMenuExist()
        {
            //return browserHelper.ElementExist(myAccountMenuItem.findsBy);
            //TODO:
            return false;
        }
        #endregion my account methods

        #region cart public methods
        public APCartPage ClickOnViewCart()
        {

            DomElement primaryMenuContainer = Container.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainer.locator);
            DomElement primaryMenuContainerNav = primaryMenuContainer.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNav.locator);
            DomElement primaryMenuContainerNavLiCart = primaryMenuContainerNav.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNavLiCart.locator);
            DomElement primaryMenuContainerNavLiCartButton = primaryMenuContainerNav.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNavLiCartButton.locator);

            //void action
            HoverWebElement(primaryMenuContainerNavLiCartButton);

            DomElement minicartContainer = primaryMenuContainerNavLiCart.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNavLiCartMinicart.locator);
            DomElement minicartContainerActions = minicartContainer.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNavLiCartMinicartActions.locator);
            DomElement minicartContainerViewCartButton = minicartContainerActions.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNavLiCartMinicartActionsViewCartButton.locator);

            minicartContainerViewCartButton.webElement.Click();

            return new APCartPage(Driver);
        }
        #endregion cart

        #region search manufacturer section public
        public void SelectManufacturer(string manufacturer)
        {
            var searchSectionContainer = Container.GetElementWaitByCSS(SearchSectionContainer.locator);
            var searchSection = searchSectionContainer.GetElementWaitByCSS(SearchSection.locator);
            var dropdownContainer = searchSection.GetElementWaitByCSS(DropdownContainer.locator);
            var dropdownOpener = dropdownContainer.GetElementWaitByCSS(DropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.FirstOrDefault(item => item.webElement.Text.Equals(manufacturer));

            if (optionItem == null) throw new NotFoundException($"{manufacturer} is not found in the dropdown");

            optionItem.webElement.Click();
        }

        public void SelectManufacturer(int manufacturer)
        {
            var searchSectionContainer = Container.GetElementWaitByCSS(SearchSectionContainer.locator);
            var searchSection = searchSectionContainer.GetElementWaitByCSS(SearchSection.locator);
            var dropdownContainer = searchSection.GetElementWaitByCSS(DropdownContainer.locator);
            var dropdownOpener = dropdownContainer.GetElementWaitByCSS(DropdownButton.locator);

            //open the dropdown clicking on the button
            dropdownOpener.webElement.Click();

            var optionItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator);

            var optionItem = optionItems.ElementAtOrDefault(manufacturer);

            if (optionItem == null) throw new NotFoundException($"{manufacturer} is not found in the dropdown");

            optionItem.webElement.Click();
        }

        public List<DomElement> GetManufacturerOptions()
        {
            throw new NotImplementedException("This method is deprecated, use GetManufacturerDropdownOptions()");
        }

        public List<string> GetManufacturerDropdownOptions()
        {
            var searchSectionContainer = Container.GetElementWaitByCSS(SearchSectionContainer.locator);
            var searchSection = searchSectionContainer.GetElementWaitByCSS(SearchSection.locator);

            var dropdownContainer = searchSection.GetElementWaitByCSS(DropdownContainer.locator);
            var dropdownHandler = dropdownContainer.GetElementWaitByCSS(DropdownButton.locator);

            //open the dropdown
            dropdownHandler.webElement.Click();

            List<string> dropdownItems = dropdownContainer.GetElementsWaitByCSS(DropdownOption.locator)
                .Select(el => el.webElement.Text)
                .ToList();

            return dropdownItems;
        }

        public void SetSearchFieldText(string text)
        {
            var searchSectionContainer = Container.GetElementWaitByCSS(SearchSectionContainer.locator);
            var searchSection = searchSectionContainer.GetElementWaitByCSS(SearchSection.locator);

            var textField = searchSection.GetElementWaitByCSS(TextField.locator);

            textField.webElement.SendKeys(text);
        }

        public CatalogItemsPage ClickOnSearchButton()
        {
            var searchSectionContainer = Container.GetElementWaitByCSS(SearchSectionContainer.locator);
            var searchSection = searchSectionContainer.GetElementWaitByCSS(SearchSection.locator);

            var searchButton = searchSection.GetElementWaitByCSS(SubmitButton.locator);
            searchButton.webElement.Click();

            //TODO
            //if textfield has no value
            //then return null
            //else return a new instance of catalog page
            return new CatalogItemsPage(Driver);
        }
        #endregion search manufacturer

        public string GetPhoneNumber()
        {
            DomElement utilityMenuContainer = Container.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainer.locator);
            DomElement contactSection = utilityMenuContainer.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerContact.locator);
            DomElement phoneNumber = contactSection.GetElementWaitByCSS(PrimaryMenu.ContactSectionPhoneNumber.locator);
            //tweak
            IJavaScriptExecutor javaScript = (IJavaScriptExecutor)Driver;
            string phoneNumberText = (string)javaScript.ExecuteScript($"return document.querySelector('{phoneNumber.locator}').textContent");
            return phoneNumberText;
        }

        public CatalogItemsPage ClickOnCategory(string categoryName)
        {
            DomElement categoryElement = Container.GetElementWaitXpath("(//a[contains(text(),'" + categoryName + "')])[2]");
            categoryElement.webElement.Click();
            return new CatalogItemsPage(base.Driver);
        }

        public string GetLastItemAddedToCart()
        {
            DomElement lastItemAddedName = Container.GetElementWaitXpath(lastItemAdded.locator);
            return lastItemAddedName.webElement.GetAttribute("innerHTML");
        }

        public int GetMiniCartQuantity(int before = 0)
        {
            UpdateCartQuantity();
            if (this.cartQuantity <= before) UpdateCartQuantity();
            return this.cartQuantity;
        }

        public void UpdateCartQuantity()
        {
            Thread.Sleep(2500);
            DomElement miniCartItemQuantityElement = this.Container.GetElementWaitXpath(
                miniCartItemQuantity.locator);
            this.cartQuantity = int.Parse(miniCartItemQuantityElement.webElement.Text);

        }

        //manual manufacturer
        public void selectEquipmentManualManufacturer(string manufacturer)
        {
            equipmentManuals.SelectManufacturer(manufacturer);
        }

        public void selectEquipmentManualManufacturer(int manufacturer)
        {
            equipmentManuals.SelectManufacturer(manufacturer);
        }


        //manual model
        public void selectEquipmentManualModel(string model)
        {
            equipmentManuals.SelectModel(model);
        }

        public void selectEquipmentManualModel(int model)
        {
            equipmentManuals.SelectModel(model);
        }

        //manual doc type
        public void selectEquipmentDocType(string docType)
        {
            equipmentManuals.SelectDocType(docType);
        }

        public void selectEquipmentDocType(int docType)
        {
            equipmentManuals.SelectDocType(docType);
        }

        #region my account menu private
        private void ClickOnAnyMyAccountMenuOption(string menuItem)
        {
            DomElement primaryMenuContainer = Container.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainer.locator);
            DomElement primaryMenuContainerNav = primaryMenuContainer.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNav.locator);
            DomElement primaryMenuContainerNavMyAccount = primaryMenuContainerNav.GetElementWaitByCSS(PrimaryMenu.PrimaryMenuContainerNavAccountMenu.locator);

            DomElement menuOptionElement = GetMenuItemByHover(primaryMenuContainerNavMyAccount, menuItem);

            menuOptionElement.webElement.Click();
        }

        private DomElement GetMenuItemByHover(DomElement menu, string menuItemText)
        {
            HoverWebElement(menu);

            List<DomElement> menuItems = menu.GetElementsWaitByCSS("li:not(.divider)");

            DomElement foundItem = menuItems.FirstOrDefault(item => item.webElement.Text.Contains(menuItemText));

            if (foundItem == null) throw new NotFoundException($"Menu item, '{menuItemText}' is not found");

            return foundItem;
        }
        #endregion my account private

        #region categories
        public void ClickOnParentCategory(string categoryName)
        {
            //TODO:
        }

        public void ClickOnSubcategory(string subcategoryName)
        {
            //TODO:
        }
        #endregion categories

        #region equipment manual menu public methods
        public void AddToCartPartNumberEqpManIframe(string partNum)
        {
            equipmentManualIframe = new EquipmentManualIframe(Driver);
            equipmentManualIframe.AddToCartPartNumber(partNum);
        }
        public void showEquipmentManualsSubmenu()
        {
            equipmentManuals.clickWidget();
        }
        #endregion
    }
}