using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface ICategoriesEndpoint : IGenericEndpoint<CategoryRequest, HttpResponseExtended<CategoryResponse>>
    {
    }
}
