using System.Collections.ObjectModel;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;
using WarehouseManager.UI.Views;

namespace WarehouseManager.UI.Components.WarehousesComponent.ViewModels
{
    public class WarehousesViewModel : ObservableObject, IPageViewModel
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<WarehouseModel> _warehouses;

        public WarehousesViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            FillData();
        }

        #region Properties

        public string Name => "Magazyny";

        public ObservableCollection<WarehouseModel> Warehouses
        {
            get => _warehouses;
            set
            {
                if (_warehouses == value) return;

                _warehouses = value;
                OnPropertyChanged(nameof(Warehouses));
            }
        }

        #endregion

        #region Properties - Commands

        

        #endregion

        #region Methods

        private void FillData()
        {
            Warehouses = new ObservableCollection<WarehouseModel>(_dbContext.Warehouses);
        }

        #endregion
    }
}
