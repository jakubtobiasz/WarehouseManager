using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    /// <summary>
    /// The model representing the Supply entity.
    /// </summary>
    public class SupplyModel : ObservableObject, IDataErrorInfo
    {
        private int _supplyId;
        private DateTime _statusUpdateTime = DateTime.Now;
        private SupplyStatusEnum _supplyStatus = SupplyStatusEnum.Added;
        private ObservableCollection<ProductToSupplyModel> _products;
        private int _supplierId;
        private SupplierModel _supplier;
        private int _warehouseId;
        private WarehouseModel _warehouse;

        [Key]
        public int SupplyId
        {
            get => _supplyId;
            set
            {
                if (_supplyId == value) return;

                _supplyId = value;
                OnPropertyChanged(nameof(SupplyId));
            }
        }

        public DateTime StatusUpdateTime
        {
            get => _statusUpdateTime;
            set
            {
                if (_statusUpdateTime == value) return;

                _statusUpdateTime = value;
                OnPropertyChanged(nameof(StatusUpdateTime));
            }
        }

        public virtual ObservableCollection<ProductToSupplyModel> Products
        {
            get => _products;
            set
            {
                if (_products == value) return;

                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        [ForeignKey(nameof(Supplier))]
        public int SupplierId
        {
            get => _supplierId;
            set
            {
                if (_supplierId == value) return;

                _supplierId = value;
                OnPropertyChanged(nameof(SupplierId));
            }
        }

        public virtual SupplierModel Supplier
        {
            get => _supplier;
            set
            {
                if (_supplier == value) return;

                _supplier = value;
                OnPropertyChanged(nameof(Supplier));
            }
        }
        
        [ForeignKey(nameof(Warehouse))]
        public int WarehouseId
        {
            get => _warehouseId;
            set
            {
                if (_warehouseId == value) return;

                _warehouseId = value;
                OnPropertyChanged(nameof(WarehouseId));
            }
        }

        public virtual WarehouseModel Warehouse
        {
            get => _warehouse;
            set
            {
                if (_warehouse == value) return;

                _warehouse = value;
                OnPropertyChanged(nameof(Warehouse));
            }
        }

        public SupplyStatusEnum SupplyStatus
        {
            get => _supplyStatus;
            set
            {
                if (_supplyStatus == value) return;

                _supplyStatus = value;
                OnPropertyChanged(nameof(SupplyStatus));
            }
        }

        [NotMapped]
        public string SupplyStatusName
        {
            get
            {
                return SupplyStatus switch
                {
                    SupplyStatusEnum.Added => "Dodana",
                    SupplyStatusEnum.Canceled => "Anulowana",
                    SupplyStatusEnum.Delivered => "Dostarczona",
                    SupplyStatusEnum.InTransit => "W transporcie",
                    _ => "n/a"
                };
            }
        }

        [NotMapped] public bool CanSave => true;

        public string Error => null;

        public string this[string columnName] => null;

        public enum SupplyStatusEnum
        {
            Added,
            InTransit,
            Delivered,
            Canceled,
        }
    }
}
