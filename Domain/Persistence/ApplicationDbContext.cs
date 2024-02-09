using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Roles, Guid>
    {
        public DbSet<User> Users { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuidBuilder)
        {
            optionsBuidBuilder.UseSqlServer("Data Source=DESKTOP-RG9IML0\\MYPC;Initial Catalog=DDD_Databse;Integrated Security=SSPI;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}