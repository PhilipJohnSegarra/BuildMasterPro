using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{
    public class ResourceService : RepositoryBased<Resource>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        public ResourceService(IDbContextFactory<ApplicationDbContext> dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<Resource>> GetAllResources()
        {
            var result = await GetAllAsync();
            return result;
        }

        public async Task AddMany(List<Resource> resources)
        {
            using var db = _db.CreateDbContext();
            await db.Resource.AddRangeAsync(resources);
            await db.SaveChangesAsync();
        }

        public async Task<Resource> GetResourceById(int id)
        {
            var result = await GetAsync(i => i.ResourceId.Equals(id));
            return result;
        }
    }
}
