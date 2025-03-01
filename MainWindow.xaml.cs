using Books2Gather.Repository;
using Books2Gather.ViewModels;
using System.Windows;

namespace Books2Gather {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookRepository _bookRepository = new BookRepository();
        private AuthorRepository _authorRepository = new AuthorRepository();
        private GenreRepository _genreRepository = new GenreRepository();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(_bookRepository, _authorRepository, _genreRepository);
        }
    }
}