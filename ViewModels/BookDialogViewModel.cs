using Books2Gather.Models;
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

        private DateTime _publishingDate;
        public DateTime PublishingDate
        {
            get => _publishingDate;
            set
            {
                if (_publishingDate != value)
                {
                    _publishingDate = value;
                    OnPropertyChanged(nameof(PublishingDate));
                }
            }
        }


        public BookDialogViewModel(Book book)
        {
            Book = book ?? new Book();
            Book.Author ??= new Author();
            Book.Genre ??= new Genre();

            if (book.PublishingDate == default || book.PublishingDate.Year < 100)
            {
                PublishingDate = DateTime.Today;
            }
            else
            {
                PublishingDate = book.PublishingDate;
            }



            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save()
        {
            Validation();

            CheckAuthor();
            CheckGenre();

            Book.PublishingDate = PublishingDate;

            _bookRepository.Update(Book);
            CloseDialog(true);
        }

        private void CheckGenre()
        {
            var existingGenre = _genreRepository.GetAll()
                .FirstOrDefault(g => g.Description == Book.Genre.Description);

            if (existingGenre != null)
            {
                Book.GenreId = existingGenre.GenreId;
                Book.Genre = existingGenre;
            }
            else
            {
                Book.Genre.GenreId = null;
                Book.GenreId = null;
                _genreRepository.Insert(Book.Genre);
            }

            Book.Genre = _genreRepository.GetById((int)Book.Genre.GenreId);
            Book.GenreId = Book.Genre.GenreId;
        }

        private void CheckAuthor()
        {
            var existingAuthor = _authorRepository.GetAll()
                .FirstOrDefault(a => a.FirstName == Book.Author.FirstName && a.LastName == Book.Author.LastName);

            if (existingAuthor != null)
            {
                Book.AuthorId = existingAuthor.AuthorId;
                Book.Author = existingAuthor;
            }
            else
            {
                Book.Author.AuthorId = null;
                Book.AuthorId = null;
                _authorRepository.Insert(Book.Author);
            }

            Book.Author = _authorRepository.GetById((int)Book.Author.AuthorId);
            Book.AuthorId = Book.Author.AuthorId;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(Book.Title) ||
                string.IsNullOrWhiteSpace(Book.ISBN) ||
                string.IsNullOrWhiteSpace(Book.Author.FirstName) ||
                string.IsNullOrWhiteSpace(Book.Author.LastName) ||
                string.IsNullOrWhiteSpace(Book.Genre.Description) ||
                PublishingDate == default ||
                Book.Prize <= 0)
            {
                MessageBox.Show("Fehler beim Ausfüllen.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
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