using AllPoints.PageObjects.NewFolder1;
using System;
using OpenQA.Selenium;
using CommonHelper;
using AllPoints.Features.MyAccount.PaymentOptions.Modals;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using System.Linq;
using AllPointsPOM.PageObjects.MyAccountPOM.Enums;

namespace AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM
{
    public class PaymentOptionsEditPage : AllPointsBaseWebPage
    {
        #region locators
        private DomElement DetailSection = new DomElement(By.CssSelector) { locator = "article.detail-section" };
        private DomElement DetailSectionCardTokenForm = new DomElement { locator = ".cardtoken form" };

        private DomElement DetailSectionCardTokenFormCheckbox = new DomElement { locator = ".row-container .checkbox" };
        private DomElement DetailSectionCardTokenFormItemSelection = new DomElement { locator = ".form-control-static" };
        private DomElement DetailSectionCardTokenFormActions = new DomElement { locator = ".form-actions" };
        private DomElement DetailSectionCardTokenFormActionsSubmit = new DomElement { locator = "input[type='submit']" };
        private DomElement DetailSectionCardTokenFormActionsCancel = new DomElement { locator = "input[type='button'][value='Cancel']" };
        #endregion locators

        #region edit card form locators
        private DomElement CardNumberLastFourDigits = new DomElement { locator = ".row-container p" };
        private DomElement CardMonthField = new DomElement { locator = ".row-container .dropdown[name='ExpirationMonth']" };
        private DomElement CardYearField = new DomElement { locator = ".row-container .dropdown[name='ExpirationYear']" };
        private DomElement CardNameField = new DomElement { locator = ".row-container input[name='NameOnCard']" };
        #endregion edit card locators

        #region modals
        public PaymentsContentModal ContentModal;
        private PaymentsInformationModal InfoModal;
        public PaymentsConfirmationModal ConfirmationModal;
        #endregion

        #region billing address locators
        private DomElement OptionsAddressContainer = new DomElement { locator = ".billingaddress-selection" };
        private DomElement OptionsAddressContainerPrevStored = new DomElement { locator = ".radio:nth-child(1) label" };
        private DomElement OptionsAddressContainerNewAdress = new DomElement { locator = ".radio:nth-child(2) label" };

        private DomElement ItemSelectorContainerAddressSelector = new DomElement { locator = ".dropdown" };
        private DomElement ItemSelectorContainerLicard = new DomElement { locator = ".licard-info" };

        private DomElement ItemSelectorContainerBillingAddressForm = new DomElement { locator = "div[ng-if] :nth-of-type(1)" };
        private DomElement ItemSelectorContainerBillingAddressFormCountrySelector = new DomElement { locator = ".row-container .ui-select-container.ui-select-bootstrap.dropdown.ng-not-empty.ng-valid.ng-valid-required" };
        private DomElement ItemSelectorContainerBillingAddressFormCountryOptionItems = new DomElement { locator = "div[ng-attr-id]" };
        private DomElement ItemSelectorContainerBillingAddressFormCountrySelectedLabel = new DomElement { locator = "span.ng-scope" };
        private DomElement ItemSelectorContainerBillingAddressFormCompanyName = new DomElement { locator = ".row-container input[name='Name']" };
        private DomElement ItemSelectorContainerBillingAddressFormStreet = new DomElement { locator = ".row-container input[name='AddressLine1']" };
        private DomElement ItemSelectorContainerBillingAddressFormApartment = new DomElement { locator = ".row-container input[name='AddressLine2']" };
        private DomElement ItemSelectorContainerBillingAddressFormCity = new DomElement { locator = ".row-container input[name='City']" };
        private DomElement ItemSelectorContainerBillingAddressFormState = new DomElement { locator = ".row-container input[name='ProvinceRegion']" };
        private DomElement ItemSelectorContainerBillingAddressFormPostal = new DomElement { locator = ".row-container input[name='Postal']" };
        #endregion

        #region constructor
        public PaymentOptionsEditPage(IWebDriver driver) : base(driver)
        {
            DetailSection.Init(driver, SeleniumConstants.defaultWaitTime);

            ContentModal = new PaymentsContentModal(driver);
            InfoModal = new PaymentsInformationModal(driver);
            ConfirmationModal = new PaymentsConfirmationModal(driver);
        }
        #endregion constructor

        public void FillCardTokenFormInput(CardTokenInputs input, string value)
        {
            DomElement sectionContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);

            switch (input)
            {
                case CardTokenInputs.ExpirationMonth:
                    DomElement dropdownExpMonth = sectionContainer.GetElementWaitByCSS(CardMonthField.locator);
                    SelectDropDownOption(dropdownExpMonth, value);
                    break;

                case CardTokenInputs.ExpirationYear:
                    DomElement dropdownExpYear = sectionContainer.GetElementWaitByCSS(CardYearField.locator);
                    SelectDropDownOption(dropdownExpYear, value);
                    break;

                case CardTokenInputs.Name:
                    sectionContainer.GetElementWaitByCSS(CardNameField.locator).webElement.SendKeys(value);
                    break;

                default:
                    throw new ArgumentException("Invalid not supported");
            }
        }

