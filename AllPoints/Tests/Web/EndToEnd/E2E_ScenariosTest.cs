using System.Threading;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OderConfirmPOM;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.AllPoints;
using AllPoints.Tests.Web.Cart;
using CommonHelper.Pages.CartPage.Enums;

namespace AllPoints.Tests.EndToEnd
{
    [TestClass]
    public class E2E_ScenariosTest : AllPointsBaseTest
    {
        private CartDataFactory dataFactory;

        public string manufacturerOption = "Fay";
        public string searchField = "Soft";

        public E2E_ScenariosTest()
        {
            dataFactory = new CartDataFactory();
        }

        [TestMethod, TestCategory("Smoke")]
        public void OnlyContactInformationadded()
        {
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                //Make sure to change name of email
                user = dataFactory.CreateLoginAccount(),

            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

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


        [TestMethod, TestCategory("Smoke")]
        public void OnlyAddressInformationadded()
        {
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                //Make sure to change name of email
                user = dataFactory.CreateLoginAccount(),

            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

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

        [TestMethod, TestCategory("Smoke")]
        public void OnlyPaymentMethodAdded()
        {
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                //Make sure to change name of email
                user = dataFactory.CreateLoginAccount(),

            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            Thread.Sleep(2000);

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            APCheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SetAddressElement(AddressInputs.ATTN, "CompanyOrName");
            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, "123 Massachusetts Avenue");
            checkoutPage.SetAddressElement(AddressInputs.Apt, "12345");
            checkoutPage.SetAddressElement(AddressInputs.City, "Cambridge");
            checkoutPage.SetAddressElement(AddressInputs.State, "MA");
            checkoutPage.SetAddressElement(AddressInputs.Postal, "02138");

            checkoutPage.ClickShippingButton();

            //Thread.Sleep(2000);

            Thread.Sleep(5000);

            checkoutPage.SelectBillingRadioButton(BillingSelectOptions.New);

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

            Thread.Sleep(2000);

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            checkoutPage.BillingSubmitClick();

            Thread.Sleep(3000);

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            Thread.Sleep(5000);

            OrderConfirmationPage orderpage = checkoutPage.PlaceOrderSubmitClick();

            Assert.IsTrue(checkoutPage.OrderConfirmationText(), "Order Confirmation text is not available");
        }

        [TestMethod, TestCategory("Smoke")]
        public void AddingAllInformationNeeded()
        {
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                //Make sure to change name of email
                user = dataFactory.CreateLoginAccount(),
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            APCheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            Thread.Sleep(5000);

            checkoutPage.SetAddressElement(AddressInputs.ATTN, "CompanyOrName");
            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, "123 Massachusetts Avenue");
            checkoutPage.SetAddressElement(AddressInputs.Apt, "12345");
            checkoutPage.SetAddressElement(AddressInputs.City, "Cambridge");
            checkoutPage.SetAddressElement(AddressInputs.State, "MA");
            checkoutPage.SetAddressElement(AddressInputs.Postal, "02138");

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

            Thread.Sleep(2000);

            checkoutPage.SelectFirstInBillingDropDown();

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            Thread.Sleep(3000);

            checkoutPage.BillingSubmitClick();

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            checkoutPage.BillingSubmitClick();

            Thread.Sleep(5000);

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            Thread.Sleep(5000);

            OrderConfirmationPage orderpage = checkoutPage.PlaceOrderSubmitClick();

            Assert.IsTrue(checkoutPage.OrderConfirmationText(), "Order Confirmation text is not available");
        }

    }
}
