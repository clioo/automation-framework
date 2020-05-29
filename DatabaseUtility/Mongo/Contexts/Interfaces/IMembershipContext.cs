using DatabaseUtility.Models;

namespace DatabaseUtility.Mongo.Contexts.Interfaces
{
    internal interface IMembershipContext : IMongoContext
    {
        IMongoRepository<User, UserContent> Users { get; }
        IMongoRepository<Account, AccountContent> Account { get; }
        IMongoRepository<AccountMaster, AccountMasterContent> AccountMaster { get; }
        IMongoRepository<Contact, ContactContent> Contact { get; }
    }
}