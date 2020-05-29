using DatabaseUtility.Constants;
using System;

namespace DatabaseUtility.Services
{
    public static class ServiceGenerator<TEntity>
    {
        public static TEntity GetInstance(string database, string connectionString)
        {
            string dataBaseName = database;
            string platformId = ConfigurationConstants.QAPlatformIdentifier;

            //TODO:
            //temporal fix

            //database name will have _TEST
            if (connectionString != ConfigurationConstants.ConnectionStringLocal || connectionString != ConfigurationConstants.ConnectionStringChrisCO)
            {
                dataBaseName += ConfigurationConstants.DevCollectionString;
            }

            //platform id will be different on dev local
            if(connectionString == ConfigurationConstants.ConnectionStringLocal)
            {
                platformId = ConfigurationConstants.LocalPlatformIdentifier;
            }

            return (TEntity)Activator.CreateInstance(
                typeof(TEntity), connectionString,
                dataBaseName,
                new Guid(ConfigurationConstants.LocalPlatformIdentifier));
        }
    }
}