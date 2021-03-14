using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Stunts
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<TValue>(ref TValue field, TValue value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            RaisePropertyChanges(propertyName);
            return true;
        }

        protected void RaisePropertyChanges([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}