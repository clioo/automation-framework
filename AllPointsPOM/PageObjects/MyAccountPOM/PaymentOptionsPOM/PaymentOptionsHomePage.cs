using AllPoints.Features.MyAccount.PaymentOptions.Modals;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;
using AllPoints.PageObjects.MyAccountPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using AllPoints.PageObjects.NewFolder1;
using AllPoints.TestDataModels;
using CommonHelper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM
{
    public class PaymentOptionsHomePage : AllPointsBaseWebPage
    {
        private DomElement DetailSection = new DomElement(By.CssSelector)
        {
            locator = ".detail-section"
        };

        private DomElement PageTitle = new DomElement
        {
            locator = "h1"
        };

        #region Account
        private DomElement DetailSectionAccountSection = new DomElement
        {
            locator = ".section-accountitems"
        };
        private DomElement DetailSectionAccountSectionSubtitle = new DomElement
        {
            locator = "h2"
        };
        private DomElement DetailSectionAccountLiCards = new DomElement
        {
            locator = ".licard-container"
        };
        private DomElement DetailSectionAccountLiCardsDefaultCard = new DomElement
        {
            locator = ".view"
        };
        private DomElement DetailSectionAccountLiCardsDefaultCardInfo = new DomElement
        {
            locator = ".licard-info"
        };
        private DomElement DetailSectionAccountItemselector = new DomElement
        {
            locator = ".item-selector"
        };
        private DomElement DetailSectionAccountItemselectorDropdrown = new DomElement
        {
            locator = ".dropdown"
        };
        private DomElement DetailSectionAccountItemSelectorDropdrownElements = new DomElement
        {
            locator = ".ui-select-choices-row-inner"
        };
        private DomElement DetailSectionAccountItemselectorDropdownItemDefaultLabel = new DomElement
        {
            locator = "span.success"
        };
        #endregion

        #region User
        private DomElement DetailSectionUserSection = new DomElement
        {
            locator = ".section-useritems"
        };
        private DomElement DetailSectionUserSectionSubtitle = new DomElement
        {
            locator = "h2"
        };
        private DomElement DetailSectionUserLiCards = new DomElement
        {
            locator = ".licard-container"
        };

        private DomElement DetailSectionUserLiCardsAddCard = new DomElement
        {
            locator = ".create"
        };

        private DomElement DetailSectionUserLiCardsAddCardLinkText = new DomElement
        {
            locator = ".create"
        };

        private DomElement DetailSectionUserLiCardsDefaultCard = new DomElement
        {
            locator = ".view"
        };

        private DomElement DetailSectionUserLiCardsDefaultCardInfo = new DomElement
        {
            locator = ".licard-info .card-token-summary"
        };

        private DomElement DetailSectionUserItemselector = new DomElement
        {
            locator = ".item-selector"
        };

        private DomElement DetailSectionUserItemselectorDropdrown = new DomElement
        {
            locator = ".dropdown"
        };

        private DomElement DetailSectionUserItemselectorDropdownItemDefaultLabel = new DomElement
        {
            locator = "span.success"
        };
        #endregion        

        #region modals
        public PaymentsContentModal ContentModal;
        public PaymentsInformationModal InfoModal;
        public PaymentsConfirmationModal ConfirmationModal;
        #endregion

        #region constructor        
        public PaymentOptionsHomePage(IWebDriver driver) : base(driver)
        {
            DetailSection.Init(driver, SeleniumConstants.defaultWaitTime);

            //initialize the modals too
            InfoModal = new PaymentsInformationModal(driver);
            ContentModal = new PaymentsContentModal(driver);
            ConfirmationModal = new PaymentsConfirmationModal(driver);
        }
        #endregion

        public bool PaymentOptionsTitleExist()
        {
            DomElement pageTitle = DetailSection.GetElementWaitByCSS(PageTitle.locator);

            return pageTitle.webElement.Displayed && pageTitle.webElement.Enabled;
        }

        public bool AccountLevelExist()
        {
            return DetailSection.IsElementPresent(DetailSectionAccountSection.locator);
        }

        public IEnumerable<string> GetPaymentsDropdownItems(AccessLevel level)
        {
            return GetItemsInDropdown(level).Select(x => x.webElement.Text);
        }

        public string GetDefaultTilePaymentData(AccessLevel level)
        {
            switch (level)
            {
                case AccessLevel.Account:
                    var defaultAccountTile = DetailSection.GetElementWaitByCSS(DetailSectionAccountSection.locator)
                        .GetElementWaitByCSS(DetailSectionAccountLiCards.locator)
                        .GetElementWaitByCSS(DetailSectionAccountLiCardsDefaultCard.locator)
                        .GetElementWaitByCSS(DetailSectionAccountLiCardsDefaultCardInfo.locator);

                    return defaultAccountTile.webElement.Text;

                case AccessLevel.User:
                    var defaultUserTile = DetailSection.GetElementWaitByCSS(DetailSectionUserSection.locator)
                        .GetElementWaitByCSS(DetailSectionUserLiCards.locator)
                        .GetElementWaitByCSS(DetailSectionUserLiCardsDefaultCard.locator)
                        .GetElementWaitByCSS(DetailSectionUserLiCardsDefaultCardInfo.locator);

                    return defaultUserTile.webElement.Text;

                default: throw new ArgumentException("Invalid access level");
            }
        }

        public PaymentOptionsCreatePage ClickOnAddNewCreditCard()
        {
            DomElement userLevelContainer = DetailSection.GetElementWaitByCSS(DetailSectionUserSection.locator);
            DomElement userLevelLicard = userLevelContainer.GetElementWaitByCSS(DetailSectionUserLiCards.locator);
            DomElement userLevelLicardTile = userLevelLicard.GetElementWaitByCSS(DetailSectionUserLiCardsAddCard.locator);

            DomElement addNewCreditCardLink = userLevelLicardTile.GetElementWaitByCSS(DetailSectionUserLiCardsAddCardLinkText.locator);
            addNewCreditCardLink.webElement.Click();

            return new PaymentOptionsCreatePage(Driver);
        }

        public bool IsDefaultLabelPresentOnDropdownItem(AccessLevel level)
        {

            List<DomElement> dropdownItems = GetItemsInDropdown(level);

            DomElement defaultItem = dropdownItems.FirstOrDefault(item => item.webElement.Text.Contains("Default"));

            if (defaultItem == null)
            {
                throw new Exception("Default item is not found on this dropdown: " + level);
            }

            DomElement defaultLabel = level == AccessLevel.Account ? DetailSectionAccountItemselectorDropdownItemDefaultLabel : DetailSectionUserItemselectorDropdownItemDefaultLabel;

            return defaultItem.IsElementPresent(defaultLabel.locator);
        }

        public void ClickOnPaymentOption(AccessLevel level, PaymentOptionModel payment)
        {
            DomElement paymentsDropdown = GetDropdownByAccessLevel(level);
            string paymentData = $"{payment.LastFourDigits} {payment.ExpirationMont}/{payment.ExpirationYear}";

            SelectDropDownAutoCompleteOption(paymentsDropdown, paymentData);
        }

        public void ClickOnPaymentOption(AccessLevel level, string termDetail)
        {
            DomElement paymentsDropdown = GetDropdownByAccessLevel(level);
            
            SelectDropDownAutoCompleteOption(paymentsDropdown, termDetail);
        }

        public bool ElementExistOnPage(AccessLevel level, ViewPaymentsElements element)
        {
            switch (level)
            {
                case AccessLevel.Account:
                    DomElement accountSection = DetailSection.GetElementWaitByCSS(DetailSectionAccountSection.locator);

                    return ElementIsPresentOnSection(accountSection, element);

                case AccessLevel.User:
                    DomElement userSection = DetailSection.GetElementWaitByCSS(DetailSectionUserSection.locator);

                    return ElementIsPresentOnSection(userSection, element);

                default:
                    return false;
            }
        }

        #region class private methods
        private List<DomElement> GetItemsInDropdown(AccessLevel level)
        {
            DomElement paymentsDropdown = GetDropdownByAccessLevel(level);

            return GetDropdownAutoCompleteOptions(paymentsDropdown);
        }

        private DomElement GetDropdownByAccessLevel(AccessLevel level)
        {
            switch (level)
            {
                case AccessLevel.Account:
                    DomElement accountLevelContainer = DetailSection.GetElementWaitByCSS(DetailSectionAccountSection.locator);
                    DomElement accountItemSelector = accountLevelContainer.GetElementWaitByCSS(DetailSectionAccountItemselector.locator);

                    return accountItemSelector.GetElementWaitByCSS(DetailSectionAccountItemselectorDropdrown.locator);

                case AccessLevel.User:
                    DomElement userLevelContainer = DetailSection.GetElementWaitByCSS(DetailSectionUserSection.locator);
                    DomElement userItemSelector = userLevelContainer.GetElementWaitByCSS(DetailSectionUserItemselector.locator);

                    return userItemSelector.GetElementWaitByCSS(DetailSectionUserItemselectorDropdrown.locator);

                default: throw new ArgumentException("Invalid access level value");
            }
        }

        private bool ElementIsPresentOnSection(DomElement container, ViewPaymentsElements element)
        {
            switch (element)
            {
                case ViewPaymentsElements.SectionSubtitle:
                    return container.IsElementPresent(DetailSectionAccountSectionSubtitle.locator);//can be only an H2 tag

                case ViewPaymentsElements.DefaultTile:
                    return container.IsElementPresent(DetailSectionAccountLiCardsDefaultCard.locator);

                default:
                    throw new ArgumentException("Element is not valid");
            }
        }
        #endregion
    }
}