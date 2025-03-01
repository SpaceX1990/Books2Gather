using Books2Gather.Models;
using Books2Gather.Repository;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Books2Gather.ViewModels
{
    public class BookDialogViewModel : INotifyPropertyChanged
    {
        public Book Book { get; set; }

        private readonly IRepository<Book> _bookRepository = new BookRepository();
        private readonly IRepository<Author> _authorRepository = new AuthorRepository();
        private readonly IRepository<Genre> _genreRepository = new GenreRepository();

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public BookDialogViewModel(Book book)
        {
            Book = book ?? new Book();
            Book.Author ??= new Author();
            Book.Genre ??= new Genre();

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }


        private void Save()
        {
            if (Book.Author == null)
                Book.Author = new Author();

            if (Book.Genre == null)
                Book.Genre = new Genre();

            Book.Title = Book.Title?.Trim();
            Book.ISBN = Book.ISBN?.Trim();
            Book.Author.FirstName = Book.Author.FirstName?.Trim();
            Book.Author.LastName = Book.Author.LastName?.Trim();
            Book.Genre.Description = Book.Genre.Description?.Trim();

            if (Book.PublishingDate == default)
                Book.PublishingDate = DateOnly.FromDateTime(DateTime.Today);

            if (Book.Prize <= 0)
                Book.Prize = 0.01m;

            // Validierung
            if (string.IsNullOrWhiteSpace(Book.Title) ||
                string.IsNullOrWhiteSpace(Book.ISBN) ||
                string.IsNullOrWhiteSpace(Book.Author.FirstName) ||
                string.IsNullOrWhiteSpace(Book.Author.LastName) ||
                string.IsNullOrWhiteSpace(Book.Genre.Description) ||
                Book.PublishingDate == default ||
                Book.Prize <= 0)
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existingAuthor = _authorRepository.GetAll()
                .FirstOrDefault(a => a.FirstName == Book.Author.FirstName && a.LastName == Book.Author.LastName);

            if (existingAuthor != null)
            {
                Book.AuthorId = existingAuthor.AuthorId;
                Book.Author = existingAuthor;
            }
            else
            {
                _authorRepository.Insert(Book.Author);
            }

            var existingGenre = _genreRepository.GetAll()
                .FirstOrDefault(g => g.Description == Book.Genre.Description);

            if (existingGenre != null)
            {
                Book.GenreId = existingGenre.GenreId;
                Book.Genre = existingGenre;
            }
            else
            {
                _genreRepository.Insert(Book.Genre);
            }

            if (Book.BookId == 0)
            {
                _bookRepository.Insert(Book);
            }
            else
            {
                _bookRepository.Update(Book);
            }

            CloseDialog(true);
        }

        private void Cancel()
        {
            CloseDialog(false);
        }

        private void CloseDialog(bool result)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Views.BookDialog dialog)
                {
                    dialog.DialogResult = result;
                    dialog.Close();
                    break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}