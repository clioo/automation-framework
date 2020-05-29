using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class OfferingResponse : Offering
    {
        public Guid CatalogIdentifier { get; set; }
        public List<Guid> CategoryIdentifiers { get; set; }
        public Guid HomeCategory { get; set; }
        public Guid Identifier { get; set; }
        public Guid PlatformIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedUtc { get; set; }
        public List<OfferingVariantEntry> VariantEntries { get; set; }
    }
}
