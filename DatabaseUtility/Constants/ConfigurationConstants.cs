namespace DatabaseUtility.Constants
{
    public static class ConfigurationConstants
    {
        public static string ConnectionStringLocal = "mongodb://localhost:27017";
        public static string ConnectionStringQA = "mongodb://co-svr-devapp:27017";
        public static string ConnectionStringDev = "mongodb://www.eovportal-softtek.com:27017";
        public static string ConnectionStringChrisCO = "mongodb://christhian.westus2.cloudapp.azure.com:27017";

        public static string QACollectionString = "_TEST";
        public static string DevCollectionString = "_TEST";

        public static string ConfigurationDatabase = "Configuration";
        public static string MembershipDatabase = "Membership";
        public static string OrderCaptureDatabase = "OrderCapture";
        public static string MerchandiseDatabase = "Merchandise";

        public static string DefaultPasswordHash = "57792B0A005FDF9BBA10DF429E06DD01";
        public static string DefaultPasswordSalt = "TEyzSLg=";

        public static string QAPlatformIdentifier = "55c4524c-5e8f-4b81-adf1-a2f1d38677ff";
        public static string LocalPlatformIdentifier = "7de3c23c-dbc0-4b84-a06e-41b127534eb0";
        public static string DevPlatformIdentifier = "55c4524c-5e8f-4b81-adf1-a2f1d38677ff";
        public static string ChrisCOPlatformIdentifier = "55c4524c-5e8f-4b81-adf1-a2f1d38677ff";
    }
}