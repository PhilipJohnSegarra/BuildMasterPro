using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BuildMasterPro.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

namespace BuildMasterPro.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<Project> Project { get; set; } = default!;
        public DbSet<ProjectTask> ProjectTask { get; set; } = default!;
        public DbSet<Resource> Resource { get; set; } = default!;
        public DbSet<ProjectUser> ProjectUsers { get; set; } = default!;
        public DbSet<TaskCategory> TaskCategories { get; set; } = default!;
        public DbSet<TaskUser> TaskUsers { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;

    }
}
