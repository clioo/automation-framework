using MongoDB.Driver;

namespace DatabaseUtility.Test
{
    public abstract class MongoTestBase
    {
        protected MongoClient _mongoClient { get; set; }
        public IMongoDatabase _mongoDb { get; set; }
        public string connectionString { get; set; }
        public string database { get; set; }
    }
}