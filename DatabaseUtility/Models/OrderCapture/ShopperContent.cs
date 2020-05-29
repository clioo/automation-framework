using System;

namespace DatabaseUtility.Models.OrderCapture
{
    public class ShopperContent : ContentBase
    {
        public string SaveForLater { get; set; }
        public Guid UserIdentifier { get; set; }

        public ShopperContent()
        {
            Identifier = Guid.NewGuid();
        }
    }
}