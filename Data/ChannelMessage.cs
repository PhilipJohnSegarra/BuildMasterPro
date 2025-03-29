using MongoDB.Bson.Serialization.Attributes;

namespace BuildMasterPro.Data
{
    public class ChannelMessage
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? ChannelMessageId { get; set; }
        [BsonElement("channel_message")]
        public string? Message { get; set; }
        [BsonElement("image_array")]
        public List<string>? ImageArray { get; set; } = new();
        [BsonElement("files")]
        public List<string>? Files { get; set; } = new();
        [BsonElement("sender_id")]
        public string? SenderID { get; set; }
        [BsonElement("sender_name")]
        public string? SenderName { get; set; } = "";
        [BsonElement("sender_image")]
        public string? SenderImage { get; set; } = "";
        [BsonElement("timestamp")]
        public DateTime? Timestamp { get; set; } = DateTime.Now;
    }
}
