using BuildMasterPro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using BuildMasterPro.Services;
namespace BuildMasterPro.Services
{
    public class ProjectTaskService
    {
        ProjectService _projectService;
        private readonly IDbContextFactory<ApplicationDbContext> _context;
        public List<ProjectTask>? AllProjectTasks { get; set; }
        public List<ProjectTask>? CurrentProjectTasks { get; set; }
        public ProjectTask? CurrentProjectTask { get; set; }

        public ProjectTaskService(IDbContextFactory<ApplicationDbContext> Context, ProjectService projectService)
        {
            _projectService = projectService;
            _context = Context;
        }

        public async Task<ProjectTask> GetTaskAsync(int id)
        {
            using var context = _context.CreateDbContext();
            var task = await context.ProjectTask.FindAsync(id);
            if (task == null)
            {
                return null;
            }
            return task;
        }

        public async Task<List<ProjectTask>> GetAllProjtaskAsync()
        {
            using var context = _context.CreateDbContext();
            this.AllProjectTasks = await context.ProjectTask.ToListAsync();
            return this.AllProjectTasks;
        }

        public async Task<List<ProjectTask>> GetCurrentProjtasksAsync()
        {
            using var context = _context.CreateDbContext();
            var currProj = await _projectService.GetCurrentProjectAsync();
            this.CurrentProjectTasks = await context.ProjectTask.Where(i => i.ProjectId == currProj.ProjectId)
                .Include(p => p.TaskCategory)
                .Include(p => p.TaskUsers)
                .OrderBy(p => p.TaskCategory.Id)
                .ToListAsync();
            return this.CurrentProjectTasks;
        }

        public async Task<ProjectTask> CreateProjectTaskAsync(ProjectTask task)
        {
            using var context = _context.CreateDbContext();
            await context.ProjectTask.AddAsync(task);
            return task;
        }

        public async Task<List<ProjectTask>> AddMany(List<ProjectTask> newTasks)
        {
            using var context = _context.CreateDbContext();
            await context.ProjectTask.AddRangeAsync(newTasks);
            foreach(var task in newTasks)
            {
                if(task.TaskUsers != null)
                {
                    await context.TaskUsers.AddRangeAsync(task.TaskUsers);
                }
            }
            await context.SaveChangesAsync();
            return newTasks;
        }

        public async Task<ProjectTask> UpdateProjectTaskAsync(ProjectTask projecttask)
        {
            using var context = _context.CreateDbContext();

            var existingTask = await context.ProjectTask.FindAsync(projecttask.TaskId);
            if (existingTask == null) throw new Exception("Project Task not found");

            context.Entry(existingTask).CurrentValues.SetValues(projecttask);
            await context.SaveChangesAsync();

            return existingTask;

        }
        public async Task<bool> DeleteProjectTaskAsync(int id)
        {
            using var context = _context.CreateDbContext();
            var task = await context.ProjectTask.FindAsync(id);
            if (task == null) 
            {
                return false;
            }
            context.Remove(task);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task RemoveCategory(TaskCategory category)
        {
            using var context = _context.CreateDbContext();
            var tasks = await context.ProjectTask.Where(x=>x.CategoryId == category.Id).ToListAsync();
            foreach (var task in tasks)
            {
                task.CategoryId = null;
                context.ProjectTask.Update(task);
            }
            await context.SaveChangesAsync();
        }
    }
}
