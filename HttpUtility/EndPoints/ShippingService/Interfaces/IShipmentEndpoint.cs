using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Shipment;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService.Interfaces
{
    public interface IShipmentEndpoint
    {
        Task<HttpEssResponse<ShipmentResponse>> GetSingle(string accountMasterExtId, ShipmentRequest request);
    }
}
