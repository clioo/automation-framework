using CommonHelper;
using FMPOnlinePOM.PageObjects.Base.Components;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPOnlinePOM.PageObjects.Cart
{
    public class CartProductItem : ProductItem
    {
        #region constructor        
        public CartProductItem(IWebDriver driver, DomElement container) : base(driver, container)
        {
        }
        #endregion constructor
    }
}