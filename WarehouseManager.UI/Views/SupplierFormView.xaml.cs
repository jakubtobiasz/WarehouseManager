using System.Windows;
using WarehouseManager.UI.ViewModels;

namespace WarehouseManager.UI.Views
{
    /// <summary>
    /// Interaction logic for SupplierAddFormView.xaml
    /// </summary>
    public partial class SupplierFormView : Window
    {
        public SupplierFormView(SupplierFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
