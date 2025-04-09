using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{ 
    public class NotificationService : RepositoryBased<Notification>
    {
        IDbContextFactory<ApplicationDbContext> _dbContext;
        public NotificationService(IDbContextFactory<ApplicationDbContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Notification>> GetAllByCurrentUser(string userId)
        {
            var result = await GetAllAsync(i => i.UserId == userId);
            var ordered = result.OrderByDescending(i=>i.CreatedAt).ToList();
            return ordered;
        }
        public async Task AddNotification(Notification notification)
        {
            await AddAsync(notification);
        }
        public async Task AddMany(List<Notification> newNotifs)
        {
            using var context = _dbContext.CreateDbContext();
            await context.Notifications.AddRangeAsync(newNotifs);
            await context.SaveChangesAsync();
        }
    }
}
