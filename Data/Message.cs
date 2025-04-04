﻿using MongoDB.Bson.Serialization.Attributes;

namespace BuildMasterPro.Data
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? MessageId { get; set; }
        [BsonElement("sender_id")]
        public string? SenderID { get; set; }
        [BsonElement("sender_picture")]
        public string? SenderPicture { get; set; }
        [BsonElement("sender_name")]
        public string? SenderName { get; set; } = "";
        [BsonElement("receiver_id")]
        public string? ReceiverID { get; set; }
        [BsonElement("receiver_picture")]
        public string? ReceiverPicture { get; set; }
        [BsonElement("receiver_name")]
        public string? ReceiverName { get; set; } = "";
        [BsonElement("message_text")]
        public string? MessageText { get; set; }
        [BsonElement("images")]
        public List<string>? Images { get; set; } = new();
        [BsonElement("files")]
        public List<string>? Files { get; set; } = new();
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        [BsonElement("timestamp")]
        public DateTime? Timestamp { get; set; } = DateTime.Now;
        [BsonElement("is_archived")]
        public bool? IsArchived { get; set; } = false;
        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; } = false;
    }
}
