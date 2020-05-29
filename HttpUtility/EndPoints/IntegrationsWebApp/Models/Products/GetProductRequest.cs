using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class GetProductRequest : EntityBase
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
