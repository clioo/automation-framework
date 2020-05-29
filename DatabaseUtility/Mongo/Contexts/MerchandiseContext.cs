using DatabaseUtility.Models;
using DatabaseUtility.Models.Merchandise;
using DatabaseUtility.Mongo.Contexts.Interfaces;
using MongoDB.Driver;

namespace DatabaseUtility.Mongo.Contexts
{
    public class MerchandiseContext : MongoContext, IMerchandiseContext
    {
        public IMongoRepository<Product, ProductContent> _products;

        public IMongoRepository<Offering, OfferingContent> _offerings;

        public IMongoRepository<Price, PriceContent> _prices;

        public IMongoRepository<Brand, BrandContent> _brands;

        public IMongoRepository<PriceList, PriceListContent> _priceLists;

        public IMongoRepository<Facet, FacetContent> _facets;

        public IMongoRepository<Category, CategoryContent> _categories;

        public IMongoRepository<Catalog, CatalogContent> _catalogs;

        public MerchandiseContext(IMongoDatabase database) : base(database)
        {
        }

        public IMongoRepository<Product, ProductContent> Products => _products ?? (_products = new MongoRepository<Product, ProductContent>(_mongoDatabase, nameof(Product)));
        public IMongoRepository<Offering, OfferingContent> Offerings => _offerings ?? (_offerings = new MongoRepository<Offering, OfferingContent>(_mongoDatabase, nameof(Offering)));
        public IMongoRepository<Price, PriceContent> Prices => _prices ?? (_prices = new MongoRepository<Price, PriceContent>(_mongoDatabase, nameof(Price)));
        public IMongoRepository<Brand, BrandContent> Brands => _brands ?? (_brands = new MongoRepository<Brand, BrandContent>(_mongoDatabase, nameof(Brand)));
        public IMongoRepository<PriceList, PriceListContent> PriceLists => _priceLists ?? (_priceLists = new MongoRepository<PriceList, PriceListContent>(_mongoDatabase, nameof(PriceList)));
        public IMongoRepository<Facet, FacetContent> Facets => _facets ?? (_facets = new MongoRepository<Facet, FacetContent>(_mongoDatabase, nameof(Facet)));
        public IMongoRepository<Category, CategoryContent> Categories => _categories ?? (_categories = new MongoRepository<Category, CategoryContent>(_mongoDatabase, "Categorie"));
        public IMongoRepository<Catalog, CatalogContent> Catalogs => _catalogs ?? (_catalogs = new MongoRepository<Catalog, CatalogContent>(_mongoDatabase, nameof(Catalog)));
    }
}