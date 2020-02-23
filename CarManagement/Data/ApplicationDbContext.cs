using Microsoft.EntityFrameworkCore;
using CarManagement.Models;

namespace CarManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Log> Log { get; set; }

        public DbSet<Car> Car { get; set; }
    }
}