        public void ClickOnMakeDefault()
        {
            DomElement cardTokenContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement cardTokenCheckbox = cardTokenContainer.GetElementWaitByCSS(DetailSectionCardTokenFormCheckbox.locator);

            cardTokenCheckbox.webElement.Click();
        }

        public void SelectPreviouslyStoreAddress(string address)
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement prevStoredAddressesDropdown = formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormItemSelection.locator);

            SelectDropDownAutoCompleteOption(prevStoredAddressesDropdown, address);
        }

        public void ClickOnBillingAddressOption(BillingAddressOptions option)
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement billingAddressOptions = formContainer.GetElementWaitByCSS(OptionsAddressContainer.locator);

            switch (option)
            {
                case BillingAddressOptions.PreviouslyStored:
                    billingAddressOptions.GetElementWaitByCSS(OptionsAddressContainerPrevStored.locator)
                        .webElement.Click();
                    break;

                case BillingAddressOptions.NewOne:
                    billingAddressOptions.GetElementWaitByCSS(OptionsAddressContainerNewAdress.locator)
                        .webElement.Click();
                    break;
            }
        }

        public void SetInputAddressValue(AddressInputs input, string value)
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement billingAddressContainer = formContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressForm.locator);

            switch (input)
            {
                case AddressInputs.CompanyName:
                    var addressInput = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormCompanyName.locator);
                    SetInputValue(addressInput, value);
                    break;

                case AddressInputs.Country:
                    var countrySelector = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormCountrySelector.locator);
                    countrySelector.webElement.Click();

                    var options = countrySelector.GetElementsWaitByCSS(ItemSelectorContainerBillingAddressFormCountryOptionItems.locator);
                    var option = options.FirstOrDefault(el => el.webElement.Text.Contains(value));
                    option.webElement.Click();
                    break;

                case AddressInputs.Street:
                    var street = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormStreet.locator);
                    SetInputValue(street, value);
                    break;

                case AddressInputs.Apartment:
                    var apartment = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormApartment.locator);
                    SetInputValue(apartment, value);
                    break;

                case AddressInputs.City:
                    var city = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormCity.locator);
                    SetInputValue(city, value);
                    break;

                case AddressInputs.State:
                    var state = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormState.locator);
                    SetInputValue(state, value);
                    break;

                case AddressInputs.Postal:
                    var postal = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormPostal.locator);
                    SetInputValue(postal, value);
                    break;
            }
        }

        public string GetBillingAddressInputValue(AddressInputs input)
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement billingAddressContainer = formContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressForm.locator);
            string value = string.Empty;

            switch (input)
            {
                case AddressInputs.CompanyName:
                    var companyNameInput = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormCompanyName.locator);
                    value = companyNameInput.webElement.GetAttribute("value");
                    break;

                case AddressInputs.Country:
                    var countrySelector = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormCountrySelector.locator);
                    var countryValue = countrySelector.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormCountrySelectedLabel.locator);
                    value = countryValue.webElement.Text;
                    break;

                case AddressInputs.Street:
                    var street = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormStreet.locator);
                    value = street.webElement.GetAttribute("value");
                    break;

                case AddressInputs.Apartment:
                    var apartment = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormApartment.locator);
                    value = apartment.webElement.GetAttribute("value");
                    break;

                case AddressInputs.City:
                    var city = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormCity.locator);
                    value = city.webElement.GetAttribute("value");
                    break;

                case AddressInputs.State:
                    var state = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormState.locator);
                    value = state.webElement.GetAttribute("value");
                    break;

                case AddressInputs.Postal:
                    var postal = billingAddressContainer.GetElementWaitByCSS(ItemSelectorContainerBillingAddressFormPostal.locator);
                    value = postal.webElement.GetAttribute("value");
                    break;

                default: throw new ArgumentException($"{input} is not supported here");
            }

            return value;
        }

        public bool IsSubmitButtonEnabled()
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement actions = formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormActions.locator);

            return formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormActionsSubmit.locator)
                .webElement.Enabled;
        }

        public PaymentOptionsHomePage ClickOnCancel()
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement actions = formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormActions.locator);

            DomElement cancelButton = formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormActionsCancel.locator);

            cancelButton.webElement.Click();

            return new PaymentOptionsHomePage(Driver);
        }

        public void ClickOnSubmit()
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement actions = formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormActions.locator);

            DomElement submitButton = formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormActionsSubmit.locator);

            submitButton.webElement.Click();
        }

        public bool ElementOnPageIsPresent(AddPaymentElements element)
        {
            switch (element)
            {
                case AddPaymentElements.DefaultCheckbox:
                    DomElement form = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
                    return form.IsElementPresent(DetailSectionCardTokenFormCheckbox.locator);

                default:
                    throw new ArgumentException("Invalid page element");
            }
        }

        public PaymentOptionsHomePage CloseModal(ModalsEnum modal)
        {
            switch (modal)
            {
                case ModalsEnum.Information:
                    InfoModal.ClickOnClose();
                    return new PaymentOptionsHomePage(Driver);

                default: throw new ArgumentException("Invalid modal: " + modal.ToString());
            }
        }

        #region private methods
        private void SetInputValue(DomElement input, string text)
        {
            input.webElement.Clear();
            input.webElement.SendKeys(text);
        }
        #endregion private methods
    }
}
