using DatabaseUtility;
using DatabaseUtility.API;
using System.Configuration;

namespace AllPoints.Features.BaseTest
{
    public abstract class DataFactoryBase
    {
        protected ECommerceProcessor processor;

        public DataFactoryBase()
        {
            string envString = ConfigurationManager.AppSettings["SiteUrl"];
            string mongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];

            processor = new ECommerceProcessor(mongoConnectionString);
        }
    }
}