﻿using BuildMasterPro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel;
namespace BuildMasterPro.Services
{
    public class ProjectService : INotifyPropertyChanged
    {
        public List<Project> Projects { get; set; } = new();
        private Project _currentProject;
        public Project? CurrentProject { 
            get => _currentProject;
            set
            {
                if(_currentProject != value)
                {
                    _currentProject = value;
                    OnPropertyChanged(nameof(CurrentProject));
                }
            }
        }
        private readonly List<Action> _listeners = new();
        private readonly IDbContextFactory<ApplicationDbContext> context;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
            foreach (var listener in _listeners)
            {
                listener();
            }
        }
        public void RegisterListener(Action listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(Action listener)
        {
            _listeners.Remove(listener);
        }
        public ProjectService(IDbContextFactory<ApplicationDbContext> Context)
        {
            context = Context;

        }
        public async Task<List<Project>> GetProjectsAsync()
        {
            using var _context = context.CreateDbContext();
            this.Projects = await _context.Project.ToListAsync();
            return this.Projects;
        }
        public async Task<Project> GetProjectAsync(int id)
        {
            using var _context = context.CreateDbContext();
            return await _context.Project.FindAsync(id);
        }
        public async Task<Project> SetCurrentProjectAsync(Project project)
        {
            using var _context = context.CreateDbContext();
            this.CurrentProject = project;
            return this.CurrentProject;
        }
        public async Task<Project> CreateProjectAsync(Project project)
        {
            using var _context = context.CreateDbContext();
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
        public async Task<Project?> UpdateProjectAsync(int id, Project UpdatedProject)
        {
            using var _context = context.CreateDbContext();
            var project = await _context.Project.FirstOrDefaultAsync(i => i.ProjectId == id);
            if(project == null)
            {
                return null;
            }
            project.ProjectName = UpdatedProject.ProjectName;
            project.Description = UpdatedProject.Description;
            project.Startdate = UpdatedProject.Startdate;
            project.Enddate = UpdatedProject.Enddate;
            project.Status = UpdatedProject.Status;
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }
        public async Task<bool> DeleteProjectAsync(int id)
        {
            using var _context = context.CreateDbContext();
            var project = await _context.Project.FindAsync(id);
            if(project == null)
            {
                return false;
            }
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
