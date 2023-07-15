using Azure.Identity;
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
    public partial class MainWindow : Window
    {
        private readonly AppDbContext dbcontext;
        public MainWindow()
        {
            InitializeComponent();
            dbcontext = new AppDbContext();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = dbcontext.Users.FirstOrDefault(o => o.Username.Equals(login.Text));
            if (user is not null)
            {
                if(user.Password.Equals(password.Password.ToString()))
                {
                    //
                }
                else
                {
                    Exception(0);
                }
            }
            else
            {
                Exception(0);
            }
        }
        

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var window = new RegisterWindow();
            window.Show();
            this.Close();
        }
        private void Exception(int i)
        {
            if (i.Equals(0))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Text = "Nie znaleźliśmy użytkownika o podanych parametrach";
            }
        }
    }
}
