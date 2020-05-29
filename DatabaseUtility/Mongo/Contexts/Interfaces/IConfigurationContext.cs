using DatabaseUtility.Models;

namespace DatabaseUtility.Mongo.Contexts.Interfaces
{
    public interface IConfigurationContext : IMongoContext
    {
        IMongoRepository<Login, LoginContent> Logins { get; }
    }
}