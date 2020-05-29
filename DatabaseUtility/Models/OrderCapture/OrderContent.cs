using System;

namespace DatabaseUtility.Models.OrderCapture
{
    public class OrderContent : ContentBase
    {
        public string AccountExternalIdentifier { get; set; }
        public string CardToken { get; set; }
        public string CardTransaction { get; set; }
        public string Cart { get; set; }
        public string Contact { get; set; }
        public string ContactCompany { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsPayingWithTerms { get; set; }
        public Owner Owner { get; set; }
        public string PONumber { get; set; }
        public string ShippingAttention { get; set; }
        public Guid ShopperIdentifier { get; set; }
        public Guid StoreIdentifier { get; set; }
        public string WebOrderNumber { get; set; }

        public OrderContent()
        {
            Identifier = Guid.NewGuid();
        }
    }
}