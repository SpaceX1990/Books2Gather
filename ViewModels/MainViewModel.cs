using Books2Gather.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace Books2Gather.ViewModels {
    public class MainViewModel {
        public ObservableCollection<Book> Books { get; set; }
        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }

        public MainViewModel() {
            Books = new ObservableCollection<Book>
            {
            new Book { Title = "Der Hobbit", ISBN = "123456789"},
            new Book { Title = "1984", ISBN = "987654321"}
        };

            EditBookCommand = new RelayCommand<Book>(EditBook);
            DeleteBookCommand = new RelayCommand<Book>(DeleteBook);
        }

        private void EditBook(Book book) {
            MessageBox.Show($"Bearbeiten: {book.Title}");
        }

        private void DeleteBook(Book book) {
            MessageBox.Show($"Löschen: {book.Title}");
        }
    }

}