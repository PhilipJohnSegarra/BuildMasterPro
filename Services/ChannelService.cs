
using BuildMasterPro.Data;
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

        public async Task<bool> DeleteChannelByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id); // Convert string to ObjectId
            var result = await _channels.DeleteOneAsync(Builders<Channel>.Filter.Eq("_id", objectId));

            return result.DeletedCount > 0;
        }

    }
}
