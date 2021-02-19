using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.EmployeeComponent.ViewModels;
using WarehouseManager.UI.Components.ProductsComponent.ViewModels;
using WarehouseManager.UI.Components.SuppliersComponent.ViewModels;
using WarehouseManager.UI.Components.SuppliesComponent.ViewModels;
using WarehouseManager.UI.Components.WarehousesComponent.ViewModels;

namespace WarehouseManager.UI.Components.AppComponent.ViewModels
{
    /// <summary>
    /// The AppViewModel class.
    /// Contains methods for AppView view.
    /// </summary>
    public class AppViewModel : ObservableObject
    {
        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public AppViewModel(SuppliersViewModel suppliersViewModel, WarehousesViewModel warehousesViewModel, EmployeesViewModel employeesViewModel, ProductsViewModel productsViewModel, SuppliesViewModel suppliesViewModel)
        {
            PageViewModels.Add(suppliersViewModel);
            PageViewModels.Add(warehousesViewModel);
            PageViewModels.Add(employeesViewModel);
            PageViewModels.Add(productsViewModel);
            PageViewModels.Add(suppliesViewModel);

            CurrentPageViewModel = PageViewModels[0];
        }

        /// <summary>
        /// Changes current page to the passed one.
        /// </summary>
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangePage((IPageViewModel) p),
                        p => p is IPageViewModel && p != CurrentPageViewModel
                    );
                }

                return _changePageCommand;
            }
        }

        /// <summary>
        /// Holds all pages.
        /// </summary>
        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels ??= new List<IPageViewModel>(); }
        }

        /// <summary>
        /// Holds current page.
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                if (_currentPageViewModel == value) return;

                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        public void ChangePage(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
