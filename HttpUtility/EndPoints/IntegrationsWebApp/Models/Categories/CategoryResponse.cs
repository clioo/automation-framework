using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class CategoryResponse : Category
    {
        public Guid CatalogIdentifier { get; set; }
        public string ParentIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }
        public Guid Identifier { get; set; }
        public Guid PlatformIdentifier { get; set; }
        public string CreatedUtc { get; set; }        
        public string UpdatedUtc { get; set; }
    }
}