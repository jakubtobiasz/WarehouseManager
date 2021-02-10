using System.Windows;
using WarehouseManager.UI.ViewModels;

namespace WarehouseManager.UI.Views
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
        }
    }
}
