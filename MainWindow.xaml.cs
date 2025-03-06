using Books2Gather.Repository;
using Books2Gather.ViewModels;
using System.Windows;
using System.Windows.Media;

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

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "🔍 Suche...")
            {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "🔍 Suche...";
                SearchBox.Foreground = Brushes.Gray;
            }
        }

    }
}