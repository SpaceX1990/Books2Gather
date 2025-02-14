using System.ComponentModel;

namespace Books2Gather.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        private string _message;
        public string Message {
            get => _message;
            set {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public MainViewModel() {
            Message = "Hallo, MVVM!";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}