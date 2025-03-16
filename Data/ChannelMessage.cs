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
        public MongoImageArray? ImageArray { get; set; }
        [BsonElement("sender_id")]
        public string? SenderID { get; set; }
        [BsonElement("sender_name")]
        public string? SenderName { get; set; } = "";
        [BsonElement("timestamp")]
        public DateTime? Timestamp { get; set; } = DateTime.Now;
    }
}
