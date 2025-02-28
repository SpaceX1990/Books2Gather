using Books2Gather.Models;
using Books2Gather.Repository;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Books2Gather.ViewModels
{
    public class BookDialogViewModel : INotifyPropertyChanged
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Genre> _genreRepository;

        private Author? _selectedAuthor;
        private Genre? _selectedGenre;

        public Book Book { get; set; }
        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }

        public Author? SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                if (Book != null && _selectedAuthor != null)
                {
                    Book.Author = _selectedAuthor;
                    Book.AuthorId = _selectedAuthor.AuthorId;
                }
                OnPropertyChanged(nameof(SelectedAuthor));
            }
        }

        public Genre? SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                if (Book != null && _selectedGenre != null)
                {
                    Book.Genre = _selectedGenre;
                    Book.GenreId = _selectedGenre.GenreId;
                }
                OnPropertyChanged(nameof(SelectedGenre));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public BookDialogViewModel(Book book, IRepository<Book> bookRepository, IRepository<Author> authorRepository, IRepository<Genre> genreRepository)
        {
            Book = book;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;

            Authors = _authorRepository != null
                ? new ObservableCollection<Author>(_authorRepository.GetAll() ?? new List<Author>())
                : new ObservableCollection<Author>();

            Genres = _genreRepository != null
                ? new ObservableCollection<Genre>(_genreRepository.GetAll() ?? new List<Genre>())
                : new ObservableCollection<Genre>();

            SelectedAuthor = Authors.FirstOrDefault(a => a.AuthorId == Book.AuthorId);
            SelectedGenre = Genres.FirstOrDefault(g => g.GenreId == Book.GenreId);

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
