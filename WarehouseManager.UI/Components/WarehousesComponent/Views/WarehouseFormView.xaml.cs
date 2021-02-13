using System;
using System.Windows;
using WarehouseManager.UI.Components.WarehousesComponent.ViewModels;

namespace WarehouseManager.UI.Components.WarehousesComponent.Views
{
    /// <summary>
    /// Interaction logic for WarehouseFormView.xaml
    /// </summary>
    public partial class WarehouseFormView : Window
    {
        public WarehouseFormView(WarehouseFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Closed += OnWindowClosed;
        }

        private void OnWindowClosed(object sender, EventArgs args)
        {
            (DataContext as WarehouseFormViewModel)?.ReloadModel();
            Closed -= OnWindowClosed;
        }
    }
}
