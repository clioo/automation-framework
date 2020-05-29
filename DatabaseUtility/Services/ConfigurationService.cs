using DatabaseUtility.Models;
using DatabaseUtility.Mongo.Contexts;
using MongoDB.Bson.Serialization;
using System;
using System.Threading.Tasks;

namespace DatabaseUtility.Services
{
    public class ConfigurationService : ServiceBase
    {
        private ConfigurationContext _context { get; set; }

        public ConfigurationService(string connectionString, string database, Guid platformIdentifier) : base(connectionString, database, platformIdentifier)
        {
            BsonClassMapExtended.RegisterClassMap<Login>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMapExtended.RegisterClassMap<LoginContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            _context = new ConfigurationContext(_mongoDb);
        }

        public async Task<Login> CreateLogin(string email)
        {
            int i = email.LastIndexOf('@');
            LoginContent loginContent = new LoginContent();
            loginContent.Email = email;
            loginContent.Username = email.Substring(0, i);
            loginContent.PlatformIdentifier = _platformIdentifier;

            Login newLogin = new Login
            {
                Contents = loginContent
            };
            var login = await _context.Logins.AddAsync(newLogin);
            return login;
        }

        public async Task ClearLoginsByEmail(string email)
        {
            var logins = await _context.Logins.AllAsync(l => l.Contents.Email == email);
            foreach (var login in logins)
            {
                await _context.Logins.DeleteAsync(login.Contents.Identifier);
            }
        }
    }
}