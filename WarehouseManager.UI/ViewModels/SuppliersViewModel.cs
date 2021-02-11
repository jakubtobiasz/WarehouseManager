using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;
using WarehouseManager.UI.Views;

namespace WarehouseManager.UI.ViewModels
{
    public class SuppliersViewModel : ObservableObject, IPageViewModel
    {

        private readonly AppDbContext _dbContext;
        private ObservableCollection<SupplierModel> _suppliers;

        public ObservableCollection<SupplierModel> Suppliers
        {
            get => _suppliers;
            set
            {
                if (_suppliers == value) return;

                _suppliers = value;
                OnPropertyChanged(nameof(Suppliers));
            }
        }

        public SuppliersViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            FillData();
        }

        public string Name => "Dostawcy";

        public void FillData()
        {
            Suppliers = new ObservableCollection<SupplierModel>(_dbContext.Suppliers);
        }
    }
}
