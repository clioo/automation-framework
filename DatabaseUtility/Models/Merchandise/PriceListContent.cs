using System;

namespace DatabaseUtility.Models.Merchandise
{
    public class PriceListContent : ContentBase
    {
        public string ExternalIdentifier { get; set; }
        public string Name { get; set; }

        public PriceListContent()
        {
            Identifier = Guid.NewGuid();
        }
    }
}