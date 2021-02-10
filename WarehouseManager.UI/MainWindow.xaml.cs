using System.Windows;
using WarehouseManager.UI.Data;

namespace WarehouseManager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(AppDbContext dbContext)
        {
            InitializeComponent();
        }
    }
}
