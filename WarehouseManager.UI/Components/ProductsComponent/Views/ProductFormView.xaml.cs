using System;
using System.Windows;
using WarehouseManager.UI.Components.ProductsComponent.ViewModels;
using WarehouseManager.UI.Components.WarehousesComponent.ViewModels;

namespace WarehouseManager.UI.Components.ProductsComponent.Views
{
    /// <summary>
    /// Interaction logic for ProductFormView.xaml
    /// </summary>
    public partial class ProductFormView : Window
    {
        public ProductFormView(ProductFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Closed += OnWindowClosed;
        }

        private void OnWindowClosed(object sender, EventArgs args)
        {
            (DataContext as ProductFormViewModel)?.ReloadModel();
            Closed -= OnWindowClosed;
        }
    }
}
