using Microsoft.EntityFrameworkCore;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Data
{
    /// <summary>
    /// The class holding all configuration related to the database connection.
    /// </summary>
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SupplierModel> Suppliers { get; set; }
        public DbSet<WarehouseModel> Warehouses { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; } 
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<SupplyModel> Supplies { get; set; }
        public DbSet<ProductToSupplyModel> ProductsToSupplies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplyModel>()
                .Property(s => s.SupplyStatus)
                .HasConversion<int>();
        }
    }
}
