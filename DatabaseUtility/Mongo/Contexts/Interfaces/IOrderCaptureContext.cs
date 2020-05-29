using DatabaseUtility.Models.OrderCapture;

namespace DatabaseUtility.Mongo.Contexts.Interfaces
{
    internal interface IOrderCaptureContext : IMongoContext
    {
        IMongoRepository<Order, OrderContent> Orders { get; }
        IMongoRepository<Shopper, ShopperContent> Shoppers { get; }
        IMongoRepository<Cart, CartContent> Carts { get; }
    }
}