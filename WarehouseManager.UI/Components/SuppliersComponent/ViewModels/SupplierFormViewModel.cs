using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.SuppliersComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.SuppliersComponent.ViewModels
{
    /// <summary>
    /// The SupplierFormViewModel class.
    /// Contains methods for SupplierFormView view.
    /// </summary>
    public class SupplierFormViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        private ICommand _saveCommand;

        public SupplierFormViewModel(AppDbContext dbContext, SupplierModel supplier = null)
        {
            _dbContext = dbContext;
            Model = supplier ?? new SupplierModel();
        }

        #region Properties

        public SupplierModel Model { get; set; }

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
                        _ => Save(),
                        _ => Model.CanSave
                    );
                }

                return _saveCommand;
            }
        }

        #endregion

        #region Methods

        private void Save()
        {
            if (Model.SupplierId == default)
            {
                _dbContext.Suppliers.Add(Model);
            }
            else
            {
                _dbContext.Suppliers.Update(Model);
            }

            _dbContext.SaveChanges();

            var window = Application.Current.Windows.OfType<SupplierFormView>().SingleOrDefault(w => w.IsActive);
            window?.Close();
        }

        public void ReloadModel()
        {
            _dbContext.Entry(Model).Reload();
        }

        #endregion
    }
}
