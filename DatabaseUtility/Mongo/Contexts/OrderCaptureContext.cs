using DatabaseUtility.Models.OrderCapture;
using DatabaseUtility.Mongo.Contexts.Interfaces;
using MongoDB.Driver;

namespace DatabaseUtility.Mongo.Contexts
{
    public class OrderCaptureContext : MongoContext, IOrderCaptureContext
    {
        public IMongoRepository<Order, OrderContent> _orders;
        public IMongoRepository<Shopper, ShopperContent> _shoppers;
        public IMongoRepository<Cart, CartContent> _carts;

        public OrderCaptureContext(IMongoDatabase database) : base(database)
        {
        }

        public IMongoRepository<Order, OrderContent> Orders => _orders ?? (_orders = new MongoRepository<Order, OrderContent>(_mongoDatabase, nameof(Order)));
        public IMongoRepository<Shopper, ShopperContent> Shoppers => _shoppers ?? (_shoppers = new MongoRepository<Shopper, ShopperContent>(_mongoDatabase, nameof(Shopper)));
        public IMongoRepository<Cart, CartContent> Carts => _carts ?? (_carts = new MongoRepository<Cart, CartContent>(_mongoDatabase, nameof(Cart)));
    }
}