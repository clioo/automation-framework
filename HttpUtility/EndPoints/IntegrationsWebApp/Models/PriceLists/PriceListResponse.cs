namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.PriceLists
{
    public class PriceListResponse : PriceList
    {
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
        public int Version { get; set; }
        public int VersionStatus { get; set; }
        public string ExternalIdentifier { get; set; }
        public string Identifier { get; set; }
        public string PlatformIdentifier { get; set; }
    }
}
