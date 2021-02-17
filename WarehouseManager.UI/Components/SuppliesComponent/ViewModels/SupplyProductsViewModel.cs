using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.SuppliesComponent.ViewModels
{
    public class SupplyProductsViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        public SupplyProductsViewModel(AppDbContext dbContext, SupplyModel supply)
        {
            Products = supply.Products;
        }

        public ObservableCollection<ProductToSupplyModel> Products { get; set; }
    }
}
