using AllPoints.AllPoints;
using AllPoints.Constants;
using AllPoints.PageObjects.CartPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AllPoints.Features.Cart
{
    //TODO
    //check all these test methods
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    public class CartTest : AllPointsBaseTest
    {
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateCartIsDisplayed()
        {
            APIndexPage indexPage = new APIndexPage(Driver);

            indexPage.Init(Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

            string htmlElementsExists = cartPage.AllComponentsExist();
            Assert.IsTrue(string.IsNullOrEmpty(htmlElementsExists), htmlElementsExists);
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateAvailabilityIsDisplay()
        {
            APIndexPage indexPage = new APIndexPage(Driver);

            indexPage.Init(Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsTrue(availabiltyItemsTag.Count() > 0);
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_InStock()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.InStock)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_Limited()
        {
            APIndexPage indexPage = new APIndexPage(Driver,Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();
            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.Limited)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_OutOfStock()
        {
            APIndexPage indexPage = new APIndexPage(Driver,Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.OutOfStockGeneral)));
        }
    }
}