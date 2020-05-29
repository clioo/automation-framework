using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllPoints.Features.MyAccount.Orders
{
    [TestClass]
    public class OrderDetails : FeatureBase
    {
        #region View

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\Orders\\OrderDetails\\C1281.csv", "C1281#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\Orders\\OrderDetails\\C1281.csv")]
        public void ValidateOrderDetailsView_C1281()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                defaultStatus = testContext.DataRow["defaultStatus"].ToString(),
                orderNumber = testContext.DataRow["OrderNumber"].ToString(),
                pageTitle = testContext.DataRow["title"].ToString(),
                pageSubtitle = testContext.DataRow["subtitle"].ToString(),
                sectionUno = testContext.DataRow["sectionDetails"].ToString(),
                date = testContext.DataRow["date"].ToString(),
                dateValue = testContext.DataRow["dateValue"].ToString(),
                totalItems = testContext.DataRow["totalItems"].ToString(),
                totalItemsValue = testContext.DataRow["totalItemsValue"].ToString(),
                poNumber = testContext.DataRow["poNumber"].ToString(),
                poNumberValue = testContext.DataRow["poNumberValue"].ToString(),
                status = testContext.DataRow["status"].ToString(),
                statusValue = testContext.DataRow["statusValue"].ToString(),
                itemTotals = testContext.DataRow["itemTotals"].ToString(),
                itemTotalsValue = "$" + string.Format("{0:0.00}", testContext.DataRow["itemTotalsValue"]),
                tax = testContext.DataRow["tax"].ToString(),
                taxValue = "$" + string.Format("{0:0.00}", testContext.DataRow["taxValue"]),
                shipping = testContext.DataRow["shipping"].ToString(),
                shippingValue = "$" + string.Format("{0:0.00}", testContext.DataRow["shippingValue"]),
                total = testContext.DataRow["total"].ToString(),
                totalValue = "$" + string.Format("{0:0.00}", testContext.DataRow["totalValue"]),
            };

            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetStatus(testData.defaultStatus);
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail(testData.orderNumber);

            Assert.IsNotNull(orderDetailPage, "Given order cannot be found");

            Assert.IsTrue(orderDetailPage.TitleExist(), "Order details title does not exist");
            Assert.AreEqual(testData.pageTitle, orderDetailPage.GetTitleText(), "Page title is not correct");

            Assert.IsTrue(orderDetailPage.SubtitleExist(), "Order details page doesn't have any subtitle");
            Assert.AreEqual(testData.pageSubtitle, orderDetailPage.GetSubtitleText(), "Order details subtitle is incorrect");

            Assert.IsTrue(orderDetailPage.DetailSubtitleExist(), "'Detail' text does not exist");
            Assert.AreEqual(testData.sectionUno, orderDetailPage.GetDetailSubtitleText(), "Detail text is not correct");

            Assert.IsTrue(orderDetailPage.DateLabelExist(), "Date label does not exist");
            Assert.AreEqual(testData.date, orderDetailPage.GetDateLabelText(), "Date label text is incorrect");
            Assert.IsTrue(orderDetailPage.CompareDatesString(testData.dateValue), "Date is not matching");

            Assert.IsTrue(orderDetailPage.TotalItemsLabelExist(), "Total items label does not exist");
            Assert.AreEqual(testData.totalItems, orderDetailPage.GetTotalItemsLabelText(), "Total items label text is not as expected");
            Assert.AreEqual(testData.totalItemsValue, orderDetailPage.GetTotalItemsValue(), "Total items value is not as expected");

            Assert.IsTrue(orderDetailPage.PoLabelExist(), "PO label does not exist");
            Assert.AreEqual(testData.poNumber, orderDetailPage.GetPoLabelText(), "PO number label text is incorrect");
            Assert.AreEqual(testData.poNumberValue, orderDetailPage.GetPoValue(), "PO number is not as expected");

            Assert.IsTrue(orderDetailPage.StatusLabelExist(), "Status label does not exist");
            Assert.AreEqual(testData.status, orderDetailPage.GetStatusLabelText(), "Status label text is not as expected");
            Assert.AreEqual(testData.statusValue, orderDetailPage.GetStatusValue(), "Status value is not as expected");

            Assert.IsTrue(orderDetailPage.ItemTotalsLabelExist(), "Item totals label does not exist");
            Assert.AreEqual(testData.itemTotals, orderDetailPage.GetItemTotalsLabelText(), "Item totals label text is not as expected");
            Assert.AreEqual(testData.itemTotalsValue, orderDetailPage.GetItemTotalsValue(), "Item totals value is not as expected");

            Assert.IsTrue(orderDetailPage.TaxLabelExist(), "Tax label does not exist");
            Assert.AreEqual(testData.tax, orderDetailPage.GetTaxLabelText(), "Tax label text is not as expected");
            Assert.AreEqual(testData.taxValue, orderDetailPage.GetTaxValue(), "Tax value is not as expected");

            Assert.IsTrue(orderDetailPage.ShippingLabelExist(), "Shipping label does not exist");
            Assert.AreEqual(testData.shipping, orderDetailPage.GetShippingLabelText(), "Shipping label text is not as expected");
            Assert.AreEqual(testData.shippingValue, orderDetailPage.GetShippingValue(), "Shipping value is not as expected");

            Assert.IsTrue(orderDetailPage.TotalLabelExist(), "Total label does not exist");
            Assert.AreEqual(testData.total, orderDetailPage.GetTotalLabelText(), "Total label text is not as expected");
            Assert.AreEqual(testData.totalValue, orderDetailPage.GetTotalValue(), "Total value is not as expected");
        }

        [TestMethod]
        public void ShippingSectionIsDisplayed_C1283()
        {
            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetStatus("Shipped");
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail("51489444-00");

            Assert.IsNotNull(orderDetailPage, "order number not found");

            Assert.IsTrue(orderDetailPage.TitleExist(), "Order detail title does not exist");
            Assert.AreEqual(orderDetailPage.GetTitleText(), "Orders details");

            Assert.IsTrue(orderDetailPage.SubtitleExist());
            Assert.AreEqual(orderDetailPage.GetSubtitleText(), "Order # 51489444-00");

            Assert.IsTrue(orderDetailPage.DetailSubtitleExist(), "Details subtitle does not exist");
            Assert.AreEqual(orderDetailPage.GetDetailSubtitleText(), "Details");

            Assert.IsTrue(orderDetailPage.ShippingSubtitleExist(), "Shipping subtitle does not exist");
            Assert.AreEqual(orderDetailPage.GetShippingSubtitle(), "Shipping", "Shipping subtitle text is not correct");
        }

        [TestMethod]
        public void DetailsTableIsDisplayed_C1329()
        {
            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetStatus("Shipped");
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail("51489444-00");

            Assert.IsNotNull(orderDetailPage, "order number not found");
            Assert.IsTrue(orderDetailPage.DetailsTableExist(), "Table of products details does not exist");
        }

        [TestMethod]
        public void ValidateWODMessage_C1339()
        {
            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetSearchBySelect("Order #");
            ordersPage.SetInputForSearchBy("51489444-00");
            ordersPage.SetFromDate("01/01/2018");
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail("51489444-00");

            Assert.IsNotNull(orderDetailPage, "Given order cannot be found");

            Assert.IsTrue(orderDetailPage.WODMessageExist(), "WOD message does not exist");
            Assert.AreEqual(@"Congratulations
This invoice received a Whole Order Discount. The Whole Order Discount is applied to the entire order after the contents of the entire order is added up. The discount is reflected in the Order Detail Total but not in the price of each individual line item below.
Contact Customer Service if you have any further questions 1-800-332-2500.", orderDetailPage.GetWODMessage(), "WOD Message is incorrect");
            Assert.IsTrue(orderDetailPage.HasDiscount(), "There is no discount");
        }

        #endregion View

        #region Details

        [TestMethod]
        public void ValidateOrderDeatilsTotals_C1330()
        {
            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetStatus("Shipped");
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail("51489444-00");

            Assert.IsNotNull(orderDetailPage, "Given order cannot be found");

            Assert.AreEqual("Total Items:", orderDetailPage.GetTotalItemsLabelText(), "Total items label text is not as expected");
            Assert.AreEqual("2", orderDetailPage.GetTotalItemsValue(), "Total items value is not as expected");
            Assert.AreEqual(orderDetailPage.CountElementTableDetails(), orderDetailPage.GetTotalItemsValue(), "Total items value are different");

            Assert.AreEqual("Item Totals", orderDetailPage.GetItemTotalsLabelText(), "Item totals label text is not as expected");
            Assert.AreEqual("$51.60", orderDetailPage.GetItemTotalsValue(), "Item totals value is not as expected");
            Assert.AreEqual(orderDetailPage.CalculateTableTotal(), orderDetailPage.GetItemTotalsValue(), "ItemsTotal value are different");

            Assert.AreEqual("Total", orderDetailPage.GetTotalLabelText(), "Total label text is not as expected");
            Assert.AreEqual("$51.60", orderDetailPage.GetTotalValue(), "Total value is not as expected");
            Assert.AreEqual(orderDetailPage.GetFinalTotal(), orderDetailPage.GetTotalValue(), "Final totals are different");
        }

        //[TestMethod]
        public void TestStatusColor_C1296()
        {
            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetFromDate("09/01/2018");
            ordersPage.SetStatus("Shipped");
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail("51489444-00");
            Assert.IsTrue(orderDetailPage.GetStatusColor("#808080"), "shipped status color is not as expected");

            ////Now all orders with "processing" status does not have details
            //driver.Navigate().Back();
            //ordersPage.GoToOrderDetail("51411479-00");
            //Assert.IsTrue(orderDetailPage.GetStatusColor("#93c562"), "processing status color is not as expected");

            ////Now in the data set, no exist order with "canceled" status
            //driver.Navigate().Back();
            //ordersPage.GoToOrderDetail("51400472-00");
            //Assert.IsTrue(orderDetailPage.GetStatusColor("#FF0006"), "canceled status color is not as expected");
        }

        #endregion Details

        #region Shipping

        [TestMethod]
        public void ValidateOrderTrackingNumbersC1284()
        {
            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetStatus("Shipped");
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail("51489444-00");

            Assert.IsNotNull(orderDetailPage, "order number not found");

            Assert.IsTrue(orderDetailPage.TitleExist(), "Order detail title does not exist");
            Assert.AreEqual(orderDetailPage.GetTitleText(), "Orders details");

            var trackingNumber = orderDetailPage.ValidateTrackingNumbers("782972116483,782972356140");

            Assert.IsTrue(string.IsNullOrEmpty(trackingNumber), $"{trackingNumber} is not found");
        }

        //[TestMethod]
        public void OrderDetailsWithNoTrackingNumbers_C1285()
        {
        }

        [TestMethod]
        public void ValidateLinksForTrackingNumbers_C1286()
        {
            var indexPage = new IndexPage(driver);
            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.SetStatus("Shipped");
            ordersPage.ClickOnSearchButton();

            var orderDetailPage = ordersPage.GoToOrderDetail("51489444-00");

            Assert.IsNotNull(orderDetailPage, "order number not found");

            Assert.IsTrue(orderDetailPage.TitleExist(), "Order detail title does not exist");
            Assert.AreEqual(orderDetailPage.GetTitleText(), "Orders details");

            Assert.IsTrue(orderDetailPage.SubtitleExist());
            Assert.AreEqual(orderDetailPage.GetSubtitleText(), "Order # 51489444-00");

            Assert.IsTrue(orderDetailPage.DetailSubtitleExist(), "Details subtitle does not exist");
            Assert.AreEqual(orderDetailPage.GetDetailSubtitleText(), "Details");

            Assert.IsTrue(orderDetailPage.ShippingSubtitleExist(), "Shipping subtitle does not exist");
            Assert.AreEqual(orderDetailPage.GetShippingSubtitle(), "Shipping", "Shipping subtitle text is not correct");

            var trackingNumber = orderDetailPage.ValidateTrackingNumbers("782972116483,782972356140");

            Assert.IsTrue(string.IsNullOrEmpty(trackingNumber), $"{trackingNumber} is not found");

            //var googlePage = orderDetailPage.ClickOnTrackingNumber("");

            //Assert.AreEqual("Google", googlePage, "Redirection failed");
        }
        #endregion Shipping

        //pending
        //[TestMethod]
        public void OrderWithNoOrderNumberCannotBeClicked_C1282()
        {
            var indexPage = new IndexPage(driver);

            indexPage.Init(url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest1@etundra.com", "1234");

            var ordersPage = indexPage.Header.ClickOnOrders();

            ordersPage.ClickOnOrderDetailWithNoNumber();

            Assert.AreEqual(driver.Title, "Orders", "Clicking an order with no order number should do nothing...");
        }
    }
}