using System.ComponentModel;

namespace WarehouseManager.UI.Common
{
    /// <summary>
    /// The abstract class for ObservableObjects.
    /// Makes it easier to implement raising PropertyChanged event.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
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

