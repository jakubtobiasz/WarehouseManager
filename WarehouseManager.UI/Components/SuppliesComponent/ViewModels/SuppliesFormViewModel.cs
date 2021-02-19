using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.SuppliesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.SuppliesComponent.ViewModels
{
    /// <summary>
    /// The SuppliesFormViewModel class.
    /// Contains methods for SuppliesFormView view.
    /// </summary>
    public class SuppliesFormViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        private ICommand _saveCommand;

        public SuppliesFormViewModel(AppDbContext dbContext, SupplyModel supply = null)
        {
            _dbContext = dbContext;
            Model = supply ?? new SupplyModel();
            Suppliers = dbContext.Suppliers.ToList();
            Warehouses = dbContext.Warehouses.ToList();
        }

        #region Properties

        public SupplyModel Model { get; set; }
        public ICollection<SupplierModel> Suppliers { get; set; }
        public ICollection<WarehouseModel> Warehouses { get; set; }

        public Dictionary<SupplyModel.SupplyStatusEnum, string> Statuses =>
            new()
            {
                {  SupplyModel.SupplyStatusEnum.Added, "Dodany" },
                {  SupplyModel.SupplyStatusEnum.InTransit, "W transporcie" },
                {  SupplyModel.SupplyStatusEnum.Delivered, "Dostarczony" },
                { SupplyModel.SupplyStatusEnum.Canceled, "Anulowany" }
            };

        #endregion

        #region Properties - Command

        /// <summary>
        /// Saves changes when conditions met.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand is null)
                {
                    _saveCommand = new RelayCommand(
                        _ => Save(),
                        _ => Model.CanSave
                    );
                }

                return _saveCommand;
            }
        }

        #endregion

        #region Methods

        private void Save()
        {
            if (Model.SupplyId == default)
            {
                _dbContext.Supplies.Add(Model);
            }
            else
            {
                Model.StatusUpdateTime = DateTime.Now;
                _dbContext.Supplies.Update(Model);
            }

            _dbContext.SaveChanges();

            var window = Application.Current.Windows.OfType<SupplyFormView>().SingleOrDefault(s => s.IsActive);
            window?.Close();
        }

        /// <summary>
        /// Reloads a Model entry.
        /// </summary>
        public void ReloadModel()
        {
            _dbContext.Entry(Model).Reload();
        }

        #endregion
    }
}
