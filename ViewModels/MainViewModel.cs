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

            Books = new ObservableCollection<Book>
            {
                new Book
                {
                    Title = "Der Hobbit",
                    ISBN = "123456789",
                    Authors = new List<Author>
                    {
                        new Author { FirstName = "J.R.R.", LastName = "Tolkien", BirthDate = new DateOnly(1892, 1, 3), Biography = "Blah", Nationality = "deutsch" }
                    },
                    Genres = new List<Genre>
                    {
                        new Genre { Description = "Fantasy" }
                    },
                    PublishingDate = new DateOnly(1937, 9, 21),
                    Prize = 12.99m
                },
                new Book
                {
                    Title = "1984",
                    ISBN = "987654321",
                    Authors = new List<Author>
                    {
                        new Author { FirstName = "George", LastName = "Orwell", BirthDate = new DateOnly(1903, 6, 25), Biography = "Blah", Nationality = "deutsch" }
                    },
                    Genres = new List<Genre>
                    {
                        new Genre { Description = "Dystopie" }
                    },
                    PublishingDate = new DateOnly(1949, 6, 8),
                    Prize = 9.99m
                }
            };

            foreach (Book book in Books) {
                bookRepository.Insert(book);
                foreach (Author author in book.Authors) {
                    authorRepository.Insert(author);
                }
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
                       book.Authors.Any(a =>
                           a.FirstName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                           a.LastName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                           $"{a.FirstName} {a.LastName}".Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) ||
                       book.Genres.Any(g => g.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }

        private void OpenBookDialog(Book book) {
            var isNew = book == null;
            var bookToEdit = isNew ? new Book() : new Book {
                Title = book.Title,
                ISBN = book.ISBN,
                Authors = new List<Author>(book.Authors),
                Genres = new List<Genre>(book.Genres),
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
                //Books.Remove(book);
                bookRepository.Delete(book);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}