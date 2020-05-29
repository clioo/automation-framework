using HttpUtility.Services.AutomationDataFactory.Models.Merchandise;
using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface IProductsFactory
    {
        void UpdateOfferingDescription(string productId, string message);
        void UpdateProp65Message(string productId, string message);
        void UpdateProductSpecifications(string productId, List<TestSpecification> specifications);
        void UpdateShippingAttributes(string productId, TestProductShipping shippingAttributes);
        void UpdateOemRelationShips(string productId, List<TestOemRelationship> oemRelationShips);
        void Complete();
        //TODO
        //check how to restore the previous product configuration
        //void RestoreProduct(string externalId);
    }
}
