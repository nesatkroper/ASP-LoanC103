using ASPLoanMSC103.Model;
using Microsoft.EntityFrameworkCore;

namespace ASPLoanMSC103.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.UserID).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(p => p.Created).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>().Property(p => p.Modified).HasDefaultValueSql("GETDATE()");
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<LoanCategory> LoanCategories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}