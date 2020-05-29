using System.Configuration;

namespace CommonHelper
{
    public static class SeleniumConstants
    {
        public static int defaultWaitTime = 20;
        public static string UrlHomePage = ConfigurationManager.AppSettings["SiteUrl"];
        public static string Browser = ConfigurationManager.AppSettings["Browser"];
    }
}