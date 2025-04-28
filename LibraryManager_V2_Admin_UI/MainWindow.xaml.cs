using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
            libraryService.LoadBooks();
            LoadBooks();
            libraryService.CreateCustomLog("Admin login");
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = TimeSpan.FromSeconds(1);
            t.Tick += TimerTick;
            t.Start();
        }

        private void LoadCategories()
        {
            var categories = Enum.GetValues(typeof(Category)).Cast<Category>();
            foreach (var category in categories)
                Genres.Items.Add(new ComboBoxItem { Content = category.ToString() });
        }

        private void LoadBooks()
        {
            BooksView.Children.Clear(); //Törli a régi könyveket a nézetből és megelőzi a duplikációt
            var books = libraryService.rep.GetAllBooks();
            foreach (var book in books)
            {
                //StackPanel
                StackPanel bookCard = new StackPanel();
                bookCard.Margin = new Thickness(10);
                bookCard.Background = new SolidColorBrush(Colors.Gray);

                //Title
                TextBlock title = new TextBlock { Text = book.Title, FontSize = 16, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                bookCard.Children.Add(title);

                //Author
                TextBlock author = new TextBlock { Text = book.Author, FontSize = 16, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                bookCard.Children.Add(author);

                //Category
                TextBlock category = new TextBlock { Text = book.Genre.ToString(), FontSize = 16, FontWeight = FontWeights.DemiBold, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                bookCard.Children.Add(category);

                //Quantity
                TextBlock quantity = new TextBlock { Text = $"{book.Quantity.ToString()} db", FontSize = 16, FontStyle = FontStyles.Italic, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                bookCard.Children.Add(quantity);

                //Edit Button
                Button editButton = new Button { Content = "Edit", Width = 100, Height = 20, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };
                bookCard.Children.Add(editButton);

                //Delete Button
                Button deleteButton = new Button { Content = "Delete", Name = $"ID_{book.ID}", Width = 100, Height = 20, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };
                deleteButton.Click += DeleteBook;
                bookCard.Children.Add(deleteButton);

                BooksView.Children.Add(bookCard);
            }
        }

        //Leellenőrzi, hogy a felhasználó csak számokat írjon be a Quantity input mezőbe
        private void Integer(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(cc => Char.IsNumber(cc));
            base.OnPreviewTextInput(e);
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            Book b = new Book(Title.Text, Author.Text, (Category)Genres.SelectedIndex, Int32.Parse(Quantity.Text));
            libraryService.AddBook(b);
            MessageBox.Show("Book added successfully!");
            LoadBooks();
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            string[] data = deleteButton.Name.Split('_');
            int id = Int32.Parse(data[1]);
            libraryService.DeleteBook(id);
            MessageBox.Show("Book deleted successfully!");
            LoadBooks();
        }

        private void CheckInt(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Quantity.Text != "" && int.Parse(Quantity.Text) > 2147483647) { } //2,147,483,647
            }
            catch
            {
                Quantity.Text = string.Empty;
                MessageBox.Show("Please enter a valid quantity!", "LibraryManager_V2", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToString();
        }
    }
}