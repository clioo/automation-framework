using DatabaseUtility.Constants;
using DatabaseUtility.Models;
using DatabaseUtility.Mongo;
using DatabaseUtility.Mongo.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace DatabaseUtility.Test
{
    [TestClass]
    public class MongoTests : MongoTestBase
    {
        public MongoTests() : base()
        {
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
            connectionString = ConfigurationConstants.ConnectionStringQA;
            database = ConfigurationConstants.ConfigurationDatabase + ConfigurationConstants.QACollectionString;
            _mongoClient = new MongoClient(connectionString);
        }

        [TestMethod]
        public void GetPublishedForPlatform()
        {
            MerchandiseContext _context;
            Guid platformIdentifier = new Guid(ConfigurationConstants.QAPlatformIdentifier);
            _context = PublishedContext<MerchandiseContext, Product, ProductContent>.GetPublishedDatabase("merchandise", _mongoClient, platformIdentifier);
            Assert.IsNotNull(_context);
        }
    }
}