using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.EmployeeComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.EmployeeComponent.ViewModels
{
    /// <summary>
    /// The EmployeeFormViewModel class.
    /// Contains methods for EmployeeFormView view.
    /// </summary>
    public class EmployeesViewModel : ObservableObject, IPageViewModel
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<EmployeeModel> _employees;

        private ICommand _addEmployeeCommand;
        private ICommand _editEmployeeCommand;
        private ICommand _removeEmployeeCommand;
        private ICommand _showManagedWarehousesCommand;

        private Window _employeeFormView;

        public EmployeesViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            FillData();
        }

        #region Properties

        public string Name => "Pracownicy";

        /// <summary>
        /// Holds a collection of Employees
        /// </summary>
        public ObservableCollection<EmployeeModel> Employees
        {
            get => _employees;
            set
            {
                if (_employees == value) return;

                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private Window EmployeeFormView
        {
            get => _employeeFormView;
            set
            {
                if (_employeeFormView == value) return;

                _employeeFormView = value;
                OnPropertyChanged(nameof(EmployeeFormView));
            }
        }

        #endregion

        #region Properties - Commands

        private ICommand AddEmployeeCommand
        {
            get
            {
                if (_addEmployeeCommand is null)
                {
                    _addEmployeeCommand = new RelayCommand(
                        _ => AddEmployee(),
                        _ => EmployeeFormView is null
                    );
                }

                return _addEmployeeCommand;
            }
        }

        private ICommand EditEmployeeCommand
        {
            get
            {
                if (_editEmployeeCommand is null)
                {
                    _editEmployeeCommand = new RelayCommand(
                        e => EditEmployee(e as EmployeeModel)
                    );
                }

                return _editEmployeeCommand;
            }
        }

        private ICommand RemoveEmployeeCommand
        {
            get
            {
                if (_removeEmployeeCommand is null)
                {
                    _removeEmployeeCommand = new RelayCommand(
                        e => RemoveEmployee(e as EmployeeModel)
                    );
                }

                return _removeEmployeeCommand;
            }
        }

        private ICommand ShowManagedWarehousesCommand
        {
            get
            {
                if (_showManagedWarehousesCommand is null)
                {
                    _showManagedWarehousesCommand = new RelayCommand(
                        e => ShowManagedWarehouses(e as EmployeeModel)
                    );
                }

                return _showManagedWarehousesCommand;
            }
        }

        #endregion

        #region Methods

        private void AddEmployee()
        {
            EmployeeFormView = new EmployeeFormView(new EmployeeFormViewModel(_dbContext));
            EmployeeFormView.Show();
            EmployeeFormView.Closed += OnFormClosed;
        }

        private void EditEmployee(EmployeeModel employee)
        {
            EmployeeFormView = new EmployeeFormView(new EmployeeFormViewModel(_dbContext, employee));
            EmployeeFormView.Show();
            EmployeeFormView.Closed += OnFormClosed;
        }

        private void RemoveEmployee(EmployeeModel employee)
        {
            if (employee.ManagedWarehouses?.Any() ?? false)
            {
                MessageBox.Show("Pracownik nie może zostać usunięty jeśli zarządza jakimś magazynem!");
                return;
            }

            var messageBoxResult = MessageBox.Show($"Czy na pewno chcesz usunąć pracownika {employee.FirstName} {employee.LastName}?",
                "Usuwanie pracownika", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.No) return;

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            FillData();
        }

        private void ShowManagedWarehouses(EmployeeModel employee)
        {
            var employeeManagedWarehousesView =
                new EmployeeManagedWarehousesView(new EmployeeManagedWarehousesViewModel(employee));
            employeeManagedWarehousesView.Show();
        }

        private void OnFormClosed(object sender, EventArgs args)
        {
            EmployeeFormView = null;
            Employees = null;
            FillData();
        }

        private void FillData()
        {
            Employees = new ObservableCollection<EmployeeModel>(_dbContext.Employees);
        }

        #endregion
    }
}
