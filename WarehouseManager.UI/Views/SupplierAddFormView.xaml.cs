using System.Windows;
using WarehouseManager.UI.ViewModels;

namespace WarehouseManager.UI.Views
{
    /// <summary>
    /// Interaction logic for SupplierAddFormView.xaml
    /// </summary>
    public partial class SupplierAddFormView : Window
    {
        public SupplierAddFormView(SupplierAddFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
