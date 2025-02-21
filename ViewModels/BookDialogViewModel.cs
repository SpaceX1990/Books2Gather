using Books2Gather.Models;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;

namespace Books2Gather.ViewModels {
    public class BookDialogViewModel : INotifyPropertyChanged {
        public Book Book { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public BookDialogViewModel(Book book) {
            Book = book;
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save() {
            // Dialog schließen, falls benötigt (über Window)
        }

        private void Cancel() {
            // Abbrechen-Logik
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
