using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LibraryManager_V2.Services;
using LibraryManager_V2.Repositories;
using LibraryManager_V2.Models;

namespace LibraryManager_V2_Admin_UI
{
    public partial class MainWindow : Window
    {
        LibraryService libraryService = new LibraryService(new BookRepository());

        public MainWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = Enum.GetValues(typeof(Category)).Cast<Category>();
            foreach (var category in categories)
            {
                Genres.Items.Add(new ComboBoxItem { Content = category.ToString() });
            }
        }

        private void Integer(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(cc => Char.IsNumber(cc));
            base.OnPreviewTextInput(e);
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {

        }
    }
}