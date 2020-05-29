using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class CategoryRequest : Category
    {
        public string ParentId { get; set; }
        public string CatalogId { get; set; }
        public string Identifier { get; set; }
    }
}