using AllPoints.Features.Cart;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.CartPOM.Enums;
using AllPoints.PageObjects.OderConfirmPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllPoints.Features.OrderConfirm
{
    [TestClass]
    public class OrderConfirmationTest : FeatureBase
    {
        private CartDataFactory dataFactory;
        public string manufacturerOption = "";
        public string searchField = "";

        public OrderConfirmationTest()
        {
            dataFactory = new CartDataFactory();
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void ValidateAllElementsInConfirmationPage()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            var user = dataFactory.CreateLoginAccount();

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(user.Email, user.Password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInAddressDropDown();

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            PaymentOptionModel cardToken = new PaymentOptionModel
            {
                CardNumber = "4596502148863902",
                ExpirationMont = "03",
                ExpirationYear = "20",
                HolderName = "Dixie Green",
                Cvv = "775"
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

            Assert.IsTrue(orderpage.SuccessMsgIsPresent(),"Success Alert Message is not Displayed");

            Assert.IsTrue(orderpage.CustomerInfoIsDisplayed(),"Customer Information is not displayed");

            Assert.IsTrue(orderpage.ShippingInfoIsDisplayed(),"Shipping Information is not displayed");

            Assert.IsTrue(orderpage.BillingInfoIsDisplayed(),"Billing Information is not displayed");

            Assert.IsTrue(orderpage.listOfItemsDisplayed(), "List Item View is not Displaying Items");


        }
    }
}
