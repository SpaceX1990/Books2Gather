using Books2Gather.Models;
using Books2Gather.Repository;
using Books2Gather.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Books2Gather.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Genre> _genreRepository;

        public ObservableCollection<Book> Books { get; set; }
        public ICollectionView FilteredBooks { get; set; }

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

        public MainViewModel(IRepository<Book> bookRepo, IRepository<Author> authorRepo, IRepository<Genre> genreRepo)
        {
            _bookRepository = bookRepo;
            _authorRepository = authorRepo;
            _genreRepository = genreRepo;

            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            LoadBooks();

            AddBookCommand = new RelayCommand(AddBookDialog);
            EditBookCommand = new RelayCommand<Book>(UpdateBookDialog);
            DeleteBookCommand = new RelayCommand<Book>(DeleteBook);
        }

        private void LoadBooks()
        {
            Books = new ObservableCollection<Book>(_bookRepository.GetAll());
            foreach (Book book in Books)
            {
                book.Author = _authorRepository.GetById((int)book.AuthorId);
                book.Genre = _genreRepository.GetById((int)book.GenreId);
            }

            FilteredBooks = CollectionViewSource.GetDefaultView(Books);
            FilteredBooks.Filter = FilterBooks;
            FilteredBooks.Refresh();
        }

        private bool FilterBooks(object item)
        {
            if (item is Book book)
            {
                var culture = CultureInfo.CurrentCulture;
                return string.IsNullOrEmpty(SearchQuery) ||
                       book.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.ISBN.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.Author.FirstName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.Author.LastName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.Genre.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       book.PublishingDate.ToString("d", culture).Contains(SearchQuery) ||
                       book.Prize.ToString("N2", culture).Contains(SearchQuery);
            }
            return false;
        }

        private void AddBookDialog()
        {
            var book = new Book();

            var dialog = new BookDialog()
            {
                DataContext = new BookDialogViewModel(book)
            };

            if (dialog.ShowDialog() == true)
            {
                book.Author = _authorRepository.GetById((int)book.AuthorId);
                book.Genre = _genreRepository.GetById((int)book.GenreId);

                Books.Add(book);

                FilteredBooks.Refresh();
            }
        }

        private void UpdateBookDialog(Book book)
        {
            var dialog = new BookDialog()
            {
                DataContext = new BookDialogViewModel(book)
            };

            if (dialog.ShowDialog() == true)
            {

            }
        }

        private void DeleteBook(Book book)
        {
            if (book == null)
                return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the book \"{book.Title}\"?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Books.Remove(book);
                _bookRepository.Delete(book);
                FilteredBooks.Refresh();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}