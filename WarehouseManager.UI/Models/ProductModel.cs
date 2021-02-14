using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    public class ProductModel : ObservableObject, IDataErrorInfo
    {
        private int _productId;
        private string _name;
        private string _description;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId) return;

                _productId = value;
                OnPropertyChanged(nameof(ProductId));
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

        [MaxLength(256)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;

                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        [NotMapped]
        public bool CanSave => this[nameof(Name)] is null && this[nameof(Description)] is null;

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

                return null;
            }
        }
    }
}
