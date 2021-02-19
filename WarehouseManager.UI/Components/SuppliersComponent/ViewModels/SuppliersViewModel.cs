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
    /// <summary>
    /// The SuppliersViewModel class.
    /// Contains methods for SuppliersView view.
    /// </summary>
    public class SuppliersViewModel : ObservableObject, IPageViewModel
    {

        private readonly AppDbContext _dbContext;
        private ObservableCollection<SupplierModel> _suppliers;
        private Window _supplierFormView = null;

        private ICommand _addCommand;
        private ICommand _removeCommand;
        private ICommand _editCommand;

        public SuppliersViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            FillData();
        }

        #region Properties

        public string Name => "Dostawcy";

        /// <summary>
        /// Holds the collection of SupplierModel instances.
        /// </summary>
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

        /// <summary>
        /// Holds an instance of SuppliersFormView.
        /// </summary>
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

        #endregion

        #region Properties - Commands

        /// <summary>
        /// Opens the adding a new supplier form.
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand is null)
                {
                    _addCommand = new RelayCommand(
                        _ => Add(),
                        _ => SuppliersFormView is null
                    );
                }

                return _addCommand;
            }
        }

        /// <summary>
        /// Opens the editing supplier form.
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand is null)
                {
                    _editCommand = new RelayCommand(
                        s => Edit(s as SupplierModel),
                        _ => SuppliersFormView is null
                    );
                }

                return _editCommand;
            }
        }

        /// <summary>
        /// Removes the passed supplier.
        /// </summary>
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand is null)
                {
                    _removeCommand = new RelayCommand(
                        s => Remove((SupplierModel) s)
                    );
                }

                return _removeCommand;
            }
        }

        #endregion

        #region Methods

        private void Add()
        {
            SuppliersFormView = new SupplierFormView(new SupplierFormViewModel(_dbContext));
            SuppliersFormView.Show();
            SuppliersFormView.Closed += OnSuppliersFormClosed;
        }

        private void Edit(SupplierModel supplier)
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

        private void Remove(SupplierModel supplier)
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

        #endregion
    }
}
