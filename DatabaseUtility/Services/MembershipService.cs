using DatabaseUtility.Models;
using DatabaseUtility.Mongo.Contexts;
using MongoDB.Bson.Serialization;
using System;
using System.Threading.Tasks;

namespace DatabaseUtility.Services
{
    public class MembershipService : ServiceBase
    {
        private MembershipContext _context { get; set; }

        public MembershipService(string connectionString, string database, Guid platformIdentifier) : base(connectionString, database, platformIdentifier)
        {
            #region BsonMaps

            BsonClassMapExtended.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<UserContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<Account>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<AccountContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<AccountMaster>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<AccountMasterContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<Contact>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<ContactContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<Address>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<AddressContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<Owner>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            #endregion BsonMaps

            _context = new MembershipContext(_mongoDb);
        }

        public async Task<AccountMaster> CreateAccountMaster(string name, bool hasPaymentTerms, string externalIdentifier = "5372226")
        {
            AccountMasterContent content = new AccountMasterContent();
            content.ExternalIdentifier = externalIdentifier;
            content.Name = name;
            content.TermsConfiguration.HasPaymentTerms = hasPaymentTerms;
            content.PlatformIdentifier = _platformIdentifier;

            AccountMaster newEntity = new AccountMaster
            {
                Contents = content
            };

            var entity = await _context.AccountMaster.AddAsync(newEntity);
            return entity;
        }

        public async Task<AccountMaster> GetAccountMaster(Guid accountMasterId)
        {
            var entity = await _context.AccountMaster.FindAsync(accountMasterId);
            return entity;
        }

        public async Task<Account> CreateAccount(Guid accountMasterId)
        {
            AccountContent content = new AccountContent(accountMasterId);

            Account newEntity = new Account
            {
                Contents = content
            };

            var entity = await _context.Account.AddAsync(newEntity);
            return entity;
        }

        public async Task<Account> GetAccount(Guid accountId)
        {
            var entity = await _context.Account.FindAsync(accountId);
            return entity;
        }

        public async Task<Contact> CreateContact(Guid accountId, string firstName, string lastName, string phoneNumber, string email)
        {
            ContactContent content = new ContactContent(accountId);
            content.FirstName = firstName;
            content.LastName = lastName;
            content.PhoneNumber = phoneNumber;
            content.ContactEmail = email;

            Contact newEntity = new Contact
            {
                Contents = content
            };

            var entity = await _context.Contact.AddAsync(newEntity);
            return entity;
        }

        public async Task<Contact> GetContact(Guid accountId)
        {
            return await _context.Contact.FindAsync(accountId);
        }

        public async Task<User> CreateUser(Guid accountId, Guid loginId, Guid contactId)
        {
            UserContent content = new UserContent(loginId, accountId, contactId);
            content.PlatformIdentifier = _platformIdentifier;

            User newEntity = new User
            {
                Contents = content
            };

            var entity = await _context.Users.AddAsync(newEntity);
            return entity;
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _context.Users.UpdateAsync(user);
        }

        public async Task<Address> CreateAddress(AddressContent address)
        {
            AddressContent content = new AddressContent
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                Name = address.Name,
                City = address.City,
                StateProvinceRegion = address.StateProvinceRegion,
                Country = address.Country,
                Postal = address.Postal,
                PlatformIdentifier = _platformIdentifier,
                IsInternational = address.IsInternational,
                Owner = new Owner { Collection = address.Owner.Collection, Identifier = address.Owner.Identifier }
            };

            Address newEntity = new Address
            {
                Contents = content
            };

            var entity = await _context.Address.AddAsync(newEntity);
            return entity;
        }

        public async Task<CardToken> CreateCardToken(CardTokenContent cardToken)
        {
            CardTokenContent content = new CardTokenContent(cardToken.TokenId)
            {
                Owner = new Owner { Collection = cardToken.Owner.Collection, Identifier = cardToken.Owner.Identifier },
                Address = cardToken.Address,
                CardType = cardToken.CardType,
                CustomerId = cardToken.CustomerId,
                Email = cardToken.Email,
                ExpirationMonth = cardToken.ExpirationMonth,
                ExpirationYear = cardToken.ExpirationYear,
                IsReadonly = cardToken.IsReadonly,
                LastFourDigits = cardToken.LastFourDigits,
                NameOnCard = cardToken.NameOnCard,
                PlatformIdentifier = _platformIdentifier
            };

            CardToken newEntity = new CardToken
            {
                Contents = content
            };

            var entity = await _context.CardToken.AddAsync(newEntity);
            return entity;
        }
    }
}