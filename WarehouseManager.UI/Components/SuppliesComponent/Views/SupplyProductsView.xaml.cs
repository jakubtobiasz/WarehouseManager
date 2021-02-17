using System.Windows;
using WarehouseManager.UI.Components.SuppliesComponent.ViewModels;

namespace WarehouseManager.UI.Components.SuppliesComponent.Views
{
    /// <summary>
    /// Interaction logic for SupplyProductsView.xaml
    /// </summary>
    public partial class SupplyProductsView : Window
    {
        public SupplyProductsView(SupplyProductsViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
