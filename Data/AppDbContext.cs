using ASPLoanMSC103.Model;
using Microsoft.EntityFrameworkCore;

namespace ASPLoanMSC103.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}