using System;
using System.Windows;
using WarehouseManager.UI.Components.EmployeeComponent.ViewModels;

namespace WarehouseManager.UI.Components.EmployeeComponent.Views
{
    /// <summary>
    /// Interaction logic for EmployeeFormView.xaml
    /// </summary>
    public partial class EmployeeFormView : Window
    {
        public EmployeeFormView(EmployeeFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Closed += OnWindowClosed;
        }

        private void OnWindowClosed(object sender, EventArgs args)
        {
            (DataContext as EmployeeFormViewModel)?.ReloadModel();
            Closed -= OnWindowClosed;
        }
    }
}
