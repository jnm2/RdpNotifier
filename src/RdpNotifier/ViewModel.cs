using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RdpNotifier
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool Set<T>(ref T location, T value, [CallerMemberName] string? propertyName = null)
        {
            if (RuntimeHelpers.Equals(location, value)) return false;
            location = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
