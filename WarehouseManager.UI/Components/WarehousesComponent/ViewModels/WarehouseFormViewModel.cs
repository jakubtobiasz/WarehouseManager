using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Commands;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.WarehousesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.WarehousesComponent.ViewModels
{
    public class WarehouseFormViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;
        private WarehouseModel _model;
        private ICommand _save;

        public WarehouseFormViewModel(AppDbContext dbContext, WarehouseModel warehouse = null)
        {
            _dbContext = dbContext;
            _model = warehouse ?? new WarehouseModel();
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

        public WarehouseModel Model
        {
            get => _model;
            set => _model = value;
        }

        public void SaveChanges()
        {
            if (Model.WarehouseId == default)
            {
                _dbContext.Warehouses.Add(Model);
            }
            else
            {
                _dbContext.Warehouses.Update(Model);
            }

            _dbContext.SaveChanges();

            var window = Application.Current.Windows.OfType<WarehouseFormView>().SingleOrDefault(w => w.IsActive);
            window?.Close();
        }

        public void ReloadModel()
        {
            _dbContext.Entry(Model).Reload();
        }
    }
}
