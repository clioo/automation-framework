using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.MultiClients.DataSeed
{
    [TestClass]
    public class ProductPublish
    {
        readonly string PlatformId = ServiceConstants.FMPPlatformId;
        readonly string PlatformHost = ServiceConstants.FMPUrl;
        readonly string ProductExternalId = "pubExternalProductId01";
        readonly string OfferingExternalId = "pubExternalOffering01";

        //TODO
        //E2E
        [TestMethod]
        public async Task CreateOfferingProductAndPublish()
        {
            string platformIdentifier = PlatformId;
            string platformName = ServiceConstants.FMPPlatformName;
            string platformHostName = PlatformHost;
            string productSku = "SKU300";
            string productExternalId = ProductExternalId;
            string offeringFullName = "Put the cloth";
            string offeringCategory = "externalDev";
            IntegrationsWebAppClient integrationsClientV2 = new IntegrationsWebAppClient(ServiceConstants.IntegrationsAPIUrl, platformName);
            IntegrationsWebAppClient integrationsClientV1 = new IntegrationsWebAppClient(ServiceConstants.IntegrationsAPIUrl, platformIdentifier);

            PostProductRequest createProductRequest = new PostProductRequest
            {
                Identifier = productExternalId,
                AlternateSkus = null,
                BrandSku = "4WW3274",
                Cost = 11,
                DisplayProductCode = productSku,
                IsOcm = false,
                IsPurchaseable = true,
                Name = "product test name",
                OemRelationships = new List<ProductOem>
                {
                    new ProductOem
                    {
                        OemName = "name",
                        Type = 2,
                        OemSku = "pika"
                    }
                },
                ProductCode = productSku,
                ProductLeadType = 2,
                Prop65Message = new ProductProp65Message
                {
                    MessageBody = "This product has been created through a test automation tool"
                },
                SearchKeywords = "test",
                Shipping = new ProductShippingAttributes()
                {
                    FreightClass = 179,
                    IsFreeShip = true,
                    IsFreightOnly = false,
                    IsQuickShip = true,
                    WeightActual = 6.72m,
                    WeightDimensional = 20.62m,
                    Height = 9.61m,
                    Width = 16.48m,
                    Length = 9.60m
                },
                Specs = new List<ProductSpecification>
                {
                    new ProductSpecification
                    {
                        Name = "pika pika",
                        Value = "chu shock"
                    }
                },
            };

            HttpResponseExtended<ProductResponse> createProductResponse = await integrationsClientV2.Products.Create(createProductRequest);

            Assert.IsNotNull(createProductResponse, $"{nameof(createProductResponse)} is null");
            Assert.IsTrue(createProductResponse.Success == true, $"Product status code is {createProductResponse.StatusCode}");
            Assert.IsNotNull(createProductResponse.Result.ExternalIdentifier, $"{nameof(createProductResponse.Result.ExternalIdentifier)} is null");

            OfferingRequest createOfferingRequest = new OfferingRequest
            {
                Identifier = OfferingExternalId,
                CatalogId = "externalDev",
                Links = new List<OfferingLink>
                {
                    new OfferingLink
                    {
                        Label = "hot side catalog",
                        Url = "http://tevin.info/unbranded-steel-chicken/primary/bypass",
                        Reference = "20"
                    }
                },
                CategoryIds = new List<string>//TODO: add a real category should be an external identifier
                {
                    offeringCategory
                },
                Description = "Description for this test offering e2e",
                FullName = offeringFullName,
                HomeCategory = offeringCategory,
                HtmlPage = new HtmlPage
                {
                    H1 = $"H1 {offeringFullName}",
                    Title = $"Title {offeringFullName}",
                    Meta = $"Meta {offeringFullName}",
                    Url = $"{offeringFullName.ToLower().Replace(" ", "-")}"
                },
                IsPreviewAble = true,
                IsPublishAble = true,
                ProductId = createProductResponse.Result.ExternalIdentifier
            };

            HttpResponseExtended<OfferingResponse> createOfferingResponse = await integrationsClientV2.Offering.Create(createOfferingRequest);

            Assert.IsNotNull(createOfferingResponse, $"{nameof(createOfferingResponse)} is null");
            Assert.IsTrue(createOfferingResponse.Success, $"Offering response status is {createOfferingResponse.StatusCode}");

            //Publish merchandise
            PublishMerchandiseRequest publishMerchandiseRequest = new PublishMerchandiseRequest
            {
                Hostname = platformHostName,
                PromoteOnSuccess = true,
                ClearUnusedCache = true
            };

            HttpResponse<PublishMerchandiseResponse> publishMerchandiseResponse = await integrationsClientV1.PublishMerchandise.Publish(publishMerchandiseRequest);

            Assert.IsNotNull(publishMerchandiseResponse, $"{nameof(publishMerchandiseResponse)} is null");
            Assert.IsNull(publishMerchandiseResponse.Message, publishMerchandiseResponse.Message);

            //publish content request
            PublishContentRequest publishContentRequest = new PublishContentRequest
            {
                Hostname = platformHostName
            };

            //HttpResponse<PublishContentResponse> publishContentResponse = await integrationsClientV1.PublishContent.Publish(publishContentRequest);

            //Assert.IsNotNull(publishContentResponse, $"{nameof(publishContentResponse)}");
        }

        [TestMethod]
        public async Task ClearOfferingProduct()
        {
            string platformIdentifier = PlatformId;
            string platformName = ServiceConstants.FMPPlatformName;

            IntegrationsWebAppClient integrationsClientV2 = new IntegrationsWebAppClient(ServiceConstants.IntegrationsAPIUrl, platformName);

            DeleteProductRequest deleteProductRequest = new DeleteProductRequest
            {
                ExternalIdentifier = ProductExternalId
            };

            HttpResponseExtended<ProductResponse> deleteProductResponse = await integrationsClientV2.Products.Remove(deleteProductRequest);

            Assert.IsTrue(deleteProductResponse.Success, $"Response on product should be 204, got {deleteProductResponse.StatusCode}");
            Assert.IsNull(deleteProductResponse.ErrorMessage, $"{nameof(deleteProductResponse.ErrorMessage)} should be null");

            DeleteOfferingRequest deleteOfferingRequest = new DeleteOfferingRequest
            {
                ExternalIdentifier = OfferingExternalId
            };

            HttpResponseExtended<OfferingResponse> deleteOfferingResponse = await integrationsClientV2.Offering.Remove(deleteOfferingRequest);

            Assert.IsNotNull(deleteOfferingResponse, $"{nameof(deleteOfferingResponse)} should not be null");
            Assert.IsTrue(deleteOfferingResponse.Success, $"Status code on offering is: {deleteOfferingResponse.StatusCode}");
        }
    }
}