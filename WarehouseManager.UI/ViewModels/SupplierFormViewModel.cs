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
    public class SupplierFormViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;
        private SupplierModel _model;
        private ICommand _save;

        public SupplierFormViewModel(AppDbContext dbContext, SupplierModel supplier = null)
        {
            _dbContext = dbContext;
            _model = supplier ?? new SupplierModel();
        }

        public ICommand Save
        {
            get
            {
                if (_save is null)
                {
                    _save = new RelayCommand(
                        _ => SaveChanges(),
                        _ => Model.CanSave
                    );
                }

                return _save;
            }
        }

        public SupplierModel Model
        {
            get => _model;
            set => _model = value;
        }

        public void SaveChanges()
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
    }
}
