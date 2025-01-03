using MongoDB.Driver;

namespace BuildMasterPro.Services
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService()
        {
            var client = new MongoClient("mongodb+srv://myAtlasDBUser:Thesis123@myatlasclusteredu.qsxtx.mongodb.net/?retryWrites=true&w=majority&appName=myAtlasClusterEDU");
            _database = client.GetDatabase("BuildMasterPro");
        }

        public async Task<IMongoCollection<T>> GetCollection<T>(string collectionName)
        {
            if(_database.GetCollection<T>(collectionName) == null)
            {
                await _database.CreateCollectionAsync(collectionName);
                return _database.GetCollection<T>(collectionName);
            }
            return _database.GetCollection<T>(collectionName);
        }
    }
}
