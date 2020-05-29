using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.Services.AutomationDataFactory.Models.Merchandise;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Implementations.Processors
{
    public class MerchandiseProcessor
    {
        readonly IIntegrationsWebAppClient _client;

        public MerchandiseProcessor(IIntegrationsWebAppClient client)
        {
            _client = client;
        }

        public async Task AddProp65Message(string productId, TestProp65Message prop65Message)
        {
            var request = new { prop65Message };

            var response = await _client.Products.PatchEntity(productId, request);

            //validations
            GenericResponseValidation(response.StatusCode, nameof(prop65Message));
        }

        public async Task AddShippingAttributes(string productId, TestProductShipping shippingAttributes)
        {
            var request = new { Shipping = shippingAttributes};
            var response = await _client.Products.PatchEntity(productId, request);

            GenericResponseValidation(response.StatusCode, nameof(shippingAttributes));
        }

        public async Task AddOfferingDescription(string offeringId, string description)
        {
            var request = new { Description = description };
            var response = await _client.Offerings.PatchEntity(offeringId, request);

            GenericResponseValidation(response.StatusCode, nameof(description));
        }

        public async Task AddProductSpecifications(string productId, List<TestSpecification> specifications)
        {
            var request = new { Specs = specifications };
            var response = await _client.Products.PatchEntity(productId, request);

            GenericResponseValidation(response.StatusCode, nameof(specifications));
        }

        public async Task AddOemRelationships(string productId, List<TestOemRelationship> oemRelationships)
        {
            var request = new
            {
                OemRelationships = oemRelationships
            };

            var response = await _client.Products.PatchEntity(productId, request);
            GenericResponseValidation(response.StatusCode, nameof(oemRelationships));
        }

        public async Task ExecutePublish(string tenantUrl)
        {
            var request = new PublishMerchandiseRequest
            {
                Hostname = tenantUrl,
                ClearUnusedCache = false,
                PromoteOnSuccess = true,
                PurgeOnSuccess = false
            };
            var publishResponse = await _client.PublishMerchandise.Publish(request);

            //validate publish has done properly
        }

        private void GenericResponseValidation(int statusCode, string entityName)
        {
            string message = null;
            switch (statusCode)
            {
                case 400:
                    message = $"The request for {entityName} contains invalid values";
                    break;
                case 404:
                    message = entityName + " does not exist or cannot be found";
                    break;
                case 500:
                    message = $"Server error, {entityName} cannot be added";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(message) || !string.IsNullOrWhiteSpace(message))
            {
                throw new Exception(message);
            }
        }
    }
}
