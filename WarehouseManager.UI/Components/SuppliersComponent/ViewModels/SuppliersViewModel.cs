using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.SuppliersComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.SuppliersComponent.ViewModels
{
    public class SuppliersViewModel : ObservableObject, IPageViewModel
    {

        private readonly AppDbContext _dbContext;
        private ObservableCollection<SupplierModel> _suppliers;
        private Window _supplierFormView = null;
        private ICommand _addNewSupplierCommand;
        private ICommand _removeSupplierCommand;
        private ICommand _editSupplierCommand;

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

        public Window SuppliersFormView
        {
            get => _supplierFormView;
            set
            {
                if (_supplierFormView == value) return;

                _supplierFormView = value;
                OnPropertyChanged(nameof(SuppliersFormView));
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
                        _ => SuppliersFormView is null
                    );
                }

                return _addNewSupplierCommand;
            }
        }

        public ICommand EditSupplierCommand
        {
            get
            {
                if (_editSupplierCommand is null)
                {
                    _editSupplierCommand = new RelayCommand(
                        s => EditSupplier(s as SupplierModel),
                        _ => SuppliersFormView is null
                    );
                }

                return _editSupplierCommand;
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

        public void AddSupplier()
        {
            SuppliersFormView = new SupplierFormView(new SupplierFormViewModel(_dbContext));
            SuppliersFormView.Show();
            SuppliersFormView.Closed += OnSuppliersFormClosed;
        }

        public void EditSupplier(SupplierModel supplier)
        {
            SuppliersFormView = new SupplierFormView(new SupplierFormViewModel(_dbContext, supplier));
            SuppliersFormView.Show();
            SuppliersFormView.Closed += OnSuppliersFormClosed;
        }

        private void OnSuppliersFormClosed(object sender, EventArgs eventArgs)
        {
            SuppliersFormView = null;
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

        private void FillData()
        {
            Suppliers = new ObservableCollection<SupplierModel>(_dbContext.Suppliers);
        }
    }
}
