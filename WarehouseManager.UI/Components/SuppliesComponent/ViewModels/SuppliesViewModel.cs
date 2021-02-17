using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.UI.Commands;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.SuppliesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;
using WarehouseManager.UI.Views;

namespace WarehouseManager.UI.Components.SuppliesComponent.ViewModels
{
    public class SuppliesViewModel : ObservableObject, IPageViewModel
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<SupplyModel> _supplies;

        private ICommand _addCommand;

        public SuppliesViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            FillData();
        }

        #region Properties

        public string Name => "Dostawy";

        public ObservableCollection<SupplyModel> Supplies
        {
            get => _supplies;
            set
            {
                if (_supplies == value) return;

                _supplies = value;
                OnPropertyChanged(nameof(Supplies));
            }
        }

        #endregion

        #region Properties - Commands

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand is null)
                {
                    _addCommand = new RelayCommand(
                        _ => Add()
                    );
                }

                return _addCommand;
            }
        }

        #endregion

        #region Methods

        private void FillData()
        {
            Supplies = new ObservableCollection<SupplyModel>(_dbContext.Supplies.Include(s => s.Supplier)
                .Include(s => s.Warehouse));
        }

        private void Add()
        {
            var form = new SupplyFormView(new SuppliesFormViewModel(_dbContext));
            form.Show();
            form.Closed += OnFormClosed;
        }

        private void OnFormClosed(object sender, EventArgs args)
        {
            Supplies = null;
            FillData();
        }

        #endregion
    }
}
