using WarehouseManager.UI.Common;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.EmployeeComponent.ViewModels
{
    /// <summary>
    /// The EmployeeManagedWarehousesViewModel class.
    /// Contains methods for EmployeeManagedWarehousesView view.
    /// </summary>
    public class EmployeeManagedWarehousesViewModel : ObservableObject
    {
        public EmployeeManagedWarehousesViewModel(EmployeeModel employee)
        {
            Employee = employee;
        }

        #region Properties

        public EmployeeModel Employee { get; set; }

        #endregion
    }
}
