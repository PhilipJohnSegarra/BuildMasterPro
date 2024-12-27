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
        public Project? CurrentProject { get; set; }
        public List<ProjectTask>? AllProjectTasks { get; set; }
        public List<ProjectTask>? CurrentProjectTasks { get; set; }
        public ProjectTask? CurrentProjectTask { get; set; }

        public ProjectTaskService(IDbContextFactory<ApplicationDbContext> Context, ProjectService projectService)
        {
            _projectService = projectService;
            _context = Context;
            this.CurrentProject = _projectService.CurrentProject;
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
            this.CurrentProjectTasks = await context.ProjectTask.Where(i => i.ProjectId == _projectService.CurrentProject!.ProjectId).ToListAsync();
            return this.CurrentProjectTasks;
        }

        public async Task<ProjectTask> CreateProjectTaskAsync(ProjectTask task)
        {
            using var context = _context.CreateDbContext();
            await context.ProjectTask.AddAsync(task);
            return task;
        }

        public async Task<ProjectTask> UpdateProjectTaskAsync(int id, ProjectTask projecttask)
        {
            using var context = _context.CreateDbContext();
            var task = await context.ProjectTask.FindAsync(id);
            if(task == null)
            {
                return null;
            }
            task.TaskName = projecttask.TaskName;
            task.TaskDescription = projecttask.TaskDescription;
            task.StartDate = projecttask.StartDate;
            task.DueDate = projecttask.DueDate;
            task.Status = projecttask.Status;
            task.Priority = projecttask.Priority;

            context.ProjectTask.Update(task);
            await context.SaveChangesAsync();
            return task;

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
    }
}
