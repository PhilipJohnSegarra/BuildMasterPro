using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{
    public class TaskUserService : RepositoryBased<TaskUser>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        public TaskUserService(IDbContextFactory<ApplicationDbContext> dbContext) : base(dbContext) 
        {
            _db = dbContext;
        }

        public async Task<List<TaskUser>> GetAll()
        {
            var taskUsers = await GetAllAsync();
            return taskUsers;
        }

        public async Task<TaskUser> Get(int id)
        {
            var result = await GetAsync(o => o.Id == id);
            return result;
        }

        public async Task AddMany(List<TaskUser> taskUsers)
        {
            using var _context = _db.CreateDbContext();
            _context.TaskUsers.AddRange(taskUsers);
            await _context.SaveChangesAsync();
        }
    }
}
