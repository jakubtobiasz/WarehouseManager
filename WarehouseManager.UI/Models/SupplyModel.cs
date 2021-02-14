using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    public class SupplyModel : ObservableObject, IDataErrorInfo
    {
        private int _supplyId;
        private DateTime _statusUpdateTime;
        private SupplyStatusEnum _supplyStatus;

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

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(SupplyStatus))
                {
                    if (SupplyStatus == default) return "Zamówienie nie może pozostać bez statusu.";
                }

                return null;
            }
        }

        public enum SupplyStatusEnum
        {
            Added,
            InTransit,
            Delivered
        }
    }
}
