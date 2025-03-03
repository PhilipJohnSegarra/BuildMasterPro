using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BuildMasterPro.Data;
using BuildMasterPro.Repositories;

namespace BuildMasterPro.Services
{
    public class UserService : RepositoryBased<ApplicationUser>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        public UserService(IDbContextFactory<ApplicationDbContext> db) : base(db)
        {
            _db = db;
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var result = await GetAllAsync();
            return result;
        }

        public async Task<ApplicationUser> Get(string id)
        {
            var result = await GetAsync(o => o.Id == id);
            return result;
        }

        public async Task<ApplicationUser> Add(ApplicationUser user)
        {
            var result = await AddAsync(user);
            return result;
        }

        public async Task Update(ApplicationUser oldEntity, ApplicationUser newEntity)
        {
            await UpdateAsync(oldEntity, newEntity);
        }

    }
}
