using Microsoft.EntityFrameworkCore;
using backend_dotnet.Models;

namespace backend_dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<Presence> Presences { get; set; } = null!;
        public DbSet<Leave> Leaves { get; set; } = null!;
        public DbSet<Payroll> Payrolls { get; set; } = null!;
        public DbSet<Training> Trainings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map entities to lowercase table names as created in the SQL schema
            modelBuilder.Entity<Employee>().ToTable("employees");
            modelBuilder.Entity<Contract>().ToTable("contracts");
            modelBuilder.Entity<Presence>().ToTable("presences");
            modelBuilder.Entity<Leave>().ToTable("leaves");
            modelBuilder.Entity<Payroll>().ToTable("payrolls");
            modelBuilder.Entity<Training>().ToTable("trainings");

            // Basic indexes and constraints
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique(false);
        }
    }
}
