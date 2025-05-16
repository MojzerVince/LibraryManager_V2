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

                //ID
                TextBlock id = new TextBlock { Text = "#" + book.ID.ToString(), FontSize = 16, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                bookCard.Children.Add(id);

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
                Button editButton = new Button { Content = "Edit", Name = $"ID_{book.ID}",  Width = 100, Height = 20, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };
                editButton.Click += EditBook;
                bookCard.Children.Add(editButton);

                //Delete Button
                Button deleteButton = new Button { Content = "Delete", Name = $"ID_{book.ID}", Width = 100, Height = 20, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };
                deleteButton.Click += DeleteBook;
                bookCard.Children.Add(deleteButton);

                BooksView.Children.Add(bookCard);
            }
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            Book b = new Book(Title.Text, Author.Text, (Category)Genres.SelectedIndex, Int32.Parse(Quantity.Text));
            libraryService.AddBook(b);
            MessageBox.Show("Book added successfully!");
            LoadBooks();
        }

        private void SaveEditedBook(object sender, RoutedEventArgs e)
        {
            StackPanel bookCard = (StackPanel)((Button)sender).Parent;
            string idText = ((TextBlock)bookCard.Children[0]).Text;
            int id = Int32.Parse(idText.Substring(1));

            Book b = new Book(
                ((TextBox)((StackPanel)((Button)sender).Parent).Children[1]).Text, 
                ((TextBox)((StackPanel)((Button)sender).Parent).Children[2]).Text, 
                (Category)Enum.Parse(typeof(Category), ((ComboBox)((StackPanel)((Button)sender).Parent).Children[3]).Text),
                ((TextBox)((StackPanel)((Button)sender).Parent).Children[4]).Text == "" ? 0 :
                    Int32.Parse(((TextBox)((StackPanel)((Button)sender).Parent).Children[4]).Text));
            libraryService.ModifyBook(id, b);
            MessageBox.Show("Book modified successfully!");
            LoadBooks();
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            StackPanel bookCard = (StackPanel)((Button)sender).Parent;
            string idText = ((TextBlock)bookCard.Children[0]).Text;
            int id = Int32.Parse(idText.Substring(1));

            libraryService.DeleteBook(id);
            MessageBox.Show("Book deleted successfully!");
            LoadBooks();
        }

        private void EditBook(object sender, RoutedEventArgs e)
        {
            StackPanel bookCard = (StackPanel)((Button)sender).Parent;
            string idText = ((TextBlock)bookCard.Children[0]).Text;
            string titleText = ((TextBlock)bookCard.Children[1]).Text;
            string authorText = ((TextBlock)bookCard.Children[2]).Text;
            Category cat = (Category)Enum.Parse(typeof(Category), ((TextBlock)bookCard.Children[3]).Text);
            int quantText = Int32.Parse(((TextBlock)bookCard.Children[4]).Text.Split(' ')[0]);
            bookCard.Children.Clear();

            int id = Int32.Parse(idText.Substring(1));

            TextBlock idString = new TextBlock { Text = idText, FontSize = 16, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Colors.Black), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
            bookCard.Children.Add(idString);

            TextBox title = new TextBox { Text = titleText, FontSize = 16, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Colors.Black), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
            bookCard.Children.Add(title);

            TextBox author = new TextBox { Text = authorText, FontSize = 16, Foreground = new SolidColorBrush(Colors.Black), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
            bookCard.Children.Add(author);

            ComboBox genres = new ComboBox { Width = 100, Height = 20, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };
            var categories = Enum.GetValues(typeof(Category)).Cast<Category>();
            foreach (var category in categories)
                genres.Items.Add(new ComboBoxItem { Content = category.ToString() });
            genres.SelectedIndex = (int)cat; //ebben az esetben az (int)-el kapjuk meg az indexet, mivel egy enumot használunk
            bookCard.Children.Add(genres);

            TextBox quantity = new TextBox { Text = quantText.ToString(), FontSize = 16, FontStyle = FontStyles.Italic, Foreground = new SolidColorBrush(Colors.Black), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
            quantity.TextChanged += CheckInt;
            quantity.PreviewTextInput += Integer;
            bookCard.Children.Add(quantity);

            Button saveButton = new Button { Content = "Save", Name = $"ID_{id}", Width = 100, Height = 20, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };
            saveButton.Click += SaveEditedBook;
            bookCard.Children.Add(saveButton);
        }

        private void LoadLogs()
        {
            LogsView.Items.Clear(); //Törli a régi logokat a nézetből és megelőzi a duplikációt

            foreach (var logs in libraryService.ReturnLogs())
            {
                //StackPanel
                StackPanel logCard = new StackPanel();
                logCard.Width = 300;
                logCard.HorizontalAlignment = HorizontalAlignment.Center;
                logCard.Margin = new Thickness(20);
                logCard.Background = new SolidColorBrush(Colors.Gray);
                //Log ID
                TextBlock logID = new TextBlock { Text = "#" + logs.ID.ToString(), FontSize = 16, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                logCard.Children.Add(logID);
                //Log Date
                TextBlock logDate = new TextBlock { Text = logs.Date.ToString(), FontSize = 16, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                logCard.Children.Add(logDate);
                //Log Message
                TextBlock logMessage = new TextBlock { Text = logs.Message.ToString(), FontSize = 16, FontWeight = FontWeights.DemiBold, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5) };
                logMessage.TextWrapping = TextWrapping.Wrap;
                logCard.Children.Add(logMessage);
                LogsView.Items.Add(logCard);
            }
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
            LoadLogs(); //nem túl optimalizált
        }

        //Leellenőrzi, hogy a felhasználó csak számokat írjon be a Quantity input mezőbe
        private void Integer(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(cc => Char.IsNumber(cc));
            base.OnPreviewTextInput(e);
        }
    }
}