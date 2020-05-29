using DatabaseUtility.Constants;
using DatabaseUtility.Services;

namespace DatabaseUtility.Test
{
    public abstract class ServiceTestBase<TEntity>
    {
        public string env;
        public TEntity service;

        public ServiceTestBase(string database)
        {
            env = ConfigurationConstants.ConnectionStringQA;
            service = ServiceGenerator<TEntity>.GetInstance(database, env);
        }
    }
}