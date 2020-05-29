using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Catalogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Categories
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class CategoriesEndpointTest : IntegrationsBaseTest<CategoryTestData>
    {
        public CategoriesEndpointTest() : base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {

        }

        [TestMethod]
        public async Task GET_Category_Success()
        {
            string externalIdentifier = "getCategory01";

            //test scenario setup
            CategoryTestData testData = new CategoryTestData
            {
                ExternalIdentifier = externalIdentifier,
                CatalogExtId = "getCategoryCatalog01"
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<CategoryResponse> response = await Client.Categories.GetSingle(externalIdentifier);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, $"Response is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //test sceanrio clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_Category_Not_Found()
        {
            string externalIdentifier = "getCategory02";

            HttpResponseExtended<CategoryResponse> response = await Client.Categories.GetSingle(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task POST_Category_Success()
        {
            string categoryName = "temporal category";

            //test scenario setup
            CategoryTestData testData = new CategoryTestData
            {
                ExternalIdentifier = "postCategory01",
                CatalogExtId = "postCategoryCatalog01",
                ParentCategoryExtId = "postCategoryParent01"
            };

            //create catalog
            CatalogRequest catalogRequest = new CatalogRequest
            {
                ExternalIdentifier = testData.CatalogExtId,
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                CreatedUtc = DateTime.UtcNow.ToString(),
                UpdatedUtc = DateTime.UtcNow.ToString(),
                CreatedBy = "temporal catalog",
                Identifier = Guid.NewGuid(),
                Name = "temporal catalog name",
            };
            await Client.Catalogs.Create(catalogRequest);

            //create parent category
            CategoryRequest parentCategoryRequest = new CategoryRequest
            {
                CatalogId = testData.CatalogExtId,
                FullName = categoryName,
                CollapseOrder = 1,
                CreatedBy = "temporal request",
                Identifier = testData.ParentCategoryExtId,
                UrlSegment = "sample-category/",
                HtmlPage = new HtmlPage
                {
                    H1 = "H1 sample title",
                    Title = "Title ",
                    Meta = "Meta ",
                    Url = "url/"
                },
                IsLanding = false,
                IsMore = false,
                IsSubcatalog = false,
                SortOrder = 1,
                ShortName = "parent short name",
                IsTopMenu = false
            };
            await Client.Categories.Create(parentCategoryRequest);

            CategoryRequest request = new CategoryRequest
            {
                CatalogId = testData.CatalogExtId,
                CollapseOrder = 1,
                Identifier = testData.ExternalIdentifier,
                FullName = categoryName,
                IsLanding = false,
                IsTopMenu = false,
                ParentId = testData.ParentCategoryExtId,
                HtmlPage = new HtmlPage
                {
                    H1 = $"H1 {categoryName}",
                    Meta = $"Meta {categoryName}",
                    Title = $"Title {categoryName}",
                    Url = $"{testData.ParentCategoryExtId}/{categoryName.Replace(" ", "-")}/".ToLower()
                },
                ShortName = categoryName,
                SortOrder = 0,
                UrlSegment = $"{categoryName.Replace(" ", "-")}/"
            };
            HttpResponseExtended<CategoryResponse> response = await Client.Categories.Create(request);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, $"Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //TODO
            //post specific validations

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //POST_Category_Invalid

        [TestMethod]
        public async Task PUT_Category_Success()
        {
            string externalIdentifier = "putCategory01";
            string categoryName = "Category x updated";

            //test scenario setup
            CategoryTestData testData = new CategoryTestData
            {
                ExternalIdentifier = externalIdentifier,
                CatalogExtId = "catalogPutCategory01"
            };
            await TestScenarioSetUp(testData);

            CategoryRequest request = new CategoryRequest
            {
                UpdatedBy = "put category request",
                CatalogId = testData.CatalogExtId,
                CollapseOrder = 1,
                Identifier = externalIdentifier,
                FullName = categoryName,
                IsLanding = false,
                IsTopMenu = false,
                IsSubcatalog = false,
                HtmlPage = new HtmlPage
                {
                    H1 = $"H1 {categoryName}",
                    Meta = $"Meta {categoryName}",
                    Title = $"Title {categoryName}",
                    Url = $"{categoryName.Replace(" ", "-")}/".ToLower()
                },
                ShortName = categoryName,
                SortOrder = 0,
                UrlSegment = $"/{categoryName.Replace(" ", "-")}/".ToLower(),
                IsMore = false,
                ContentZoneId = Guid.Empty
            };
            HttpResponseExtended<CategoryResponse> response = await Client.Categories.Update(externalIdentifier, request);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //TODO
            //specific validations

            await TestScenarioCleanUp(testData);
        }

        //TODO
        //PUT_Category_NotFound

        //TODO
        //PUT_Category_Invalid

        [TestMethod]
        public async Task DELETE_Category_Success()
        {
            string externalIdentifier = "deleteCategory01";

            //test scenario setup
            CategoryTestData testData = new CategoryTestData
            {
                ExternalIdentifier = externalIdentifier,
                CatalogExtId = "deleteCategoryCatalog01"
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<CategoryResponse> response = await Client.Categories.Remove(externalIdentifier);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.IsTrue(response.Success, $"Response status is not successful");
            Assert.AreEqual(200, response.StatusCode);

            //TODO
            //delete specific validations

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task DELETE_Category_NotFound()
        {
            string externalIdentifier = "deleteCategory02";

            HttpResponseExtended<CategoryResponse> response = await Client.Categories.Remove(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result, "result object should be null");
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task PATCH_Category_Success()
        {
            string externalIdentifier = "patchCategory01";

            //test scenario setup
            CategoryTestData testData = new CategoryTestData
            {
                ExternalIdentifier = externalIdentifier,
                CatalogExtId = "patchCatalogCategory01"
            };
            await TestScenarioSetUp(testData);

            var request = new
            {
                UpdatedBy = "successfully patched",
                FullName = "full name patched",
                ThumbnailImage = "imageurl"
            };
            HttpResponseExtended<CategoryResponse> response = await Client.Categories.PatchEntity(externalIdentifier, request);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.IsTrue(response.Success, $"Status code is: {response.StatusCode}");

            //specific validations
            Assert.AreEqual(request.FullName, response.Result.FullName);
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.ThumbnailImage, response.Result.ThumbnailImage);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PATCH_Category_Not_Found()
        {
            string externalIdentifier = "patchCategory02";

            var request = new
            {
                UpdatedBy = "invalid request"
            };

            HttpResponseExtended<CategoryResponse> response = await Client.Categories.PatchEntity(externalIdentifier, request);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be success");
            Assert.IsNull(response.Result, "Result object should be null");
        }

        public override async Task TestScenarioSetUp(CategoryTestData testData)
        {
            string categoryName = "temporal test category";

            CatalogRequest catalogRequest = new CatalogRequest
            {
                CreatedBy = "temporal request",
                PlatformIdentifier = new Guid(ServiceConstants.AllPointsPlatformId),
                CreatedUtc = DateTime.UtcNow.ToString(),
                UpdatedUtc = DateTime.UtcNow.ToString(),
                ExternalIdentifier = testData.CatalogExtId,
                Identifier = Guid.NewGuid(),
                Name = "temporal catalog name",

            };
            CategoryRequest categoryRequest = new CategoryRequest
            {
                Identifier = testData.ExternalIdentifier,
                CatalogId = testData.CatalogExtId,
                CollapseOrder = 1,
                CreatedBy = "temporal request",
                IsSubcatalog = false,
                FullName = categoryName,
                ShortName = categoryName,
                UrlSegment = $"{categoryName.Replace(" ", "-")}/",
                HtmlPage = new HtmlPage
                {
                    H1 = $"H1 {categoryName}",
                    Meta = $"Meta {categoryName}",
                    Title = $"Title {categoryName}",
                    Url = $"{categoryName.Replace(" ", "-")}/"
                },
                IsMore = false,
                SortOrder = 1,
                IsLanding = false,
                IsTopMenu = false
            };

            //parent category external id was provided
            if (!string.IsNullOrEmpty(testData.ParentCategoryExtId))
            {
                CategoryRequest parentCategoryRequest = new CategoryRequest
                {
                    Identifier = testData.ParentCategoryExtId,
                    CatalogId = testData.CatalogExtId,
                    FullName = "parent category name",
                    ShortName = "parent name",
                    CollapseOrder = 1,
                    CreatedBy = "temporal request",
                    HtmlPage = new HtmlPage
                    {
                        Url = "parent/,",
                        H1 = "H1 parent",
                        Meta = "Meta",
                        Title = "Title"
                    },
                    IsLanding = true,
                    IsSubcatalog = false,
                    SortOrder = 1,
                    UrlSegment = "parent/",
                    IsMore = false,
                    IsTopMenu = false
                };
                await Client.Categories.Create(parentCategoryRequest);

                categoryRequest.ParentId = testData.ParentCategoryExtId;
            }

            await Client.Catalogs.Create(catalogRequest);
            await Client.Categories.Create(categoryRequest);
        }

        public override async Task TestScenarioCleanUp(CategoryTestData testData)
        {
            await Client.Categories.Remove(testData.ExternalIdentifier);
            await Client.Catalogs.Remove(testData.CatalogExtId);

            if (!string.IsNullOrEmpty(testData.ParentCategoryExtId))
                await Client.Categories.Remove(testData.ParentCategoryExtId);
        }
    }
}
