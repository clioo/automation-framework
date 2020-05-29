using HttpUtiityTests.EnvConstants;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using HttpUtility.Services.AutomationDataFactory.Models.Merchandise;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HttpUtiityTests.Services
{
    [TestClass]
    public class AllPointsProductsDataFactoryTest
    {
        readonly IAutomationDataFactory DataFactory;

        public AllPointsProductsDataFactoryTest()
        {
            var config = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = ServiceConstants.IntegrationsAPIUrl,
                ShippingServiceApiUrl = ServiceConstants.ShippingServiceApiUrl,
                TenantExternalIdentifier = ServiceConstants.AllPointsPlatformExtId,
                TenantInternalIdentifier = ServiceConstants.AllPointsPlatformId,
                TenantSiteUrl = ServiceConstants.AllPointsUrl
            };
            DataFactory = new AutomationDataFactory(config);
        }

        [TestMethod]
        public void Update_Product_ExplicitId_Success()
        {
            string productId = "FWNL2207-prod";
            string messageBody = "Typescript is the new javascript";
            DataFactory.Products.UpdateProp65Message(productId, messageBody);
        }

        [TestMethod]
        public void Update_Product_CustomDescription()
        {
            string productId = "FWNL2207-prod";
            DataFactory.Products.UpdateOfferingDescription(productId, "My custom description q:");
        }

        [TestMethod]
        public void Update_Product_Specifications()
        {
            string productId = "FWNL2207-prod";
            var specifications = new List<TestSpecification>
            {
                new TestSpecification
                {
                    Name = "peep",
                    Value = "1234"
                }
            };
            DataFactory.Products.UpdateProductSpecifications(productId, specifications);
            DataFactory.Products.Complete();
        }

        [TestMethod]
        public void Update_Product_Shipping()
        {
            string productId = "MXMX2255-prod";
            var shipping = new TestProductShipping
            {
                IsQuickShip = true,
                IsFreeShip = false,
                IsFreightOnly = false,
                FreightClass = 115,
                WeightActual = 4.5M,
                WeightDimensional = 20,
                Width = 7,
                Height = 1,
                Length = 13
            };

            DataFactory.Products.UpdateOfferingDescription(productId, "This product is dangerous");
            DataFactory.Products.UpdateProp65Message(productId, "The prop65 is enabled for this product");
            DataFactory.Products.UpdateShippingAttributes(productId, shipping);
            DataFactory.Products.Complete();
        }

        [TestMethod]
        public void Update_Product_OEMRelationShips()
        {
            string productId = "OG2220-prod";
            var oemRelationShips = new List<TestOemRelationship> 
            { 
                new TestOemRelationship
                {
                    Type = 1,
                    OemName = "test name",
                    OemSku = "q:"
                }
            };
            DataFactory.Products.UpdateOemRelationShips(productId, oemRelationShips);
            DataFactory.Products.Complete();
        }
    }
}
