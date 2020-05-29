using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DatabaseUtility.Models
{
    public abstract class ContentBase
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public Guid Identifier { get; set; }
        [BsonIgnoreIfNull]
        public Guid? PlatformIdentifier { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public int Version { get; set; }
        public int VersionStatus { get; set; }
    }
}