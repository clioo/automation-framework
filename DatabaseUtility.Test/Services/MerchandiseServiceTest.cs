using DatabaseUtility.API.Models.Merchandise;
using DatabaseUtility.Constants;
using DatabaseUtility.Models.Merchandise;
using DatabaseUtility.Models.Merchandise.ProductContents;
using DatabaseUtility.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DatabaseUtility.Test.Services
{
    [TestClass]
    public class MerchandiseServiceTest : ServiceTestBase<MerchandiseService>
    {
        public MerchandiseServiceTest() : base(ConfigurationConstants.MerchandiseDatabase)
        {
        }

        [TestMethod]
        public void CreateNewDocument()
        {
            var newCatalog = service.CreateCatalog("catalog test").Result;

            var newBrand = service.CreateBrand("super brand", 1).Result;

            var newCategory = service.CreateCategory("test catalog", newCatalog.Contents.Identifier, 1, true, true, 1).Result;

            var newPriceList = service.CreatePriceList("christmasPriceList", Guid.NewGuid().ToString()).Result;

            List<ProdImage> productImages = new List<ProdImage>
            {
                new ProdImage
                {
                    AltText = "this is an image",
                    ExternalIdentifier = "",
                    IsEnabled = true,
                    IsPrimary = true,
                    SortOrder = 1,
                    SourceUrl = "https://s3.amazonaws.com/dfs-allpoints-multisite-test-images/product-images/21000237-423-01-01.jpg"
                }
            };

            ProdShipping prodShipping = new ProdShipping
            {
                FreightClass = "143.90",
                IsFreeShip = true,
                IsFreightOnly = false,
                IsQuickShip = true,
                WeightActual = "11.98",
                WeightDimensional = "15.28",
                Height = "9.4",
                Width = "6.33",
                Length = "14"
            };

            List<Guid> altBrands = new List<Guid>
            {
                Guid.NewGuid(),//random guids
                Guid.NewGuid()//rangom guids
            };

            List<Specification> prodSpecs = new List<Specification>
            {
                new Specification { Name = "name", Value = "value" }
            };

            List<PossibleFacetValue> possibleValues = new List<PossibleFacetValue>
            {
                new PossibleFacetValue("FACET 1", 1)
            };

            ProductCreate productContent = new ProductCreate
            {
                BrandSku = "5HP012828",
                Cost = "58.0",
                Name = "test name",
                Description = "test description",
                ProductCode = "UAT12828",
                Prop65Message = "this is dangerous, click here",
                PrimaryBrandId = newBrand.Contents.Identifier,
                IsPurchaseable = true,
                Images = productImages,
                Shipping = prodShipping,
                AltBrands = altBrands,
                Specifications = prodSpecs
            };

            var newFacet = service.CreateFacet("name of facet", 1, possibleValues).Result;

            var newProduct = service.CreateProduct(productContent).Result;

            var newPrice = service.CreatePrice(newPriceList.Contents.Identifier, newProduct.Contents.Identifier, null).Result;

            var newOffering = service.CreateOffering("frozen turkey", "explosive turkey", newProduct.Contents.Identifier, newCategory.Contents.Identifier).Result;

            Assert.IsNotNull(newBrand);
            Assert.IsTrue(newBrand.Contents.FullName == "super brand");
            Assert.IsNotNull(newProduct);
            Assert.IsNotNull(newPriceList);
            Assert.IsTrue(newPriceList.Contents.Name == "christmasPriceList");
            Assert.IsNotNull(newPrice);
            Assert.IsTrue(newOffering.Contents.FullName.Equals("explosive turkey"));
            Assert.IsNotNull(newOffering);
        }
    }
}