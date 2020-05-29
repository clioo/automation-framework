using HttpUtiityTests.CredentialManager;
using System.Collections.Generic;
using System.Configuration;

namespace FMPOnlineTests.Constants
{
    static class EnvironmentConstants
    {
        //tenant
        static public readonly string WebAppUrl = ConfigurationManager.AppSettings.Get("WebAppUrl");
        static public readonly string PlatformId = ConfigurationManager.AppSettings.Get("PlatformId");
        static public readonly string PlatformExternalId = ConfigurationManager.AppSettings.Get("PlatformExternalId");

        //apis
        static public readonly string IntegrationsApiUrl = ConfigurationManager.AppSettings.Get("IntegrationsAPIUrl");
        static public readonly string ShippingServiceApiUrl = ConfigurationManager.AppSettings.Get("ShippingServiceAPIUrl");

        //browser settings
        static public readonly string IsBrowserHeadless = ConfigurationManager.AppSettings.Get("IsBrowserHeadless");

        //Deprecated
        static public readonly string SiteUrl = ConfigurationManager.AppSettings["SiteUrl"];
        static public readonly string MongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
        static public readonly string Browser = ConfigurationManager.AppSettings["Browser"].ToLower();
        static public readonly string IntegrationsApiV1Url = ConfigurationManager.AppSettings["IntegrationsAPIUrl"] + "/api/V1";
        static public readonly string IntegrationsApiV2Url = ConfigurationManager.AppSettings["IntegrationsAPIUrl"] + "/api/V2";
        static public readonly string CustomerServiceUrl = ConfigurationManager.AppSettings["CustomerServiceUrl"];
        public const string PlatformIdentifier = "b6018f81-571a-4116-b416-4368c323fdaa";
        static private IDictionary<string, string> User = CredentialRetriever.GetCredentials();
        static public readonly string Username = User["username"];
        static public readonly string UserPassword = User["password"];
    }
}