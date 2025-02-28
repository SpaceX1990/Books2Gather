using Books2Gather.Models;
using Books2Gather.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Books2Gather.Repository;

namespace Books2Gather.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Author> authorRepository;
        private readonly IRepository<Genre> genreRepository;

        public ObservableCollection<Book> Books { get; set; }
        public ICollectionView FilteredBooks { get; }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                FilteredBooks.Refresh();
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }

        public MainViewModel()
        {
            bookRepository = new BookRepository();
            Books = new ObservableCollection<Book>(bookRepository.GetAll());

            FilteredBooks = CollectionViewSource.GetDefaultView(Books);
            FilteredBooks.Filter = FilterBooks;

            AddBookCommand = new RelayCommand(() => OpenBookDialog(null));
            EditBookCommand = new RelayCommand<Book>(OpenBookDialog);
            DeleteBookCommand = new RelayCommand<Book>(DeleteBook);
        }

        private bool FilterBooks(object item)
        {
            if (item is Book book)
            {
                return string.IsNullOrEmpty(SearchQuery) ||
                       book.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.ISBN.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private void OpenBookDialog(Book book)
        {
            var isNew = book == null;
            var bookToEdit = isNew ? new Book() : new Book
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                PublishingDate = book.PublishingDate,
                Prize = book.Prize
            };

            var dialog = new BookDialog()
            {
                DataContext = new BookDialogViewModel(bookToEdit, bookRepository)
            };

            if (dialog.ShowDialog() == true)
            {
                if (isNew)
                {
                    bookRepository.Insert(bookToEdit);
                    Books.Add(bookToEdit);
                }
                else
                {
                    bookRepository.Update(bookToEdit);
                    var index = Books.IndexOf(book);
                    if (index >= 0)
                    {
                        Books[index] = bookToEdit;
                    }
                }
                OnPropertyChanged(nameof(Books));
            }
        }

        private void DeleteBook(Book book)
        {
            if (book == null)
                return;

            var result = MessageBox.Show(
                $"Soll das Buch \"{book.Title}\" wirklich gelöscht werden?",
                "Löschen bestätigen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                bookRepository.Delete(book);
                Books.Remove(book);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
