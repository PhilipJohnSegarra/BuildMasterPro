using BuildMasterPro.Data;
using BuildMasterPro.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildMasterPro.Services
{
    public class ProjectUserService : RepositoryBased<ProjectUser>
    {
        public ProjectUserService(IDbContextFactory<ApplicationDbContext> dbContext) : base(dbContext) 
        { 

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

        public async Task<ProjectUser> Add(ProjectUser projectUser)
        {
            var result = await AddAsync(projectUser);
            return result;
        }

        public async Task Update(ProjectUser oldEntity, ProjectUser newEntity)
        {
            await UpdateAsync(oldEntity, newEntity);
        }
    }
}
