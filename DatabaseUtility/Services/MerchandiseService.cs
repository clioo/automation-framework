using DatabaseUtility.API.Models.Merchandise;
using DatabaseUtility.Models;
using DatabaseUtility.Models.Merchandise;
using DatabaseUtility.Models.Merchandise.Common;
using DatabaseUtility.Models.Merchandise.ProductContents;
using DatabaseUtility.Mongo;
using DatabaseUtility.Mongo.Contexts;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseUtility.Services
{
    public class MerchandiseService : ServiceBase
    {
        private MerchandiseContext _context { get; set; }

        public MerchandiseService(string connectionString, string database, Guid platformIdentifier) : base(connectionString, database, platformIdentifier)
        {
            #region BsonMaps

            BsonClassMap.RegisterClassMap<Offering>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<OfferingContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<HtmlPage>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Price>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<PriceContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Brand>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<BrandContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<PriceList>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<PriceListContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Facet>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<FacetContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Catalog>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<CatalogContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Category>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<CategoryContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<ProductContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            #endregion BsonMaps

            _context = PublishedContext<MerchandiseContext, Offering, OfferingContent>.GetPublishedDatabase("merchandise", _mongoClient, platformIdentifier);
            //_context = new MerchandiseContext(_mongoDb);
        }

        public async Task<Offering> CreateOffering(string description, string productName, Guid productIdentifier, Guid homeCategoryId)
        {
            OfferingContent content = new OfferingContent(productName);
            content.HtmlPage = new HtmlPage(productName);
            content.Description = description;
            content.PlatformIdentifier = _platformIdentifier;
            content.ProductIdentifier = productIdentifier;
            content.FullName = productName;
            content.HomeCategory = homeCategoryId;

            Offering newEntity = new Offering
            {
                Contents = content
            };

            var entity = await _context.Offerings.AddAsync(newEntity);

            return entity;
        }

        public async Task<Price> CreatePrice(Guid priceListIdentifier, Guid productIdentifier,
            List<VolumePrice> volumePrices, string actual = "200", string addToCart = "0",
            bool isDiscountable = false, bool isEnabled = true, string list = "155.77",
            string sale = "0", string signIn = "0", string webOnly = "0")
        {
            PriceContent content = new PriceContent();
            content.Actual = actual;
            content.AddToCart = addToCart;
            content.IsDiscountable = isDiscountable;
            content.IsEnabled = isEnabled;
            content.List = list;
            content.PriceListIdentifier = priceListIdentifier;
            content.ProductListIdentifier = productIdentifier;
            content.Sale = sale;
            content.SignIn = signIn;
            content.VolumePrices = volumePrices;
            content.WebOnly = webOnly;

            Price newEntity = new Price
            {
                Contents = content
            };

            var entity = await _context.Prices.AddAsync(newEntity);

            return entity;
        }

        public async Task<Brand> CreateBrand(string fullName, int favorability)
        {
            BrandContent content = new BrandContent(favorability, fullName);
            content.PlatformIdentifier = _platformIdentifier;

            Brand newEntity = new Brand
            {
                Contents = content
            };

            var entity = await _context.Brands.AddAsync(newEntity);

            return entity;
        }

        public async Task<PriceList> CreatePriceList(string name, string externalId)
        {
            PriceListContent content = new PriceListContent();
            content.Name = name;
            content.ExternalIdentifier = externalId;
            content.PlatformIdentifier = _platformIdentifier;

            PriceList newEntity = new PriceList
            {
                Contents = content
            };

            var entity = await _context.PriceLists.AddAsync(newEntity);

            return entity;
        }

        public async Task<Product> CreateProduct(ProductCreate productContents)
        {
            ProductContent content = new ProductContent(productContents.ProductCode);

            content.BrandSku = productContents.BrandSku;
            content.Prop65Message = new Prop65Message { MessageBody = productContents.Prop65Message };
            content.PrimaryBrandIdentifier = productContents.PrimaryBrandId;
            content.Cost = productContents.Cost;
            content.Description = productContents.Description;
            content.IsPurchaseable = productContents.IsPurchaseable;
            content.ExternalIdentifier = null;
            content.Name = productContents.Name;
            content.Images = productContents.Images;
            content.Shipping = productContents.Shipping;
            content.AlternateBrandIdentifiers = productContents.AltBrands;
            content.PlatformIdentifier = _platformIdentifier;
            content.AlternateSkus = null;
            content.Inventory = null;
            content.Notes = new List<string>();//empty
            content.OemRelationships = new List<OemItem>();//empty
            content.Specs = productContents.Specifications;
            content.ProductFacets = productContents.ProductFacets;

            Product newEntity = new Product
            {
                Contents = content
            };

            var entity = await _context.Products.AddAsync(newEntity);

            return entity;
        }

        public async Task<Category> CreateCategory(string fullName, Guid catalogId, int collapseOrder, bool isTopMenu, bool isLanding, int sortOrder)
        {
            CategoryContent content = new CategoryContent(fullName);
            content.CatalogIdentifier = catalogId;
            content.CollapseOrder = collapseOrder;
            content.IsTopMenu = isTopMenu;
            content.IsLanding = isLanding;
            content.SortOrder = sortOrder;
            content.HierarchyLevel = 0;
            content.LeftBower = 1;
            content.RightBower = 1;
            content.PlatformIdentifier = _platformIdentifier;
            content.ParentIdentifier = null;

            Category newEntity = new Category
            {
                Contents = content
            };

            var entity = await _context.Categories.AddAsync(newEntity);

            return entity;
        }

        public async Task<Facet> CreateFacet(string name, int sortOrder, List<PossibleFacetValue> possibleValues)
        {
            FacetContent content = new FacetContent();
            content.Name = name;
            content.PlatformIdentifier = _platformIdentifier;
            content.SortOrder = sortOrder;
            content.PossibleFacetValues = possibleValues;

            Facet newEntity = new Facet
            {
                Contents = content
            };

            var entity = await _context.Facets.AddAsync(newEntity);

            return entity;
        }

        public async Task<Catalog> CreateCatalog(string name)
        {
            CatalogContent content = new CatalogContent();
            content.ExternalIdentifier = null;
            content.Menu = null;
            content.OEMs = null;
            content.PlatformIdentifier = _platformIdentifier;
            content.Name = name;

            Catalog newEntity = new Catalog
            {
                Contents = content
            };

            var entity = await _context.Catalogs.AddAsync(newEntity);

            return entity;
        }
    }
}