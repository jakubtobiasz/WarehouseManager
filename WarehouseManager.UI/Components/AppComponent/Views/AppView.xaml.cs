using System.Windows;
using WarehouseManager.UI.Components.AppComponent.ViewModels;

namespace WarehouseManager.UI.Components.AppComponent.Views
{
    /// <summary>
    /// Interaction logic for AppView.xaml
    /// </summary>
    public partial class AppView : Window
    {
        public AppView(AppViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
