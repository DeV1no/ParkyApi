using Microsoft.EntityFrameworkCore;
using Parky.Entity;

namespace Parky.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trails> Trails { get; set; }
        public DbSet<User> Users { get; set; }
    }
}