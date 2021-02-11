using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.ViewModels;
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

            services.AddTransient<SupplierAddFormViewModel>();
            services.AddTransient<SupplierAddFormView>();
            services.AddTransient<SuppliersViewModel>();
            services.AddTransient<RecipientViewModel>();
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
