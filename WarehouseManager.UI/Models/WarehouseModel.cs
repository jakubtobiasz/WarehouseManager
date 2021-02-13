using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    public class WarehouseModel : ObservableObject, IDataErrorInfo
    {
        private int _warehouseId;
        private string _name;
        private string _cityName;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarehouseId
        {
            get => _warehouseId;
            set
            {
                if (value == _warehouseId) return;

                _warehouseId = value;
                OnPropertyChanged(nameof(WarehouseId));
            }
        }

        [MaxLength(64)]
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [MaxLength(64)]
        public string CityName
        {
            get => _cityName;
            set
            {
                if (value == _cityName) return;

                _cityName = value;
                OnPropertyChanged(nameof(CityName));
            }
        }

        [NotMapped]
        public bool CanSave => this[nameof(Name)] is null;

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Name))
                {
                    if (string.IsNullOrEmpty(Name)) return "Nazwa nie może pozostać pusta.";
                    if (Name.Length < 3) return "Minimalna długość nazwy to 3 znaki.";
                    if (Name.Length > 64) return "Maksymalna długość nazwy to 64 znaki.";
                }

                if (columnName == nameof(CityName))
                {
                    if (string.IsNullOrEmpty(CityName)) return "Nazwa nie może pozostać pusta.";
                    if (CityName.Length < 3) return "Minimalna długość nazwy to 3 znaki.";
                    if (CityName.Length > 64) return "Maksymalna długość nazwy to 64 znaki.";
                }

                return null;
            }
        }
    }
}
