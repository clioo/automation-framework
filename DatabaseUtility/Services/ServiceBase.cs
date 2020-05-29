using MongoDB.Driver;
using System;

namespace DatabaseUtility.Services
{
    public abstract class ServiceBase
    {
        protected MongoClient _mongoClient { get; set; }
        public IMongoDatabase _mongoDb { get; set; }
        public Guid _platformIdentifier { get; set; }

        public ServiceBase(string connectionString, string database, Guid platformIdentifier)
        {
            _mongoClient = new MongoClient(connectionString);
            _mongoDb = _mongoClient.GetDatabase(database);
            _platformIdentifier = platformIdentifier;
        }
    }
}