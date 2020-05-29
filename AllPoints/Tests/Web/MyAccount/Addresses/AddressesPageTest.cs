﻿using AllPoints.Constants;
using AllPoints.Features.Models;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM;
using AllPoints.PageObjects.MyAccountPOM.Enums;
using AllPoints.Pages;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace AllPoints.AllPoints.Web.MyAccount.Addresses
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    [TestCategory(TestCategoriesConstants.ReadyToGo)]
    public class AddressesPageTest : AllPointsBaseTest
    {
        #region View
        //TODO
        //NoSuchElementException "modal-dialog .modal-content"
        //Test case on test rail -> C1109
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void DesignateUserAddressAsDefault()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                country = "US",
                street = "elm street",
                city = "elm city",
                CompanyName = "KDA",
                postal = "22800",
                state = "CO"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            Assert.IsNotNull(indexPage, "login failed");

            AddressesHomePage addressesHome = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesHome.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);

            addressesHome = createAddressPage.ClickOnSubmit();

            addressesHome.InformationModal.ClickOnClose();

            addressesHome.ClickOnAddressInDropdown(AccessLevel.User, testData);
            addressesHome.ContentModal.ClickOnMakeDefault();

            //TODO
            //check if busy is present
            addressesHome.WaitForAppBusy();

            addressesHome.InformationModal.ClickOnClose();

            bool defaultLabelExist = addressesHome.IsDefaultLabelPresentOnDropdownItem(AccessLevel.User);
            string actualDefaultAddress = addressesHome.GetDefaultTileAddressData(AccessLevel.User);

            Assert.IsTrue(defaultLabelExist, "Default label does not exist");

            Assert.IsTrue(actualDefaultAddress.Contains(testData.street));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.country));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.postal));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.state));

            bool accountTileExist = addressesHome.DefaultTileExist(AccessLevel.Account);

            Assert.IsFalse(accountTileExist, "default account level tile is already present");
        }

        //TODO
        //account address data factory response deserealization
        //Test Case on test rail -> C1108
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void DesignateAccountAddressAsDefault()
        {
            //test data
            TestAccountAddress testAddress = new TestAccountAddress
            {
                Apartment = "349",
                City = "Denver",
                Country = "US",
                CompanyName = "dfs",
                Postal = "80019",
                StateProvinceRegion = "CO",
                Street = "Walnut street",
                ExternalIdentifier = nameof(testAddress) + "C1108"
            };
            AddressModel selectedAddress = new AddressModel
            {
                apartment = testAddress.Apartment,
                city = testAddress.City,
                country = testAddress.Country,
                CompanyName = testAddress.CompanyName,
                postal = testAddress.Postal,
                state = testAddress.StateProvinceRegion,
                street = testAddress.Street
            };

            //this method is not ready
            var testUser = DataFactory.Users.CreateTestUser();

            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesHome = indexPage.Header.ClickOnAddresses();

            addressesHome.ClickOnAddressInDropdown(AccessLevel.Account, selectedAddress);
            addressesHome.ContentModal.ClickOnMakeDefault();

            //TODO
            //check if busy is present
            addressesHome.WaitForAppBusy();
            addressesHome.InformationModal.ClickOnClose();

            bool defaultLabelExist = addressesHome.IsDefaultLabelPresentOnDropdownItem(AccessLevel.Account);
            string actualDefaultAddress = addressesHome.GetDefaultTileAddressData(AccessLevel.Account);

            Assert.IsTrue(defaultLabelExist, "Default label does not exist");
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.Street), $"street not found, {testAddress.Street}");
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.Apartment), $"apartment not found, {testAddress.Apartment}");
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.Country), $"country not found, {testAddress.Country}");
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.Postal), $"postal not found, {testAddress.Postal}");
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.StateProvinceRegion), $"state not found, {testAddress.StateProvinceRegion}");

            bool userTileExist = addressesHome.DefaultTileExist(AccessLevel.User);

            Assert.IsFalse(userTileExist, "default user level tile is already present");
        }

        //Test case on test rail -> C1390, C1391
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void CheckBothDropdownsItemsCount()
        {
            //TODO: this TC needs specific test data (20 level account/user addresses)
            var testData = new { Email = "", Password = "" };


            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.Email, testData.Password);

            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();

            int accountLevelItemsCount = addressesPage.GetAddressesDropdownItems(AccessLevel.Account).Count();
            int userLevelItemsCount = addressesPage.GetAddressesDropdownItems(AccessLevel.User).Count();

            Assert.IsTrue(accountLevelItemsCount > 20, "Account level dropdpown items count is not as expected");
            Assert.IsTrue(userLevelItemsCount > 20, "User level dropdown items count is not as expected");

            //addressesPage.Header.ClickOnSignOut();
        }
        #endregion View

        #region Delete
        //TODO
        //stale elment exception
        //Test case on test rail C1114
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void DeleteAddressCase()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                CompanyName = "QA Softtek",
                street = "Walnut street",
                apartment = "07",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesPage.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testData.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);

            //TODO
            createAddressPage.ClickOnSubmitt();
            //addressesPage = createAddressPage.InformationModal.ClickOnClose();

            addressesPage = createAddressPage.ClickOnSubmit();

            addressesPage.InformationModal.ClickOnClose();

            //delete address
            //TODO
            System.Threading.Thread.Sleep(1500);
            addressesPage.WaitForAppBusy();

            addressesPage.ClickOnAddressInDropdown(AccessLevel.User, testData);

            addressesPage.ContentModal.ClickOnDelete();

            addressesPage.WaitForAppBusy();

            addressesPage.ConfirmationModal.ClickOnDelete();

            addressesPage.WaitForAppBusy();

            var dropdownItems = addressesPage.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //deleted address data
            string deletedAddress = FormatAddress(testData);

            //search the address in the user level dropdown
            string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(deletedAddress));

            Assert.IsNull(expectedAddress, "Selected address is still in the dropdown o:");
        }

        //TODO
        //Test case on test rail -> C1
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void DeleteAddressThatIsDefault()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                CompanyName = "QA Automation Softtek",
                street = "test street",
                apartment = "07",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            Assert.IsNotNull(indexPage, "Login failed");

            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesPage.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testData.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);
            createAddressPage.SetCheckboxDefault();

            addressesPage = createAddressPage.ClickOnSubmit();

            addressesPage.InformationModal.ClickOnClose();

            //TODO
            //does not find any address in dropdown
            System.Threading.Thread.Sleep(1000);
            addressesPage.ClickOnAddressInDropdown(AccessLevel.User, testData);

            addressesPage.ContentModal.ClickOnDelete();

            //validating that only exist 1 modal
            bool confirmationModalExist = addressesPage.ConfirmationModalExist();

            Assert.IsFalse(confirmationModalExist, "This element should not exist");
        }
        #endregion Delete

        #region Add
        //TODO
        //NoSuchElementException "modal-dialog .modal-content"
        //Test case on test rail -> C1096
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AddUserLevelAddressIgnoreAutoComplete()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testAddress = new AddressModel
            {
                apartment = "07",
                city = "Denver",
                country = "US",
                postal = "12345",
                state = "CO",
                street = "Walnut street",
                CompanyName = "QA Softtek"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesPage.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testAddress.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testAddress.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testAddress.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testAddress.city);
            createAddressPage.SetInputValue(AddressInputs.State, testAddress.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testAddress.postal);

            bool submitButtonEnabled = createAddressPage.AddNewButtonIsEnabled();
            Assert.IsTrue(submitButtonEnabled, "Submit button is not enabled");

            addressesPage = createAddressPage.ClickOnSubmit();

            addressesPage.InformationModal.ClickOnClose();

            var dropdownItems = addressesPage.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //just created address data
            string createdAddress = FormatAddress(testAddress);

            //search the address in the user level dropdown
            string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));

            Assert.IsNotNull(expectedAddress, "Address is not found in dropdown");
        }

        //test case on test rail -> C1097
        //TODO
        //.modal-content
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void AddUserLevelAddressAutoComplete()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testAddress = new AddressModel
            {
                apartment = "07",
                city = "Denver",
                country = "US",
                postal = "12345",
                state = "CO",
                street = "Walnut street",
                CompanyName = "QA Softtek"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesHomePage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createNewAddressPage = addressesHomePage.ClickOnAddNewAddress();

            //fill address form
            createNewAddressPage.SetInputValue(AddressInputs.CompanyName, testAddress.CompanyName);
            createNewAddressPage.SetStreetAutoComplete(testAddress.street, testAddress.city, testAddress.state, testAddress.country);
            createNewAddressPage.SetInputValue(AddressInputs.Apartment, testAddress.apartment);
            createNewAddressPage.SetInputValue(AddressInputs.Postal, testAddress.postal);

            bool submitButtonEnabled = createNewAddressPage.AddNewButtonIsEnabled();
            Assert.IsTrue(submitButtonEnabled, "Submit button is not enabled");

            addressesHomePage = createNewAddressPage.ClickOnSubmit();

            addressesHomePage.InformationModal.ClickOnClose();

            var dropdownItems = addressesHomePage.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //just created address data
            string createdAddress = FormatAddress(testAddress);

            //search the address in the user level dropdown
            string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));

            Assert.IsNotNull(expectedAddress, "Address is not found in dropdown");
        }

        //Test case on test rail -> C1098
        //TODO
        //NoSuchElementException "modal-dialog .modal-content"
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AddUserLevelAddressAutocompleteEdited()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testAddress = new AddressModel
            {
                apartment = "07",
                city = "Denver",
                country = "US",
                postal = "12345",
                state = "CO",
                street = "Walnut street",
                CompanyName = "QA Softtek"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesHomePage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressesPage = addressesHomePage.ClickOnAddNewAddress();

            //fill address form
            createAddressesPage.SetInputValue(AddressInputs.CompanyName, testAddress.CompanyName);
            createAddressesPage.SetStreetAutoComplete(testAddress.street, testAddress.city, testAddress.state, testAddress.country);
            createAddressesPage.SetInputValue(AddressInputs.Apartment, testAddress.apartment);
            createAddressesPage.SetInputValue(AddressInputs.City, testAddress.city);
            createAddressesPage.SetInputValue(AddressInputs.Postal, testAddress.postal);

            bool submitButtonEnabled = createAddressesPage.AddNewButtonIsEnabled();
            Assert.IsTrue(submitButtonEnabled, "Submit button is not enabled");

            addressesHomePage = createAddressesPage.ClickOnSubmit();

            addressesHomePage.InformationModal.ClickOnClose();

            var dropdownItems = addressesHomePage.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //just created address data
            string createdAddress = FormatAddress(testAddress);

            //search the address in the user level dropdown
            string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));

            Assert.IsNotNull(expectedAddress, "Address is not found in dropdown");
        }

        //TODO
        //modal.content NoSuchElementException
        //Test case on test rail -> C1231, C1232
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AddInternationalAddress()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testAddress = new AddressModel
            {
                country = "MX",
                CompanyName = "intl address ;)",
                street = "calle sexta",
                state = "Estado de Baja California",
                city = "Ensenada",
                postal = "22790",
                apartment = "12"
            };
            string defaultCountry = "US - United States";
            var nonAceptedCountries = new
            {
                Somalia = "Somalia",
                Cuba = "Cuba",
                Iran = "Iran",
                NorthKorea = "North Korea",
                Lebannon = "Lebannon",
                Syria = "Syria"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage addAddressPage = addressesPage.ClickOnAddNewAddress();

            string expectedCountry = addAddressPage.GetCurrentCountryValue();
            var currentCountries = addAddressPage.GetCountriesFromDropdown().ToList();

            addAddressPage.SetInputValue(AddressInputs.Country, testAddress.country);
            addAddressPage.SetInputValue(AddressInputs.CompanyName, testAddress.CompanyName);
            addAddressPage.SetInputValue(AddressInputs.State, testAddress.state);
            addAddressPage.SetInputValue(AddressInputs.Street, testAddress.street);
            addAddressPage.SetInputValue(AddressInputs.City, testAddress.city);
            addAddressPage.SetInputValue(AddressInputs.Postal, testAddress.postal);
            addAddressPage.SetInputValue(AddressInputs.Apartment, testAddress.apartment);

            addressesPage = addAddressPage.ClickOnSubmit();

            addressesPage.InformationModal.ClickOnClose();

            var dropdownItems = addressesPage.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //just created address data
            string createdAddress = FormatAddress(testAddress);

            string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));

            Assert.IsTrue(defaultCountry.Contains(expectedCountry), $"'{expectedCountry}' is not the default country");
            CollectionAssert.DoesNotContain(currentCountries, nonAceptedCountries.Cuba, $"'{nonAceptedCountries.Cuba}' cannot be here");
            CollectionAssert.DoesNotContain(currentCountries, nonAceptedCountries.Iran, $"'{nonAceptedCountries.Iran}' cannot be here");
            CollectionAssert.DoesNotContain(currentCountries, nonAceptedCountries.Lebannon, $"'{nonAceptedCountries.Lebannon}' cannot be here");
            CollectionAssert.DoesNotContain(currentCountries, nonAceptedCountries.NorthKorea, $"'{nonAceptedCountries.NorthKorea}' cannot be here");
            CollectionAssert.DoesNotContain(currentCountries, nonAceptedCountries.Somalia, $"'{nonAceptedCountries.Somalia}' cannot be here");
            CollectionAssert.DoesNotContain(currentCountries, nonAceptedCountries.Syria, $"'{nonAceptedCountries.Syria}' cannot be here");
            Assert.IsNotNull(expectedAddress, "The just added address is not found on the dropdown");
        }

        //TODO
        //"The just added address is not found on the dropdown"
        //Test case on test rail -> C1234, C1112
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AddIntlAddressSetAsDefault()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testAddress = new AddressModel
            {
                country = "MX",
                CompanyName = "intl address ;)",
                street = "calle sexta",
                state = "Estado de Baja California",
                city = "Ensenada",
                postal = "22790",
                apartment = "12"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage addAddressPage = addressesPage.ClickOnAddNewAddress();

            string expectedCountry = addAddressPage.GetCurrentCountryValue();
            var currentCountries = addAddressPage.GetCountriesFromDropdown().ToList();

            addAddressPage.SetInputValue(AddressInputs.Country, testAddress.country);
            addAddressPage.SetInputValue(AddressInputs.CompanyName, testAddress.CompanyName);
            addAddressPage.SetInputValue(AddressInputs.State, testAddress.state);
            addAddressPage.SetInputValue(AddressInputs.Street, testAddress.street);
            addAddressPage.SetInputValue(AddressInputs.City, testAddress.city);
            addAddressPage.SetInputValue(AddressInputs.Postal, testAddress.postal);
            addAddressPage.SetInputValue(AddressInputs.Apartment, testAddress.apartment);
            addAddressPage.SetCheckboxDefault();

            //return new addressPage when modal is closed because its the action that creates the redirection
            addressesPage = addAddressPage.ClickOnSubmit();

            addressesPage.InformationModal.ClickOnClose();

            var dropdownItems = addressesPage.GetAddressesDropdownItems(AccessLevel.User).ToList();
            bool defaultLabelExist = addressesPage.IsDefaultLabelPresentOnDropdownItem(AccessLevel.User);
            string actualDefaultAddress = addressesPage.GetDefaultTileAddressData(AccessLevel.User);

            //just created address data
            string createdAddress = FormatAddress(testAddress);

            string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));
            bool userLevelTileExist = addressesPage.DefaultTileExist(AccessLevel.User);

            Assert.IsNotNull(expectedAddress, "The just added address is not found on the dropdown");
            Assert.IsTrue(defaultLabelExist, "Default label does not exist");

            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.street));
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.apartment));
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.country));
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.postal));
            Assert.IsTrue(actualDefaultAddress.Contains(testAddress.state));
            Assert.IsTrue(userLevelTileExist, "default user level tile is already present");
        }
        #endregion Add

        #region Edit
        //TODO
        //NoSuchElementException modal dialog
        //Test case on test rail -> C1099
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void EditUserLevelAddress()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                CompanyName = "QA Softtek",
                street = "Walnut street",
                apartment = "07",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345"
            };
            AddressModel newAddress = new AddressModel
            {
                street = "Elm street",
                apartment = "apt 123",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345",
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesHome = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesHome.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testData.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);

            addressesHome = createAddressPage.ClickOnSubmit();

            addressesHome.InformationModal.ClickOnClose();

            //Start updating the address
            addressesHome.ClickOnAddressInDropdown(AccessLevel.User, testData);
            EditAddressPage editAddressPage = addressesHome.ContentModal.ClickOnEdit();
            editAddressPage.SetInputValue(AddressInputs.Street, newAddress.street);
            editAddressPage.SetInputValue(AddressInputs.Apartment, newAddress.apartment);
            addressesHome = editAddressPage.ClickOnSubmit();

            //TODO
            //check if an animation is present
            addressesHome.WaitForAppBusy();
            //TODO
            //dropdown is not populated
            System.Threading.Thread.Sleep(1500);
            var dropdownItems = addressesHome.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //just edited address data
            string expectedAddress = FormatAddress(newAddress);

            //search the address in the user level dropdown
            string actualAddress = dropdownItems.FirstOrDefault();

            Assert.AreEqual(expectedAddress, actualAddress);
        }

        //Test case on test rail -> C1100
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditDefaultUserLevelAddress()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                CompanyName = "QA Softtek",
                street = "street A",
                apartment = "07",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesPage.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testData.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);

            addressesPage = createAddressPage.ClickOnSubmit();

            addressesPage.InformationModal.ClickOnClose();

            //Start updating the address
            addressesPage.ClickOnAddressInDropdown(AccessLevel.User, testData);

            EditAddressPage editAddressPage = addressesPage.ContentModal.ClickOnEdit();
            editAddressPage.SetCheckboxDefault();
            addressesPage = editAddressPage.ClickOnSubmit();
            //TODO
            //check if busy animation is present
            addressesPage.WaitForAppBusy();
            System.Threading.Thread.Sleep(1000);

            bool defaultLabelExist = addressesPage.IsDefaultLabelPresentOnDropdownItem(AccessLevel.User);
            string actualDefaultAddress = addressesPage.GetDefaultTileAddressData(AccessLevel.User);

            Assert.IsTrue(defaultLabelExist, "Default label does not exist");
            Assert.IsTrue(actualDefaultAddress.Contains(testData.street));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.apartment));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.country));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.postal));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.state));

            //addressesPage.Header.ClickOnSignOut();
        }

        //Test case on test rail -> ?
        //TODO
        //assert fails
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditAddressWithNoChangesMade()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                CompanyName = "QA Softtek",
                street = "Walnut street",
                apartment = "07",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            Assert.IsNotNull(indexPage);

            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesPage.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testData.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);
            createAddressPage.SetCheckboxDefault();

            addressesPage = createAddressPage.ClickOnSubmit();

            addressesPage.InformationModal.ClickOnClose();

            //start updating the address
            addressesPage.ClickOnAddressInDropdown(AccessLevel.User, testData);

            EditAddressPage editAddressPage = addressesPage.ContentModal.ClickOnEdit();
            editAddressPage.SetCheckboxDefault();
            editAddressPage.SetCheckboxDefault();
            editAddressPage.ClickOnSubmit();

            //TODO
            //check if busy animation is present
            editAddressPage.WaitForAppBusy();

            addressesPage = editAddressPage.NoEditsModal.ClickOnCloseFooter();

            bool defaultLabelExist = addressesPage.IsDefaultLabelPresentOnDropdownItem(AccessLevel.User);

            string actualDefaultAddress = addressesPage.GetDefaultTileAddressData(AccessLevel.User);

            Assert.IsTrue(defaultLabelExist, "Default label does not exist");

            Assert.IsTrue(actualDefaultAddress.Contains(testData.street));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.apartment));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.country));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.postal));
            Assert.IsTrue(actualDefaultAddress.Contains(testData.state));
        }

        //TODO
        //modal.content NoSuchElementException
        //Test case on test rail -> C1233
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditInternationalAddress()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                CompanyName = "QA Softtek",
                street = "Walnut street",
                apartment = "07",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345"
            };
            var newAddress = new AddressModel
            {
                street = "Calle septima",
                apartment = "suite 12",
                state = "estado de baja california",
                city = "ensenada",
                country = "MX",
                postal = "22890",
            };
            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesHome = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesHome.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testData.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);

            addressesHome = createAddressPage.ClickOnSubmit();

            addressesHome.InformationModal.ClickOnClose();

            //start updating the address
            addressesHome.ClickOnAddressInDropdown(AccessLevel.User, testData);

            EditAddressPage editAddressPage = addressesHome.ContentModal.ClickOnEdit();

            editAddressPage.SetInputValue(AddressInputs.Country, newAddress.country);
            editAddressPage.SetInputValue(AddressInputs.State, newAddress.state);
            editAddressPage.SetInputValue(AddressInputs.Street, newAddress.street);
            editAddressPage.SetInputValue(AddressInputs.City, newAddress.city);
            editAddressPage.SetInputValue(AddressInputs.Apartment, newAddress.apartment);
            editAddressPage.SetInputValue(AddressInputs.Postal, newAddress.postal);

            addressesHome = editAddressPage.ClickOnSubmit();

            System.Threading.Thread.Sleep(1500);

            var dropdownItems = addressesHome.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //just edited address data
            string createdAddress = FormatAddress(newAddress);

            //search the address in the user level dropdown
            string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));

            Assert.IsNotNull(expectedAddress, "Address is not found in dropdown");
        }

        //TODO
        //NoSuchElementException "modal-dialog .modal-content"
        //Test case on test rail -> C1235
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditIntlAddressSetAsDefault()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            AddressModel testData = new AddressModel
            {
                CompanyName = "QA Softtek",
                street = "Walnut street",
                apartment = "07",
                state = "CO",
                city = "Denver",
                country = "US",
                postal = "12345"
            };
            AddressModel newAddress = new AddressModel
            {
                street = "Calle novena",
                apartment = "apt 2",
                state = "estado de baja california",
                city = "ensenada",
                country = "MX",
                postal = "22780",
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            AddressesHomePage addressesHome = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesHome.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testData.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testData.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testData.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testData.city);
            createAddressPage.SetInputValue(AddressInputs.State, testData.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testData.postal);

            addressesHome = createAddressPage.ClickOnSubmit();

            addressesHome.InformationModal.ClickOnClose();

            //start to update the address
            addressesHome.ClickOnAddressInDropdown(AccessLevel.User, testData);

            EditAddressPage editAddressPage = addressesHome.ContentModal.ClickOnEdit();

            editAddressPage.SetInputValue(AddressInputs.Country, newAddress.country);
            editAddressPage.SetInputValue(AddressInputs.State, newAddress.state);
            editAddressPage.SetInputValue(AddressInputs.Street, newAddress.street);
            editAddressPage.SetInputValue(AddressInputs.City, newAddress.city);
            editAddressPage.SetInputValue(AddressInputs.Apartment, newAddress.apartment);
            editAddressPage.SetInputValue(AddressInputs.Postal, newAddress.postal);
            editAddressPage.SetCheckboxDefault();

            addressesHome = editAddressPage.ClickOnSubmit();

            //TODO
            addressesHome.WaitForAppBusy();

            var dropdownItems = addressesHome.GetAddressesDropdownItems(AccessLevel.User).ToList();

            //just edited address data
            string expectedAddress = "Default " + FormatAddress(newAddress);
            string actualAddress = dropdownItems.FirstOrDefault();

            //search the address in the user level dropdown
            Assert.AreEqual(expectedAddress, actualAddress);
        }
        #endregion Edit

        //helper method
        private string FormatAddress(AddressModel address)
        {
            string HasApartment() => string.IsNullOrEmpty(address.apartment) || string.IsNullOrWhiteSpace(address.apartment) ?
                "" : $" {address.apartment}";

            string createdAddress = string.IsNullOrEmpty(address.apartment) ?
                $@"{address.street},
{address.city}, {address.country} {address.postal}"
                :
                $@"{address.street}, {address.apartment}
{address.city}, {address.country} {address.postal}";


            string stringAddress = $@"{address.street},{HasApartment()}
{address.city}, {address.country} {address.postal}";

            return stringAddress;
        }
    }
}
