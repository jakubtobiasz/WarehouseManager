using Microsoft.EntityFrameworkCore;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SupplierModel> Suppliers { get; set; }
    }
}
