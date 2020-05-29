using HttpUtiityTests.CredentialManager;
using System.Collections.Generic;
using System.Configuration;

namespace HttpUtiityTests.EnvConstants
{
    static public class ServiceConstants
    {
        static private IDictionary<string, string> User = CredentialRetriever.GetCredentials();
        static public string Username = User["username"];
        static public string Password = User["password"];
        static public string CustomerServiceUrl = ConfigurationManager.AppSettings["customerServiceUrl"];
        static public string FMPPointsPlatformExtId = ConfigurationManager.AppSettings["fmpPlatformExtId"];
        static public string FmpPlatformExtId = ConfigurationManager.AppSettings["fmpPlatformExtId"];
        static public string FMPUrl = ConfigurationManager.AppSettings["fmpUrl"];
        static public string FMPPlatformId = "b6018f81-571a-4116-b416-4368c323fdaa";
        static public string FMPPlatformName = "FMP";
        static public string AllPointsPlatformExtId = ConfigurationManager.AppSettings["allPointsPlatformExtId"];
        static public string AllPointsUrl = ConfigurationManager.AppSettings["allpointsUrl"];
        static public string AllPointsPlatformId = "55c4524c-5e8f-4b81-adf1-a2f1d38677ff";
        static public string AllPointsPlatformName = "AllPoints";
        static public string IntegrationsAPIUrl = ConfigurationManager.AppSettings["integrationsAPIUrl"];
        static public string ShippingServiceApiUrl = ConfigurationManager.AppSettings["shippingServiceApiUrl"] + "/api";
        static public string AuthName = ConfigurationManager.AppSettings.Get("authName");
        static public string AuthPassword = ConfigurationManager.AppSettings.Get("authPassword");
        static public string AuthPublicKey = ConfigurationManager.AppSettings.Get("authPublicKey");
        static public string MongoConnectionString = ConfigurationManager.AppSettings.Get("MongoConnectionString");
    }
}

