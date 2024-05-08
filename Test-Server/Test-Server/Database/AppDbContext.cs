using Microsoft.EntityFrameworkCore;
using Test_Server.Database.Entities;

namespace Test_Server.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Operator> Operators { get; set; }
        public DbSet<TestLog> Logs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>().HasKey(x => x.Id);

            modelBuilder.Entity<Operator>().HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<Operator>().HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Operator>().Property(b => b.Code)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Operator>().Property(b => b.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
