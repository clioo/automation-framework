namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.PriceLists
{
    public class PriceListRequest : PriceList
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Identifier { get; set; }
    }
}
