using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.ProductsComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.ProductsComponent.ViewModels
{
    public class ProductFormViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        private ICommand _saveCommand;

        public ProductFormViewModel(AppDbContext dbContext, ProductModel product= null)
        {
            _dbContext = dbContext;
            Model = product ?? new ProductModel();
        }

        #region Properties

        public ProductModel Model { get; set; }

        #endregion

        #region Properties - Command

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand is null)
                {
                    _saveCommand = new RelayCommand(
                        _ => SaveChanges(),
                        _ => Model.CanSave
                    );
                }

                return _saveCommand;
            }
        }

        #endregion

        #region Methods

        public void SaveChanges()
        {
            if (Model.ProductId == default)
            {
                _dbContext.Products.Add(Model);
            }
            else
            {
                _dbContext.Products.Update(Model);
            }

            _dbContext.SaveChanges();

            var window = Application.Current.Windows.OfType<ProductFormView>().SingleOrDefault(p => p.IsActive);
            window?.Close();
        }

        public void ReloadModel()
        {
            _dbContext.Entry(Model).Reload();
        }

        #endregion
    }
}
