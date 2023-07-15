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
    public partial class RegisterWindow : Window
    {
        private readonly AppDbContext dbcontext;
        public RegisterWindow()
        {
            InitializeComponent();
            dbcontext = new AppDbContext();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if(!login.Text.Equals(string.Empty) && 
                !password.Password.ToString().Equals(string.Empty) &&
                !rePassword.Password.ToString().Equals(string.Empty) &&
                !firstname.Text.Equals(string.Empty) &&
                !lastname.Text.Equals(string.Empty))
            {
                if (dbcontext.Users.FirstOrDefault(o => o.Username.Equals(login.Text)) is null)
                {
                    if (password.Password.ToString().Equals(rePassword.Password.ToString()))
                    {
                        var user = new User()
                        {
                            Username = login.Text,
                            Password = rePassword.Password.ToString(),
                            Firstname = firstname.Text,
                            Lastname = lastname.Text,
                            LibraryId = Guid.NewGuid().ToString()
                        };
                        dbcontext.Users.Add(user);
                        dbcontext.SaveChanges();
                        Exception(3);
                    }
                    else Exception(1);
                }
                else Exception(2);
                
            }
            else Exception(0);
        }
        
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }
        private void Exception(int i)
        {
            if (i.Equals(0))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Red;
                exceptions.Text = "Uzupełnij wszyskie pola!";
            }
            if(i.Equals(1))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Red;
                exceptions.Text = "Hasła nie są takie same!";
            }
            if(i.Equals(2))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Red;
                exceptions.Text = "Taki użytkownik już istnieje!";
            }
            if (i.Equals(3))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Green;
                exceptions.Text = "Użytkownik został dodany";
            }
        }
    }
}
