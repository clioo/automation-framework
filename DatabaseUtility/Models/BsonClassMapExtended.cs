using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtility.Models
{
    public static class BsonClassMapExtended 
    {
        public static void RegisterClassMap<TClass>(Action<BsonClassMap<TClass>> classMapInitializer)
        {
            bool isRegister = BsonClassMap.IsClassMapRegistered(typeof(TClass));
            if (!isRegister)
            {
                BsonClassMap.RegisterClassMap<TClass>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
