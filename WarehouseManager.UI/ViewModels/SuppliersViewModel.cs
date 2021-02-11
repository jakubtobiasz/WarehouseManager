using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Commands;
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
        private Window _supplierAddFormView = null;
        private ICommand _addNewSupplier;

        public SuppliersViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            FillData();
        }

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

        public Window SupplierCreatorView
        {
            get => _supplierAddFormView;
            set
            {
                if (_supplierAddFormView == value) return;

                _supplierAddFormView = value;
                OnPropertyChanged(nameof(SupplierCreatorView));
            }
        }

        public ICommand AddNewSupplier
        {
            get
            {
                if (_addNewSupplier is null)
                {
                    _addNewSupplier = new RelayCommand(
                        _ => OpenSupplierCreator(),
                        _ => SupplierCreatorView is null
                    );
                }

                return _addNewSupplier;
            }
        }

        public string Name => "Dostawcy";

        public void FillData()
        {
            Suppliers = new ObservableCollection<SupplierModel>(_dbContext.Suppliers);
        }

        public void OpenSupplierCreator()
        {
            SupplierCreatorView = new SupplierAddFormView(new SupplierAddFormViewModel(_dbContext));
            SupplierCreatorView.Show();
            SupplierCreatorView.Closed += OnCreatorClosed;
        }

        public void OnCreatorClosed(object sender, EventArgs eventArgs)
        {
            SupplierCreatorView = null;
            Suppliers = null;
            FillData();
        }
    }
}
