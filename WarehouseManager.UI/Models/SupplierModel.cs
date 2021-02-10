using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.UI.Models
{
    public class SupplierModel : ObservableObject
    {
        private int _supplierId;
        private string _name;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId
        {
            get => _supplierId;
            set
            {
                if (value == _supplierId) return;

                _supplierId = value;
                OnPropertyChanged("SupplierId");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;

                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
