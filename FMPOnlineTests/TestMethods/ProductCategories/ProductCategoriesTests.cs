using FMPOnlineTests.Constants;
using FMPOnlineTests.DataFactory.CategoriesFactory;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FMPOnlineTests.TestMethods.ProductCategories
{
    [TestClass]
    public class ProductCategoriesTests : FmpBaseTest
    {
        private CategoryDataFactory TestDataFactory;

        public ProductCategoriesTests()
        {
            TestDataFactory = new CategoryDataFactory();
        }

        //[TestMethod]        
        public void GoToAnyDummyCategory()
        {

        }

        [TestMethod]
        public void LoginFmpCase()
        {
            var testUser = TestDataFactory.CreateLoginAccount();

            //This is only for testing purposes.
            //The index, login POMs needs to be created
            Driver.Navigate().GoToUrl(Url + "/Account/SignIn");
            Driver.FindElement(By.CssSelector("#LoginUsername")).SendKeys(testUser.Email);
            Driver.FindElement(By.CssSelector("#LoginPassword")).SendKeys(testUser.Password);
            Driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(1500);

            //check if the my account menu option exist
            try
            {
                Driver.FindElement(By.CssSelector("li.dropdown.user"));
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Login failed");
            }
        }
    }
}