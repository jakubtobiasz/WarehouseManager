using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WarehouseManager.UI.Commands;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.EmployeeComponent.ViewModels;
using WarehouseManager.UI.Components.ProductsComponent.ViewModels;
using WarehouseManager.UI.Components.SuppliersComponent.ViewModels;
using WarehouseManager.UI.Components.WarehousesComponent.ViewModels;
using WarehouseManager.UI.Views;

namespace WarehouseManager.UI.Components.AppComponent.ViewModels
{
    public class AppViewModel : ObservableObject
    {
        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public AppViewModel(SuppliersViewModel suppliersViewModel, WarehousesViewModel warehousesViewModel, EmployeesViewModel employeesViewModel, ProductsViewModel productsViewModel)
        {
            PageViewModels.Add(suppliersViewModel);
            PageViewModels.Add(warehousesViewModel);
            PageViewModels.Add(employeesViewModel);
            PageViewModels.Add(productsViewModel);

            CurrentPageViewModel = PageViewModels[0];
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel) p),
                        p => p is IPageViewModel && p != CurrentPageViewModel
                    );
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels ??= new List<IPageViewModel>(); }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                if (_currentPageViewModel == value) return;

                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        public void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
