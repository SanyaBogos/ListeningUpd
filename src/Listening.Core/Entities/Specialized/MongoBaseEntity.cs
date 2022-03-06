using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Listening.Server.Entities.Specialized
{
    public class MongoBaseEntity : LogInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
