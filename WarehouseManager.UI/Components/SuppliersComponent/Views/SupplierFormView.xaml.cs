using System;
using System.Windows;
using WarehouseManager.UI.Components.SuppliersComponent.ViewModels;

namespace WarehouseManager.UI.Components.SuppliersComponent.Views
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
            Closed += OnWindowClosed;
        }

        public void OnWindowClosed(object sender, EventArgs args)
        {
            (DataContext as SupplierFormViewModel)?.ReloadModel();
            Closed -= OnWindowClosed;
        }
    }
}
