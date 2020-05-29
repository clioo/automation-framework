using HttpUtiityTests.CredentialManager;
using System.Collections.Generic;
using System.Configuration;

namespace AllPoints.Features.BaseTest.Constants
{
    static class EnvironmentConstants
    {
        //platform
        static public readonly string AllPointsPlatformName = ConfigurationManager.AppSettings.Get("AllPointsPlatformName");
        static public readonly string AllPointsPlatformId = ConfigurationManager.AppSettings.Get("AllPointsPlatformId");
        static public readonly string AllPointsPlatformExtId = ConfigurationManager.AppSettings.Get("AllPointsExternalId");

        static public readonly string AllPointsWebAppUrl = ConfigurationManager.AppSettings.Get("AllPointsUrl");

        //apis
        static public readonly string IntegrationsApiUrl = ConfigurationManager.AppSettings.Get("IntegrationsAPIUrl");
        static public readonly string ShippingServiceApiUrl = ConfigurationManager.AppSettings.Get("ShippingServiceApiUrl");

        //TODO
        //remove these props
        static public readonly string FmpPlatformName = ConfigurationManager.AppSettings.Get("FmpPlatformName");
        static public readonly string FmpPlatformId = ConfigurationManager.AppSettings.Get("FmpPlatformId");
        static public readonly string FmpPlatformExtId = ConfigurationManager.AppSettings.Get("FmpExternalId");
        static public readonly string FmpWebAppUrl = ConfigurationManager.AppSettings.Get("FmpUrl");

        //browser settings
        static public readonly string HeadlessBrowser = ConfigurationManager.AppSettings.Get("IsBrowserHeadless");
        static public readonly string Browser = ConfigurationManager.AppSettings.Get("Browser");

        //Deprecated
        //this is temporally here
        public const string Chris = "http://christhian.westus2.cloudapp.azure.com";
        public const string QASofttek = "http://www.eovportal-softtek-qa.com";
        //public const string SofttekDemo = "http://www.eovportal-softtek-demo.com";
        public const string DevSandbox = "http://www.eovportal-softtek.com";
        // ->
        static public readonly string HostUrl = ConfigurationManager.AppSettings["SiteUrl"];
        static public readonly string MongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
        static public readonly string CustomerServiceUrl = ConfigurationManager.AppSettings["CustomerServiceUrl"];
        public const string PlatformIdentifier = "55c4524c-5e8f-4b81-adf1-a2f1d38677ff";
        public const string PlatformName = "AllPoints";
        static public readonly string PlatformExternalId = HostUrl == "http://www.eovportal-softtek.com" ? "AllPoints" : "AllPointsUAT";
        static private IDictionary<string, string> User = CredentialRetriever.GetCredentials();
        static public readonly string Username = User["username"];
        static public readonly string UserPassword = User["password"];
    }
}
