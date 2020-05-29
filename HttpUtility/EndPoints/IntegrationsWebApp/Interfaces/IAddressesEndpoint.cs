using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Addresses;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IAddressesEndpoint
    {
        Task<AddressResponse> Create(string accountMasterExtId, AddressRequest postRequest);
        Task<AddressResponse> Remove(string externalIdentifier);
        Task<AddressResponse> Update(string externalIdentifier, AddressRequest putRequest);
    }
}
