using Books2Gather.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Books2Gather.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
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

        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }

        public MainViewModel() {
            Books = new ObservableCollection<Book>
            {
                new Book
                {
                    Title = "Der Hobbit",
                    ISBN = "123456789",
                    AuthorList = new List<Author>
                    {
                        new Author { Firstname = "J.R.R.", Lastname = "Tolkien", Birthday = new DateOnly(1892, 1, 3) }
                    },
                    GenreList = new List<Genre>
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
                    AuthorList = new List<Author>
                    {
                        new Author { Firstname = "George", Lastname = "Orwell", Birthday = new DateOnly(1903, 6, 25) }
                    },
                    GenreList = new List<Genre>
                    {
                        new Genre { Description = "Dystopie" }
                    },
                    PublishingDate = new DateOnly(1949, 6, 8),
                    Prize = 9.99m
                }
            };

            FilteredBooks = CollectionViewSource.GetDefaultView(Books);
            FilteredBooks.Filter = FilterBooks;

            EditBookCommand = new RelayCommand<Book>(EditBook);
            DeleteBookCommand = new RelayCommand<Book>(DeleteBook);
        }

        private bool FilterBooks(object item) {
            if (item is Book book) {
                return string.IsNullOrEmpty(SearchQuery) ||
                       book.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.ISBN.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.AuthorList.Any(a =>
                           a.Firstname.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                           a.Lastname.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                           $"{a.Firstname} {a.Lastname}".Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) ||
                       book.GenreList.Any(g => g.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }

        private void EditBook(Book book) {
            throw new NotImplementedException();
        }

        private void DeleteBook(Book book) {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
