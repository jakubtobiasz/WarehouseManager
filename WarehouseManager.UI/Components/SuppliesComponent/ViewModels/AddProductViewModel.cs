using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.SuppliesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.SuppliesComponent.ViewModels
{
    /// <summary>
    /// The AddProductViewModel class.
    /// Contains methods for AddProductView view.
    /// </summary>
    public class AddProductViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        private ICommand _saveCommand;

        public AddProductViewModel(AppDbContext dbContext, SupplyModel supply)
        {
            _dbContext = dbContext;

            Products = _dbContext.Products.ToList();
            Model = new ProductToSupplyModel
            {
                SupplyId = supply.SupplyId
            };
        }

        #region Properties

        /// <summary>
        /// Holds a collection of ProductModel instances.
        /// </summary>
        public ICollection<ProductModel> Products { get; set; }

        /// <summary>
        /// Holds a ProductToSupplyModel instance.
        /// </summary>
        public ProductToSupplyModel Model { get; set; }

        #endregion

        #region Properties - Commands

        /// <summary>
        /// Saves changes when conditions met.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand is null)
                {
                    _saveCommand = new RelayCommand(
                        _ => Save()
                    );
                }

                return _saveCommand;
            }
        }

        #endregion

        #region Methods

        private void Save()
        {
            _dbContext.ProductsToSupplies.Add(Model);
            _dbContext.SaveChanges();

            var window = Application.Current.Windows.OfType<AddProductView>().SingleOrDefault(a => a.IsActive);
            window?.Close();
        }

        #endregion
    }
}
