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
    public partial class MyBooksWindow : Window
    {
        private readonly AppDbContext _dbcontext;
        private readonly int _id;
        public MyBooksWindow(AppDbContext dbcontext, int id)
        {
            InitializeComponent();
            _dbcontext = dbcontext;
            _id = id;
            var user= _dbcontext.Users.OfType<User>().Include(o=>o.BorrowedBooks).ThenInclude(o=>o.Author).FirstOrDefault(o => o.Id.Equals(_id));
            var booksLast = user.BorrowedBooks.LastOrDefault();
            var stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(10) };
            panel.Children.Add(stackPanel);
            if(user.BorrowedBooks.Count > 0)
            {
                foreach (var book in user.BorrowedBooks)
                {

                    if (stackPanel.Children.Count > 1)
                    {

                        stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(10) };
                        panel.Children.Add(stackPanel);
                    }
                    if (stackPanel.Children.Count > 0 && book.Equals(booksLast))
                    {
                        stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(10) };
                        panel.Children.Add(stackPanel);
                    }

                    var grid = new Grid();
                    var image = new Image() { Source = new BitmapImage(new Uri(@"/Images/book.png", UriKind.Relative)), Height = 240, Width = 350, Margin = new Thickness(10, 0, 10, 0) };
                    var bookHolder = new StackPanel() { Height = 240, Width = 350, Margin = new Thickness(0) };
                    var infoGrid = new Grid();
                    infoGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    infoGrid.RowDefinitions.Add(new RowDefinition());
                    infoGrid.RowDefinitions.Add(new RowDefinition());
                    infoGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    infoGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    var title = new TextBlock() { Text = $"Tytuł: {book.Title}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(45, 10, 0, 0) };
                    var autor = new TextBlock() { Text = $"Autor: {book.Author.Fullname}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(45, 0, 0, 0) };
                    var release = new TextBlock() { Text = $"Rok wydania: {book.ReleaseYear}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(45, 0, 0, 0) };
                    var button = new Button() { Content = "Oddaj", Background = Brushes.LightGray, Height = 25, Width = bookHolder.Width / 3, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(20, 0, 0, 0), Tag = book.Id };
                    button.Click += new RoutedEventHandler(GiveBack_Click);
                    infoGrid.Children.Add(title);
                    infoGrid.Children.Add(autor);
                    infoGrid.Children.Add(release);
                    infoGrid.Children.Add(button);
                    Grid.SetRow(title, 0);
                    Grid.SetRow(autor, 1);
                    Grid.SetRow(release, 2);
                    Grid.SetColumn(button, 1);
                    Grid.SetRowSpan(button, 2);

                    grid.Children.Add(image);
                    grid.Children.Add(bookHolder);
                    grid.Children.Add(infoGrid);
                    stackPanel.Children.Add(grid);
                }
            }
            else
            {
                var msg = new TextBlock() { Text = $"Nie masz żadnych wypożyczonych książek", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment= VerticalAlignment.Top, Margin = new Thickness(5, 0, 0, 0) };
                stackPanel.Children.Add(msg);
            }
            
        }
        private void GiveBack_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse((sender as Button).Tag.ToString());
            var book = _dbcontext.Books.FirstOrDefault(x => x.Id == id);
            var user = _dbcontext.Users.OfType<User>().Include(o => o.BorrowedBooks).FirstOrDefault(o => o.Id.Equals(_id));
            user.BorrowedBooks.Remove(book);
            _dbcontext.Update(user);
            _dbcontext.SaveChanges();

            var window = new MyBooksWindow(_dbcontext, _id);
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
