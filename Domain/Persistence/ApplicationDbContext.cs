using Contracts.Common;
using Dapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Roles, Guid>
    {
        private string _connectionString = "";
        public DbSet<User> Users { get; set; }

        public IEnumerable<T> ExecuteQuery<T>(string tsql)
        {
            using (var connect = new SqlConnection(DataLocals.ConnectionString))
            {
                return connect.Query<T>(tsql);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuidBuilder)
        {
            //optionsBuidBuilder.UseSqlServer("Data Source=DESKTOP-RG9IML0\\MYPC;Initial Catalog=DDD_Databse;Integrated Security=SSPI;TrustServerCertificate=true");
            optionsBuidBuilder.UseSqlServer("Data Source=TRUONG\\SQLEXPRESS;Initial Catalog=DDD_Databse;Integrated Security=SSPI;TrustServerCertificate=true;Persist Security Info=True;");

            optionsBuidBuilder.EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}