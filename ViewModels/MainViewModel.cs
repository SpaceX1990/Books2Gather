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

        public MainViewModel(IRepository<Book> bookRepo, IRepository<Author> authorRepo, IRepository<Genre> genreRepo)
        {
            bookRepository = bookRepo;
            authorRepository = authorRepo;
            genreRepository = genreRepo;

            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Books = new ObservableCollection<Book>(bookRepository.GetAll());
            foreach (Book book in Books)
            {
                book.Author = authorRepository.GetById((int)book.AuthorId);
                book.Genre = genreRepository.GetById((int)book.GenreId);
            }

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

        private void OpenBookDialog(Book book)
        {

            var isNew = book == null;
            var bookToEdit = isNew ? new Book() : new Book
            {
                Title = book.Title,
                ISBN = book.ISBN,
                Author = book.Author,
                Genre = book.Genre,
                PublishingDate = book.PublishingDate,
                Prize = book.Prize
            };

            var dialog = new BookDialog()
            {
                DataContext = new BookDialogViewModel(bookToEdit)
            };

            if (dialog.ShowDialog() == true)
            {
                if (isNew)
                {
                    Books.Add(bookToEdit);
                    bookRepository.Insert(bookToEdit);
                }
                else
                {
                    var index = Books.IndexOf(book);
                    if (index >= 0)
                    {
                        Books[index] = bookToEdit;
                        bookRepository.Update(bookToEdit);
                    }
                }
                FilteredBooks.Refresh();
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
                bookRepository.Delete(book);
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