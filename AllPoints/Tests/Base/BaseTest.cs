using AllPoints.Enums;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Configuration;
using System.IO;

namespace AllPoints.Base
{
    public abstract class BaseTest
    {
        protected string Url { get; set; }
        protected IWebDriver Driver;
        protected IAutomationDataFactory DataFactory;

        //define the web driver instance [chrome, iexplorer, firefox]
        protected virtual IWebDriver GetDriverInstance(BrowsersEnum browser)
        {
            string dir = Directory.GetCurrentDirectory();

            switch (browser)
            {
                case BrowsersEnum.Chrome:
                    if (IsBrowserHeadless())
                    {
                      ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--headless");
                        options.AddArgument("-no-sandbox");
                        //under test flags
                        options.AddArgument("--start-maximized");
                        options.AddArgument("--window-size=1920x1080");
                        //****
                        return new ChromeDriver(dir, options);
                    }
                    return new ChromeDriver(dir);

                case BrowsersEnum.IExplorer:
                    //headless mode not supported yet..
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.IgnoreZoomLevel = true;
                    ieOptions.ElementScrollBehavior = InternetExplorerElementScrollBehavior.Bottom;
                    return new InternetExplorerDriver(ieOptions);
                //TODO
                //add firefox driver
                //case Firefox:

                default:
                    throw new ArgumentException("Browser is not supported -> " + browser);
            }
        }

        //determine if headless is set
        protected bool IsBrowserHeadless()
        {
            //headless is true by default
            string headlessString = ConfigurationManager.AppSettings.Get("IsBrowserHeadless") ?? bool.TrueString;

            bool headless = (headlessString.ToLower() == bool.TrueString.ToLower());

            return headless;
        }
    }
}
