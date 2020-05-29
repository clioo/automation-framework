using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Brands;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Catalogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Merchandise
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class OfferingTest : IntegrationsBaseTest<OfferingTestData>
    {
        public OfferingTest() : base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {
            
        }

        [TestMethod]
        public async Task GET_Offering_Success()
        {
            string externalIdentifier = "getOffering01";
            OfferingTestData testData = new OfferingTestData
            {
                OfferingExtId = externalIdentifier,
                CatalogExtId = "getOfferingCatalog01",
                BrandExtId = "getOfferingBrand01",
                ProductExtId = "getOfferingProduct01",
                CategoryExtId = "getOfferingCategory01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<OfferingResponse> response = await Client.Offerings.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "The response object is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsNotNull(response.Result, "The result object is null");
            Assert.AreEqual(externalIdentifier, response.Result.ExternalIdentifier);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_Offering_Not_Found()
        {
            string externalIdentifier = "getOffering02";

            HttpResponseExtended<OfferingResponse> response = await Client.Offerings.GetSingle(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public async Task DELETE_Offering_Success()
        {
            string externalIdentifier = "deleteOffering01";
            OfferingTestData testData = new OfferingTestData
            {
                OfferingExtId = externalIdentifier,
                BrandExtId = "deleteOfferingBrand01",
                ProductExtId = "deleteOfferingProduct01",
                CatalogExtId = "deleteOfferingCatalog01",
                CategoryExtId = "deleteOfferingCategory01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<OfferingResponse> response = await Client.Offerings.Remove(externalIdentifier);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.AreEqual(204, response.StatusCode);
            Assert.IsTrue(response.Success, $"Status code is: {response.StatusCode}");

            //delete specific validations
            var getResponse = await Client.Offerings.GetSingle(externalIdentifier);
            Assert.AreEqual(404, getResponse.StatusCode);
            Assert.IsNull(getResponse.Result);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task DELETE_Offering_Not_Found()
        {
            string externalIdentifier = "deleteOffering02";

            HttpResponseExtended<OfferingResponse> response = await Client.Offerings.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result);
            Assert.IsFalse(response.Success, "Response status should not be successful");
        }

        [TestMethod]
        public async Task POST_Offering_Success()
        {
            string offeringFullName = "frozen turkey ready to deploy";
            string externalIdentifier = "postOfferingExternal01";

            OfferingTestData testData = new OfferingTestData
            {
                OfferingExtId = externalIdentifier,
                CategoryExtId = "offeringCategoryExternal01",
                CatalogExtId = "offeringCatalogExternal01",
                ProductExtId = "offeringProductExternal01",
                BrandExtId = "offeringBrandExternal01"
            };
            
            //create catalog
            CatalogRequest catalogRequest = new CatalogRequest
            {
                Name = "Seed catalog name",
                Identifier = Guid.NewGuid(),
                CreatedBy = "Seed helper",
                ExternalIdentifier = testData.CatalogExtId,
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                CreatedUtc = DateTime.UtcNow.ToString(),
                UpdatedUtc = DateTime.UtcNow.ToString()
            };
            await Client.Catalogs.Create(catalogRequest);

            //create a category
            CategoryRequest categoryRequest = new CategoryRequest
            {
                CatalogId = testData.CatalogExtId,
                Identifier = testData.CategoryExtId,
                FullName = "sample category name",
                IsLanding = false,
                ShortName = "sample name",
                IsTopMenu = false,
                UrlSegment = "sample-category-name/",
                CollapseOrder = 1,
                HtmlPage = new HtmlPage
                {
                    H1 = "H1 sample thing",
                    Title = "Title sample thing",
                    Meta = "Meta sample thing",
                    Url = "any-thing/"
                },
                IsSubcatalog = false,
                IsMore = false,
                CreatedBy = "temporal test method"
            };
            await Client.Categories.Create(categoryRequest);

            //create a brand
            PostBrandRequest brandRequest = new PostBrandRequest
            {
                CreatedBy = "temporal test method",
                CreatedUtc = DateTime.Now.ToString(),
                UpdatedUtc = DateTime.Now.ToString(),
                ExternalIdentifier = testData.BrandExtId,
                Identifier = Guid.NewGuid(),
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                FullName = "test name",
                ShortName = "test name short",
                HtmlPage = new HtmlPage
                {
                    H1 = "test",
                    Meta = "meta",
                    Title = "test title",
                    Url = "test/"
                },
                Favorability = 2,
                UrlSegment = "test/"
            };
            await Client.Brands.Create(brandRequest);

            //create a product
            ProductRequest productRequest = new ProductRequest
            {
                CreatedBy = "temporal product request",
                Identifier = testData.ProductExtId,
                Cost = 11,
                DisplayProductCode = "temporal-sku",
                IsOcm = false,
                IsPurchaseable = true,
                Name = "product test name",
                ProductCode = "temporal-sku",
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
            await Client.Products.Create(productRequest);

            OfferingRequest request = new OfferingRequest
            {
                Identifier = externalIdentifier,
                CatalogId = testData.CatalogExtId,
                ProductId = testData.ProductExtId,
                HomeCategory = testData.CategoryExtId,
                CategoryIds = new List<string>(),
                Description = "Description for this offering p:",
                FullName = offeringFullName,
                HtmlPage = new HtmlPage
                {
                    H1 = $"H1 {offeringFullName}",
                    Title = $"Title {offeringFullName}",
                    Meta = $"Meta {offeringFullName}",
                    Url = $"{offeringFullName.Replace(" ", "-")}"
                },
                IsPublishAble = true,
                IsPreviewAble = true,
                Links = new List<OfferingLink>(),
                CreatedBy = "post offering request"
            };

            HttpResponseExtended<OfferingResponse> response = await Client.Offerings.Create(request);

            Assert.IsNotNull(response, $"{nameof(response)} cannot be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, $"Response status is not successful");
            Assert.IsNotNull(response.Result, $"{nameof(response.Result)} is null");

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PATCH_Offering_Success()
        {
            string externalIdentifier = "patchOffering01";
            OfferingTestData testData = new OfferingTestData
            {
                OfferingExtId = externalIdentifier,
                BrandExtId = "patchOfferingBrand01",
                ProductExtId = "patchOfferingProduct01",
                CatalogExtId = "patchOfferingCatalog01",
                CategoryExtId = "patchOfferingCategory01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            var request = new
            {
                UpdatedBy = "Me on patch",
                Description = "Description patched",
                //CatalogId = "catalogNon",
                FullName = "fullname pathed"
            };
            HttpResponseExtended<OfferingResponse> response = await Client.Offerings.PatchEntity(externalIdentifier, request);

            Assert.IsNotNull(response, "response should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, $"Status code is: {response.StatusCode}");
            Assert.IsNotNull(response.Result, "Result response should not be null");

            //patch specific validations
            Assert.AreEqual(request.Description, response.Result.Description);
            Assert.AreEqual(request.FullName, response.Result.FullName);
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PATCH_Offering_Not_Found()
        {
            string externalIdentifier = "patchOffering02";

            var request = new
            {
                UpdatedBy = "npot found request"
            };

            HttpResponseExtended<OfferingResponse> response = await Client.Offerings.PatchEntity(externalIdentifier, request);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be success");
            Assert.IsNull(response.Result, "result object should be null");
        }

        public override async Task TestScenarioCleanUp(OfferingTestData testData)
        {
            //delete catalog
            await Client.Catalogs.Remove(testData.CatalogExtId);

            //delete product
            await Client.Products.Remove(testData.ProductExtId);

            //detele brand
            await Client.Brands.Remove(testData.BrandExtId);

            //delete category
            await Client.Categories.Remove(testData.CategoryExtId);

            //delete offering
            await Client.Offerings.Remove(testData.OfferingExtId);
        }

        public override async Task TestScenarioSetUp(OfferingTestData testData)
        {
            //create catalog
            CatalogRequest catalogRequest = new CatalogRequest
            {
                Name = "Seed catalog name",
                Identifier = Guid.NewGuid(),
                CreatedBy = "Seed helper",
                ExternalIdentifier = testData.CatalogExtId,
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                CreatedUtc = DateTime.UtcNow.ToString(),
                UpdatedUtc = DateTime.UtcNow.ToString()
            };
            await Client.Catalogs.Create(catalogRequest);

            //create a category
            CategoryRequest categoryRequest = new CategoryRequest
            {
                CatalogId = testData.CatalogExtId,
                Identifier = testData.CategoryExtId,
                FullName = "sample category name",
                IsLanding = false,
                ShortName = "sample name",
                IsTopMenu = false,
                UrlSegment = "sample-category-name/",
                CollapseOrder = 1,
                HtmlPage = new HtmlPage
                {
                    H1 = "H1 sample thing",
                    Title = "Title sample thing",
                    Meta = "Meta sample thing",
                    Url = "any-thing/"
                },
                IsSubcatalog = false,
                IsMore = false,
                CreatedBy = "temporal test method"
            };
            await Client.Categories.Create(categoryRequest);

            //create a brand
            PostBrandRequest brandRequest = new PostBrandRequest
            {
                CreatedBy = "temporal test method",
                CreatedUtc = DateTime.Now.ToString(),
                UpdatedUtc = DateTime.Now.ToString(),
                ExternalIdentifier = testData.BrandExtId,
                Identifier = Guid.NewGuid(),
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                FullName = "test name",
                ShortName = "test name short",
                HtmlPage = new HtmlPage
                {
                    H1 = "test",
                    Meta = "meta",
                    Title = "test title",
                    Url = "test/"
                },
                Favorability = 2,
                UrlSegment = "test/"
            };
            await Client.Brands.Create(brandRequest);

            //create a product
            ProductRequest productRequest = new ProductRequest
            {
                CreatedBy = "temporal product request",
                Identifier = testData.ProductExtId,
                Cost = 11,
                DisplayProductCode = "temporal-sku",
                IsOcm = false,
                IsPurchaseable = true,
                Name = "product test name",
                ProductCode = "temporal-sku",
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
            await Client.Products.Create(productRequest);

            //create a offering
            string offeringFullName = "temporal frozen Sturkey";
            OfferingRequest offeringRequest = new OfferingRequest
            {
                CreatedBy = "temporal offering",
                Identifier = testData.OfferingExtId,
                CatalogId = testData.CatalogExtId,
                ProductId = testData.ProductExtId,
                HomeCategory = testData.CategoryExtId,
                CategoryIds = new List<string>(),
                Description = "temporal Description for this offering",
                FullName = offeringFullName,
                HtmlPage = new HtmlPage
                {
                    H1 = $"H1 {offeringFullName}",
                    Title = $"Title {offeringFullName}",
                    Meta = $"Meta {offeringFullName}",
                    Url = $"{offeringFullName.Replace(" ", "-")}"
                },
                IsPublishAble = true,
                IsPreviewAble = true,
                Links = new List<OfferingLink>(),
            };
            await Client.Offerings.Create(offeringRequest);
        }
    }
}
