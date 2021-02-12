using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    public class SupplierModel : ObservableObject
    {
        private int _supplierId;
        private string _name;
        private string _nip;
        private string _accountNumber;

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

        [MinLength(10)]
        [MaxLength(10)]
        public string NIP
        {
            get => _nip;
            set
            {
                if (value == _nip) return;

                _nip = value;
                OnPropertyChanged(nameof(NIP));
            }
        }

        [MinLength(26)]
        [MaxLength(26)]
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (value == _accountNumber) return;

                _accountNumber = value;
                OnPropertyChanged(nameof(AccountNumber));
            }
        }
    }
}
