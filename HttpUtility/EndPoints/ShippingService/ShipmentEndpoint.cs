using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Shipment;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShipmentEndpoint : EndPoint<HttpEssResponse<ShipmentResponse>>, IShipmentEndpoint
    {
        public ShipmentEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpEssResponse<ShipmentResponse>> GetSingle(string accountMasterExtId, ShipmentRequest request)
        {
            MethodUrl = $"/{accountMasterExtId}/shipments";

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Post(stringPayload);

            return response;
        }
    }
}
