using BuildMasterPro.Data;
using MongoDB.Driver;

namespace BuildMasterPro.Services
{
    public class TaskActivityImagesService
    {
        MongoService _mongoService;
        IMongoCollection<TaskActivityImages> _collection;
        public TaskActivityImagesService(MongoService mongoService)
        {
            _mongoService = mongoService;
            _collection = _mongoService.GetCollection<TaskActivityImages>("TaskActivityFiles");
        }
        public async Task Insert(TaskActivityImages document)
        {
            await _collection.InsertOneAsync(document);
        }
        public async Task<TaskActivityImages> GetByTaskActivityId(int taskActivityId)
        {
            var filter = Builders<TaskActivityImages>.Filter.Eq(x => x.TaskActivityID, taskActivityId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<TaskActivityImages>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
