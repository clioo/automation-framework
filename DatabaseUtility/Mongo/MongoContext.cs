using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace DatabaseUtility.Mongo
{
    public abstract class MongoContext : IMongoContext
    {
        protected IMongoDatabase _mongoDatabase;

        public MongoContext(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool PingDb()
        {
            bool pinged = false;
            try
            {
                _mongoDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
                pinged = true;
            }
            catch
            {
            }
            return pinged;
        }
    }
}