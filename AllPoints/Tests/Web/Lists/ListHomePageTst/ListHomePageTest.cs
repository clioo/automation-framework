using AllPoints.PageObjects.ListPOM.HomePagePOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllPoints.PageObjects.ListPOM.ListSummaryPOM;
using AllPoints.AllPoints;

namespace AllPoints.Features.Lists.ListHomePageTst
{
    [TestClass]
    public class ListHomePageTest : AllPointsBaseTest
    {
        [TestMethod]
        public void CreateNewList()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            listPage.ClickCreateaNewList();

            listPage.SendListName("AutoNameList");

            listPage.ClickCreateListButton();

            Assert.IsTrue(listPage.SuccessListCreated(), "List was not created");
        }

        [TestMethod]
        public void ValidateDuplicateList()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            listPage.ClickCreateaNewList();

            listPage.SendListName("AutoNameList");

            listPage.ClickCreateListButton();

            Assert.IsTrue(listPage.DangerListnotCreated(), "List is already created");
        }

        [TestMethod]
        public void RenameList()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            listPage.ClickOnFirstList();

            APListSummaryPage summaryListPage = new APListSummaryPage(Driver);

            summaryListPage.ClickOnIcon();

            summaryListPage.ClickOnRenameList();

            summaryListPage.SendNewListName("NewAutoNameList");

            summaryListPage.ClickUpdatebutton();
        }

        [TestMethod]
        public void ValidateDuplicatesNotAllowed()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            listPage.ClickCreateaNewList();

            listPage.SendListName("AutoNameList");

            listPage.ClickCreateListButton();

            Assert.IsTrue(listPage.DangerListnotCreated(), "List Was Created. No Duplicate available");
        }

        [TestMethod]
        public void ValidateSpecificationofListCard()
        {
            //This Method is validating:
            //1. Cover image
            //2. List Name
            //3. Number of Items
            //4. Creation Date
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            Assert.IsTrue(listPage.CoverImageIsDisplayed(), "Image Section is not Displayed");

            Assert.IsTrue(listPage.ListNameIsDisplayed(), "Title List is not Displayed");

            Assert.IsTrue(listPage.NumberOfItemsIsDisplayed(), "Number of Items and Date are not Displayed");
        }

        [TestMethod]
        public void ValidateUserHasNoList()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test3@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            Assert.IsTrue(listPage.ListPageisEmty(), "List Page already have Lists added");
        }

        [TestMethod]
        public void ValidateItemsDislayedInList()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            listPage.ClickOnFirstList();

            APListSummaryPage summaryListPage = new APListSummaryPage(Driver);

            Assert.IsTrue(summaryListPage.TotalOfItemsDisplayed(), "Total of Items it not displayed");

            Assert.IsTrue(summaryListPage.ItemCardIsDisplayed(), "No items are added to the List");

            Assert.IsTrue(summaryListPage.SKUisDiplayed(), "SKU is displayed in Item Card");
        }

        [TestMethod]
        public void DeleteList()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            indexPage.Header.ClickOnLists();

            APListHomePage listPage = new APListHomePage(Driver);

            listPage.ClickDeleteLink();

            listPage.ClickDeleteOnModal();
        }
    }

}
