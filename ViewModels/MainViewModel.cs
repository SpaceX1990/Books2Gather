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
        public string SearchQuery {
            get => _searchQuery;
            set {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                FilteredBooks.Refresh();
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }

        public MainViewModel() {
            bookRepository = new BookRepository();
            authorRepository = new AuthorRepository();
            genreRepository = new GenreRepository();

            Books = new ObservableCollection<Book>(bookRepository.GetAll());
            foreach (Book book in Books) {
                book.Author = authorRepository.GetById(book.AuthorId);
                book.Genre = genreRepository.GetById(book.GenreId);
            }

            FilteredBooks = CollectionViewSource.GetDefaultView(Books);
            FilteredBooks.Filter = FilterBooks;

            AddBookCommand = new RelayCommand(() => OpenBookDialog(null));
            EditBookCommand = new RelayCommand<Book>(OpenBookDialog);
            DeleteBookCommand = new RelayCommand<Book>(DeleteBook);
        }

        private bool FilterBooks(object item) {
            if (item is Book book) {
                return string.IsNullOrEmpty(SearchQuery) ||
                       book.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.ISBN.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.Author.FirstName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.Author.LastName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.Genre.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.PublishingDate.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.Prize.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private void OpenBookDialog(Book book) {
            //book.Author = authorRepository.GetById(book.AuthorId);
            //book.Genre = genreRepository.GetById(book.GenreId);

            var isNew = book == null;
            var bookToEdit = isNew ? new Book() : new Book {
                Title = book.Title,
                ISBN = book.ISBN,
                Author = book.Author,
                Genre = book.Genre,
                PublishingDate = book.PublishingDate,
                Prize = book.Prize
            };

            var dialog = new BookDialog() {
                DataContext = new BookDialogViewModel(bookToEdit)
            };

            if (dialog.ShowDialog() == true) {
                if (isNew) {
                    Books.Add(bookToEdit);
                }
                else {
                    var index = Books.IndexOf(book);
                    if (index >= 0) {
                        Books[index] = bookToEdit;
                    }
                }
            }
        }

        private void AddBook() {

        }

        private void DeleteBook(Book book) {
            var books = bookRepository.GetAll();
            if (book == null)
                return;

            var result = MessageBox.Show(
                $"Soll das Buch \"{book.Title}\" wirklich gelöscht werden?",
                "Löschen bestätigen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes) {
                Books.Remove(book);
                bookRepository.Delete(book);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}