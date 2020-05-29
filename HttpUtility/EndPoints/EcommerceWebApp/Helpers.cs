using Newtonsoft.Json;
using System;

namespace HttpUtility.EndPoints.EcommerceWebApp
{
    public static class Helpers
    {
        public static HttpResponse TryParseEntity<T>(this HttpResponse response)
        {
            var readData = response.Entity;
            if (readData is T)
            {
                response.Entity = (T)readData;
                return response;
            }
            try
            {
                var serializedEntity = JsonConvert.SerializeObject(response.Entity);
                var entity = JsonConvert.DeserializeObject<T>(serializedEntity);
                response.Entity = entity;
                return response;
            }
            catch (Exception e)
            {
                throw new InvalidCastException(e.Message, e.InnerException);
            }
        }
    }
}