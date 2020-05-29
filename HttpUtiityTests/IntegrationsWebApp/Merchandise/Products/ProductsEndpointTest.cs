using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Brands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Merchandise
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class ProductsEndpointTest : IntegrationsBaseTest<ProductTestData>
    {
        public ProductsEndpointTest() : base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {

        }

        [TestMethod]
        public async Task POST_Product_Success()
        {
            string externalIdentifier = "postProductExternalId01";

            //test scenario setup
            ProductTestData testData = new ProductTestData
            {
                ExternalIdentifier = externalIdentifier,
                BrandExtId = "postProductBrandExternalId01"
            };
            
            //create brand
            PostBrandRequest brandRequest = new PostBrandRequest
            {
                CreatedBy = "temporal request",
                CreatedUtc = DateTime.UtcNow.ToString(),
                UpdatedUtc = DateTime.UtcNow.ToString(),
                UrlSegment = "temporal/",
                FullName = "temporal brand",
                ExternalIdentifier = testData.BrandExtId,
                Favorability = 1,
                HtmlPage = new HtmlPage
                {
                    H1 = "H1 temporal heading",
                    Meta = "Meta temporal",
                    Title = "Title temporal",
                    Url = "test-brand/"
                },
                Identifier = Guid.NewGuid(),
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                ShortName = "temporal brand"
            };
            await Client.Brands.Create(brandRequest);

            ProductRequest request = new ProductRequest
            {
                CreatedBy = "postRequest success",
                Identifier = externalIdentifier,
                Cost = 11,
                DisplayProductCode = "postsku01",
                IsOcm = false,
                IsPurchaseable = true,
                Name = "product test name",
                ProductCode = "postsku01",
                Prop65Message = new ProductProp65Message
                {
                    MessageBody = "This product has been created through a test automation tool"
                },
                SearchKeywords = "test,seed",
                Shipping = new ProductShippingAttributes
                {
                    FreightClass = 179,
                    IsFreeShip = false,
                    IsFreightOnly = false,
                    IsQuickShip = false,
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
                        Name = "name of spec",
                        Value = "value of spec"
                    }
                },
                OemRelationships = new List<ProductOem>
                {
                    new ProductOem
                    {
                        OemName = "oem name 01",
                        Type = 1,
                        OemSku = "FakeOemSku"
                    }
                },
                ProductLeadType = 1,
                BrandSku = "fake",
                PrimaryBrandIdentifier = testData.BrandExtId
            };
            HttpResponseExtended<ProductResponse> response = await Client.Products.Create(request);

            Assert.IsNotNull(response, $"{nameof(response)} is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, $"Status code is {response.StatusCode}");

            //post specific validations
            Assert.AreEqual(request.Identifier, response.Result.ExternalIdentifier);
            Assert.AreEqual(request.CreatedBy, response.Result.CreatedBy);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //pending
        //[TestMethod]
        public async Task POST_Product_Invalid()
        {
            string externalIdentifier = "postProductExternalId02";
            ProductRequest request = new ProductRequest
            {
                //does not have the CreatedBy property
                ProductCode = "nothing",
                Identifier = externalIdentifier
            };

            HttpResponseExtended<ProductResponse> response = await Client.Products.Create(request);

            Assert.IsNotNull(response, $"{nameof(response)} is null");
            //response is 500
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success, $"Status code is {response.StatusCode}");
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GET_Product_Success()
        {
            string externalIdentifier = "getProductExternalId01";

            //test scenario setup
            ProductTestData testData = new ProductTestData
            {
                ExternalIdentifier = externalIdentifier,
                BrandExtId = "getProductBrand01"
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<ProductResponse> response = await Client.Products.GetSingle(externalIdentifier);

            Assert.IsNotNull(response, "response is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, $"Response status is: {response.StatusCode}");
            Assert.IsNotNull(response.Result, "Product code is null");
            Assert.AreEqual(externalIdentifier, response.Result.ExternalIdentifier);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_Product_Not_Found()
        {
            string externalIdentifier = "getProduct02";

            HttpResponseExtended<ProductResponse> response = await Client.Products.GetSingle(externalIdentifier);

            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_Product_Success()
        {
            string externalIdentifier = "deleteProductExternalId01";

            //test scenario setup
            ProductTestData testData = new ProductTestData
            {
                ExternalIdentifier = externalIdentifier,
                BrandExtId = "deleteProductBrand01"
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<ProductResponse> response = await Client.Products.Remove(externalIdentifier);

            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.StatusCode == 204, $"Response should be 204, got {response.StatusCode}");
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Result, "result object is null");

            //delete brand
            await Client.Brands.Remove(testData.BrandExtId);
        }

        [TestMethod]
        public async Task DELETE_Product_Not_Found()
        {
            string externalIdentifier = "deleteProductExternalId02";
            //test scenario setup

            HttpResponseExtended<ProductResponse> response = await Client.Products.Remove(externalIdentifier);

            Assert.IsNotNull(response, "response is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result, "result object should be null");
        }

        [TestMethod]
        public async Task PUT_Product_Success()
        {
            string externalIdentifier = "putProductExternal01";

            //test scenario set up
            ProductTestData testData = new ProductTestData
            {
                ExternalIdentifier = externalIdentifier,
                BrandExtId = "putProductBrand01"
            };
            await TestScenarioSetUp(testData);

            ProductRequest request = new ProductRequest
            {
                UpdatedBy = "Me by put",
                Cost = 110,
                DisplayProductCode = "updatedSku",
                IsOcm = true,
                IsPurchaseable = true,
                Name = "product test name (updated)",
                ProductCode = "updatedSku",
                Prop65Message = new ProductProp65Message
                {
                    MessageBody = "This product has been created through a test automation tool"
                },
                SearchKeywords = "seed,data",
                Shipping = new ProductShippingAttributes()
                {
                    FreightClass = 179,
                    IsFreeShip = false,
                    IsFreightOnly = false,
                    IsQuickShip = false,
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
                        Name = "name of spec",
                        Value = "value of spec"
                    }
                },
                OemRelationships = new List<ProductOem>
                {
                    new ProductOem
                    {
                        OemName = "oem name 01",
                        Type = 1,
                        OemSku = "FakeOemSku"
                    }
                },
                PrimaryBrandIdentifier = testData.BrandExtId,
                AlternateSkus = new List<string>
                {
                    "data",
                    "seed"
                },
                BrandSku = "temporal",
                ProductLeadType = 1,
                CreatedBy = "cannot be updated"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<ProductResponse> response = await Client.Products.Update(externalIdentifier, request);

            Assert.IsNotNull(response, $"{nameof(response)} cannot be null");
            Assert.IsTrue(response.Success, $"Request status code is: {response.StatusCode}");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //put specific validations
            Assert.AreEqual(request.Cost, response.Result.Cost);
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.DisplayProductCode, response.Result.DisplayProductCode);
            Assert.AreEqual(request.Name, response.Result.Name);
            Assert.AreEqual(request.ProductCode, response.Result.ProductCode);
            Assert.AreNotEqual(request.CreatedBy, response.Result.CreatedBy);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PUT_Product_Not_Found()
        {
            string externalIdentifier = "putProductExternal02";
            string brandExternalId = "putProductBrand02";

            ProductRequest request = new ProductRequest
            {
                UpdatedBy = "Me by put",
                Identifier = externalIdentifier,
                Cost = 11,
                DisplayProductCode = "postsku01",
                IsOcm = false,
                IsPurchaseable = true,
                Name = "product test name",
                ProductCode = "postsku01",
                Prop65Message = new ProductProp65Message
                {
                    MessageBody = "This product has been created through a test automation tool"
                },
                SearchKeywords = "test,seed",
                Shipping = new ProductShippingAttributes()
                {
                    FreightClass = 179,
                    IsFreeShip = false,
                    IsFreightOnly = false,
                    IsQuickShip = false,
                    WeightActual = 6.72m,
                    WeightDimensional = 20.62m,
                    Height = 9.61m,
                    Width = 16.48m,
                    Length = 9.60m
                },
                Specs = new List<ProductSpecification>()
                {
                    new ProductSpecification
                    {
                        Name = "name of spec",
                        Value = "value of spec"
                    }
                },
                OemRelationships = new List<ProductOem>
                {
                    new ProductOem
                    {
                        OemName = "oem name 01",
                        Type = 1,
                        OemSku = "FakeOemSku"
                    }
                },
                ProductLeadType = 1,
                BrandSku = "fake",
                PrimaryBrandIdentifier = brandExternalId
            };

            //await Client.Products.Create(request);

            HttpResponseExtended<ProductResponse> response = await Client.Products.Update(externalIdentifier, request);

            Assert.IsNotNull(response, $"{nameof(response)} cannot be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, $"Request status code is: {response.StatusCode}");
            Assert.IsNull(response.Result, "Result object should not be null");
        }

        //TODO
        //add PUT_Invalid

        [TestMethod]
        public async Task PATCH_Product_Success()
        {
            string externalIdentifier = "patchProductExternalId01";

            //test scenario setup
            ProductTestData testData = new ProductTestData
            {
                ExternalIdentifier = externalIdentifier,
                BrandExtId = "patchProduct01"
            };
            await TestScenarioSetUp(testData);

            var request = new
            {
                Name = "patched name",
                UpdatedBy = "Me ;3",
                DisplayProductCode = "patched;3"
            };
            HttpResponseExtended<ProductResponse> response = await Client.Products.PatchEntity(externalIdentifier, request);

            Assert.IsNotNull(response, $"{nameof(response)} is null o:");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, $"Current status response: {response.StatusCode}");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //patch validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.Name, response.Result.Name);
            Assert.AreEqual(request.DisplayProductCode, response.Result.DisplayProductCode);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PATCH_Not_Found()
        {
            string externalIdentifier = "patchProduct02";

            var request = new
            {
                UpdatedBy = "Not found scenario"
            };

            HttpResponseExtended<ProductResponse> response = await Client.Products.PatchEntity(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result);
        }

        public override async Task TestScenarioSetUp(ProductTestData testData)
        {
            PostBrandRequest brandRequest = new PostBrandRequest
            {
                ExternalIdentifier = testData.BrandExtId,
                CreatedBy = "temporal test",
                CreatedUtc = DateTime.UtcNow.ToString(),
                Favorability = 1,
                FullName = "sample full name",
                Identifier = Guid.NewGuid(),
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                ShortName = "sample short name",
                UrlSegment = "sample-brand/",
                HtmlPage = new HtmlPage
                {
                    H1 = "H1 temporal heading",
                    Meta = "Meta temporal ",
                    Title = "Title temporal",
                    Url = "sample-url/"
                },
                UpdatedUtc = DateTime.UtcNow.ToString()
            };
            ProductRequest productRequest = new ProductRequest
            {
                PrimaryBrandIdentifier = testData.BrandExtId,
                BrandSku = "fake",
                Cost = 100,
                CreatedBy = "sample test helper",
                DisplayProductCode = "makukkos",
                Identifier = testData.ExternalIdentifier,
                ProductCode = "makukkos",
                Name = "product fake",
                IsOcm = false,
                IsPurchaseable = false,
                ProductLeadType = 2,
                SearchKeywords = "makukkos",
                Specs = new List<ProductSpecification>
                {
                    new ProductSpecification
                    {
                        Name = "spec name",
                        Value = "spec value"
                    }
                },
                Shipping = new ProductShippingAttributes
                {
                    FreightClass = 12,
                    Width = 120,
                    Length = 12,
                    Height = 120,
                    IsFreeShip = true,
                    IsFreightOnly = false,
                    IsQuickShip = true,
                    WeightActual = 125,
                    WeightDimensional = 300
                }
            };

            await Client.Brands.Create(brandRequest);
            await Client.Products.Create(productRequest);
        }

        public override async Task TestScenarioCleanUp(ProductTestData testData)
        {
            await Client.Brands.Remove(testData.BrandExtId);
            await Client.Products.Remove(testData.ExternalIdentifier);
        }
    }
}
