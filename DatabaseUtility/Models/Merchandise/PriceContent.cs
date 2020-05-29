using System;
using System.Collections.Generic;

namespace DatabaseUtility.Models.Merchandise
{
    public class PriceContent : ContentBase
    {
        public string Actual { get; set; }
        public string AddToCart { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsDiscountable { get; set; }
        public bool IsEnabled { get; set; }
        public string List { get; set; }
        public Guid PriceListIdentifier { get; set; }
        public Guid ProductListIdentifier { get; set; }
        public string Sale { get; set; }
        public string SignIn { get; set; }
        public List<VolumePrice> VolumePrices { get; set; }
        public string WebOnly { get; set; }

        public PriceContent()
        {
            Identifier = Guid.NewGuid();
        }
    }

    public class VolumePrice
    {
        public string Amount { get; set; }
        public int Quantity { get; set; }
    }
}