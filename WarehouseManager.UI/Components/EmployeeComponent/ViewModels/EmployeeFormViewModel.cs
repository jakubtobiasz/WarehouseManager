using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.EmployeeComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.EmployeeComponent.ViewModels
{
    public class EmployeeFormViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        private ICommand _saveCommand;

        public EmployeeFormViewModel(AppDbContext dbContext, EmployeeModel employee = null)
        {
            _dbContext = dbContext;
            Model = employee ?? new EmployeeModel();
        }

        #region Properties

        public EmployeeModel Model { get; set; }

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
            if (Model.EmployeeId == default)
            {
                _dbContext.Employees.Add(Model);
            }
            else
            {
                _dbContext.Employees.Update(Model);
            }

            _dbContext.SaveChanges();

            var window = Application.Current.Windows.OfType<EmployeeFormView>().SingleOrDefault(w => w.IsActive);
            window?.Close();
        }

        public void ReloadModel()
        {
            _dbContext.Entry(Model).Reload();
        }

        #endregion
    }
}
