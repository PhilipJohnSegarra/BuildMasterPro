
using BuildMasterPro.Data;
using Microsoft.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BuildMasterPro.Services
{
    public class ChannelService
    {
        MongoService _mongoService;
        IMongoCollection<Channel> _channels;
        public ChannelService(MongoService mongoService)
        {
            _mongoService = mongoService;
            _channels = _mongoService.GetCollection<Channel>("Channels");
        }

        public async Task<List<Channel>> GetProjChannelsAsync(int projectId)
        {
            var result = await _channels.Aggregate()
                .Match(Builders<Channel>.Filter.Eq(i => i.ProjectId, projectId))
                .ToListAsync();
            return result;
        }

        public async Task AddNewChannelAsync(Channel newChannel)
        {
            await _channels.InsertOneAsync(newChannel);
        }

        public async Task AddMessageAsync(string channelId, ChannelMessage message)
        {
            var filter = Builders<Channel>.Filter.Eq(c => c.ChannelId, channelId);

            // Step 1: Ensure 'channel_messages' is an array if it's null
            var check = await _channels.Find(filter).FirstOrDefaultAsync();
            if (check != null && check.ChannelMessages == null)
            {
                var initUpdate = Builders<Channel>.Update.Set(c => c.ChannelMessages, new List<ChannelMessage>());
                await _channels.UpdateOneAsync(filter, initUpdate);
            }

            // Step 2: Now safely push the message
            var pushUpdate = Builders<Channel>.Update.Push(c => c.ChannelMessages, message);
            await _channels.UpdateOneAsync(filter, pushUpdate);
        }



        public async Task<bool> DeleteChannelByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id); // Convert string to ObjectId
            var result = await _channels.DeleteOneAsync(Builders<Channel>.Filter.Eq("_id", objectId));

            return result.DeletedCount > 0;
        }
        public async Task<Channel?> GetChannelByIdAsync(string channelId)
        {
            var filter = Builders<Channel>.Filter.Eq(c => c.ChannelId, channelId);
            return await _channels.Find(filter).FirstOrDefaultAsync();
        }


    }
}
