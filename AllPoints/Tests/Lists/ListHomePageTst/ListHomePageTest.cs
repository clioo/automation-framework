using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using OpenQA.Selenium;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.PageObjects.ListPOM.HomePagePOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllPoints.PageObjects.ListPOM.ListSummaryPOM;
using System.Threading;

namespace AllPoints.Features.Lists.ListHomePageTst
{
    [TestClass]
    public class ListHomePageTest : FeatureBase
    {
        [TestMethod]
        public void CreateNewList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            listPage.ClickCreateaNewList();

            listPage.SendListName("AutoNameList");

            listPage.ClickCreateListButton();

            Assert.IsTrue(listPage.SuccessListCreated(), "List was not created");
        }

        [TestMethod]
        public void ValidateDuplicateList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            listPage.ClickCreateaNewList();

            listPage.SendListName("AutoNameList");

            listPage.ClickCreateListButton();

            Assert.IsTrue(listPage.DangerListnotCreated(), "List is already created");
        }

        [TestMethod]
        public void RenameList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            listPage.ClickOnFirstList();

            ListSummaryPage summaryListPage = new ListSummaryPage(driver);

            summaryListPage.ClickOnIcon();

            summaryListPage.ClickOnRenameList();

            summaryListPage.SendNewListName("NewAutoNameList");

            summaryListPage.ClickUpdatebutton();
        }

        [TestMethod]
        public void ValidateDuplicatesNotAllowed()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            listPage.ClickCreateaNewList();

            listPage.SendListName("AutoNameList");

            listPage.ClickCreateListButton();

            Assert.IsTrue(listPage.DangerListnotCreated(),"List Was Created. No Duplicate available");
        }

        [TestMethod]
        public void ValidateSpecificationofListCard()
        {
            //This Method is validating:
            //1. Cover image
            //2. List Name
            //3. Number of Items
            //4. Creation Date
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            Assert.IsTrue(listPage.CoverImageIsDisplayed(), "Image Section is not Displayed");

            Assert.IsTrue(listPage.ListNameIsDisplayed(), "Title List is not Displayed");

            Assert.IsTrue(listPage.NumberOfItemsIsDisplayed(), "Number of Items and Date are not Displayed");
        }

        [TestMethod]
        public void ValidateUserHasNoList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test3@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            Assert.IsTrue(listPage.ListPageisEmty(),"List Page already have Lists added");
        }

        [TestMethod]
        public void ValidateItemsDislayedInList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            listPage.ClickOnFirstList();

            ListSummaryPage summaryListPage = new ListSummaryPage(driver);

            Assert.IsTrue(summaryListPage.TotalOfItemsDisplayed(),"Total of Items it not displayed");

            Assert.IsTrue(summaryListPage.ItemCardIsDisplayed(),"No items are added to the List");

            Assert.IsTrue(summaryListPage.SKUisDiplayed(),"SKU is displayed in Item Card");
        }

        [TestMethod]
        public void DeleteList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            ListHomePage listPage = new ListHomePage(driver);

            listPage.ClickDeleteLink();

            listPage.ClickDeleteOnModal();
        }
    }

}
