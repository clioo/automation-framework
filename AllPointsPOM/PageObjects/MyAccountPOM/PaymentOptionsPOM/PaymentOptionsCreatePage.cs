using AllPoints.Features.Models;
using AllPoints.Features.MyAccount.PaymentOptions.Modals;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using AllPoints.PageObjects.NewFolder1;
using AllPoints.TestDataModels;
using AllPointsPOM.PageObjects.MyAccountPOM.Enums;
using AllPointsPOM.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using CommonHelper;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM
{
    public class PaymentOptionsCreatePage : AllPointsBaseWebPage
    {
        private DomElement DetailSection = new DomElement(By.CssSelector) { locator = "article.container" };
        private DomElement DetailSectionCardTokenForm = new DomElement { locator = ".cardtoken form" };

        private DomElement DetailSectionCardTokenFormCheckbox = new DomElement { locator = ".row-container .checkbox" };
        private DomElement DetailSectionCardTokenFormItemSelection = new DomElement { locator = ".form-control-static" };
        private DomElement DetailSectionCardTokenFormActions = new DomElement { locator = ".form-actions" };
        private DomElement DetailSectionCardTokenFormActionsSubmit = new DomElement { locator = "input[type='submit']" };
        private DomElement DetailSectionCardTokenFormActionsCancel = new DomElement { locator = "input[type='button'][value='Cancel']" };

        #region cenpos form
        private string FrameName = "cenposPayIFrameId";
        private DomElement CenposFrame = new DomElement { locator = "#cenpos-plugin iframe" };
        private DomElement CenposFrameForm = new DomElement { locator = "form#TokenCardForm" };
        private DomElement CardNumberField = new DomElement { locator = ".row input[name='cardNumber']" };
        private DomElement CardMonthField = new DomElement { locator = ".row input[name='month']" };
        private DomElement CardYearField = new DomElement { locator = ".row input[name='year']" };
        private DomElement CardNameField = new DomElement { locator = ".row input[name='cardName']" };
        private DomElement CardCvvField = new DomElement { locator = ".row input[name='cvv']" };
        #endregion

        #region modals
        public PaymentsContentModal ContentModal;
        private PaymentsInformationModal InfoModal;
        public PaymentsConfirmationModal ConfirmationModal;
        #endregion

        #region billing address
        private DomElement BillingAddressOptionsContainer = new DomElement { locator = "ul.billingaddress-selection" };
        private DomElement BillingAddressOptionItem = new DomElement { locator = "li.radio label" };

        private DomElement OptionsAddressContainer = new DomElement { locator = ".billingaddress-selection" };//not very semantic
        private DomElement OptionsAddressContainerPrevStored = new DomElement { locator = ".radio:nth-child(1) label" };////not very semantic
        private DomElement OptionsAddressContainerNewAdress = new DomElement { locator = ".radio:nth-child(2) label" };////not very semantic

        private DomElement ItemSelectorContainerAddressSelector = new DomElement { locator = ".dropdown" };
        private DomElement ItemSelectorContainerLicard = new DomElement { locator = ".licard-info" };

        private DomElement ItemSelectorContainerBillingAddressForm = new DomElement { locator = "div[ng-if] :nth-of-type(1)" };
        private DomElement ItemSelectorContainerBillingAddressFormCountrySelector = new DomElement { locator = ".row-container .ui-select-container.ui-select-bootstrap.dropdown.ng-not-empty.ng-valid.ng-valid-required" };
        private DomElement ItemSelectorContainerBillingAddressFormCountryOptionItems = new DomElement { locator = "div[ng-attr-id]" };
        private DomElement ItemSelectorContainerBillingAddressFormCompanyName = new DomElement { locator = ".row-container input[name='Name']" };
        private DomElement ItemSelectorContainerBillingAddressFormStreet = new DomElement { locator = ".row-container input[name='AddressLine1']" };
        private DomElement ItemSelectorContainerBillingAddressFormApartment = new DomElement { locator = ".row-container input[name='AddressLine2']" };
        private DomElement ItemSelectorContainerBillingAddressFormCity = new DomElement { locator = ".row-container input[name='City']" };
        private DomElement ItemSelectorContainerBillingAddressFormState = new DomElement { locator = ".row-container input[name='ProvinceRegion']" };
        private DomElement ItemSelectorContainerBillingAddressFormPostal = new DomElement { locator = ".row-container input[name='Postal']" };
        #endregion

        public PaymentOptionsCreatePage(IWebDriver driver) : base(driver)
        {
            DetailSection.Init(driver, SeleniumConstants.defaultWaitTime);

            ContentModal = new PaymentsContentModal(driver);
            InfoModal = new PaymentsInformationModal(driver);
            ConfirmationModal = new PaymentsConfirmationModal(driver);
        }

        public void FillCardTokenForm(PaymentOptionModel cardToken)
        {
            string parentWindow = Driver.WindowHandles[0];

            DomElement cardTokenContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement cardTokenContainerFrame = cardTokenContainer.GetElementWaitByCSS(CenposFrame.locator);

            IWebDriver iFrame = Driver.SwitchTo().Frame(FrameName);

            Helper.FindByCondition(By.CssSelector(CenposFrameForm.locator), (el) => el.Displayed);

            IWebElement cardTokenContainerFrameFormCardNumber = iFrame.FindElement(By.CssSelector(CardNumberField.locator));
            IWebElement cardTokenContainerFrameFormCardMonth = iFrame.FindElement(By.CssSelector(CardMonthField.locator));
            IWebElement cardTokenContainerFrameFormCardYear = iFrame.FindElement(By.CssSelector(CardYearField.locator));
            IWebElement cardTokenContainerFrameFormCardName = iFrame.FindElement(By.CssSelector(CardNameField.locator));
            IWebElement cardTokenContainerFrameFormCardCvv = iFrame.FindElement(By.CssSelector(CardCvvField.locator));

            cardTokenContainerFrameFormCardNumber.SendKeys(cardToken.CardNumber);
            cardTokenContainerFrameFormCardMonth.SendKeys(cardToken.ExpirationMont);
            cardTokenContainerFrameFormCardYear.SendKeys(cardToken.ExpirationYear);
            cardTokenContainerFrameFormCardName.SendKeys(cardToken.HolderName);
            cardTokenContainerFrameFormCardCvv.SendKeys(cardToken.Cvv);

            Driver.SwitchTo().Window(parentWindow);
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

        public void SelectPreviouslyStoredAddress(TestAddress address)
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement prevStoredAddressesDropdown = formContainer.GetElementWaitByCSS(DetailSectionCardTokenFormItemSelection.locator);

            string GetStreet()
            {
                if (string.IsNullOrEmpty(address.Apartment) || string.IsNullOrWhiteSpace(address.Apartment))
                    return address.Street + ",";
                return address.Street + ", " + address.Apartment;
            }

            string storedAddress = $@"{GetStreet()}
                {address.City}, {address.Country} {address.Postal}";

            SelectDropDownAutoCompleteOption(prevStoredAddressesDropdown, storedAddress);
        }

        public void SelectBillingAddressOption(BillingAddressOptionsEnum selectedOption)
        {
            DomElement formContainer = DetailSection.GetElementWaitByCSS(DetailSectionCardTokenForm.locator);
            DomElement billingAddressOptionsContainer = formContainer.GetElementWaitByCSS(BillingAddressOptionsContainer.locator);
            var options = billingAddressOptionsContainer.GetElementsWaitByCSS(BillingAddressOptionItem.locator);

            switch (selectedOption)
            {
                case BillingAddressOptionsEnum.AddNew:
                    options.FirstOrDefault(x => x.webElement.Text.Contains("Assign a new address")).webElement.Click();
                    break;

                case BillingAddressOptionsEnum.Stored:
                    options.FirstOrDefault(x => x.webElement.Text.Contains("Use a previously stored address")).webElement.Click();
                    break;

                default:
                    throw new ArgumentException($"{selectedOption} is not valid");
            }
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

                default: throw new ArgumentException("Invalid modal value: " + modal.ToString());
            }
        }

        #region private methods
        private void SetInputValue(DomElement input, string text)
        {
            input.webElement.SendKeys(text);
        }
        #endregion private methods
    }
}
