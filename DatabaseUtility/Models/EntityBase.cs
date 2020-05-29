using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DatabaseUtility.Models
{
    public abstract class EntityBase<TEntity>
        where TEntity : ContentBase
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public TEntity Contents { get; set; }
    }
}