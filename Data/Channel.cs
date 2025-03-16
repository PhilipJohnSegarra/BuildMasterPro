using MongoDB.Bson.Serialization.Attributes;

namespace BuildMasterPro.Data
{
    public class Channel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? ChannelId { get; set; }
        [BsonElement("channel_name")]
        public string? ChannelName { get; set; }
        [BsonElement("channel_desc")]
        public string? ChannelDescription { get; set; }
        [BsonElement("channel_members")]
        public List<string>? ChannelMemberIds { get; set; }
        [BsonElement("channel_messages")]
        public List<ChannelMessage>? ChannelMessages { get; set; }
        [BsonElement("channel_category")]
        public string? Category { get; set; }
        [BsonElement("channel_category")]
        public int? ProjectId { get; set; }

    }
}
