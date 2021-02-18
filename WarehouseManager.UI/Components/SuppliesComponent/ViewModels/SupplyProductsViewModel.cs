using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.UI.Commands;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.SuppliesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.SuppliesComponent.ViewModels
{
    public class SupplyProductsViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;
        private readonly SupplyModel _supply;

        private ICommand _addCommand;
        private ICommand _removeCommand;

        public SupplyProductsViewModel(AppDbContext dbContext, SupplyModel supply)
        {
            _dbContext = dbContext;
            _supply = supply;

            FillData();
        }

        #region Properties

        public ObservableCollection<ProductToSupplyModel> Products { get; set; }

        #endregion

        #region Properties - Commands

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand is null)
                {
                    _addCommand = new RelayCommand(
                        _ => Add(),
                        p => (p as ProductToSupplyModel)?.Supply.SupplyStatus == SupplyModel.SupplyStatusEnum.Added
                    );
                }

                return _addCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand is null)
                {
                    _removeCommand = new RelayCommand(
                        p => Remove(p as ProductToSupplyModel),
                        p => (p as ProductToSupplyModel)?.Supply.SupplyStatus == SupplyModel.SupplyStatusEnum.Added
                    );
                }

                return _removeCommand;
            }
        }

        #endregion

        #region Methods

        public void Add()
        {
            var form = new AddProductView(new AddProductViewModel(_dbContext, _supply));
            form.Show();
            form.Closed += OnFormClosed;
        }

        private void OnFormClosed(object sender, EventArgs args)
        {
            _dbContext.Entry(_supply).Reload();
            FillData();
        }

        public void Remove(ProductToSupplyModel model)
        {
            _dbContext.ProductsToSupplies.Remove(model);
            _dbContext.SaveChanges();
            _dbContext.Entry(_supply).Reload();
            FillData();
        }

        public void FillData()
        {
            Products = null;
            Products = _supply.Products;
        }

        #endregion
    }
}
