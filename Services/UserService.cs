using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using System.Security.Claims;

namespace BuildMasterPro.Services
{
    public class UserService : RepositoryBased<ApplicationUser>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IDbContextFactory<ApplicationDbContext> db, UserManager<ApplicationUser> userManager) : base(db)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var result = await GetAllAsync(i => i.IsDeleted.Equals(false));
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
        public async Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }
    }
}
