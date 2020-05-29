using System;

namespace HttpUtility.EndPoints.EcommerceWebApp.Models
{
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ExternalIdentifier { get; set; }
        public Guid Identifier { get; set; }
        public bool IsInternational { get; set; }
        public string Name { get; set; }
        public Guid PlatformIdentifier { get; set; }
        public string Postal { get; set; }
        public string StateProvinceRegion { get; set; }
    }
}