using Microsoft.EntityFrameworkCore;

namespace ShipmentsApi.Models
{
    public class ShipmentsContext : DbContext
    {
        public ShipmentsContext(DbContextOptions<ShipmentsContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; } = null!;
    }
}