using System;

namespace HttpUtility.Services.AutomationDataFactory.Utils
{
    static public class ApiUrlHelper
    {
        static public bool UrlContainsHttps(string url)
        {
            return url.Contains("https");
        }

        static public string GetRequesterFormatUrl(string url)
        {
            if (url != null)
            {
                if (url.Contains("https")) return url.Replace("https://", "");
                if (url.Contains("http")) return url.Replace("http://", "");
            }
            return url;
        }

        static public void ValidateUrl(string url, string name)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
                throw new ArgumentException($"{name} has an invalid url value: {url}");
        }
    }
}
