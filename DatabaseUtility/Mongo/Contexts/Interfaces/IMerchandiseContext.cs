using DatabaseUtility.Models;

namespace DatabaseUtility.Mongo.Contexts.Interfaces
{
    public interface IMerchandiseContext
    {
        IMongoRepository<Product, ProductContent> Products { get; }
    }
}