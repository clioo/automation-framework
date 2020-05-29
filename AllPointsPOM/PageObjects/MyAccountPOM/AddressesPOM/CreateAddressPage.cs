using AllPoints.PageObjects.MyAccountPOM.AddressesPOM;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Modals;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.Pages
{
    public class AddAddressPage : AllPointsBaseWebPage
    {
        private DomElement Container = new DomElement(By.CssSelector)
        {
            locator = "article.container .detail-section"
        };
        private DomElement AddressForm = new DomElement
        {
            locator = ".address form"
        };
        private DomElement CountrySelector = new DomElement
        {
            locator = ".ui-select-container.ui-select-bootstrap.dropdown.ng-not-empty.ng-valid.ng-valid-required"
        };
        private DomElement CountrySelectedLabel = new DomElement
        {
            locator = "span.ng-scope"
        };
        private DomElement CountryOptionItems = new DomElement
        {
            locator = "div[ng-attr-id]"
        };
        private DomElement DefaultCheckbox = new DomElement
        {
            locator = ".checkbox label"
        };

        #region web elements
        private DomElement sectionTitle = new DomElement
        {
            locator = "h1"
        };
        private DomElement CompanyNameInput = new DomElement
        {
            locator = "input[name='Name']"
        };
        private DomElement streetSelectDropdownlist = new DomElement
        {
            locator = "input[name='AddressLine1']"
        };
        private DomElement aptNumberInput = new DomElement
        {
            locator = "input[name='AddressLine2']"
        };
        private DomElement cityInput = new DomElement
        {
            locator = "input[name='City']"
        };
        private DomElement stateSelect = new DomElement
        {
            locator = "input[name='ProvinceRegion']"
        };
        private DomElement postalInput = new DomElement
        {
            locator = "input[name='Postal']"
        };
        private DomElement defaultCheck = new DomElement
        {
            locator = "input[type='checkbox'][name='IsDefaultCheck']"
        };
        private DomElement addNewButton = new DomElement
        {
            locator = "#submit"
        };
        private DomElement cancelButton = new DomElement
        {
            locator = "#cancel"
        };
        #endregion web elements

        //modals
        public AddressesInformationModal InformationModal;

        #region constructor
        public AddAddressPage(IWebDriver driver) : base(driver)
        {
            InformationModal = new AddressesInformationModal(driver);
            Container.Init(driver, SeleniumConstants.defaultWaitTime);

            this.InitializeElements();
        }
        #endregion constructor

        #region elements init finds by
        private void InitializeElements()
        {
            this.sectionTitle.findsBy = By.CssSelector(this.sectionTitle.locator);

            this.streetSelectDropdownlist.findsBy = By.CssSelector(this.streetSelectDropdownlist.locator);

            this.aptNumberInput.findsBy = By.CssSelector(this.aptNumberInput.locator);

            this.cityInput.findsBy = By.CssSelector(this.cityInput.locator);

            this.stateSelect.findsBy = By.CssSelector(this.stateSelect.locator);

            this.postalInput.findsBy = By.CssSelector(this.postalInput.locator);

            this.defaultCheck.findsBy = By.CssSelector(this.defaultCheck.locator);

            this.cancelButton.findsBy = By.CssSelector(this.cancelButton.locator);

            this.addNewButton.findsBy = By.CssSelector(this.addNewButton.locator);
        }
        #endregion elements init finds by

        public string GetSectionTitle()
        {
            return Helper.GetElementWait(this.sectionTitle.findsBy).Text;
        }

        public void SetInputValue(AddressInputs input, string value)
        {
            var addressForm = Container.GetElementWaitByCSS(AddressForm.locator);

            switch (input)
            {
                case AddressInputs.Country:
                    var countryInput = addressForm.GetElementWaitByCSS(CountrySelector.locator);
                    countryInput.webElement.Click();
                    var options = countryInput.GetElementsWaitByCSS(CountryOptionItems.locator);
                    var option = options.FirstOrDefault(el => el.webElement.Text.Contains(value));
                    option.webElement.Click();
                    break;

                case AddressInputs.CompanyName:
                    var companyNameField = addressForm.GetElementWaitByCSS(CompanyNameInput.locator);
                    companyNameField.webElement.Clear();
                    companyNameField.webElement.SendKeys(value);
                    break;

                case AddressInputs.State:
                    var stateField = addressForm.GetElementWaitByCSS(stateSelect.locator);
                    stateField.webElement.Clear();
                    stateField.webElement.SendKeys(value);
                    break;

                case AddressInputs.City:
                    var cityField = addressForm.GetElementWaitByCSS(cityInput.locator);
                    cityField.webElement.Clear();
                    cityField.webElement.SendKeys(value);
                    break;

                case AddressInputs.Street:
                    var streetField = addressForm.GetElementWaitByCSS(streetSelectDropdownlist.locator);
                    streetField.webElement.Clear();
                    streetField.webElement.SendKeys(value);
                    break;

                case AddressInputs.Postal:
                    var postalField = addressForm.GetElementWaitByCSS(postalInput.locator);
                    postalField.webElement.Clear();
                    postalField.webElement.SendKeys(value);
                    break;

                case AddressInputs.Apartment:
                    var apartmentField = addressForm.GetElementWaitByCSS(aptNumberInput.locator);
                    if (!string.IsNullOrEmpty(value))
                    {
                        apartmentField.webElement.Clear();
                        apartmentField.webElement.SendKeys(value);
                    }
                    break;

                default: throw new ArgumentException($"{input} is not supported");
            }
        }

        public IEnumerable<string> GetCountriesFromDropdown()
        {
            var addressForm = Container.GetElementWaitByCSS(AddressForm.locator);
            var addressSelector = addressForm.GetElementWaitByCSS(CountrySelector.locator);

            //open the dropdown in order to see the items
            addressSelector.webElement.Click();

            var items = addressSelector.GetElementsWaitByCSS(CountryOptionItems.locator);

            return items.Select(el => el.webElement.Text);
        }

        public string GetCurrentCountryValue()
        {
            var addressForm = Container.GetElementWaitByCSS(AddressForm.locator);
            var countrySelector = addressForm.GetElementWaitByCSS(CountrySelector.locator);
            var currentCountry = countrySelector.GetElementWaitByCSS(CountrySelectedLabel.locator);
            string value = currentCountry.webElement.Text;

            return value;
        }

        public void SetStreetAutoComplete(string street, string city, string state, string country)
        {
            string fullAddress = string.Format($"{street} {city} {state} {country}");

            Actions action = new Actions(Driver);

            this.streetSelectDropdownlist.webElement = Helper.GetElementWait(this.streetSelectDropdownlist.findsBy);

            this.streetSelectDropdownlist.webElement.SendKeys(fullAddress);

            Thread.Sleep(1500);

            //select the first element
            action.SendKeys(Keys.ArrowDown)
                .SendKeys(Keys.Enter)
                .Build();

            action.Perform();

            Thread.Sleep(1500);
        }

        public void SetCheckboxDefault()
        {
            var addressForm = Container.GetElementWaitByCSS(AddressForm.locator);
            var defaultCheckbox = addressForm.GetElementWaitByCSS(DefaultCheckbox.locator);

            defaultCheckbox.webElement.Click();
        }

        public AddressesHomePage ClickOnSubmit()
        {
            this.addNewButton.webElement = Helper.GetElementWait(this.addNewButton.findsBy);

            this.sectionTitle.webElement = Helper.GetElementWait(this.sectionTitle.findsBy);

            addNewButton.webElement.Click();

            //if page is add address
            if (this.sectionTitle.webElement.Text.Equals("Create New Address"))
            {
                InformationModal.ClickOnClose();
            }

            return new AddressesHomePage(Driver);
        }

        public AddressesInformationModal ClickOnSubmitt()
        {
            var sectionContainer = BodyContainer.GetElementWaitByCSS(Container.locator);
            var form = sectionContainer.GetElementWaitByCSS(AddressForm.locator);
            var submitButton = form.GetElementWaitByCSS(addNewButton.locator);

            submitButton.webElement.Click();

            return new AddressesInformationModal(Driver);
        }

        public void ClickOnCancelButton()
        {
            cancelButton.webElement.Click();
        }

        public bool AddNewButtonIsEnabled()
        {
            this.addNewButton.webElement = Helper.GetElementWait(this.addNewButton.findsBy);

            return this.addNewButton.webElement.Enabled;
        }
    }
}