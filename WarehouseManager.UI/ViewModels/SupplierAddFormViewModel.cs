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
    public class SupplierAddFormViewModel : ObservableObject
    {
        private AppDbContext _dbContext;
        private SupplierModel _model;
        private ICommand _save;

        public SupplierAddFormViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _model = new SupplierModel();
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
            _dbContext.Suppliers.Add(Model);
            _dbContext.SaveChanges();

            var window = Application.Current.Windows.OfType<SupplierAddFormView>().SingleOrDefault(w => w.IsActive);
            window?.Close();
        }
    }
}
