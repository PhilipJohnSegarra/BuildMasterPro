using MongoDB.Bson.Serialization.Attributes;

namespace BuildMasterPro.Data
{
    public class TaskActivityImages
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? TaskActivityImagesId { get; set; }
        [BsonElement("taskactivity_id")]
        public int? TaskActivityID { get; set; }
        [BsonElement("task_id")]
        public int? TaskID { get; set; }
        [BsonElement("images_array")]
        public List<string>? ImageArray { get; set; } = new();

    }
}
