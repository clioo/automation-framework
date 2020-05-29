using DatabaseUtility.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DatabaseUtility.Mongo
{
    public static class PublishedContext<TEntity, TModel, TContents>
        where TModel : EntityBase<TContents>
        where TContents : ContentBase
    {
        public static TEntity GetPublishedDatabase(string database, MongoClient client, Guid platformIdentifier)
        {
            List<string> dbs = new List<string>();
            using (IAsyncCursor<BsonDocument> cursor = client.ListDatabases())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        dbs.Add(doc["name"].AsString); // database name
                    }
                }
            }
            dbs = dbs.Where(d => d.Contains(database)).ToList();
            foreach (var db in dbs)
            {
                var mongoDb = client.GetDatabase(db);
                TEntity context = (TEntity)Activator.CreateInstance(typeof(TEntity), mongoDb);
                TModel model = (TModel)Activator.CreateInstance(typeof(TModel));
                Type type = context.GetType();
                string modelTypeName = model.GetType().Name;
                string propertyName = type.GetProperties().FirstOrDefault(m => m.Name.Contains(modelTypeName)).Name;
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                object collectionObj = propertyInfo.GetValue(context);
                IMongoRepository<TModel, TContents> collection = (IMongoRepository<TModel, TContents>)collectionObj;
                var product = collection.AllAsync(p => p.Contents.PlatformIdentifier == platformIdentifier).Result;
                if (product.Count > 0)
                {
                    return context;
                }
            }
            return default(TEntity);
        }
    }
}