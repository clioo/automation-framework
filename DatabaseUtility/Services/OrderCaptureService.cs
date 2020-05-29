using DatabaseUtility.Models;
using DatabaseUtility.Models.OrderCapture;
using DatabaseUtility.Mongo.Contexts;
using MongoDB.Bson.Serialization;
using System;
using System.Threading.Tasks;

namespace DatabaseUtility.Services
{
    public class OrderCaptureService : ServiceBase
    {
        private OrderCaptureContext _context { get; set; }

        public OrderCaptureService(string connectionString, string database, Guid platformIdentifier) : base(connectionString, database, platformIdentifier)
        {
            #region BsonMaps

            BsonClassMapExtended.RegisterClassMap<Order>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<OrderContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<Shopper>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<ShopperContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<Cart>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<CartContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            #endregion BsonMaps

            _context = new OrderCaptureContext(_mongoDb);
        }

        public async Task<Order> CreateOrder(OrderContent order)
        {
            OrderContent content = new OrderContent
            {
                PlatformIdentifier = _platformIdentifier,
                Owner = new Owner { Collection = order.Owner.Collection, Identifier = order.Owner.Identifier },
                IsPayingWithTerms = order.IsPayingWithTerms,
                WebOrderNumber = order.WebOrderNumber,
                ExternalIdentifier = order.ExternalIdentifier
            };

            Order newEntity = new Order
            {
                Contents = content
            };

            var entity = await _context.Orders.AddAsync(newEntity);
            return entity;
        }

        public async Task<Shopper> CreateShopper(ShopperContent shopper)
        {
            ShopperContent content = new ShopperContent
            {
                PlatformIdentifier = _platformIdentifier,
                UserIdentifier = shopper.UserIdentifier
            };

            Shopper newEntity = new Shopper
            {
                Contents = content
            };

            var entity = await _context.Shoppers.AddAsync(newEntity);
            return entity;
        }

        public async Task<Cart> CreateCart(CartContent cart)
        {
            CartContent content = new CartContent
            {
                PlatformIdentifier = _platformIdentifier,
                CartItems = cart.CartItems,
                ShopperIdentifier = cart.ShopperIdentifier,
                StoreIdentifier = cart.StoreIdentifier,
                //TODO: See if we calculate the tax or just map it.
                Tax = cart.Tax,
            };

            Cart newEntity = new Cart
            {
                Contents = content
            };

            var entity = await _context.Carts.AddAsync(newEntity);
            return entity;
        }
    }
}