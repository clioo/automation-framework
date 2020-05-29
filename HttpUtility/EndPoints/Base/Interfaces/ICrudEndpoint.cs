using System.Threading.Tasks;

namespace HttpUtility.EndPoints.Base.Interfaces
{
    public interface ICrudEndpoint<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> Create(TRequest postRequest);
        Task<TResponse> GetSingle(string externalIdentifier);
        Task<TResponse> Remove(string externalIdentifier);
        Task<TResponse> Update(string externalIdentifier, TRequest putRequest);
    }
}
