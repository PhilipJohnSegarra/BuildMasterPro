using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{
    public class TaskActivityService : RepositoryBased<TaskActivity>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        public TaskActivityService(IDbContextFactory<ApplicationDbContext> dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<TaskActivity>> GetAll()
        {
            var TaskActivities = await GetAllAsync();
            return TaskActivities;
        }

        public async Task<TaskActivity> Get(int id)
        {
            var result = await GetAsync(o => o.Id == id);
            return result;
        }
        public async Task<TaskActivity> Add(TaskActivity TaskActivity)
        {
            var result = await AddAsync(TaskActivity);
            return result;
        }

        public async Task AddMany(List<TaskActivity> TaskActivities)
        {
            using var _context = _db.CreateDbContext();
            _context.TaskActivities.AddRange(TaskActivities);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMany(List<TaskActivity> TaskActivities)
        {
            using var _context = _db.CreateDbContext();
            _context.TaskActivities.RemoveRange(TaskActivities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskActivity>> GetByTask(ProjectTask task)
        {
            using var _context = _db.CreateDbContext();
            var result = await _context.TaskActivities.Where(i => i.TaskId == task.TaskId)
                .Include(i => i.User).ToListAsync();
            return result;
        }
    }
}
