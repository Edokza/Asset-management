using AssetManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Asset> Assets { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
    }
}
