namespace DatabaseUtility.API.Models.Membership
{
    public class AddressCreate
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsInternational { get; set; }
        public string Postal { get; set; }
        public string StateProvinceRegion { get; set; }
        public bool setAsDefault { get; set; }
        public bool IsAccountMasterLevel { get; set; }
    }
}