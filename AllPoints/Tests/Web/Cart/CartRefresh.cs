using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OderConfirmPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using AllPoints.PageObjects.OfferingPOM;
using System.Linq;
using AllPoints.TestDataModels;
using AllPoints.Constants;
using AllPoints.AllPoints;
using CommonHelper.Pages.CartPage.Enums;

namespace AllPoints.Features.Cart
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    public class CartRefresh : AllPointsBaseTest
    {
        public string manufacturerOption = "";
        public string searchField = "";

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void CarRefreshWhenAddedANON_T5504()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //select the third option on the dropdown by index
            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            //wait until busy animation is present
            catalogItemPage.WaitForAppBusy();

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void CarRefreshWhenRemovedANON_T5503()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //New method for Manufacturer dropdown
            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();
            catalogItemPage.AddtoCartbuttonInCatalog();

            //wait until busy animation is done
            catalogItemPage.WaitForAppBusy();

            APCartPage cartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Assert.IsTrue(cartMainPage.HeadsUpCartInfo(), "Totals are not displayed");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void CarRefreshSFLANON_T5502()
        {
            var indexPage = new APIndexPage(Driver, Url);

            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            catalogItemPage.WaitForAppBusy();

            APCartPage cartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Move items to Save For Later section
            cartMainPage.MovetoSFL();

            //TODO
            //wait until save for later section has rendered
            Thread.Sleep(5000);

            Assert.IsTrue(cartMainPage.HeadsUpCartInfo(), "Cart is not Empty");

            cartMainPage.SeeSFLItems();

            Assert.IsTrue(cartMainPage.MovetoCartLinkEnable(), "Save For Later Section is Empty");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void CarRefresh_ProductDetails()
        {
            var indexPage = new APIndexPage(Driver, Url);

            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            catalogItemPage.WaitForAppBusy();

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Assert.IsTrue(CartMainPage.ImageSectionInCart(), "Image is not Displayed");

            Assert.IsTrue(CartMainPage.TitleSectionIsDisplayed(), "Title is not Displayed");

            Assert.IsTrue(CartMainPage.SKUSectionIsDisplayed(), "SKU Section is not Displayed");

            Assert.IsTrue(CartMainPage.LineItemSectionIsDisplayed(), "Prices or Quantity are not Displayed");

            Assert.IsTrue(CartMainPage.AddToListLink(), "Add to list Link is not displayed");

        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]        
        public void ValidateMergeCart()
        {
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                email = "Email",
                password = "Password",
                country = "Country",
                countryShort = "CountryShort",
                address = "StreetAddress",
                state = "State",
                city = "City",
                zipCode = "ZipCode",
                apt = "Apt",
                firstname = "FirstName",
                lastname = "LastName",
                company = "Company",
                phonenumber = "PhoneNumber",
                attn = "ATTN",
            };

            manufacturerOption = indexPage.Header.GetManufacturerDropdownOptions().ElementAt(2);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.AddtoCartInOffering();

            Thread.Sleep(2000);

            APLoginPage loginPage = offeringProductpage.ClickOnSignInOffering();

            indexPage = loginPage.Login(testData.email, testData.password);

            Thread.Sleep(2000);

            offeringProductpage.SavingsMsgDisplayed();

            offeringProductpage.YourPriceLabelforAUTH();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AddAddressChecktoutoMyAccount()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //TODO
            //use a datafactory tool instead csv (shipping.csv)
            var testData = new
            {
                country = "Country",
                countryShort = "CountryShort",
                address = "StreetAddress",
                state = "State",
                city = "City",
                zipCode = "ZipCode",
                apt = "Apt",
                firstname = "FirstName",
                lastname = "LastName",
                company = "Company",
                phonenumber = "PhoneNumber",
                attn = "ATTN"
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test", "1234");


            manufacturerOption = indexPage.Header.GetManufacturerDropdownOptions().ElementAtOrDefault(2);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            APCheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            //checkoutPage.SelectAddressRadioButton(AddressSelectOptions.New);

            Thread.Sleep(5000);

            checkoutPage.SetAddressElement(AddressInputs.ATTN, testData.attn);

            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, testData.address);

            checkoutPage.SetAddressElement(AddressInputs.Apt, testData.apt);

            checkoutPage.SetAddressElement(AddressInputs.City, testData.city);

            checkoutPage.SetAddressElement(AddressInputs.State, testData.state);

            checkoutPage.SetAddressElement(AddressInputs.Postal, testData.zipCode);

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            //checkoutPage.SelectBillingRadioButton(BillingSelectOptions.Existing);
            PaymentOptionModel cardToken = new PaymentOptionModel
            {
                CardNumber = "4111111111111111",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077"
            };

            checkoutPage.SetBillingElement(BillingInputs.CardHolderName, cardToken.HolderName);
            checkoutPage.SetBillingElement(BillingInputs.CardNumber, cardToken.CardNumber);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationMonth, cardToken.ExpirationMont);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationYear, cardToken.ExpirationYear);
            checkoutPage.SetBillingElement(BillingInputs.CVV, cardToken.Cvv);

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInBillingDropDown();

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            Thread.Sleep(5000);

            checkoutPage.BillingSubmitClick();

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            Thread.Sleep(5000);

            //checkoutPage.PlaceOrderSubmitClick();
            OrderConfirmationPage orderpage = checkoutPage.PlaceOrderSubmitClick();

            Assert.IsTrue(checkoutPage.OrderConfirmationText(), "Order Confirmation text is not available");

            //APCheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            indexPage = orderpage.ContinueShoppingClick();

            //AddressesHomePage addresspage = indexPage.Header.ClickOnAddresses();

            //var dropdownItems = addresspage.GetAddressesDropdownItems(AccessLevel.User).ToList();

            ////just created address data
            //string createdAddress = string.IsNullOrEmpty(testData.apt) ?
            //    $"{testData.address}, {testData.city} {testData.countryShort} {testData.zipCode}"
            //    :
            //    $"{testData.address}, {testData.apt}, {testData.city} {testData.countryShort} {testData.zipCode}";

            ////search the address in the user level dropdown
            //string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));

            //Assert.IsNotNull(expectedAddress, "Address is not found in dropdown");
        }

        //Script with a new user with Contact Info and Address
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]        
        public void E2ETestwithContactInfoandAddressUser()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //TODO
            //use a datafactory tool instead csv (checkout.csv)
            var testData = new
            {
                country = "Country",
                countryShort = "CountryShort",
                address = "StreetAddress",
                state = "State",
                city = "City",
                zipCode = "ZipCode",
                apt = "Apt",
                firstname = "FirstName",
                lastname = "LastName",
                company = "Company",
                phonenumber = "PhoneNumber",
                attn = "ATTN",
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test", "1234");

            manufacturerOption = indexPage.Header.GetManufacturerDropdownOptions().ElementAtOrDefault(2);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            APCheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInAddressDropDown();

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            PaymentOptionModel cardToken = new PaymentOptionModel
            {
                CardNumber = "4111111111111111",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077"
            };

            checkoutPage.SetBillingElement(BillingInputs.CardHolderName, cardToken.HolderName);
            checkoutPage.SetBillingElement(BillingInputs.CardNumber, cardToken.CardNumber);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationMonth, cardToken.ExpirationMont);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationYear, cardToken.ExpirationYear);
            checkoutPage.SetBillingElement(BillingInputs.CVV, cardToken.Cvv);

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInBillingDropDown();

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            Thread.Sleep(5000);

            checkoutPage.BillingSubmitClick();

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            Thread.Sleep(5000);

            OrderConfirmationPage orderpage = checkoutPage.PlaceOrderSubmitClick();

            Assert.IsTrue(checkoutPage.OrderConfirmationText(), "Order Confirmation text is not available");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void CartEmptyHeadsUpMsg_ANON()
        {
            var indexPage = new APIndexPage(Driver, Url);

            APCartPage cartMainPage = indexPage.Header.ClickOnViewCart();

            cartMainPage.ContentHeadsUpInfo();   
        }        
    }
}