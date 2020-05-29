using System;

namespace DatabaseUtility.Mongo
{
    public interface IMongoContext : IDisposable
    {
        bool PingDb();
    }
}