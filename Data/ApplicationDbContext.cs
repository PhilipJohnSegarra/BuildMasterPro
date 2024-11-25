using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BuildMasterPro.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuildMasterPro.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        
        public DbSet<BuildMasterPro.Data.Project> Project { get; set; } = default!;
        public DbSet<BuildMasterPro.Data.ProjectTask> ProjectTask { get; set; } = default!;

    }
}
