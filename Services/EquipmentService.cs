using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{
    public class EquipmentService : RepositoryBased<Equipment>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        public EquipmentService(IDbContextFactory<ApplicationDbContext> db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Equipment>> GetAllEquipment()
        {
            var result = await GetAllAsync();
            return result;
        }
        public async Task<Equipment> GetOneAsync(int id)
        {
            var result = await GetAsync(i => i.EquipmentId == id);
            return result;
        }
    }
}
