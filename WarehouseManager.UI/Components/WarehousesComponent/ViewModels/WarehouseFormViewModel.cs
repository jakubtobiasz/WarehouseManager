using System.Collections.Generic;
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

        private ICommand _saveCommand;

        public WarehouseFormViewModel(AppDbContext dbContext, WarehouseModel warehouse = null)
        {
            _dbContext = dbContext;
            Model = warehouse ?? new WarehouseModel();
            Employees = dbContext.Employees.ToList();
        }

        #region Properties

        public WarehouseModel Model { get; set; }
        public ICollection<EmployeeModel> Employees { get; set; }

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

        #endregion
    }
}
