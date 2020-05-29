using CommonHelper;
using CommonHelper.BaseComponents;
using FMPOnlinePOM.PageObjects.Base.Components;
using FMPOnlinePOM.PageObjects.IndexHome.Enums;
using OpenQA.Selenium;
using System;

namespace FMPOnlinePOM.PageObjects.HomePage
{
    public class IndexHomePage : BasePOM
    {
        #region page locators
        private DomElement MainContainer = new DomElement
        {
            locator = "#main"
        };
        //locators for zones are pending on the development team
        private DomElement TopZoneContainer = new DomElement
        {
            locator = ".home-top-content"
        };
        private DomElement BottomZoneContainer = new DomElement
        {
            locator = ".home-bottom-content"
        };
        #endregion

        public Base.Components.UtilityMenu UtilityMenu;
        public TopHeader TopHeader;

        #region constructor
        public IndexHomePage(IWebDriver driver) : base(driver)
        {
            UtilityMenu = new Base.Components.UtilityMenu(Driver);
            TopHeader = new TopHeader(Driver);
            //TODO:
            //initialize dependencies
            //account menu, account menu left
        }

        public IndexHomePage(IWebDriver driver, string url) : base(driver)
        {
            Driver = driver;
            Driver.Navigate().GoToUrl(url);
            UtilityMenu = new Base.Components.UtilityMenu(Driver);
            TopHeader = new TopHeader(Driver);
            //TODO:
            //initialize dependencies
            //account menu, account menu left
        }
        #endregion

        public string GetCoBrandSectionText(IndexHomePageSectionsEnum section)
        {
            DomElement mainContainer = BodyContainer.GetElementWaitByCSS(MainContainer.locator);

            switch (section)
            {
                case IndexHomePageSectionsEnum.Top:
                    return mainContainer.GetElementWaitByCSS(TopZoneContainer.locator).webElement.Text;

                case IndexHomePageSectionsEnum.Bottom:
                    return mainContainer.GetElementWaitByCSS(BottomZoneContainer.locator).webElement.Text;

                default: throw new ArgumentException($"{section} is not supported yet..");
            }
        }
    }
}
