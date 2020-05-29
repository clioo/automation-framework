using AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Enums;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace AllPoints.Features.MyAccount.Orders
{
    [TestClass]
    public class ViewOrders : FeatureBase
    {
        #region View

        [TestMethod]
        public void OrderHistoryWithNoOrders_C1251()
        {
            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            Assert.IsNotNull(indexPage, "Login failed");

            var ordersPage = indexPage.Header.ClickOnOrders();

            //validate the message when no orders
            Assert.IsTrue(ordersPage.TextWhenNoOrdersExist(), "Text does not exist");

            Assert.IsTrue(ordersPage.TextWhenNoOrdersIsCorrect("We could not find any order activity in the last 30 days."));
        }

        //[TestMethod]
        public void OrderHistoryWithLessThan15_C1252()
        {
            //Validations:
            //title is 'Orders'
            //Below of title is a subtitle text 'Last 30 days of order activity'
            //theres only 1 pagination bar
            //table orders list is (Date, Order number, Web order number, Status, Total)
            //in another step validate the default Orders view (30 days)

            Assert.IsTrue(false);
        }

        //[TestMethod]
        public void OrderHistoryWithMoreThan15_C1253_()
        {
            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test2@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.IsTrue(ordersPage.OrdersTitleExist(), "Orders title does not exist");
            Assert.IsTrue(ordersPage.OrdersTitleTextIsCorrect("Orders"), "Orders title is incorrect");

            //Validate the default view (31 days)
            Assert.IsTrue(ordersPage.ValidateDefaultViewIsInRange(31), "Default view is not 31 days");

            ordersPage.SetFromDate("10/03/2017");
            ordersPage.SetToDate("10/01/2018");

            ordersPage.ClickOnSearchButton();

            //pagination bar the table has 15 rows per pagination
            Assert.AreEqual(ordersPage.GetTableRowsCount(), 15, "Table does not has 15 rows per pagination");

            //the table has the following headers in the same order
            Assert.IsTrue(ordersPage.TableHasHeaders("Date,Order #,PO #,Web reference #,Status,Total"), "The table has incorrect headers");
        }

        //[TestMethod]
        public void ValidatePagination_C1254()
        {
            //validations:

            Assert.IsTrue(false);
        }

        #endregion View

        #region Order Table

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1256.csv", "C1256#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1256.csv")]
        public void OrdersHasAnOrderNumber_C1256()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                columnName = testContext.DataRow["columnName"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetFromDate(testData.fromDate);
            ordersPage.SetFromDate(testData.toDate);

            //Validations:
            //all orders has an order number
            Assert.IsTrue(ordersPage.AnyColumnHasEmptyValues(testData.columnName), "Theres an empty value");

            //logout
            ordersPage.Header.ClickOnSignOut();
        }

        //[TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1255.csv", "C1255#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1255.csv")]
        public void ValidateStatusColors_C1255()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                status = testContext.DataRow["status"].ToString(),
                cssColor = testContext.DataRow["cssColor"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            //ordersPage.SetFromDate(testData.fromDate);
            //ordersPage.SetToDate(testData.toDate);

            ordersPage.SetFromDateOneYearLessThanNow();

            ordersPage.SetStatus(testData.status);

            ordersPage.ClickOnSearchButton();

            //TODO:
            Assert.IsTrue(ordersPage.StatusColorIsCorrect(testData.status, testData.cssColor), $"{testData.status} color does not correspond");

            testContext.WriteLine($"Status color: {testData.status} validation passed");
        }

        #endregion Order Table

        #region Search

        //[TestMethod]
        public void SearchByWebReference()
        {
            //var testDataN = new
            //{
            //    email = testContext.DataRow["email"].ToString(),
            //    password = testContext.DataRow["password"].ToString(),
            //    fromDate = testContext.DataRow["toDate"].ToString(),
            //    toDate = testContext.DataRow["fromDate"].ToString(),
            //    searchBy = testContext.DataRow["searchBy"].ToString(),
            //    searchValue = testContext.DataRow["searchValue"].ToString()
            //};

            var testData = new
            {
                email = "STKtest4@etundra.com",
                password = "1234",
                fromDate = DateTime.Now.AddYears(-1).ToString("MM/dd/yyyy"),
                toDate = DateTime.Now.ToString("MM/dd/yyyy"),
                searchBy = "Web",
                searchValue = "12345"
            };

            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            Assert.IsNotNull(indexPage, "Login failed");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.FilterSetSearchOption(testData.searchBy, testData.searchValue);
            ordersPage.FilterSetDates(testData.fromDate, testData.toDate);
            ordersPage.FilterSubmit();
            bool filterTimeout = ordersPage.IsAppBusy();
            Assert.IsFalse(filterTimeout);
            var filterDisplayResult = ordersPage.OrderResultTableOrMessage();
            Assert.IsTrue(filterDisplayResult == FilterSearchResultEnum.Message || filterDisplayResult == FilterSearchResultEnum.Table);
        }

        [TestMethod]
        public void SearchByWebReferenceNoOrdersMessage()
        {
            //var testDataN = new
            //{
            //    email = testContext.DataRow["email"].ToString(),
            //    password = testContext.DataRow["password"].ToString(),
            //    fromDate = testContext.DataRow["toDate"].ToString(),
            //    toDate = testContext.DataRow["fromDate"].ToString(),
            //    searchBy = testContext.DataRow["searchBy"].ToString(),
            //    searchValue = testContext.DataRow["searchValue"].ToString()
            //};

            var testData = new
            {
                email = "STKtest4@etundra.com",
                password = "1234",
                fromDate = DateTime.Now.AddYears(-1).ToString("MM/dd/yyyy"),
                toDate = DateTime.Now.ToString("MM/dd/yyyy"),
                searchBy = "Web",
                searchValue = "12345"
            };

            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            Assert.IsNotNull(indexPage, "Login failed");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.FilterSetSearchOption(testData.searchBy, testData.searchValue);
            ordersPage.FilterSetDates(testData.fromDate, testData.toDate);
            ordersPage.FilterSubmit();
            bool filterTimeout = ordersPage.IsAppBusy();
            Assert.IsFalse(filterTimeout);
            var filterDisplayResult = ordersPage.OrderResultTableOrMessage();
            Assert.IsTrue(filterDisplayResult == FilterSearchResultEnum.Message);
            Assert.AreEqual(ordersPage.OrderResultMessageGet(), "We could not find any matching order activity based on the search request.");
        }

        //[TestMethod]
        public void SearchByWebReferenceHaveOrders()
        {
            //var testDataN = new
            //{
            //    email = testContext.DataRow["email"].ToString(),
            //    password = testContext.DataRow["password"].ToString(),
            //    fromDate = testContext.DataRow["toDate"].ToString(),
            //    toDate = testContext.DataRow["fromDate"].ToString(),
            //    searchBy = testContext.DataRow["searchBy"].ToString(),
            //    searchValue = testContext.DataRow["searchValue"].ToString()
            //};

            var testData = new
            {
                email = "STKtest4@etundra.com",
                password = "1234",
                fromDate = DateTime.Now.AddYears(-1).ToString("MM/dd/yyyy"),
                toDate = DateTime.Now.ToString("MM/dd/yyyy"),
                searchBy = "Web",
                searchValue = "272727"
            };

            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            Assert.IsNotNull(indexPage, "Login failed");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.FilterSetSearchOption(testData.searchBy, testData.searchValue);
            ordersPage.FilterSetDates(testData.fromDate, testData.toDate);
            ordersPage.FilterSubmit();

            bool filterTimeout = ordersPage.IsAppBusy();
            Assert.IsFalse(filterTimeout);

            FilterSearchResultEnum filterDisplayResult = ordersPage.OrderResultTableOrMessage();
            Assert.IsTrue(filterDisplayResult == FilterSearchResultEnum.Table, "There is no Orders Table");

            bool containOrders = ordersPage.OrderResultTableHaveValues();
            Assert.IsTrue(containOrders, "Orders are not being display");
        }

        [TestMethod]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\C1278.csv", "C1278#csv", DataAccessMethod.Sequential)]
        //[DeploymentItem("TestData\\C1278.csv")]
        public void ValidateElementsForSearch_C1278()
        {
            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest2@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            //validate the to date field
            Assert.IsTrue(ordersPage.ToDateSearchFieldExist(), "to date search does not exist");

            //validate the from date field
            Assert.IsTrue(ordersPage.FromDateSearchFieldExist(), "from date search does not exist");

            //validate that the to date displays a datepicker when is clicked
            Assert.IsTrue(ordersPage.ToDatePickerExist(), "To date picker does not exist in the page");

            Assert.IsTrue(ordersPage.FromDatePickerExist(), "From date picker does not exist in the page");

            //validate the 'to date' field
            Assert.IsTrue(ordersPage.ToDateSearchFieldExist(), "to date search doesn't exist");

            //validate the button is disabled when the search dates are empty
            Assert.IsTrue(ordersPage.SearchButtonIsDisabledCorrectly(), "Search button is not disabled when some fields are empty");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1279.csv", "C1279#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1279.csv")]
        public void SearchOrderByDate_C1279()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                fromDate = testContext.DataRow["toDate"].ToString(),
                toDate = testContext.DataRow["fromDate"].ToString()
            };

            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.IsTrue(ordersPage.FromDateSearchFieldExist(), "from date search field doesn't exist in the page");

            Assert.IsTrue(ordersPage.ToDateSearchFieldExist(), "to date search field doesn't exist in the page");

            //TODO:
            //set a from date
            //validate that the 'from date' value must be less than the 'to date'
            //validate that the 'from date' is minor to 'to date' by 1 year maximum
            ordersPage.SetFromDate(testData.fromDate);

            //TODO:
            //set a 'to date'
            //validate that the 'to date' cannot be greater than today
            //validate that the 'to date' cannot be less than 'from date'
            ordersPage.SetToDate(testData.toDate);

            ordersPage.ClickOnSearchButton();

            Assert.IsTrue(ordersPage.SearchButtonIsDisabledCorrectly(), "search button behavior is incorrect");

            //orders are ordered by newest to oldest
            Assert.IsTrue(ordersPage.TableContentIsOrderedByDate(), "Orders table is not ordered by its date");

            //orders are in the search range
            Assert.IsTrue(ordersPage.ValidateDatesAreInSearchRange(testData.fromDate, testData.toDate), "Orders in table are not in the search range");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1280.csv", "C1280#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1280.csv")]
        public void NoResultsOnSearchByDate_C1280()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString(),
                expectedText = testContext.DataRow["expectedText"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersHome = indexPage.Header.ClickOnOrders();

            ordersHome.SetFromDate(testData.fromDate);

            ordersHome.SetToDate(testData.toDate);

            ordersHome.ClickOnSearchButton();

            Assert.IsTrue(ordersHome.TextWhenNoOrdersExist(), "Message for no results does not exist");

            Assert.IsTrue(ordersHome.TextWhenNoOrdersIsCorrect(testData.expectedText), "Message for no results is incorrect");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1303.csv", "C1303#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1303.csv")]
        public void SearchByStatus_C1303()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                defaultStatus = testContext.DataRow["defaultStatus"].ToString(),
                statuses = testContext.DataRow["statuses"].ToString(),
                status = testContext.DataRow["status"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.IsTrue(ordersPage.StatusDropDownExist(), "Status select does not exist");

            //has a default value
            Assert.AreEqual(testData.defaultStatus, ordersPage.GetStatusDropDownCurrentValue(), "Current status is not as expected");

            //the ddl has the following status: - Processing -Shipped - Canceled
            Assert.IsTrue(ordersPage.StatusDropDownHasStatuses(testData.statuses), "Current statuses are not as expected");

            //set from date
            ordersPage.SetFromDate(testData.fromDate);

            //set to date
            ordersPage.SetToDate(testData.toDate);

            ordersPage.SetStatus(testData.status);

            ordersPage.ClickOnSearchButton();

            //ALL the results after search has the selected status
            Assert.IsTrue(ordersPage.StatusesAfterSearchAreOk(testData.status), "Statuses results are not as expected");

            ordersPage.Header.ClickOnSignOut();
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1308.csv", "C1308#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1308.csv")]
        public void SearchByStatusWithNoRecords_C1308()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                statuses = testContext.DataRow["statuses"].ToString(),
                defaultStatus = testContext.DataRow["defaultStatus"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString(),
                status = testContext.DataRow["status"].ToString(),
                expectedText = testContext.DataRow["expectedText"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.IsTrue(ordersPage.StatusDropDownExist(), "dropdown does not exist");

            Assert.IsTrue(ordersPage.StatusDropDownHasStatuses(testData.statuses), "statuses does not match with the given test data");

            Assert.AreEqual(ordersPage.GetStatusDropDownCurrentValue(), testData.defaultStatus, "default status is not as expected");

            ordersPage.SetFromDate(testData.fromDate);
            ordersPage.SetToDate(testData.toDate);

            ordersPage.SetStatus(testData.status);

            ordersPage.ClickOnSearchButton();

            Assert.IsTrue(ordersPage.TextWhenNoOrdersExist(), "No text found");
            Assert.IsTrue(ordersPage.TextWhenNoOrdersIsCorrect(testData.expectedText), "Not as expected");

            Thread.Sleep(2000);

            ordersPage.Header.ClickOnSignOut();
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1297-C1315-C1322.csv.csv", "C1297-C1315-C1322#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1297-C1315-C1322.csv")]
        public void ValidateElementsToSearchBy()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                defaultRangeText = testContext.DataRow["defaultRangeText"].ToString(),
                searchByOption = testContext.DataRow["searchByOption"].ToString(),
                scenario = testContext.DataRow["testCase"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.IsTrue(ordersPage.DefaultViewTextIsCorrect(testData.defaultRangeText), "Default view text is not correct");

            Assert.IsTrue(ordersPage.SearchTextIsBold(), "Search text is not as expected");

            Assert.IsTrue(ordersPage.SearchByDropdownExist(), "Search by dropdown does not exist");

            Assert.IsTrue(ordersPage.SearchByHasOption(testData.searchByOption), "Option is not found");

            Assert.IsTrue(ordersPage.FromDateSearchFieldExist(), "from date field does not exist");
            Assert.IsTrue(ordersPage.ToDateSearchFieldExist(), "to date field does not exist");

            Assert.IsTrue(ordersPage.StatusDropDownExist(), "Statuses dropdown does not exist");

            Assert.IsTrue(ordersPage.SearchButtonIsDisabledCorrectly(), "Search button behavior not as expected");

            testContext.WriteLine(testData.scenario);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1298-C1316-C1323.csv", "C1298-C1316-C1323#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1298-C1316-C1323.csv")]
        public void SearchBy()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                subtitle = testContext.DataRow["subtitle"].ToString(),
                searchBy = testContext.DataRow["searchBy"].ToString(),
                searchValue = testContext.DataRow["searchValue"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.AreEqual(ordersPage.GetSearchSubtitleText(), testData.subtitle, "Subtitle text is not correct");

            Assert.IsTrue(ordersPage.SearchTextIsBold(), "Search text is not BOLD as expected");

            Assert.IsTrue(ordersPage.SearchByDropdownExist(), "Search dropdown does not exist");

            ordersPage.SetSearchBySelect(testData.searchBy);

            ordersPage.SetInputForSearchBy(testData.searchValue);

            ordersPage.SetFromDate(testData.fromDate);

            ordersPage.SetToDate(testData.toDate);

            ordersPage.ClickOnSearchButton();

            //validate the results
            Assert.IsTrue(ordersPage.ValidateSearchBy(
                testData.searchBy, testData.searchValue), $"{testData.searchValue} not found in {testData.searchBy}");

            ordersPage.Header.ClickOnSignOut();

            //Log the results
            testContext.WriteLine($"{testData.searchBy} with: {testData.searchValue} passed");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1320-C1321-C1324.csv", "C1320-C1321-C1324#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1320-C1321-C1324.csv")]
        public void SearchByPartialText()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString(),
                searchBy = testContext.DataRow["searchBy"].ToString(),
                searchValue = testContext.DataRow["searchValue"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.IsTrue(ordersPage.DefaultEducateTextExist(), "Default range text does not exist");

            ordersPage.SetFromDate(testData.fromDate);

            ordersPage.SetToDate(testData.toDate);

            ordersPage.SetSearchBySelect(testData.searchBy);

            ordersPage.SetInputForSearchBy(testData.searchValue);

            ordersPage.ClickOnSearchButton();

            ordersPage.ValidateSearchBy(testData.searchBy, testData.searchValue);

            ordersPage.Header.ClickOnSignOut();

            testContext.WriteLine($"Search by {testData.searchBy}: passed");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1317-C1317-C1325.csv", "C1317-C1317-C1325#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1317-C1317-C1325.csv")]
        public void SearchByOutOfRange()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                searchText = testContext.DataRow["searchText"].ToString(),
                searchBy = testContext.DataRow["searchBy"].ToString(),
                searchValue = testContext.DataRow["searchValue"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString(),
                expectedMessage = testContext.DataRow["expectedMessage"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.AreEqual(ordersPage.GetSearchSubtitleText(), testData.searchText, "Subtitle text is not correct");

            Assert.IsTrue(ordersPage.SearchTextIsBold(), "Search text is not BOLD as expected");

            Assert.IsTrue(ordersPage.SearchByDropdownExist(), "Search dropdown does not exist");

            ordersPage.SetSearchBySelect(testData.searchBy);

            ordersPage.SetInputForSearchBy(testData.searchValue);

            ordersPage.SetFromDate(testData.toDate);

            ordersPage.SetToDate(testData.fromDate);

            ordersPage.ClickOnSearchButton();

            //validate the results
            Assert.IsTrue(ordersPage.TextWhenNoOrdersIsCorrect(testData.expectedMessage), "Expected message nos as expected");

            ordersPage.Header.ClickOnSignOut();

            testContext.WriteLine($"{testData.searchBy} with: {testData.searchValue}");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\C1309-C1318-C1326.csv", "C1309-C1318-C1326#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\C1309-C1318-C1326.csv")]
        public void SearchANonExistentByFilter()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                fromDate = testContext.DataRow["fromDate"].ToString(),
                toDate = testContext.DataRow["toDate"].ToString(),
                searchBy = testContext.DataRow["searchBy"].ToString(),
                searchByText = testContext.DataRow["searchByText"].ToString(),
                expectedText = testContext.DataRow["resultAfterSearch"].ToString()
            };

            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();           

            indexPage = loginPage.Login(testData.email, testData.password);

            var ordersPage = indexPage.Header.ClickOnOrders();

            Assert.IsTrue(ordersPage.DefaultEducateTextExist(), "Default range text does not exist");

            ordersPage.SetSearchBySelect(testData.searchBy);

            ordersPage.SetInputForSearchBy(testData.searchByText);

            ordersPage.SetFromDate(testData.fromDate);

            ordersPage.SetToDate(testData.toDate);

            ordersPage.ClickOnSearchButton();

            Assert.IsTrue(ordersPage.TextWhenNoOrdersIsCorrect(testData.expectedText));

            testContext.WriteLine($"{testData.searchBy} with {testData.searchByText}: passed");
        }

        #endregion Search
    }
}