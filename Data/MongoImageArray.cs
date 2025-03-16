using MongoDB.Bson.Serialization.Attributes;

namespace BuildMasterPro.Data
{
    public class MongoImageArray
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? ImageArrayId { get; set; }
        [BsonElement("images")]
        public List<string>? Images { get; set; }
    }
}
