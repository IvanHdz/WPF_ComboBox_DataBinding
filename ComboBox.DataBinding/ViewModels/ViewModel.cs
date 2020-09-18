using System.ComponentModel;

namespace ComboBox.DataBinding.ViewModels
{
    /// <summary>
    /// Provides common functionality for ViewModel classes.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}