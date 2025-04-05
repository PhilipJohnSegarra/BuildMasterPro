using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{
    public class ProjectUserService : RepositoryBased<ProjectUser>
    {
        IDbContextFactory<ApplicationDbContext> _db;
        ProjectService _projectService;
        public ProjectUserService(IDbContextFactory<ApplicationDbContext> dbContext, ProjectService projectService) : base(dbContext) 
        { 
            _db = dbContext;
            _projectService = projectService;
        }
        public async Task<List<ProjectUser>> GetAll()
        {
            var result = await GetAllAsync();
            return result;
        }

        public async Task<ProjectUser> Get(int id)
        {
            var result = await GetAsync(o => o.ProjectUserId == id);
            return result;
        }
        public async Task<ProjectUser?> GetByUserId(string id)
        {
            using var _context = _db.CreateDbContext();
            var result = await _context.ProjectUsers.FirstOrDefaultAsync(x=>x.UserId == id);
            return result ?? null;
        }

        public async Task<List<ProjectUser>> GetAllByCurrentProject()
        {
            using var _context = _db.CreateDbContext();
            var currProj = await _projectService.GetCurrentProjectAsync();
            var result = await _context.ProjectUsers.Where(o => o.ProjectId.Equals(currProj.ProjectId))
                         .Include(o => o.User).ToListAsync();
            return result;
        }

        public async Task<ProjectUser> Add(ProjectUser projectUser)
        {
            var result = await AddAsync(projectUser);
            return result;
        }

        public async Task AddMany(List<ProjectUser> projectUsers)
        {
            using var _context = _db.CreateDbContext();
            _context.ProjectUsers.AddRange(projectUsers);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProjectUser oldEntity, ProjectUser newEntity)
        {
            await UpdateAsync(oldEntity, newEntity);
        }
        public async Task Remove(ProjectUser projUser)
        {
            using var _context = _db.CreateDbContext();
            _context.ProjectUsers.Remove(projUser);
            await _context.SaveChangesAsync();
        }
    }
}
