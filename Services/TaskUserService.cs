﻿using BuildMasterPro.Data;
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
            foreach(var user in taskUsers)
            {
                user.ProjectTask = null;
                await _context.TaskUsers.AddAsync(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMany(List<TaskUser> taskUsers)
        {
            using var _context = _db.CreateDbContext();
            _context.TaskUsers.RemoveRange(taskUsers);
            await _context.SaveChangesAsync();
        }
        public async Task Remove(TaskUser taskUsers)
        {
            using var _context = _db.CreateDbContext();
            _context.TaskUsers.Remove(taskUsers);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskUser>> GetByTask(ProjectTask task)
        {
            using var _context = _db.CreateDbContext();
            var result = await _context.TaskUsers.Where(i => i.TaskId == task.TaskId)
                .Include(i => i.User).ToListAsync();
            return result;
        }
    }
}
