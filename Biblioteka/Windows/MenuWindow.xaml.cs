using Azure.Identity;
using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    public partial class MenuWindow : Window
    {
        private readonly AppDbContext _dbcontext;
        public MenuWindow(AppDbContext dbcontext, int id)
        {
            InitializeComponent();
            _dbcontext = dbcontext;
            var user = _dbcontext.Users.FirstOrDefault(o => o.Id.Equals(id));
            if(user.GetType() == typeof(Employee))
            {
                addAuthor.Visibility= Visibility.Visible;
                addBook.Visibility= Visibility.Visible;
                users.Visibility= Visibility.Visible;
                photo.Visibility= Visibility.Hidden;
            }
        }
        private void Books_Click(object sender, RoutedEventArgs e)
        {


        }
        private void MyBooks_Click(object sender, RoutedEventArgs e)
        {


        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();

        }
        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {


        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Users_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Exception(int i)
        {
           
        }
    }
}
