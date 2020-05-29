using DatabaseUtility.Constants;
using DatabaseUtility.Models;
using DatabaseUtility.Mongo.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace DatabaseUtility.Test
{
    [TestClass]
    public class LoginTests : MongoTestBase
    {
        private ConfigurationContext _context { get; set; }

        public LoginTests() : base()
        {
            BsonClassMap.RegisterClassMap<Login>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<LoginContent>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            connectionString = ConfigurationConstants.ConnectionStringQA;
            database = ConfigurationConstants.ConfigurationDatabase + ConfigurationConstants.QACollectionString;
            _mongoClient = new MongoClient(connectionString);
            _mongoDb = _mongoClient.GetDatabase(database);
            _context = new ConfigurationContext(_mongoDb);
        }

        [TestMethod]
        public void PingDB()
        {
            Assert.IsTrue(_context.PingDb());
        }

        [TestMethod]
        public void GetAll()
        {
            var allLogins = _context.Logins.AllAsync().Result;
            Assert.IsTrue(allLogins.Count > -1);
        }

        [TestMethod]
        public void GetAllTestTundra()
        {
            var allLogins = _context.Logins.AllAsync(l => l.Contents.Email == "test@etundra.com").Result;
            Assert.IsTrue(allLogins.Count > -1);
        }

        [TestMethod]
        public void GetAllTestTundraById()
        {
            var login = _context.Logins.FindAsync(new Guid("ae1566cf-7c3d-4981-895f-c5d0ecfd61ce")).Result;
            Assert.IsNotNull(login);
        }

        [TestMethod]
        public void Create()
        {
            LoginContent loginContent = new LoginContent
            {
                Email = "Jhonnatan2@Guerrero.com",
                Identifier = Guid.NewGuid(),
            };
            Login newLogin = new Login
            {
                Contents = loginContent
            };
            var login = _context.Logins.AddAsync(newLogin).Result;
            Assert.IsNotNull(login);
        }
    }
}