using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Modals;
using AllPoints.PageObjects.NewFolder1;
using AllPoints.Pages;
using CommonHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM
{
    public class EditAddressPage : AllPointsBaseWebPage
    {
        #region web elements
        private DomElement Container = new DomElement(By.CssSelector)
        {
            locator = "article.container .detail-section"
        };
        private DomElement SectionTitle = new DomElement
        {
            locator = "h1"
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
        private DomElement CompanyNameInput = new DomElement
        {
            locator = "input[name='Name']"
        };
        private DomElement DefaultCheckbox = new DomElement
        {
            locator = ".checkbox label"
        };
        private DomElement StreetSelectDropdownlist = new DomElement
        {
            locator = "input[name='AddressLine1']"
        };
        private DomElement AptNumberInput = new DomElement
        {
            locator = "input[name='AddressLine2']"
        };
        private DomElement CityInput = new DomElement
        {
            locator = "input[name='City']"
        };
        private DomElement StateSelect = new DomElement
        {
            locator = "[ng-model='address.StateProvinceRegion']"
        };
        private DomElement PostalInput = new DomElement
        {
            locator = "input[name='Postal']"
        };
        private DomElement SubmitButton = new DomElement
        {
            locator = "#submit"
        };
        private DomElement CancelButton = new DomElement
        {
            locator = "#cancel"
        };
        #endregion web elements

        #region modals
        public AddressesInformationModal InformationModal;
        public AddressesNoEditsModal NoEditsModal;
        #endregion modals

        #region constructor
        public EditAddressPage(IWebDriver driver) : base(driver)
        {
            InformationModal = new AddressesInformationModal(driver);
            NoEditsModal = new AddressesNoEditsModal(driver);

            Container.Init(driver, SeleniumConstants.defaultWaitTime);

            //TODO:
            //stop using this method and replace it with containers
            this.InitializeElements();
        }
        #endregion constructor

        #region elements init finds by
        private void InitializeElements()
        {
            this.StreetSelectDropdownlist.findsBy = By.CssSelector(this.StreetSelectDropdownlist.locator);
        }
        #endregion elements init finds by

        public string GetSectionTitle()
        {
            //TODO:
            //needs refactor to stop using 'Helper'
            //get the element through containers
            return Helper.GetElementWait(SectionTitle.findsBy).Text;
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
                    var stateField = addressForm.GetElementWaitByCSS(StateSelect.locator);
                    stateField.webElement.Clear();
                    stateField.webElement.SendKeys(value);
                    break;

                case AddressInputs.City:
                    var cityField = addressForm.GetElementWaitByCSS(CityInput.locator);
                    cityField.webElement.Clear();
                    cityField.webElement.SendKeys(value);
                    break;

                case AddressInputs.Street:
                    var streetField = addressForm.GetElementWaitByCSS(StreetSelectDropdownlist.locator);
                    streetField.webElement.Clear();
                    streetField.webElement.SendKeys(value);
                    break;

                case AddressInputs.Postal:
                    var postalField = addressForm.GetElementWaitByCSS(PostalInput.locator);
                    postalField.webElement.Clear();
                    postalField.webElement.SendKeys(value);
                    break;

                case AddressInputs.Apartment:
                    var apartmentField = addressForm.GetElementWaitByCSS(AptNumberInput.locator);
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

        //with no autocomplete
        public void SetStreet(string street)
        {
            this.StreetSelectDropdownlist.webElement = Helper.GetElementWait(this.StreetSelectDropdownlist.findsBy);

            this.StreetSelectDropdownlist.webElement.Clear();

            this.StreetSelectDropdownlist.webElement.SendKeys(street);
        }

        public void SetApartment(string apartment)
        {
            this.AptNumberInput.webElement = Helper.GetElementWait(this.AptNumberInput.findsBy);

            this.AptNumberInput.webElement.Clear();

            if (!string.IsNullOrEmpty(apartment) || !string.IsNullOrWhiteSpace(apartment))
            {
                this.AptNumberInput.webElement.SendKeys(apartment);
            }
        }

        public void SetCheckboxDefault()
        {
            var addressForm = Container.GetElementWaitByCSS(AddressForm.locator);
            var defaultCheckbox = addressForm.GetElementWaitByCSS(DefaultCheckbox.locator);

            defaultCheckbox.webElement.Click();
        }

        public AddressesHomePage ClickOnSubmit()
        {
            var addressForm = Container.GetElementWaitByCSS(AddressForm.locator);
            var submitButton = addressForm.GetElementWaitByCSS(SubmitButton.locator);
            var sectionTitle = Container.GetElementWaitByCSS(SectionTitle.locator);

            submitButton.webElement.Click();

            if (sectionTitle.webElement.Text.Equals("Create New Address"))
            {
                InformationModal.ClickOnClose();
            }

            return new AddressesHomePage(Driver);
        }

        public void ClickOnCancelButton()
        {
            var addressForm = Container.GetElementWaitByCSS(AddressForm.locator);
            var cancelButton = addressForm.GetElementWaitByCSS(CancelButton.locator);
            var sectionTitle = Container.GetElementWaitByCSS(SectionTitle.locator);

            cancelButton.webElement.Click();
        }

        public bool AddNewButtonIsEnabled()
        {
            this.SubmitButton.webElement = Helper.GetElementWait(this.SubmitButton.findsBy);

            return this.SubmitButton.webElement.Enabled;
        }        
    }
}