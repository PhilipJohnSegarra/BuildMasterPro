using BuildMasterPro.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BuildMasterPro.Services
{
    public class ProjectService : INotifyPropertyChanged
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        public List<Project> Projects { get; set; } = new();
        private Project? _currentProject;

        public Project? CurrentProject
        {
            get => _currentProject;
            set
            {
                if (_currentProject != value)
                {
                    _currentProject = value;
                    OnPropertyChanged(nameof(CurrentProject));
                }
            }
        }

        private readonly List<Action> _listeners = new();
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            foreach (var listener in _listeners)
            {
                listener();
            }
        }

        public void RegisterListener(Action listener) => _listeners.Add(listener);
        public void UnregisterListener(Action listener) => _listeners.Remove(listener);

        public ProjectService(IDbContextFactory<ApplicationDbContext> dbContext, ProtectedSessionStorage sessionStorage)
        {
            _contextFactory = dbContext;
            _sessionStorage = sessionStorage;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            using var _context = _contextFactory.CreateDbContext();
            Projects = await _context.Project.Where(i=>i.isDeleted == false).Include(i => i.ProjectUsers).ToListAsync();
            return Projects;
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Project.FindAsync(id);
        }

        public async Task SetCurrentProjectAsync(Project project)
        {
            _currentProject = project;
            OnPropertyChanged(nameof(CurrentProject));

            // 🔹 Serialize project before storing
            var serializedProject = JsonSerializer.Serialize(project);
            await _sessionStorage.SetAsync("currentProject", serializedProject);

            var result = await _sessionStorage.GetAsync<string>("currentProject");

            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                // 🔹 Deserialize back to a Project object
                CurrentProject = JsonSerializer.Deserialize<Project>(result.Value);
            }

        }

        public async Task<Project?> GetCurrentProjectAsync()
        {
            var result = await _sessionStorage.GetAsync<string>("currentProject");

            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                // 🔹 Deserialize back to a Project object
                CurrentProject = JsonSerializer.Deserialize<Project>(result.Value);
                return CurrentProject;
            }
            
            return null;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> UpdateProjectAsync(Project updatedProject)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Project.Update(updatedProject);
            await _context.SaveChangesAsync();
            return updatedProject;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            using var _context = _contextFactory.CreateDbContext();
            var project = await _context.Project.FindAsync(id);

            if (project == null) return false;

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Client?> GetProjectClient(Project project)
        {
            using var _context = _contextFactory.CreateDbContext();
            var client = await _context.Clients.FirstOrDefaultAsync(i => i.Id == project.ClientId);
            return client;
            
        }

        public async Task<Client> AddClientInformation(Client client)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
        public async Task<Client> UpdateClientInformation(Client client)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client;
        }
    }
}
