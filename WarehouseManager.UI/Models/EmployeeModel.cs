using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    public class EmployeeModel : ObservableObject, IDataErrorInfo
    {
        private int _employeeId;
        private DateTime _hireDate;
        private string _firstName;
        private string _lastName;
        private string _accountNumber;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                if (value == _employeeId) return;

                _employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }

        [DataType(DataType.Date)]
        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                if (value == _hireDate) return;

                _hireDate = value;
                OnPropertyChanged(nameof(HireDate));
            }
        }

        [MaxLength(64)]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;

                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        [MaxLength(128)]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;

                _lastName = value;
                OnPropertyChanged(nameof(LastName));
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

        [NotMapped]
        public bool CanSave => this[nameof(HireDate)] is null && this[nameof(FirstName)] is null && this[nameof(LastName)] is null && this[nameof(AccountNumber)] is null;

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(FirstName))
                {
                    if (string.IsNullOrEmpty(FirstName)) return "Imię nie może pozostać pusta.";
                    if (FirstName.Length < 3) return "Minimalna długość imienia to 3 znaki.";
                    if (FirstName.Length > 64) return "Maksymalna długość imienia to 64 znaki.";
                    if (!Regex.IsMatch(FirstName, @"^[a-zA-Z-]+$"))
                        return "Imie może zawierać tylko litery od myślniki.";
                }

                if (columnName == nameof(LastName))
                {
                    if (string.IsNullOrEmpty(LastName)) return "Nazwisko nie może pozostać pusta.";
                    if (LastName.Length < 3) return "Minimalna długość nazwiska to 3 znaki.";
                    if (LastName.Length > 64) return "Maksymalna długość nazwiska to 128 znaki.";
                    if (!Regex.IsMatch(LastName, @"^[a-zA-Z-]+$"))
                        return "Imie może zawierać tylko litery od myślniki.";
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
