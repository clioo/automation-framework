using AllPoints.PageObjects.CartPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AllPoints.Features.Cart
{
    [TestClass]
    public class Cart : FeatureBase
    {
        //TODO: All Test must Wait till "Loading Cart finish"
        [TestMethod]
        [TestCategory("Regression")]
        public void ValidateCartIsDisplay()
        {
            IndexPage indexPage = new IndexPage(driver);

            indexPage.Init(url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            CartPage cartPage = indexPage.Header.ClickOnViewCart();

            string htmlElementsExists = cartPage.AllComponentsExist();
            Assert.IsTrue(string.IsNullOrEmpty(htmlElementsExists), htmlElementsExists);
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void ValidateAvailabilityIsDisplay()
        {
            IndexPage indexPage = new IndexPage(driver);

            indexPage.Init(url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            CartPage cartPage = indexPage.Header.ClickOnViewCart();

            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsTrue(availabiltyItemsTag.Count() > 0);
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void Availability_InStock()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            CartPage cartPage = indexPage.Header.ClickOnViewCart();

            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.InStock)));
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void Availability_Limited()
        {
            IndexPage indexPage = new IndexPage(driver,url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            CartPage cartPage = indexPage.Header.ClickOnViewCart();
            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.Limited)));
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void Availability_OutOfStock()
        {
            IndexPage indexPage = new IndexPage(driver,url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            CartPage cartPage = indexPage.Header.ClickOnViewCart();

            IDictionary<string, string> availabiltyItemsTag = cartPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.OutOfStockGeneral)));
        }
    }
}