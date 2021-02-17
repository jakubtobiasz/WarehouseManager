using System;
using System.Windows;
using WarehouseManager.UI.Components.SuppliesComponent.ViewModels;

namespace WarehouseManager.UI.Components.SuppliesComponent.Views
{
    /// <summary>
    /// Interaction logic for SupplyFormView.xaml
    /// </summary>
    public partial class SupplyFormView : Window
    {
        public SupplyFormView(SuppliesFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Closed += OnWindowClosed;
        }

        public void OnWindowClosed(object sender, EventArgs args)
        {
            (DataContext as SuppliesFormViewModel)?.ReloadModel();
            Closed -= OnWindowClosed;
        }
    }
}
