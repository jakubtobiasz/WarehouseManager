using System.ComponentModel;

namespace WarehouseManager.UI.Models
{
    public abstract class ObservableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
    }
}

