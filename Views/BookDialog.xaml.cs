using System.Windows;

namespace Books2Gather.Views {
    public partial class BookDialog : Window {
        public BookDialog() {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
