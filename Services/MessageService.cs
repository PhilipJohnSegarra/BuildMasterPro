using BuildMasterPro.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BuildMasterPro.Services
{
    public class MessageService
    {
        MongoService _MongoService;
        public List<Message> Messages { get; set; } = new();
        public MessageService(MongoService mongoService)
        {
            _MongoService = mongoService;
        }

        public async Task<List<Message>> GetLatestMessagesForUser(ApplicationUser user)
        {
            var messageCollection = _MongoService.GetCollection<Message>("Messages");

            // Aggregation pipeline using the fluent syntax
            var messages = await messageCollection.Aggregate()
                // Match messages where the user is either the sender or the receiver
                .Match(Builders<Message>.Filter.Or(
                    Builders<Message>.Filter.Eq(m => m.SenderID, user.Id),
                    Builders<Message>.Filter.Eq(m => m.ReceiverID, user.Id)
                ))
                // Add a field to identify the correspondent (the other user in the conversation)
                .Project(m => new
                {
                    m,
                    Correspondent = m.SenderID == user.Id ? m.ReceiverID : m.SenderID
                })
                // Group by correspondent and get the latest message
                .Group(x => x.Correspondent, g => new
                {
                    Correspondent = g.Key,
                    LatestMessage = g.OrderByDescending(m => m.m.Timestamp).FirstOrDefault()
                })
                // Select only the latest messages
                .Project(g => g.LatestMessage.m)
                .ToListAsync();

            this.Messages = messages.OrderByDescending(m => m.Timestamp).ToList();
            return this.Messages;
        }
        public async Task<List<Message>> GetAllMessagesForUsers(ApplicationUser user, ApplicationUser user2)
        {
            var messageCollection = _MongoService.GetCollection<Message>("Messages");
            var matchStage = Builders<Message>.Filter.Or(
                        Builders<Message>.Filter.And(
                            Builders<Message>.Filter.Eq(m => m.SenderID, user.Id),
                            Builders<Message>.Filter.Eq(m => m.ReceiverID, user2.Id)
                            ),
                        Builders<Message>.Filter.And(
                            Builders<Message>.Filter.Eq(m => m.SenderID, user2.Id),
                            Builders<Message>.Filter.Eq(m => m.ReceiverID, user.Id)
                            )
                    );
            // Aggregation pipeline using the fluent syntax
            var messages = await messageCollection.Aggregate()
                // Match messages where the user is either the sender or the receiver
                .Match(matchStage)
                .ToListAsync();

            return messages.OrderBy(m => m.Timestamp).ToList(); ;
        }
    }
}
