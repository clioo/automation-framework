using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IPublishMerchandiseEndpoint
    {
        Task<HttpResponse<PublishMerchandiseResponse>> Publish(PublishMerchandiseRequest request);
    }
}
