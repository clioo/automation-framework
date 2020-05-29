using DatabaseUtility.Models;
using DatabaseUtility.Mongo.Contexts.Interfaces;
using MongoDB.Driver;

namespace DatabaseUtility.Mongo.Contexts
{
    public class ConfigurationContext : MongoContext, IConfigurationContext
    {
        public IMongoRepository<Login, LoginContent> _logins;

        public ConfigurationContext(IMongoDatabase database) : base(database)
        {
        }

        public IMongoRepository<Login, LoginContent> Logins => _logins ?? (_logins = new MongoRepository<Login, LoginContent>(_mongoDatabase, nameof(Login)));
    }
}