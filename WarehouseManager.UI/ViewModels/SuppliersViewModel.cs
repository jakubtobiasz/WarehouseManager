using System;
using System.Collections.ObjectModel;
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
        private Window _supplierFormView = null;
        private ICommand _addNewSupplierCommand;
        private ICommand _removeSupplierCommand;

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
            get => _supplierFormView;
            set
            {
                if (_supplierFormView == value) return;

                _supplierFormView = value;
                OnPropertyChanged(nameof(SupplierCreatorView));
            }
        }

        public ICommand AddNewSupplierCommand
        {
            get
            {
                if (_addNewSupplierCommand is null)
                {
                    _addNewSupplierCommand = new RelayCommand(
                        _ => AddSupplier(),
                        _ => SupplierCreatorView is null
                    );
                }

                return _addNewSupplierCommand;
            }
        }

        public ICommand RemoveSupplierCommand
        {
            get
            {
                if (_removeSupplierCommand is null)
                {
                    _removeSupplierCommand = new RelayCommand(
                        s => RemoveSupplier((SupplierModel) s)
                    );
                }

                return _removeSupplierCommand;
            }
        }

        public string Name => "Dostawcy";

        public void FillData()
        {
            Suppliers = new ObservableCollection<SupplierModel>(_dbContext.Suppliers);
        }

        public void AddSupplier()
        {
            SupplierCreatorView = new SupplierFormView(new SupplierFormViewModel(_dbContext));
            SupplierCreatorView.Show();
            SupplierCreatorView.Closed += OnAddSupplierClosed;
        }

        public void OnAddSupplierClosed(object sender, EventArgs eventArgs)
        {
            SupplierCreatorView = null;
            Suppliers = null;
            FillData();
        }

        public void RemoveSupplier(SupplierModel supplier)
        {
            var messageBoxResult = MessageBox.Show($"Czy na pewno chcesz usunąć dostawcę {supplier.Name}?",
                "Usuwanie dostawcy", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.No) return;

            _dbContext.Suppliers.Remove(supplier);
            _dbContext.SaveChanges();
            FillData();
        }
    }
}
