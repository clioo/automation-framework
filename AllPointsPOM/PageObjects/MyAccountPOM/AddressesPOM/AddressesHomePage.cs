using AllPoints.Features.Models;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Modals;
using AllPoints.PageObjects.MyAccountPOM.Enums;
using AllPoints.Pages.Components;
using CommonHelper;
using CommonHelper.Pages.AddressesPage;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllPoints.Pages
{
    public class AddressesHomePage : BaseAddressesHomePage
    {
        public Header Header;
        public AccountMenuLeft AccountMenuLeft;
        #region General web elements
        private DomElement DetailSection = new DomElement(By.CssSelector)
        {
            locator = ".detail-section"
        };

        private DomElement PageTitle = new DomElement
        {
            locator = "h1"
        };
        #endregion web elements

        #region Account section
        private DomElement DetailSectionAccountSection = new DomElement
        {
            locator = ".section-accountitems"
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

        private DomElement DetailSectionAccountItemselectorDropdrown = new DomElement
        {
            locator = ".item-selector .dropdown"
        };

        private DomElement DetailSectionAccountItemSelectorDropdrownElements = new DomElement
        {
            locator = ".ui-select-choices-row-inner"
        };

        private DomElement DetailSectionAccountItemselectorDropdownItemDefaultLabel = new DomElement
        {
            locator = "span.success"
        };
        #endregion Account section

        #region user level section
        private DomElement DetailSectionUserSection = new DomElement
        {
            locator = ".section-useritems"
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
            locator = ".licard-info .address-summary"
        };

        private DomElement DetailSectionUserItemselectorDropdrown = new DomElement
        {
            locator = ".item-selector .dropdown"
        };

        private DomElement DetailSectionUserItemselectorDropdownItemDefaultLabel = new DomElement
        {
            locator = "span.success"
        };
        #endregion user level section

        #region modals
        public AddressesContentModal ContentModal;

        public AddressesInformationModal InformationModal;

        public AddressesConfirmationModal ConfirmationModal;
        #endregion modals

        #region class constructor
        public AddressesHomePage(IWebDriver driver) : base(driver)
        {
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            DetailSection.Init(driver, SeleniumConstants.defaultWaitTime);

            ContentModal = new AddressesContentModal(driver);

            InformationModal = new AddressesInformationModal(driver);

            ConfirmationModal = new AddressesConfirmationModal(driver);
        }
        #endregion constructor

        public AddAddressPage ClickOnAddNewAddress()
        {
            var addNewAddressLink = DetailSection.GetElementWaitByCSS(DetailSectionUserSection.locator)
                .GetElementWaitByCSS(DetailSectionUserLiCards.locator)
                .GetElementWaitByCSS(DetailSectionUserLiCardsAddCard.locator)
                .GetElementWaitByCSS(DetailSectionUserLiCardsAddCardLinkText.locator);

            addNewAddressLink.webElement.Click();

            return new AddAddressPage(Driver);
        }

        public void ClickOnAddressInDropdown(AccessLevel level, AddressModel address)
        {
            string addressData = GetFullAddress(address.street, address.city, address.country, address.apartment, address.postal);

            var dropdown = GetDropdownByAccessLevel(level);

            SelectDropDownAutoCompleteOption(dropdown, addressData);
        }

        public IEnumerable<string> GetAddressesDropdownItems(AccessLevel level)
        {
            return GetItemsInDropdown(level).Select(x => x.webElement.Text);
        }

        public bool IsDefaultLabelPresentOnDropdownItem(AccessLevel level)
        {

            List<DomElement> dropdownItems = GetItemsInDropdown(level);
            DomElement defaultItem = dropdownItems.FirstOrDefault(item => item.webElement.Text.Contains("Default"));

            if (defaultItem == null)
            {
                throw new Exception("Default item is not found on dropdown: " + level);
            }

            //define the access level section for label
            DomElement defaultLabel = level == AccessLevel.Account ? DetailSectionAccountItemselectorDropdownItemDefaultLabel : DetailSectionUserItemselectorDropdownItemDefaultLabel;

            return defaultItem.IsElementPresent(defaultLabel.locator);
        }

        public string GetDefaultTileAddressData(AccessLevel level)
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

        public bool DefaultTileExist(AccessLevel level)
        {
            switch (level)
            {
                case AccessLevel.Account:
                    if (!DetailSection.IsElementPresent(DetailSectionAccountSection.locator)) return false;
                    var defaultAccountTile = DetailSection.GetElementWaitByCSS(DetailSectionAccountSection.locator)
                        .GetElementWaitByCSS(DetailSectionAccountLiCards.locator);

                    return defaultAccountTile.IsElementPresent(DetailSectionAccountLiCardsDefaultCard.locator);

                case AccessLevel.User:
                    var defaultUserTile = DetailSection.GetElementWaitByCSS(DetailSectionUserSection.locator)
                        .GetElementWaitByCSS(DetailSectionUserLiCards.locator);

                    return defaultUserTile.IsElementPresent(DetailSectionUserLiCardsDefaultCard.locator);

                default:
                    return false;
            }
        }

        public bool ConfirmationModalExist()
        {
            //edited by: Carlos Acosta
            //date: 13/feb/2019
            //comments: Helper deprecated
            List<DomElement> elements = DetailSection.GetElementsWait(ConfirmationModal.Container.locator);
            bool modalExists = elements.Count > 1;
            return modalExists;
        }

        public void ClickOnAddressInDropdownStateInitials(AccessLevel level, AddressModel address)
        {
            string addressData = GetAddressWithOutCountry(address.street, address.city, address.region, address.apartment, address.postal);
            var dropdown = GetDropdownByAccessLevel(level);
            SelectDropDownAutoCompleteOption(dropdown, addressData);
        }

        #region Private methods
        //this method should be implemented on test layer
        private string GetFullAddress(string street, string city, string country, string apt, string postal)
        {
            if (string.IsNullOrEmpty(apt)) return $@"{street},
{city}, {country} {postal}";

            return $@"{street}, {apt}
{city}, {country} {postal}";
        }

        private string GetAddressWithOutCountry(string street, string city, string region, string apt, string postal)
        {
            if (string.IsNullOrEmpty(apt)) return $@"{street},
            {city}, {region} {postal}";

            return $@"{street}, {apt}
            {city}, {region} {postal}";
        }


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
                    var accountLevelContainer = DetailSection.GetElementWaitByCSS(DetailSectionAccountSection.locator)
                        .GetElementWaitByCSS(DetailSectionAccountItemselectorDropdrown.locator);

                    return accountLevelContainer;

                case AccessLevel.User:
                    var userLevelDropdown = DetailSection.GetElementWaitByCSS(DetailSectionUserSection.locator)
                        .GetElementWaitByCSS(DetailSectionUserItemselectorDropdrown.locator);

                    return userLevelDropdown;

                default: throw new ArgumentException("Invalid access level value");
            }
        }
        #endregion Private methods
    }
}