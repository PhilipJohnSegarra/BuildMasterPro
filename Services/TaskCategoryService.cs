using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{
    public class TaskCategoryService : RepositoryBased<TaskCategory>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ProjectService _projectService;
        public TaskCategoryService(IDbContextFactory<ApplicationDbContext> dbContext, ProjectService projService) : base(dbContext)
        {
            _dbContextFactory = dbContext;
            _projectService = projService;
        }

        public async Task<List<TaskCategory>> GetAllAsync()
        {
            var result = await GetAllAsync();
            return result;
        }

        public async Task<List<TaskCategory>> GetAllByCurrentProject()
        {
            var currentProject = await _projectService.GetCurrentProjectAsync();
            using var context = _dbContextFactory.CreateDbContext();
            var result = await context.TaskCategories.Where(x=>x.ProjectId == currentProject.ProjectId).ToListAsync();
            return result;
        }

        public async Task<TaskCategory> AddNewAsync(TaskCategory newCategory)
        {
            var result = await AddAsync(newCategory);
            return result;
        }
        public async Task Remove(TaskCategory category)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.TaskCategories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
