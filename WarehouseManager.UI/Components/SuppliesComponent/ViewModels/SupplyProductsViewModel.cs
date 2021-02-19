using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.SuppliesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.SuppliesComponent.ViewModels
{
    /// <summary>
    /// The SupplyProductsViewModel class.
    /// Contains methods for SupplyProductsView view.
    /// </summary>
    public class SupplyProductsViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;
        private readonly SupplyModel _supply;

        private ICommand _addCommand;
        private ICommand _removeCommand;

        public SupplyProductsViewModel(AppDbContext dbContext, SupplyModel supply)
        {
            _dbContext = dbContext;
            _supply = supply;

            FillData();
        }

        #region Properties

        /// <summary>
        /// Holds the collection of ProductToSupplyModel instances.
        /// </summary>
        public ObservableCollection<ProductToSupplyModel> Products { get; set; }

        #endregion

        #region Properties - Commands

        /// <summary>
        /// Opens the adding a new ProductToSupply form.
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand is null)
                {
                    _addCommand = new RelayCommand(
                        _ => Add(),
                        p => (p as ProductToSupplyModel)?.Supply.SupplyStatus == SupplyModel.SupplyStatusEnum.Added
                    );
                }

                return _addCommand;
            }
        }

        /// <summary>
        /// Removes provided ProductToSupply entry.
        /// </summary>
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand is null)
                {
                    _removeCommand = new RelayCommand(
                        p => Remove(p as ProductToSupplyModel),
                        p => (p as ProductToSupplyModel)?.Supply.SupplyStatus == SupplyModel.SupplyStatusEnum.Added
                    );
                }

                return _removeCommand;
            }
        }

        #endregion

        #region Methods

        private void Add()
        {
            var form = new AddProductView(new AddProductViewModel(_dbContext, _supply));
            form.Show();
            form.Closed += OnFormClosed;
        }

        private void OnFormClosed(object sender, EventArgs args)
        {
            _dbContext.Entry(_supply).Reload();
            FillData();
        }

        private void Remove(ProductToSupplyModel model)
        {
            _dbContext.ProductsToSupplies.Remove(model);
            _dbContext.SaveChanges();
            _dbContext.Entry(_supply).Reload();
            FillData();
        }

        private void FillData()
        {
            Products = null;
            Products = _supply.Products;
        }

        #endregion
    }
}
