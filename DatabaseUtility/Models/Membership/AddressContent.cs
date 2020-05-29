using System;

namespace DatabaseUtility.Models
{
    public class AddressContent : ContentBase
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsInternational { get; set; }
        public DateTime LastUsedDateUtc { get; set; }
        public Owner Owner { get; set; }
        public string Postal { get; set; }
        public string StateProvinceRegion { get; set; }

        public AddressContent()
        {
            Identifier = Guid.NewGuid();
        }
    }
}