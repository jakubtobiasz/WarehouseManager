using Microsoft.EntityFrameworkCore;

namespace WarehouseManager.UI.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
