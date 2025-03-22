using BuildMasterPro.Data;
using MongoDB.Driver;
using System.Collections.Concurrent;

namespace BuildMasterPro.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _messageCollection;
        private readonly ConcurrentBag<Action<Message>> _subscribers = new();

        public MessageService(MongoService mongoService)
        {
            _messageCollection = mongoService.GetCollection<Message>("Messages");
            StartListeningForChanges();
        }

        public async Task<List<Message>> GetLatestMessagesForUser(ApplicationUser user)
        {

            // Aggregation pipeline using the fluent syntax
            var messages = await _messageCollection.Aggregate()
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

            return messages.OrderByDescending(m => m.Timestamp).ToList();
        }

        public async Task<List<Message>> GetAllMessagesForUsers(ApplicationUser user, ApplicationUser user2)
        {
            var matchStage = Builders<Message>.Filter.Or(
                        Builders<Message>.Filter.And(
                            Builders<Message>.Filter.Eq(m => m.SenderID, user.Id),
                            Builders<Message>.Filter.Eq(m => m.ReceiverID, user2!.Id)
                            ),
                        Builders<Message>.Filter.And(
                            Builders<Message>.Filter.Eq(m => m.SenderID, user2.Id),
                            Builders<Message>.Filter.Eq(m => m.ReceiverID, user.Id)
                            )
                    );
            // Aggregation pipeline using the fluent syntax
            var messages = await _messageCollection.Aggregate()
                // Match messages where the user is either the sender or the receiver
                .Match(matchStage)
                .ToListAsync();

            return messages.OrderBy(m => m.Timestamp).ToList(); ;
        }

        public async Task SendMessageAsync(Message message)
        {
            await _messageCollection.InsertOneAsync(message);
        }

        public void SubscribeToMessages(Action<Message> callback)
        {
            _subscribers.Add(callback);
        }

        private void StartListeningForChanges()
        {
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Message>>()
                .Match("{ operationType: 'insert' }");

            var cursor = _messageCollection.Watch(pipeline);

            Task.Run(async () =>
            {
                await cursor.ForEachAsync(change =>
                {
                    var newMessage = change.FullDocument;
                    foreach (var subscriber in _subscribers)
                    {
                        subscriber.Invoke(newMessage);
                    }
                });
            });
        }
    }
}
