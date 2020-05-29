using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations.Processors;
using HttpUtility.Services.AutomationDataFactory.Models.Merchandise;
using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class ProductsFactory : IProductsFactory
    {
        readonly MerchandiseProcessor _merchandiseProcessor;
        readonly string _tenantUrl;

        public ProductsFactory(MerchandiseProcessor merchandiseProcessor, string tenantUrl)
        {
            _merchandiseProcessor = merchandiseProcessor;
            _tenantUrl = tenantUrl;
        }

        public void UpdateProp65Message(string productId, string message)
        {
            Validator();
            var prop65Message = message == null ? null: new TestProp65Message() { MessageBody = message};            

            _merchandiseProcessor.AddProp65Message(productId, prop65Message).Wait();
        }

        public void UpdateOfferingDescription(string productId, string message)
        {
            Validator();
            string offeringId = $"{productId.Replace("prod", "off")}";
            _merchandiseProcessor.AddOfferingDescription(offeringId, message).Wait();
        }

        public void UpdateProductSpecifications(string productId, List<TestSpecification> specifications)
        {
            Validator();
            _merchandiseProcessor.AddProductSpecifications(productId, specifications).Wait();
        }

        public void UpdateShippingAttributes(string productId, TestProductShipping shippingAttributes)
        {
            Validator();
            _merchandiseProcessor.AddShippingAttributes(productId, shippingAttributes).Wait();
        }

        public void UpdateOemRelationShips(string productId, List<TestOemRelationship> oemRelationships)
        {
            Validator();
            _merchandiseProcessor.AddOemRelationships(productId, oemRelationships).Wait();
        }

        //TODO:
        //replaces
        //modelReferences

        public void Complete()
        {
            _merchandiseProcessor.ExecutePublish(_tenantUrl).Wait();
        }

        private void Validator()
        {
            //generic validation
            if (string.IsNullOrEmpty(_tenantUrl) || string.IsNullOrWhiteSpace(_tenantUrl))
            {
                throw new System.ArgumentException($"{nameof(_tenantUrl)} must have a value");
            }
        }
    }
}
