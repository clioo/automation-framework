using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Brands
{
    public class Brand
    {
        public int Favorability { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string UrlSegment { get; set; }
        public HtmlPage HtmlPage { get; set; }
        public string ExternalIdentifier { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }
}
