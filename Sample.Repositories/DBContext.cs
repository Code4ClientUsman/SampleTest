
using Microsoft.EntityFrameworkCore;
using Sample.Domains;

namespace Sample.Repositories
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
        }
    }
}
