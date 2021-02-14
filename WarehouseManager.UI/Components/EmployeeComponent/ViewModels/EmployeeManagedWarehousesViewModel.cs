using WarehouseManager.UI.Common;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.EmployeeComponent.ViewModels
{
    public class EmployeeManagedWarehousesViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        public EmployeeManagedWarehousesViewModel(AppDbContext dbContext, EmployeeModel employee)
        {
            _dbContext = dbContext;
            Employee = employee;
        }

        #region Properties

        public EmployeeModel Employee { get; set; }

        #endregion

        #region Methods

        #endregion
    }
}
