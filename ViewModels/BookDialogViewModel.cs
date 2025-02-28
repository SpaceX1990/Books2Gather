using Books2Gather.Models;
using Books2Gather.Repository;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;

namespace Books2Gather.ViewModels
{
    public class BookDialogViewModel : INotifyPropertyChanged
    {
        private readonly IRepository<Book> _bookRepository;

        public Book Book { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public BookDialogViewModel(Book book, IRepository<Book> bookRepository)
        {
            Book = book;
            _bookRepository = bookRepository;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save()
        {
            if (Book.BookId == 0)
            {
                _bookRepository.Insert(Book);
            }
            else
            {
                _bookRepository.Update(Book);
            }

            OnPropertyChanged(nameof(Book));
            CloseDialog(true);
        }

        private void Cancel()
        {
            CloseDialog(false);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseDialog(bool result)
        {
            if (App.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault(w => w.IsActive) is System.Windows.Window window)
            {
                window.DialogResult = result;
                window.Close();
            }
        }
    }
}