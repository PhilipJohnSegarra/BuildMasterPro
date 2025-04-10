using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Concurrent;
using BuildMasterPro.Data;

namespace BuildMasterPro.Services
{
    public class ChannelService : BackgroundService
    {
        private readonly MongoService _mongoService;
        private readonly IMongoCollection<Channel> _channels;
        private readonly ConcurrentBag<Action<Channel>> _subscribers = new();
        public event Action<Channel>? OnChannelUpdated;

        public ChannelService(MongoService mongoService)
        {
            _mongoService = mongoService;
            _channels = _mongoService.GetCollection<Channel>("Channels");
        }

        public async Task<List<Channel>> GetProjChannelsAsync(int projectId)
        {
            var filter = Builders<Channel>.Filter.Eq(i => i.ProjectId, projectId);
            return await _channels.Find(filter).ToListAsync();
        }

        public async Task AddNewChannelAsync(Channel newChannel)
        {
            await _channels.InsertOneAsync(newChannel);
        }

        public async Task AddMessageAsync(string channelId, ChannelMessage message)
        {
            var filter = Builders<Channel>.Filter.Eq(c => c.ChannelId, channelId);

            // Ensure 'channel_messages' is an array if it's null
            var check = await _channels.Find(filter).FirstOrDefaultAsync();
            if (check != null && check.ChannelMessages == null)
            {
                var initUpdate = Builders<Channel>.Update.Set(c => c.ChannelMessages, new List<ChannelMessage>());
                await _channels.UpdateOneAsync(filter, initUpdate);
            }

            // Push the new message
            var pushUpdate = Builders<Channel>.Update.Push(c => c.ChannelMessages, message);
            await _channels.UpdateOneAsync(filter, pushUpdate);
        }

        public async Task<bool> DeleteChannelByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var result = await _channels.DeleteOneAsync(Builders<Channel>.Filter.Eq("_id", objectId));
            return result.DeletedCount > 0;
        }

        public async Task<Channel?> GetChannelByIdAsync(string channelId)
        {
            var filter = Builders<Channel>.Filter.Eq(c => c.ChannelId, channelId);
            return await _channels.Find(filter).FirstOrDefaultAsync();
        }

        public void SubscribeToMessages(Action<Channel> callback)
        {
            _subscribers.Add(callback);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Channel>>()
        .Match(Builders<ChangeStreamDocument<Channel>>.Filter.In(
            cs => cs.OperationType,
            new[] { ChangeStreamOperationType.Update, ChangeStreamOperationType.Insert }
        ));

            using var cursor = await _channels.WatchAsync(pipeline, cancellationToken: stoppingToken);

            while (!stoppingToken.IsCancellationRequested && await cursor.MoveNextAsync(stoppingToken))
            {
                foreach (var change in cursor.Current)
                {
                    var updatedChannelId = change.DocumentKey["_id"].AsObjectId.ToString();
                    var updatedChannel = await GetChannelByIdAsync(updatedChannelId);

                    if (updatedChannel != null)
                    {
                        OnChannelUpdated?.Invoke(updatedChannel); // Notify Blazor UI
                    }
                }
            }
        }
        public async Task<bool> UpdateChannelDetailsAsync(string channelId, string newName, string newDescription, string newCategory)
        {
            var filter = Builders<Channel>.Filter.Eq(c => c.ChannelId, channelId);
            var update = Builders<Channel>.Update
                .Set(c => c.ChannelName, newName)
                .Set(c => c.ChannelDescription, newDescription)
                .Set(c => c.Category, string.IsNullOrEmpty(newCategory) ? null : newCategory);

            var result = await _channels.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
