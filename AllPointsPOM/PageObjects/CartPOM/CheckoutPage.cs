using AllPoints.PageObjects.CartPOM.Enums;
using AllPoints.PageObjects.NewFolder1;
using AllPoints.PageObjects.OderConfirmPOM;
using CommonHelper;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.PageObjects.CartPOM
{
    public class CheckoutPage : AllPointsBaseWebPage
    {
        #region Contructor

        public CheckoutPage(IWebDriver driver) : base(driver)
        {
            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        #endregion Contructor

        //This snippet is showing Mandatory field for Contact Information
        //Sections in Checkout Page

            //TODO: Update if necessary
        #region Locators for Fields

        private DomElement textconfirmation = new DomElement()
        {
            locator = "document.querySelector('.intro-section')"
        };

        private DomElement textSuccess = new DomElement()
        {
            locator = "document.querySelector('.success-alert')"
        };

        private DomElement userInformationContent = new DomElement(By.CssSelector)
        {
            locator = "div[ui-view='customer'] customer div.summary div *"
        };

        #endregion Locators for Fields

        public bool OrderConfirmationText()
        {
            var objtextConfirmation = Helper.GetElementByJs(textconfirmation.locator);
            return objtextConfirmation.Enabled;
        }

        public bool SuccessText()
        {
            var objSuccessText = Helper.GetElementByJs(textSuccess.locator);
            return objSuccessText.Enabled;
        }

        //Component Base Code Selectors

        #region Details Summary

        private DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = ".checkout-app"
        };

        private DomElement detailSummarySection = new DomElement
        {
            locator = ".section.checkout-items-section"
        };

        #region Customer Info

        private DomElement detailContactSection = new DomElement
        {
            locator = ".customer"
        };

        private DomElement detailContactEditForm = new DomElement
        {
            locator = ".edit form"
        };

        private DomElement detailContactEditFormInputFirstName = new DomElement
        {
            locator = "input[name='FirstName']"
        };

        private DomElement detailContactEditFormInputLastName = new DomElement
        {
            locator = "input[name='LastName']"
        };

        private DomElement detailContactEditFormInputCompany = new DomElement
        {
            locator = "input[name='Company']"
        };

        private DomElement detailContactEditFormInputPhone = new DomElement
        {
            locator = "input[name='PhoneNumber']"
        };

        private DomElement detailContactEditFormInputEmail = new DomElement
        {
            locator = "input[name='Email']"
        };

        private DomElement detailContactEditFormInputOpt = new DomElement
        {
            locator = "input[name='OptIn']"
        };

        private DomElement detailContactEditSubmitButton = new DomElement
        {
            locator = "input.step-submit"
        };

        #endregion Customer Info

        #region Shipping

        private DomElement detailShippingSection = new DomElement
        {
            locator = ".shipping"
        };

        private DomElement detailShippingEditForm = new DomElement
        {
            locator = ".edit form"
        };

        private DomElement detailShippingEditFormInput = new DomElement
        {
            locator = ".address-select-or-new"
        };

        private DomElement detailShippingEditFormInputRadio = new DomElement
        {
            locator = ".radio"
        };

        private DomElement detailShippingEditFormInputRadioInput = new DomElement
        {
            locator = "input"
        };

        private DomElement detailShippingEditFormInputExistingDropdown = new DomElement
        {
            locator = ".dropdown"
        };

        private DomElement detailShippingEditFormInputExistingDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row-inner"
        };

        private DomElement detailShippingEditFormInputNewCountryDropdown = new DomElement
        {
            locator = ".country .dropdown"
        };

        private DomElement detailShippingEditFormInputNewCountryDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row"
        };

        private DomElement detailShippingEditFormInputNewATTN = new DomElement
        {
            locator = "input[name='Name']"
        };

        private DomElement detailShippingEditFormInputNewStreet = new DomElement
        {
            locator = "input[name='AddressLine1']"
        };

        private DomElement detailShippingEditFormInputNewApt = new DomElement
        {
            locator = "input[name='AddressLine2']"
        };

        private DomElement detailShippingEditFormInputNewCity = new DomElement
        {
            locator = "input[name='City']"
        };

        private DomElement detailShippingEditFormInputNewRegion = new DomElement
        {
            locator = "input[name='StateProvinceRegion']"
        };

        private DomElement detailShippingEditFormInputNewStateDropdown = new DomElement
        {
            locator = ".state .dropdown"
        };

        private DomElement detailShippingEditFormInputNewStateDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row"
        };

        private DomElement detailShippingEditFormInputNewZip = new DomElement
        {
            locator = "input[name='Postal']"
        };

        private DomElement detailShippingEditSubmitButton = new DomElement
        {
            locator = "input.step-submit"
        };
        private DomElement proceedToShippingButton = new DomElement
        {
            locator = "input#submit"
        };

        private DomElement checkutStepContainer = new DomElement(By.XPath)
        {
            locator = "(//div[contains(@class,'checkout-step')])"
        };

        private DomElement assignNewCardButton = new DomElement(By.XPath)
        {
            locator = "(//div[contains(@class,'billing')]//input[@type='radio'])[2]"
        };

        #endregion Shipping

        #region Billing

        private DomElement detailBillingSection = new DomElement
        {
            locator = ".billing"
        };

        private DomElement detailBillingEditForm = new DomElement
        {
            locator = ".edit form"
        };

        private DomElement detailBillingEditFormInput = new DomElement
        {
            locator = ".content"
        };

        private DomElement detailBillingEditFormInputRadio = new DomElement
        {
            locator = ".radio"
        };

        private DomElement detailBillingEditFormInputRadioInput = new DomElement
        {
            locator = "input"
        };

        private DomElement detailBillingEditFormInputExistingDropdown = new DomElement
        {
            locator = ".dropdown .btn"
        };

        private DomElement detailBillingEditFormInputExistingDropdownOnly = new DomElement
        {
            locator = ".dropdown"
        };

        private DomElement detailBillingEditFormInputExistingDropdownOption = new DomElement
        {
            locator = ".ui-select-choices-row-inner"
        };

        private DomElement detailBillingEditFormInputNewCenPos = new DomElement
        {
            locator = "#cenpos-plugin"
        };

        private DomElement detailBillingEditFormInputNewCenPosForm = new DomElement
        {
            locator = "#TokenCardForm"
        };

        private DomElement detailBillingEditFormInputNewCenPosFormCardNumber = new DomElement
        {
            locator = "input[name='cardNumber']"
        };

        private DomElement detailBillingEditFormInputNewCenPosFormExpMonth = new DomElement
        {
            locator = "input[name='month']"
        };

        private DomElement detailBillingEditFormInputNewCenPosFormExpYear = new DomElement
        {
            locator = "input[name='year']"
        };

        private DomElement detailBillingEditFormInputNewCenPosFormCardName = new DomElement
        {
            locator = "input[name='cardName']"
        };

        private DomElement detailBillingEditFormInputNewCenPosFormCVV = new DomElement
        {
            locator = "input[name='cvv']"
        };

        private DomElement detailBillingEditSubmitButton = new DomElement
        {
            locator = "input.step-submit"
        };

        #endregion Billing

        #region Billing New Credit Card
        private DomElement detailContentiFrame = new DomElement
        {
            locator = ".content"
        };

        private DomElement detailBillingSectionlayoutPanelEdit = new DomElement
        {
            locator = ".edit"
        };

        private DomElement detailCenposPlugIn = new DomElement
        {
            locator = ".cenpos-plugin .ïframecenpos"
        };

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

        private DomElement detailBillingSectionCardForm = new DomElement
        {
            locator = ".layaoutPanelTokenCardForm"
        };
        #endregion

        #region Place Order

        private DomElement detailPlaceOrderSection = new DomElement
        {
            locator = ".placeorder "
        };

        private DomElement detailPlaceOrderContent = new DomElement
        {
            locator = ".content"
        };

        private DomElement detailPlaceOrderSubmitButton = new DomElement
        {
            locator = "#submit"
        };

        #endregion Place Order

        #region Cart

        private DomElement detailCartSection = new DomElement
        {
            locator = ".checkout-items"
        };

        private DomElement detailCartItems = new DomElement
        {
            locator = ".cart-items"
        };

        private DomElement detailCartItem = new DomElement
        {
            locator = ".cart-item"
        };

        private DomElement detailCartItemContent = new DomElement
        {
            locator = ".item-content"
        };

        private DomElement detailCartItemContentProduct = new DomElement
        {
            locator = ".product-section"
        };

        private DomElement detailCartItemContentProductSKU = new DomElement
        {
            locator = ".sku-section .sku"
        };

        private DomElement detailCartItemAvailability = new DomElement
        {
            locator = ".sku-availability-section"
        };

        private DomElement detailCartItemAvailabilityTag = new DomElement
        {
            locator = ".availability-content"
        };

        #endregion Cart

        #endregion Details Summary

        //Component Base Code Functions
        public IDictionary<string, string> AvailabiltyTagGet()
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

        public bool ContactButtonIsEnable()
        {
            DomElement customerForm = detailSummary.GetElementWaitByCSS(detailContactSection.locator);
            DomElement contactSubmitButton = customerForm.GetElementWaitByCSS(detailContactEditSubmitButton.locator);
            return contactSubmitButton.webElement.Enabled;
        }

        public void SetContactElement(ContactInputs field, string text)
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

        public void ContactSubmitClick()
        {
            DomElement contactForm = detailSummary.GetElementWaitByCSS(detailContactSection.locator);
            DomElement contactSubmitButton = contactForm.GetElementWaitByCSS(detailContactEditSubmitButton.locator);
            contactSubmitButton.webElement.Click();
        }

        public bool ShippingButtonIsEnable()
        {
            DomElement shippingSection = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement shippingForm = shippingSection.GetElementWaitByCSS(detailShippingEditForm.locator);
            DomElement addressSubmitButton = shippingForm.GetElementWaitByCSS(detailShippingEditSubmitButton.locator);
            return addressSubmitButton.webElement.Enabled;
        }

        public void ClickShippingButton()
        {
            DomElement shippingSection = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement shippingForm = shippingSection.GetElementWaitByCSS(detailShippingEditForm.locator);
            DomElement addressSubmitButton = shippingForm.GetElementWaitByCSS(detailShippingEditSubmitButton.locator);
            addressSubmitButton.webElement.Click();
        }

        public void SelectAddressRadioButton(AddressSelectOptions option)
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

        public void SelectFirstInAddressDropDown()
        {
            DomElement shippingForm = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement addressInput = shippingForm.GetElementWaitByCSS(detailShippingEditFormInput.locator);
            DomElement addressDropDown = addressInput.GetElementWaitByCSS(detailShippingEditFormInputExistingDropdown.locator);
            addressDropDown.webElement.Click();
            Thread.Sleep(2000);
            List<DomElement> dropdownOptions = addressDropDown.GetElementsWaitByCSS(detailShippingEditFormInputExistingDropdownOption.locator);
            dropdownOptions.FirstOrDefault().webElement.Click();
        }

        public void SetAddressElement(AddressInputs field, string text)
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

        public void ShippingSubmitClick()
        {
            DomElement shippingSection = detailSummary.GetElementWaitByCSS(detailShippingSection.locator);
            DomElement shippingForm = shippingSection.GetElementWaitByCSS(detailShippingEditForm.locator);
            DomElement addressSubmitButton = shippingForm.GetElementWaitByCSS(detailShippingEditSubmitButton.locator);
            addressSubmitButton.webElement.Click();
        }

        public bool BillingButtonIsEnable()
        {
            DomElement billingSection = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingForm = billingSection.GetElementWaitByCSS(detailBillingEditForm.locator);
            DomElement billingSubmitButton = billingForm.GetElementWaitByCSS(detailBillingEditSubmitButton.locator);
            return billingSubmitButton.webElement.Enabled;
        }

        public void SelectBillingRadioButton(BillingSelectOptions option)
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

        public void SelectFirstInBillingDropDown()
        {
            DomElement billingForm = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingDropDown = billingForm.GetElementWaitByCSS(detailBillingEditFormInputExistingDropdown.locator);
            billingDropDown.webElement.Click();
            DomElement billingOnlyDropdown = billingForm.GetElementWaitByCSS(detailBillingEditFormInputExistingDropdownOnly.locator);
            List<DomElement> dropdownOptions = billingOnlyDropdown.GetElementsWaitByCSS(detailBillingEditFormInputExistingDropdownOption.locator);
            dropdownOptions.FirstOrDefault().webElement.Click();
        }

        public void SetBillingElement(BillingInputs field, string text)
        {
            DomElement billingForm = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingContentIframe = billingForm.GetElementWaitByCSS(detailContentiFrame.locator);
            DomElement formEdit = billingContentIframe.GetElementWaitByCSS(detailBillingSectionlayoutPanelEdit.locator);
            Thread.Sleep(3000);
            IWebDriver iFrame = Driver.SwitchTo().Frame(FrameName);
            Thread.Sleep(2000);
            Helper.FindByCondition(By.CssSelector(CenposFrameForm.locator), (el) => el.Displayed);

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

        public void BillingSubmitClick()
        {
            DomElement billingSection = detailSummary.GetElementWaitByCSS(detailBillingSection.locator);
            DomElement billingForm = billingSection.GetElementWaitByCSS(detailBillingEditForm.locator);
            DomElement billingSubmitButton = billingForm.GetElementWaitByCSS(detailBillingEditSubmitButton.locator);
            billingSubmitButton.webElement.Click();
        }

        public bool PlaceOrderButtonIsEnable()
        {
            DomElement placeOrderSection = detailSummary.GetElementWaitByCSS(detailPlaceOrderSection.locator);
            DomElement placeOrderContent = placeOrderSection.GetElementWaitByCSS(detailPlaceOrderContent.locator);
            DomElement placeOrderSubmitButton = placeOrderContent.GetElementWaitByCSS(detailPlaceOrderSubmitButton.locator);
            return placeOrderSubmitButton.webElement.Enabled;
        }

        public OrderConfirmationPage PlaceOrderSubmitClick()
        {
            DomElement placeOrderSection = detailSummary.GetElementWaitByCSS(detailPlaceOrderSection.locator);
            DomElement placeOrderContent = placeOrderSection.GetElementWaitByCSS(detailPlaceOrderContent.locator);
            DomElement placeOrderSubmitButton = placeOrderContent.GetElementWaitUntil(detailPlaceOrderSubmitButton.locator,
                (el) =>  el.Displayed );
            //Added this try-catch due to randomly fail
            placeOrderSubmitButton.webElement.Click();

            return new OrderConfirmationPage(Driver);
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

        public void NextStep()
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

        public bool UserInfoIsPopulated()
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

        public void ClickEditAction(EditActions field)
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

        public void ClickOnAssingNewCard()
        {
            DomElement assignNewCardButton = detailSummary.GetElementWaitXpath(this.assignNewCardButton.locator);
            assignNewCardButton.webElement.Click();
        }


    }
}