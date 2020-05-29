using CommonHelper;
using FMPOnlinePOM.PageObjects.Base.Components;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPOnlinePOM.PageObjects.SignInRegister
{
    public class CheckoutProductItem : ProductItem
    {
        public CheckoutProductItem(IWebDriver driver, DomElement container) : base(driver, container)
        {
        }
    }
}
