using DatabaseUtility.Models;
using DatabaseUtility.Mongo.Contexts.Interfaces;
using MongoDB.Driver;

namespace DatabaseUtility.Mongo.Contexts
{
    public class MembershipContext : MongoContext, IMembershipContext
    {
        public IMongoRepository<User, UserContent> _users;
        public IMongoRepository<Account, AccountContent> _accounts;
        public IMongoRepository<AccountMaster, AccountMasterContent> _accountsMaster;
        public IMongoRepository<Contact, ContactContent> _contactsInformation;
        public IMongoRepository<Address, AddressContent> _addresses;
        public IMongoRepository<CardToken, CardTokenContent> _cardTokens;

        public MembershipContext(IMongoDatabase database) : base(database)
        {
        }

        public IMongoRepository<User, UserContent> Users => _users ?? (_users = new MongoRepository<User, UserContent>(_mongoDatabase, nameof(User)));
        public IMongoRepository<Account, AccountContent> Account => _accounts ?? (_accounts = new MongoRepository<Account, AccountContent>(_mongoDatabase, nameof(Account)));
        public IMongoRepository<AccountMaster, AccountMasterContent> AccountMaster => _accountsMaster ?? (_accountsMaster = new MongoRepository<AccountMaster, AccountMasterContent>(_mongoDatabase, nameof(AccountMaster)));
        public IMongoRepository<Contact, ContactContent> Contact => _contactsInformation ?? (_contactsInformation = new MongoRepository<Contact, ContactContent>(_mongoDatabase, nameof(Contact)));
        public IMongoRepository<Address, AddressContent> Address => _addresses ?? (_addresses = new MongoRepository<Address, AddressContent>(_mongoDatabase, nameof(Address) + "e"));
        public IMongoRepository<CardToken, CardTokenContent> CardToken => _cardTokens ?? (_cardTokens = new MongoRepository<CardToken, CardTokenContent>(_mongoDatabase, nameof(CardToken)));
    }
}