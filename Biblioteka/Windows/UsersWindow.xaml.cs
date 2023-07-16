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
    public partial class UsersWindow : Window
    {
        private readonly AppDbContext _dbcontext;
        private readonly int _id;
        public UsersWindow(AppDbContext dbcontext, int id)
        {
            InitializeComponent();
            _dbcontext = dbcontext;
            _id = id;
            var users= _dbcontext.Users.OfType<User>().Include(o=>o.BorrowedBooks).ThenInclude(o=>o.Author).ToList();
            var employee = _dbcontext.Users.OfType<Employee>().Include(o=>o.Role).FirstOrDefault(o => o.Id.Equals(_id));
            var role = employee.Role.RoleType;

            

            foreach (var user in users)
            {
                var grid = new Grid() { Margin = new Thickness(10), Background = Brushes.AliceBlue };

                panel.Children.Add(grid);
               

                var infoHolder = new StackPanel() { Orientation = Orientation.Vertical, Margin = new Thickness(0) };

                var username = new TextBlock() { Text = $"Nazwa użytkownika: {user.Username}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(5, 10, 0, 0) };
                var firstname = new TextBlock() { Text = $"Imię: {user.Firstname}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(5, 0, 0, 0) };
                var lastname = new TextBlock() { Text = $"Nazwisko: {user.Lastname}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(5, 0, 0, 0) };
                var libraryId = new TextBlock() { Text = $"ID: {user.LibraryId}", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(5, 0, 0, 0) };

                if(role.Equals("Admin"))
                {
                    var button = new Button() { Background=Brushes.Red, Width=60, Height=25, Content="Usuń", Tag=user.Id, HorizontalAlignment = HorizontalAlignment.Left};
                    button.Click += new RoutedEventHandler(Delete_Click);
                    infoHolder.Children.Add(button);
                }
                infoHolder.Children.Add(username);
                infoHolder.Children.Add(firstname);
                infoHolder.Children.Add(lastname);
                infoHolder.Children.Add(libraryId);

                grid.Children.Add(infoHolder);

                var borrowed = new StackPanel() { Orientation = Orientation.Vertical, Margin = new Thickness(0) };
                if(user.BorrowedBooks is null)
                {
                    var bookTitle = new TextBlock() { Text = $"-", HorizontalAlignment = HorizontalAlignment.Center };
                    borrowed.Children.Add(bookTitle);
                }
                else
                {
                    foreach (var book in user.BorrowedBooks)
                    {
                        var bookTitle = new TextBlock() { Text = $"{book.Title}", HorizontalAlignment = HorizontalAlignment.Center };
                        borrowed.Children.Add(bookTitle);
                    }
                }
                
                grid.Children.Add(borrowed);
            }                       
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse((sender as Button).Tag.ToString());
            var user = _dbcontext.Users.FirstOrDefault(x => x.Id == id);

            _dbcontext.Users.Remove(user);
            _dbcontext.SaveChanges();
            var window = new UsersWindow(_dbcontext, _id);
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
