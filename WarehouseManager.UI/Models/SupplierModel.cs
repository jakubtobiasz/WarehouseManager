using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    public class SupplierModel : ObservableObject, IDataErrorInfo
    {
        private int _supplierId;
        private string _name;
        private string _nip;
        private string _accountNumber;
        private ObservableCollection<SupplyModel> _supplies;

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

        [MaxLength(64)]
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

        public virtual ObservableCollection<SupplyModel> Supplies
        {
            get => _supplies;
            set
            {
                if (_supplies == value) return;

                _supplies = value;
                OnPropertyChanged(nameof(Supplies));
            }
        }

        [NotMapped]
        public bool CanSave => this[nameof(Name)] is null && this[nameof(NIP)] is null && this[nameof(AccountNumber)] is null;

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

                if (columnName == nameof(NIP))
                {
                    if ($"{NIP}".Length != 10) return "Długość numeru NIP musi wynosić 10";
                    if (!Regex.IsMatch(NIP, @"^[\d]+$")) return "NIP musi być liczbą.";
                }

                if (columnName == nameof(AccountNumber))
                {
                    if ($"{AccountNumber}".Length != 26) return "Długośc numeru rachunku musi wynosić 26";
                    if (!Regex.IsMatch(AccountNumber, @"^[\d]+$")) return "Numer rachunku musi być liczbą.";
                }

                return null;
            }
        }
    }
}
