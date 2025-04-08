using DapperVsEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DapperVsEFCore.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Student> Students { get; set; }
    }
}
