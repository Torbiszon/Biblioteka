using Azure.Identity;
using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BooksWindow : Window
    {
        private readonly AppDbContext _dbcontext;
        private readonly int _id;
        public BooksWindow(AppDbContext dbcontext, int id)
        {
            InitializeComponent();
            _dbcontext = dbcontext;
            _id = id;
            var books= _dbcontext.Books.Include(o=>o.Author).ToList();
            var employee = _dbcontext.Users.OfType<Employee>().FirstOrDefault(o => o.Id.Equals(_id));
            var booksLast = books.LastOrDefault();
            var stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(10) };
            panel.Children.Add(stackPanel);
            foreach (var book in books)
            {
                if (stackPanel.Children.Count > 1)
                {                   
                    stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(10) };
                    panel.Children.Add(stackPanel);
                }
                if (stackPanel.Children.Count > 0 && books.Count%2!=0 && book.Equals(booksLast))
                {
                    stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(10) };
                    panel.Children.Add(stackPanel);
                }

                var grid = new Grid();
                var image = new Image() { Source =  new BitmapImage( new Uri(@"/Images/book.png", UriKind.Relative)), Height = 240, Width = 350, Margin=new Thickness(10,0,10,0) };
                var bookHolder = new StackPanel() { Height= 240, Width= 350 , Margin= new Thickness(0)};
                var infoGrid = new Grid();
                infoGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30)});
                infoGrid.RowDefinitions.Add(new RowDefinition());
                infoGrid.RowDefinitions.Add(new RowDefinition());
                infoGrid.ColumnDefinitions.Add(new ColumnDefinition());
                infoGrid.ColumnDefinitions.Add(new ColumnDefinition());

                var title = new TextBlock() { Text = $"Tytuł: {book.Title}", HorizontalAlignment=HorizontalAlignment.Left, Margin=new Thickness(45,10,0,0)};
                var autor = new TextBlock() { Text = $"Autor: {book.Author.Fullname}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(45, 0, 0, 0) };
                var release = new TextBlock() { Text = $"Rok wydania: {book.ReleaseYear}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(45, 0, 0, 0) };
                var button = new Button() { Content = "Wypożycz", Background = Brushes.LightGray , Height= 25, Width=bookHolder.Width/3, HorizontalAlignment = HorizontalAlignment.Left, Margin=new Thickness(20,0,0,0), Tag=book.Id};
                button.Click += new RoutedEventHandler(Borrow_Click);
                infoGrid.Children.Add(title);
                infoGrid.Children.Add(autor);
                infoGrid.Children.Add(release);
                infoGrid.Children.Add(button);
                Grid.SetRow(title, 0);
                Grid.SetRow(autor, 1);
                Grid.SetRow(release, 2);
                Grid.SetColumn(button, 1);
                Grid.SetRowSpan(button, 2);
                if(employee!=null)
                {
                    button.IsEnabled = false;
                    var button1 = new Button() { Content = "Usuń", Background = Brushes.IndianRed, Height = 25, Width = bookHolder.Width / 3, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(20, 0, 0, 0), Tag= book.Id };
                    button1.Click += new RoutedEventHandler(Delete_Click);
                    infoGrid.Children.Add(button1);
                    Grid.SetColumn(button1, 1);
                    Grid.SetRow(button1, 3);
                }
                grid.Children.Add(image);
                grid.Children.Add(bookHolder);
                grid.Children.Add(infoGrid);
                stackPanel.Children.Add(grid);                                
            }           
        }
        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse((sender as Button).Tag.ToString());
            var book = _dbcontext.Books.FirstOrDefault(x => x.Id == id);
            var user = _dbcontext.Users.OfType<User>().FirstOrDefault(o => o.Id.Equals(_id));
            if(user.BorrowedBooks.IndexOf(book) < 0)
            {
                user.BorrowedBooks.Add(book);
                _dbcontext.Update(user);
                _dbcontext.SaveChanges();
                message.Text = $"{book.Title}: Pomyślnie wypożyczyłeś książkę";
                message.Visibility= Visibility.Visible;
            }
            else
            {
                message.Text = $"{book.Title}: Ta książka jest już przez Ciebie wypożyczona";
                message.Visibility = Visibility.Visible;
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse((sender as Button).Tag.ToString());
            var book = _dbcontext.Books.FirstOrDefault(x => x.Id == id);

            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
            var window = new BooksWindow(_dbcontext, _id);
            window.Show();
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var window = new MenuWindow(_dbcontext, _id);
            window.Show();
            this.Close();
        }
    }
}
