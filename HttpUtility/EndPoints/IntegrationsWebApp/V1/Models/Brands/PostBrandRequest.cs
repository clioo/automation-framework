using System;

namespace HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Brands
{
    public class PostBrandRequest : Brand
    {
        public Guid Identifier { get; set; }
        public Guid PlatformIdentifier { get; set; }
    }
}
