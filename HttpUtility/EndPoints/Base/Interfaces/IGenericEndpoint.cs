using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IGenericEndpoint<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> Create(TRequest postRequest);
        Task<TResponse> Remove(string externalIdentifier);
        Task<TResponse> GetSingle(string externalIdentifier);
        Task<TResponse> Update(string externalIdentifier, TRequest putRequest);
        Task<TResponse> PatchEntity(string externalIdentifier, object patchRequest);
    }
}
