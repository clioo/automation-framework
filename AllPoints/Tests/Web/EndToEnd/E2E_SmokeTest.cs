using AllPoints.AllPoints;
using AllPoints.Features;
using AllPoints.Features.Models;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.PageObjects.MyAccountPOM.DashboardPOM;
using AllPoints.PageObjects.MyAccountPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using AllPointsPOM.PageObjects.Base.Components.Modals;
using AllPointsPOM.PageObjects.MyAccountPOM.Enums;
using CommonHelper.Pages.CartPage.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllPoints.Tests.EndToEnd
{
    [TestClass]
    public class E2E_SmokeTest : AllPointsBaseTest
    {

        [TestMethod, TestCategory("Smoke")]
        public void E2E05()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            // log in
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login("jvillegas@dfsupply.com", "test1234");

            DashboardHomePage accountDashboard = indexPage.Header.ClickOnDashboard();
            Assert.IsTrue(accountDashboard.ContactInfoExist());

            ContactInfoEditPage contactInfoEdit = accountDashboard.ContactInfoEdit();
            Assert.AreEqual("Edit contact information", contactInfoEdit.GetSectionTitle());
            contactInfoEdit.ClickOnCancel();

            Assert.IsTrue(accountDashboard.AddressesExist());

            AddressesHomePage addressPage = accountDashboard.ClickAddressesLink();

            AddressModel addressModel = new AddressModel
            {
                street = "134 Manton Street",
                apartment = "134",
                state = "Pennsylvania",
                city = "Philadelphia",
                postal = "19147",
                country = "US",
                region = "PA",
                CompanyName = "Test company"
            };

            addressPage.ClickOnAddressInDropdownStateInitials(AccessLevel.User, addressModel);
            addressPage.WaitForAppBusy();
            addressPage.ContentModal.ClickOnCancel();
            addressPage.WaitForAppBusy();

            /**/
            AddAddressPage addAddressPage = addressPage.ClickOnAddNewAddress();

            AddressModel newAddressModel = new AddressModel
            {
                street = "134 Testing Street",
                apartment = "134",
                state = "Colorado",
                city = "Aspen",
                postal = "12345",
                country = "US",
                region = "CO",
                CompanyName = "Test company"
            };

            addAddressPage.SetInputValue(PageObjects.MyAccountPOM.AddressesPOM.AddressInputs.Country, newAddressModel.country);
            addAddressPage.SetInputValue(PageObjects.MyAccountPOM.AddressesPOM.AddressInputs.CompanyName, newAddressModel.CompanyName);
            addAddressPage.SetInputValue(PageObjects.MyAccountPOM.AddressesPOM.AddressInputs.Street, newAddressModel.street);
            addAddressPage.SetInputValue(PageObjects.MyAccountPOM.AddressesPOM.AddressInputs.Apartment, newAddressModel.apartment);
            addAddressPage.SetInputValue(PageObjects.MyAccountPOM.AddressesPOM.AddressInputs.City, newAddressModel.city);
            addAddressPage.SetInputValue(PageObjects.MyAccountPOM.AddressesPOM.AddressInputs.State, newAddressModel.state);
            addAddressPage.SetInputValue(PageObjects.MyAccountPOM.AddressesPOM.AddressInputs.Postal, newAddressModel.postal);

            addressPage = addAddressPage.ClickOnSubmit();
            addAddressPage.InformationModal.ClickOnClose();
            addressPage.WaitForAppBusy();


            addressPage.ClickOnAddressInDropdownStateInitials(AccessLevel.User, addressModel);
            addressPage.WaitForAppBusy();

            addressPage.ContentModal.ClickOnMakeDefault();
            addressPage.WaitForAppBusy();
            //TEMPORARY SOLUTION FOR LOADING ANIMATIONS AND LOADING TIMES
            System.Threading.Thread.Sleep(3000);

            addressPage.InformationModal.ClickOnClose();
            /*
            */

            accountDashboard = addressPage.Header.ClickOnDashboard();

            accountDashboard.WaitForAppBusy();
            Assert.IsTrue(accountDashboard.PaymentOptionsExist());

            PaymentOptionsEditPage paymentEditPage = accountDashboard.ClickEditPaymentLink();

            paymentEditPage.ClickOnCancel();

            PaymentOptionsHomePage paymentPage = accountDashboard.ClickPaymentsLink();
            Assert.IsTrue(paymentPage.PaymentOptionsTitleExist());
            IEnumerable<String> paymentItems = paymentPage.GetPaymentsDropdownItems(AccessLevel.Account);

            paymentPage.ClickOnPaymentOption(AccessLevel.Account, paymentItems.First());
            paymentPage.WaitForAppBusy();
            paymentPage.ContentModal.ClickOnMakeDefault();
            paymentPage.WaitForAppBusy();
            paymentPage.InfoModal.ClickOnClose();

            paymentItems = paymentPage.GetPaymentsDropdownItems(AccessLevel.User);

            paymentPage.ClickOnPaymentOption(AccessLevel.User, paymentItems.First());
            paymentPage.WaitForAppBusy();
            paymentPage.ContentModal.ClickOnMakeDefault();
            paymentPage.WaitForAppBusy();
            // TEMPORARY SOLUTION SO IFRAME LOADS AND DOM ELEMENT WAIT DOESN'T CRASH/FAIL THE TEST
            // *** FUTURE SOLUTION TO DO
            System.Threading.Thread.Sleep(4000);
            paymentPage.InfoModal.ClickOnClose();

            paymentPage.WaitForAppBusy();
            PaymentOptionsCreatePage paymentCreatePage = paymentPage.ClickOnAddNewCreditCard();
            // TEMPORARY SOLUTION SO IFRAME LOADS AND DOM ELEMENT WAIT DOESN'T CRASH/FAIL THE TEST
            // *** FUTURE SOLUTION TO DO
            System.Threading.Thread.Sleep(5000);

            PaymentOptionModel paymentOptionModel = new PaymentOptionModel
            {
                CardNumber = "4111111111111111",
                ExpirationMont = "12",
                ExpirationYear = "2022",
                Cvv = "077",
                HolderName = "Test Test",
                LastFourDigits = "1293"
            };
            paymentCreatePage.WaitForAppBusy();
            paymentCreatePage.FillCardTokenForm(paymentOptionModel);
            paymentCreatePage.ClickOnMakeDefault();
            paymentCreatePage.SelectPreviouslyStoreAddress("Test company");
            paymentCreatePage.ClickOnSubmit();
            System.Threading.Thread.Sleep(3000);

            // assert if click on submit is valid ( assert/bool that all fields are valid)
            // if assert for click on submit is valid then assert modal if creditcard was a succesfully added
            paymentPage = paymentCreatePage.CloseModal(ModalsEnum.Information);

            // TEMPORARY SOLUTION
            System.Threading.Thread.Sleep(3000);

            // step to change to a US adress and change state to colorado
            // this before searching for item


            //
            indexPage.Header.SetSearchFieldText("KE50750-4");
            CatalogItemsPage catalogItemsPage = indexPage.Header.ClickOnSearchButton();

            catalogItemsPage.AddToCartFirstItemInCatalog();

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

            // TEMPORARY SOLUTION
            System.Threading.Thread.Sleep(6000);

        }

        [TestMethod, TestCategory("Smoke")]
        public void E2E06()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            // log in
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login("jvillegas@dfsupply.com", "test1234");

            indexPage.Header.SetSearchFieldText("103-1035");
            CatalogItemsPage catalogItemsPage = indexPage.Header.ClickOnSearchButton();

            catalogItemsPage.AddToCartFirstItemInCatalog();

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

            Assert.IsTrue(cartPage.ShippingOptionsPriced());

            string shippingPrice = cartPage.SelectShipping("Showroom Pickup");
            cartPage.WaitForAppBusy();
            Assert.AreEqual(cartPage.GetShippingTotal(), "Free");

            shippingPrice = cartPage.SelectShipping("Ground");
            cartPage.WaitForAppBusy();
            Assert.AreEqual(cartPage.GetShippingTotal(), shippingPrice);

            APCheckoutPage checkoutPage = cartPage.ProceedToCheckOut();
            checkoutPage.WaitForAppBusy();
            checkoutPage.ClickShippingEdit();
            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.New);
            AddressModel addressModel = new AddressModel
            {
                street = "street 1",
                state = "Alabama",
                city = "city 1",
                postal = "12345",
                country = "Mexico",
                region = "CL",
                CompanyName = "test"
            };
            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, addressModel.street);
            checkoutPage.SetAddressElement(AddressInputs.City, addressModel.city);
            checkoutPage.SetAddressElement(AddressInputs.State, addressModel.state);
            checkoutPage.SetAddressElement(AddressInputs.Postal, addressModel.postal);
            checkoutPage.SetAddressElement(AddressInputs.Country, addressModel.country);
            checkoutPage.SetAddressElement(AddressInputs.ATTN, addressModel.CompanyName);
            checkoutPage.ShippingButtonIsEnable();
            checkoutPage.ShippingSubmitClick();
            System.Threading.Thread.Sleep(6000);

            //checkoutPage.NextStep();

            //Assert.AreEqual(checkoutPage.ShippingWarning(), "Cannot estimate shipping");

        }

        [TestMethod, TestCategory("Smoke")]
        public void E2E07()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            // log in
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login("jvillegas@dfsupply.com", "test1234");

            //TODO implementar wait, solucion temporal

            // Equipment manuals
            indexPage.Header.showEquipmentManualsSubmenu();
            System.Threading.Thread.Sleep(3000);
            indexPage.Header.selectEquipmentManualManufacturer("American Range");
            //TODO implementar wait, solucion temporal
            System.Threading.Thread.Sleep(3000);
            indexPage.Header.selectEquipmentManualModel("AMBG");
            //TODO implementar wait, solucion temporal
            System.Threading.Thread.Sleep(3000);
            indexPage.Header.selectEquipmentDocType("List of Replacement Parts");
            //TODO implementar wait, solucion temporal
            System.Threading.Thread.Sleep(8000);

            indexPage.Header.AddToCartPartNumberEqpManIframe("A29300");

            //TODO implementar wait, solucion temporal
            System.Threading.Thread.Sleep(5000);

            EquipmenManualIframeModal equipmentManualIframeModal = new EquipmenManualIframeModal(Driver);
            Assert.IsTrue(equipmentManualIframeModal.ModalMessage("A29300 has been added to your cart"));
            equipmentManualIframeModal.closeModal();

            //TODO implementar wait, solucion temporal
            System.Threading.Thread.Sleep(5000);

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

        }

    }
}
