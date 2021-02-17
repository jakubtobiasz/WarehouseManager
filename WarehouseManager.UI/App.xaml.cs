using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManager.UI.Components.AppComponent.ViewModels;
using WarehouseManager.UI.Components.AppComponent.Views;
using WarehouseManager.UI.Components.EmployeeComponent.ViewModels;
using WarehouseManager.UI.Components.ProductsComponent.ViewModels;
using WarehouseManager.UI.Components.SuppliersComponent.ViewModels;
using WarehouseManager.UI.Components.SuppliersComponent.Views;
using WarehouseManager.UI.Components.SuppliesComponent.ViewModels;
using WarehouseManager.UI.Components.WarehousesComponent.ViewModels;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Views;

namespace WarehouseManager.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source = warehouse_manager.db");
            });

            services.AddTransient<SupplierFormView>();
            services.AddTransient<SupplierFormViewModel>();
            services.AddTransient<SuppliersViewModel>();
            services.AddTransient<WarehousesViewModel>();
            services.AddTransient<EmployeesViewModel>();
            services.AddTransient<ProductsViewModel>();
            services.AddTransient<SuppliesViewModel>();
            services.AddTransient<AppViewModel>();
            services.AddSingleton<AppView>();
        }

        protected void OnStartup(object sender, StartupEventArgs startupEventArgs)
        {
            var appView = _serviceProvider.GetService<AppView>();
            appView?.Show();
        }
    }
}
