using System.Windows;
using WarehouseManager.UI.Components.EmployeeComponent.ViewModels;

namespace WarehouseManager.UI.Components.EmployeeComponent.Views
{
    /// <summary>
    /// Interaction logic for EmployeeManagedWarehousesView.xaml
    /// </summary>
    public partial class EmployeeManagedWarehousesView : Window
    {
        public EmployeeManagedWarehousesView(EmployeeManagedWarehousesViewModel viewModel)
        {
            DataContext = viewModel;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
