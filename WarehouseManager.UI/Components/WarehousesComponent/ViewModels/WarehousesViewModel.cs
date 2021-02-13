using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.UI.Commands;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.WarehousesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;
using WarehouseManager.UI.Views;

namespace WarehouseManager.UI.Components.WarehousesComponent.ViewModels
{
    public class WarehousesViewModel : ObservableObject, IPageViewModel
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<WarehouseModel> _warehouses;

        private ICommand _addWarehouseCommand;
        private ICommand _editWarehouseCommand;
        private ICommand _removeWarehouseCommand;

        private Window _warehouseFormView;

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

        private Window WarehouseFormView
        {
            get => _warehouseFormView;
            set
            {
                if (_warehouseFormView == value) return;

                _warehouseFormView = value;
                OnPropertyChanged(nameof(WarehouseFormView));
            }
        }

        #endregion

        #region Properties - Commands

        public ICommand AddWarehouseCommand
        {
            get
            {
                if (_addWarehouseCommand is null)
                {
                    _addWarehouseCommand = new RelayCommand(
                        _ => AddWarehouse(),
                        _ => WarehouseFormView is null
                    );
                }

                return _addWarehouseCommand;
            }
        }

        public ICommand EditWarehouseCommand
        {
            get
            {
                if (_editWarehouseCommand is null)
                {
                    _editWarehouseCommand = new RelayCommand(
                        w => EditWarehouse(w as WarehouseModel)
                    );
                }

                return _editWarehouseCommand;
            }
        }

        public ICommand RemoveWarehouseCommand
        {
            get
            {
                if (_removeWarehouseCommand is null)
                {
                    _removeWarehouseCommand = new RelayCommand(
                        w => RemoveWarehouse(w as WarehouseModel)
                    );
                }

                return _removeWarehouseCommand;
            }
        }

        #endregion

        #region Methods

        private void AddWarehouse()
        {
            WarehouseFormView = new WarehouseFormView(new WarehouseFormViewModel(_dbContext));
            WarehouseFormView.Show();
            WarehouseFormView.Closed += OnFormClosed;
        }

        private void EditWarehouse(WarehouseModel warehouse)
        {
            WarehouseFormView = new WarehouseFormView(new WarehouseFormViewModel(_dbContext, warehouse));
            WarehouseFormView.Show();
            WarehouseFormView.Closed += OnFormClosed;
        }

        private void RemoveWarehouse(WarehouseModel warehouse)
        {
            var messageBoxResult = MessageBox.Show($"Czy na pewno chcesz usunąć magazyn {warehouse.Name}?",
                "Usuwanie dostawcy", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.No) return;

            _dbContext.Warehouses.Remove(warehouse);
            _dbContext.SaveChanges();
            FillData();
        }

        private void OnFormClosed(object sender, EventArgs args)
        {
            WarehouseFormView = null;
            Warehouses = null;
            FillData();
        }

        private void FillData()
        {
            Warehouses = new ObservableCollection<WarehouseModel>(_dbContext.Warehouses.Include(w => w.Manager));
        }

        #endregion
    }
}
