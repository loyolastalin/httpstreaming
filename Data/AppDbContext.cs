using httpstreaming.Models;
using Microsoft.EntityFrameworkCore;

namespace httpstreaming.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Platform> Platforms { get; set; }
        
    }
}