using CommonHelper.BaseComponents;
using CommonHelper.Pages.CartPage.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.Pages.CartPage
{
    public abstract class BaseCheckoutPage : BasePOM
    {
        #region Constructor
        public BaseCheckoutPage(IWebDriver driver) : base(driver) { }
        #endregion

        #region Details Summary

        protected DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = ".checkout-app"
        };

        protected DomElement detailSummarySection = new DomElement
        {
            locator = ".section.checkout-items-section"
        };

        #region Customer Info

        protected DomElement detailContactSection = new DomElement
        {
            locator = ".customer"
        };

        protected DomElement detailContactEditForm = new DomElement
        {
            locator = ".edit form"
        };

        protected DomElement detailContactEditFormInputFirstName = new DomElement
        {
            locator = "input[name='FirstName']"
        };

        protected DomElement detailContactEditFormInputLastName = new DomElement
        {
            locator = "input[name='LastName']"
        };

        protected DomElement detailContactEditFormInputCompany = new DomElement
        {
            locator = "input[name='Company']"
        };

        protected DomElement detailContactEditFormInputPhone = new DomElement
        {
            locator = "input[name='PhoneNumber']"
        };

        protected DomElement detailContactEditFormInputEmail = new DomElement
        {
            locator = "input[name='Email']"
        };

        protected DomElement detailContactEditFormInputOpt = new DomElement
        {
            locator = "input[name='OptIn']"
        };

        protected DomElement detailContactEditSubmitButton = new DomElement
        {
            locator = "input.step-submit"
        };

        #endregion Customer Info

        #region Shipping

        protected DomElement detailShippingSection = new DomElement
        {
            locator = ".shipping"
        };

        protected DomElement detailShippingEditForm = new DomElement
        {
            locator = ".edit form"
        };

        protected DomElement detailShippingEditLink = new DomElement
        {
            locator = ".edit a"
        };

        protected DomElement detailShippingEditFormInput = new DomElement
        {
            locator = ".address-select-or-new"
        };

        protected DomElement detailShippingEditFormInputRadio = new DomElement
        {
            locator = ".radio"
        };

        protected DomElement detailShippingEditFormInputRadioInput = new DomElement
        {
            locator = "input"
        };

        protected DomElement detailShippingEditFormInputExistingDropdown = new DomElement
        {
            locator = ".dropdown"
        };

        protected DomElement detailShippingEditFormInputExistingDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row-inner"
        };

        protected DomElement detailShippingEditFormInputNewCountryDropdown = new DomElement
        {
            locator = ".country .dropdown"
        };

        protected DomElement detailShippingEditFormInputNewCountryDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row"
        };

        protected DomElement detailShippingEditFormInputNewATTN = new DomElement
        {
            locator = "input[name='Name']"
        };

        protected DomElement detailShippingEditFormInputNewStreet = new DomElement
        {
            locator = "input[name='AddressLine1']"
        };

        protected DomElement detailShippingEditFormInputNewApt = new DomElement
        {
            locator = "input[name='AddressLine2']"
        };

        protected DomElement detailShippingEditFormInputNewCity = new DomElement
        {
            locator = "input[name='City']"
        };

        protected DomElement detailShippingEditFormInputNewRegion = new DomElement
        {
            locator = "input[name='StateProvinceRegion']"
        };

        protected DomElement detailShippingEditFormInputNewStateDropdown = new DomElement
        {
            locator = ".state .dropdown"
        };

        protected DomElement detailShippingEditFormInputNewStateDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row"
        };

        protected DomElement detailShippingEditFormInputNewZip = new DomElement
        {
            locator = "input[name='Postal']"
        };

        protected DomElement detailShippingEditSubmitButton = new DomElement
        {
            locator = "input.step-submit"
        };
        protected DomElement proceedToShippingButton = new DomElement
        {
            locator = "input#submit"
        };

        protected DomElement checkutStepContainer = new DomElement(By.XPath)
        {
            locator = "(//div[contains(@class,'checkout-step')])"
        };

        protected DomElement assignNewCardButton = new DomElement(By.XPath)
        {
            locator = "(//div[contains(@class,'billing')]//input[@type='radio'])[2]"
        };

        #endregion Shipping

        #region Billing

        protected DomElement detailBillingSection = new DomElement
        {
            locator = ".billing"
        };

        protected DomElement detailBillingEditForm = new DomElement
        {
            locator = ".edit form"
        };

        protected DomElement detailBillingEditFormInput = new DomElement
        {
            locator = ".content"
        };

        protected DomElement detailBillingEditFormInputRadio = new DomElement
        {
            locator = ".radio"
        };

        protected DomElement detailBillingEditFormInputRadioInput = new DomElement
        {
            locator = "input"
        };

        protected DomElement detailBillingEditFormInputExistingDropdown = new DomElement
        {
            locator = ".dropdown .btn"
        };

        protected DomElement detailBillingEditFormInputExistingDropdownOnly = new DomElement
        {
            locator = ".dropdown"
        };

        protected DomElement detailBillingEditFormInputExistingDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row-inner"
        };

        protected DomElement detailBillingEditFormInputNewCenPos = new DomElement
        {
            locator = "#cenpos-plugin"
        };

        protected DomElement detailBillingEditFormInputNewCenPosForm = new DomElement
        {
            locator = "#TokenCardForm"
        };

        protected DomElement detailBillingEditFormInputNewCenPosFormCardNumber = new DomElement
        {
            locator = "input[name='cardNumber']"
        };

        protected DomElement detailBillingEditFormInputNewCenPosFormExpMonth = new DomElement
        {
            locator = "input[name='month']"
        };

        protected DomElement detailBillingEditFormInputNewCenPosFormExpYear = new DomElement
        {
            locator = "input[name='year']"
        };

        protected DomElement detailBillingEditFormInputNewCenPosFormCardName = new DomElement
        {
            locator = "input[name='cardName']"
        };

        protected DomElement detailBillingEditFormInputNewCenPosFormCVV = new DomElement
        {
            locator = "input[name='cvv']"
        };

        protected DomElement detailBillingEditSubmitButton = new DomElement
        {
            locator = "input.step-submit"
        };

        #endregion Billing

        #region Billing New Credit Card
        protected DomElement detailContentiFrame = new DomElement
        {
            locator = ".content"
        };

        protected DomElement detailBillingSectionlayoutPanelEdit = new DomElement
        {
            locator = ".edit"
        };

        protected DomElement detailCenposPlugIn = new DomElement
        {
            locator = ".cenpos-plugin .ïframecenpos"
        };

        #region cenpos form
        protected string FrameName = "cenposPayIFrameId";
        protected DomElement CenposFrame = new DomElement { locator = "#cenpos-plugin iframe" };
        protected DomElement CenposFrameForm = new DomElement { locator = "form#TokenCardForm" };
        protected DomElement CardNumberField = new DomElement { locator = ".row input[name='cardNumber']" };
        protected DomElement CardMonthField = new DomElement { locator = ".row input[name='month']" };
        protected DomElement CardYearField = new DomElement { locator = ".row input[name='year']" };
        protected DomElement CardNameField = new DomElement { locator = ".row input[name='cardName']" };
        protected DomElement CardCvvField = new DomElement { locator = ".row input[name='cvv']" };
        #endregion

        protected DomElement detailBillingSectionCardForm = new DomElement
        {
            locator = ".layaoutPanelTokenCardForm"
        };
        #endregion

        #region Place Order

        protected DomElement detailPlaceOrderSection = new DomElement
        {
            locator = ".placeorder "
        };

        protected DomElement detailPlaceOrderContent = new DomElement
        {
            locator = ".content"
        };

        protected DomElement detailPlaceOrderSubmitButton = new DomElement
        {
            locator = "#submit"
        };

        #endregion Place Order

        #region Cart

        protected DomElement detailCartSection = new DomElement
        {
            locator = ".checkout-items"
        };

        protected DomElement detailCartItems = new DomElement
        {
            locator = ".cart-items"
        };

        protected DomElement detailCartItem = new DomElement
        {
            locator = ".cart-item"
        };

        protected DomElement detailCartItemContent = new DomElement
        {
            locator = ".item-content"
        };

        protected DomElement detailCartItemContentProduct = new DomElement
        {
            locator = ".product-section"
        };

        protected DomElement detailCartItemContentProductSKU = new DomElement
        {
            locator = ".sku-section .sku"
        };

        protected DomElement detailCartItemAvailability = new DomElement
        {
            locator = ".sku-availability-section"
        };

        protected DomElement detailCartItemAvailabilityTag = new DomElement
        {
            locator = ".availability-content"
        };

        #endregion Cart

        #endregion Details Summary
        //This snippet is showing Mandatory field for Contact Information
        //Sections in Checkout Page

        //TODO: Update if necessary
        #region Locators for Fields

        protected DomElement textconfirmation = new DomElement(By.CssSelector)
        {
            //updated by: Carlos Acosta
            locator = ".intro-section"
        };

        protected DomElement textSuccess = new DomElement()
        {
            locator = ".success-alert"
        };

        protected DomElement userInformationContent = new DomElement(By.CssSelector)
        {
            locator = "div[ui-view='customer'] customer div.summary div *"
        };

        #endregion Locators for Fields

        public virtual bool OrderConfirmationText()
        {
            //updated by: Carlos Acosta
            //date: 17/02/2019
            var objtextConfirmation = Driver.FindElement(By.CssSelector(textconfirmation.locator));
            return objtextConfirmation.Enabled;
        }

        public virtual bool SuccessText()
        {
            var objSuccessText = Driver.FindElement(By.CssSelector(textSuccess.locator));
            return objSuccessText.Enabled;
        }


        //Component Base Code Functions
        public virtual IDictionary<string, string> AvailabiltyTagGet()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            DomElement cartDetail = detailSummary.GetElementWaitByCSS(detailSummarySection.locator);
            DomElement cartItemsSection = cartDetail.GetElementWaitByCSS(detailCartItems.locator);
            List<DomElement> cartItems = cartItemsSection.GetElementsWaitByCSS(detailCartItem.locator);
            foreach (var item in cartItems)
            {
                //Get Cart Item Sku
                DomElement itemContent = item.GetElementWaitByCSS(detailCartItemContent.locator);
                DomElement itemProduct = itemContent.GetElementWaitByCSS(detailCartItemContentProduct.locator);
                DomElement itemSku = itemProduct.GetElementWaitByCSS(detailCartItemContentProductSKU.locator);

                //Get Cart Item Availabilty Tag
                DomElement itemAvailabilty = item.GetElementWaitByCSS(detailCartItemAvailability.locator);
                DomElement itemAvailabilyTag = itemAvailabilty.GetElementWaitByCSS(detailCartItemAvailabilityTag.locator);
                dict.Add(itemSku.webElement.Text, itemAvailabilyTag.webElement.Text);
            }

            return dict;
        }

        public virtual bool ContactButtonIsEnable()
        {
            DomElement customerForm = detailSummary.GetElementWaitByCSS(detailContactSection.locator);
            DomElement contactSubmitButton = customerForm.GetElementWaitByCSS(detailContactEditSubmitButton.locator);
            return contactSubmitButton.webElement.Enabled;
        }

        public virtual void SetContactElement(ContactInputs field, string text)
        {
            switch (field)
            {
                case ContactInputs.Company:
                    SetContactInput(detailContactEditFormInputCompany, text);
                    break;

                case ContactInputs.Email:
                    SetContactInput(detailContactEditFormInputEmail, text);
                    break;

                case ContactInputs.FirstName:
                    SetContactInput(detailContactEditFormInputFirstName, text);
                    break;

                case ContactInputs.LastName:
                    SetContactInput(detailContactEditFormInputLastName, text);
                    break;

                case ContactInputs.OPT:
                    SetContactInput(detailContactEditFormInputOpt, text);
                    break;

                case ContactInputs.PhoneNumber:
                    SetContactInput(detailContactEditFormInputPhone, text);
                    break;
            }
        }

        public virtual void ContactSubmitClick()
        {
            DomElement contactForm = detailSummary.GetElementWaitByCSS(detailContactSection.locator);
            DomElement contactSubmitButton = contactForm.GetElementWaitByCSS(detailContactEditSubmitButton.locator);
            contactSubmitButton.webElement.Click();
        }

        public virtual bool ShippingButtonIsEnable()
        {
            DomElement shippingSection = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement shippingForm = shippingSection.GetElementWaitByCSS(detailShippingEditForm.locator);
            DomElement addressSubmitButton = shippingForm.GetElementWaitByCSS(detailShippingEditSubmitButton.locator);
            return addressSubmitButton.webElement.Enabled;
        }

        public virtual void ClickShippingButton()
        {
            DomElement shippingSection = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement shippingForm = shippingSection.GetElementWaitByCSS(detailShippingEditForm.locator);
            DomElement addressSubmitButton = shippingForm.GetElementWaitByCSS(detailShippingEditSubmitButton.locator);
            addressSubmitButton.webElement.Click();
        }

        public virtual void SelectAddressRadioButton(AddressSelectOptions option)
        {
            switch (option)
            {
                case AddressSelectOptions.Existing:
                    //SelectAddressRadioButtonByOptionText("Use stored address");
                    SelectFirstInAddressDropDown();
                    break;

                case AddressSelectOptions.New:
                    SelectAddressRadioButtonByOptionText("Assign a new address");
                    break;
            }
        }

        public virtual void SelectFirstInAddressDropDown()
        {
            DomElement shippingForm = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement addressInput = shippingForm.GetElementWaitByCSS(detailShippingEditFormInput.locator);
            DomElement addressDropDown = addressInput.GetElementWaitByCSS(detailShippingEditFormInputExistingDropdown.locator);
            addressDropDown.webElement.Click();
            Thread.Sleep(2000);
            List<DomElement> dropdownOptions = addressDropDown.GetElementsWaitByCSS(detailShippingEditFormInputExistingDropdownOption.locator);
            dropdownOptions.FirstOrDefault().webElement.Click();
        }

        public virtual void SetAddressElement(AddressInputs field, string text)
        {
            switch (field)
            {
                case AddressInputs.Apt:
                    SetAddressInput(detailShippingEditFormInputNewApt, text);
                    break;

                case AddressInputs.ATTN:
                    SetAddressInput(detailShippingEditFormInputNewATTN, text);
                    break;

                case AddressInputs.City:
                    SetAddressInput(detailShippingEditFormInputNewCity, text);
                    break;

                case AddressInputs.Country:
                    SetAddressDropDown(detailShippingEditFormInputNewCountryDropdown, text);
                    break;

                case AddressInputs.Postal:
                    SetAddressInput(detailShippingEditFormInputNewZip, text);
                    break;

                case AddressInputs.Region:
                    SetAddressInput(detailShippingEditFormInputNewRegion, text);
                    break;

                case AddressInputs.State:
                    SetAddressDropDown(detailShippingEditFormInputNewStateDropdown, text);
                    break;

                case AddressInputs.StreetAddress:
                    SetAddressInput(detailShippingEditFormInputNewStreet, text);
                    break;
            }
        }

        public virtual void ShippingSubmitClick()
        {
            DomElement shippingSection = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement shippingForm = shippingSection.GetElementWaitByCSS(detailShippingEditForm.locator);
            DomElement addressSubmitButton = shippingForm.GetElementWaitByCSS(detailShippingEditSubmitButton.locator);
            addressSubmitButton.webElement.Click();
        }

        public virtual bool BillingButtonIsEnable()
        {
            DomElement billingSection = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingForm = billingSection.GetElementWaitByCSS(detailBillingEditForm.locator);
            DomElement billingSubmitButton = billingForm.GetElementWaitByCSS(detailBillingEditSubmitButton.locator);
            return billingSubmitButton.webElement.Enabled;
        }

        public virtual void SelectBillingRadioButton(BillingSelectOptions option)
        {
            switch (option)
            {
                case BillingSelectOptions.Existing:
                    //SelectBillingRadioButtonByOptionText("Use stored credit card or terms");
                    SelectFirstInBillingDropDown();
                    break;

                case BillingSelectOptions.New:
                    SelectBillingRadioButtonByOptionText("Assign a new credit card");
                    break;
            }
        }

        public virtual void SelectFirstInBillingDropDown()
        {
            DomElement billingForm = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingDropDown = billingForm.GetElementWaitByCSS(detailBillingEditFormInputExistingDropdown.locator);
            billingDropDown.webElement.Click();
            DomElement billingOnlyDropdown = billingForm.GetElementWaitByCSS(detailBillingEditFormInputExistingDropdownOnly.locator);
            List<DomElement> dropdownOptions = billingOnlyDropdown.GetElementsWaitByCSS(detailBillingEditFormInputExistingDropdownOption.locator);
            dropdownOptions.FirstOrDefault().webElement.Click();
        }

        public virtual void SetBillingElement(BillingInputs field, string text)
        {
            DomElement billingForm = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingContentIframe = billingForm.GetElementWaitByCSS(detailContentiFrame.locator);
            DomElement formEdit = billingContentIframe.GetElementWaitByCSS(detailBillingSectionlayoutPanelEdit.locator);
            Thread.Sleep(3000);
            IWebDriver iFrame = Driver.SwitchTo().Frame(FrameName);
            Thread.Sleep(2000);
            detailSummary.GetElementWaitUntil(CenposFrameForm.locator, (el) => el.Displayed);

            switch (field)
            {
                case BillingInputs.CardHolderName:
                    IWebElement cardTokenContainerFrameFormCardName = iFrame.FindElement(By.CssSelector(CardNameField.locator));
                    SetBillingInput(cardTokenContainerFrameFormCardName, text);
                    break;

                case BillingInputs.CardNumber:
                    IWebElement cardTokenContainerFrameFormCardNumber = iFrame.FindElement(By.CssSelector(CardNumberField.locator));
                    SetBillingInput(cardTokenContainerFrameFormCardNumber, text);
                    break;

                case BillingInputs.CVV:
                    IWebElement cardTokenContainerFrameFormCardCvv = iFrame.FindElement(By.CssSelector(CardCvvField.locator));
                    SetBillingInput(cardTokenContainerFrameFormCardCvv, text);
                    break;

                case BillingInputs.ExpirationMonth:
                    IWebElement cardTokenContainerFrameFormCardMonth = iFrame.FindElement(By.CssSelector(CardMonthField.locator));
                    SetBillingInput(cardTokenContainerFrameFormCardMonth, text);
                    break;

                case BillingInputs.ExpirationYear:
                    IWebElement cardTokenContainerFrameFormCardYear = iFrame.FindElement(By.CssSelector(CardYearField.locator));
                    SetBillingInput(cardTokenContainerFrameFormCardYear, text);
                    break;
            }
        }

        public virtual void BillingSubmitClick()
        {
            DomElement billingSection = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingForm = billingSection.GetElementWaitByCSS(detailBillingEditForm.locator);
            DomElement billingSubmitButton = billingForm.GetElementWaitByCSS(detailBillingEditSubmitButton.locator);
            billingSubmitButton.webElement.Click();
        }

        public virtual bool PlaceOrderButtonIsEnable()
        {
            DomElement placeOrderSection = detailSummary.GetElementWaitByCSS(detailPlaceOrderSection.locator);
            DomElement placeOrderContent = placeOrderSection.GetElementWaitByCSS(detailPlaceOrderContent.locator);
            DomElement placeOrderSubmitButton = placeOrderContent.GetElementWaitByCSS(detailPlaceOrderSubmitButton.locator);
            return placeOrderSubmitButton.webElement.Enabled;
        }

        public virtual void PlaceOrderSubmitClick()
        {
            base.ScrollToTop();
            DomElement placeOrderSection = detailSummary.GetElementWaitByCSS(detailPlaceOrderSection.locator);
            DomElement placeOrderContent = placeOrderSection.GetElementWaitByCSS(detailPlaceOrderContent.locator);
            DomElement placeOrderSubmitButton = placeOrderContent.GetElementWaitUntil(detailPlaceOrderSubmitButton.locator,
                (el) => el.Displayed);
            //Added this try-catch due to randomly fail
            placeOrderSubmitButton.webElement.Click();
        }

        private void SelectAddressRadioButtonByOptionText(string optionText)
        {
            DomElement shippingForm = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement addressInput = shippingForm.GetElementWaitByCSS(detailShippingEditFormInput.locator);
            List<DomElement> radioOptions = addressInput.GetElementsWaitByCSS(detailShippingEditFormInputRadio.locator);
            DomElement choosenElement = radioOptions.Where(o => o.webElement.Text.Contains(optionText)).FirstOrDefault();
            choosenElement.locator = choosenElement.locator.Replace("(2)", "(3)");
            choosenElement.GetElementWaitByCSS("label").webElement.Click();
        }

        private void SetAddressInput(DomElement inputField, string text)
        {
            DomElement shippingForm = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement addressInput = shippingForm.GetElementWaitByCSS(detailShippingEditFormInput.locator);
            DomElement input = addressInput.GetElementWaitByCSS(inputField.locator);
            SetInputField(input, text);
        }

        private void SetAddressDropDown(DomElement inputField, string text)
        {
            DomElement shippingForm = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement addressInput = shippingForm.GetElementWaitByCSS(detailShippingEditFormInput.locator);
            DomElement input = addressInput.GetElementWaitByCSS(inputField.locator);
            SelectDropDownAutoCompleteOption(input, text);
        }

        private void SelectBillingRadioButtonByOptionText(string optionText)
        {
            DomElement billingForm = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingInput = billingForm.GetElementWaitByCSS(detailBillingEditFormInput.locator);
            List<DomElement> radioOptions = billingInput.GetElementsWaitByCSS(detailBillingEditFormInputRadio.locator);
            DomElement choosenElement = radioOptions.Where(o => o.webElement.Text.Contains(optionText)).FirstOrDefault();
            choosenElement.locator = choosenElement.locator.Replace("(2)", "(3)");
            choosenElement.GetElementWaitByCSS("label").webElement.Click();
        }

        private void SetBillingInput(IWebElement inputField, string text)
        {
            string parentWindow = Driver.WindowHandles[0];

            inputField.SendKeys(text);

            Driver.SwitchTo().Window(parentWindow);
        }


        private void SetContactInput(DomElement inputField, string text)
        {
            DomElement contactForm = detailSummary.GetElementWaitByCSS(detailContactSection.locator);
            DomElement contactInput = contactForm.GetElementWaitByCSS(detailContactEditForm.locator);
            DomElement input = contactInput.GetElementWaitByCSS(inputField.locator);
            SetInputField(input, text);
        }

        private void ClickToAddCreditCard()
        {
            DomElement billingForm = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingInput = billingForm.GetElementWaitByCSS(detailBillingEditFormInput.locator);
        }

        public virtual void NextStep()
        {
            DomElement addressSubmitButton = detailSummary.GetElementWaitUntil(proceedToShippingButton.locator, (el) => el.Enabled);
            try
            {
                addressSubmitButton.webElement.Click();
            }
            catch (System.Exception)
            {
                addressSubmitButton = detailSummary.GetElementWaitUntil(proceedToShippingButton.locator, (el) => el.Enabled);
                addressSubmitButton.webElement.Click();
            }
        }

        public virtual bool UserInfoIsPopulated()
        {
            List<DomElement> userInformationContent = this.detailSummary.GetElementsWaitByCSS(this.userInformationContent.locator);
            foreach (DomElement element in userInformationContent)
            {
                if (element.webElement.Text == "" || element.webElement.Displayed == false)
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void ClickEditAction(EditActions field)
        {
            DomElement stepButton;
            switch (field)
            {
                case EditActions.ContactInformation:
                    stepButton = detailSummary.GetElementWaitXpath(
                        $"{checkutStepContainer.locator}[1]//a");
                    stepButton.webElement.Click();
                    break;
                case EditActions.ShippingInformation:
                    stepButton = detailSummary.GetElementWaitXpath(
                        $"{checkutStepContainer.locator}[2]//a");
                    stepButton.webElement.Click();
                    break;
                case EditActions.SecureBillingInformation:
                    stepButton = detailSummary.GetElementWaitXpath(
                        $"{checkutStepContainer.locator}[3]//a");
                    stepButton.webElement.Click();
                    break;
            }
        }

        public virtual void ClickOnAssingNewCard()
        {
            DomElement assignNewCardButton = detailSummary.GetElementWaitXpath(this.assignNewCardButton.locator);
            assignNewCardButton.webElement.Click();
        }

        public virtual void ClickShippingEdit()
        {
            DomElement shippingSection = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement shippingLink = shippingSection.GetElementWaitByCSS(detailShippingEditLink.locator);
            shippingLink.webElement.Click();
        }

    }
}
