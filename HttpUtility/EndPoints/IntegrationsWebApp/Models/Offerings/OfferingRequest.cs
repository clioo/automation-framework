using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class OfferingRequest : Offering
    {
        public string Identifier { get; set; }
        public string CatalogId { get; set; }
        public string ProductId { get; set; }
        public List<string> CategoryIds { get; set; }
        public string HomeCategory { get; set; }
    }
}
