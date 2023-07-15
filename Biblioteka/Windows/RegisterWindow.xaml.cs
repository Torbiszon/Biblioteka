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
                exceptions.Text = "Uzupełnij wszyskie pola!";
            }
            if(i.Equals(1))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Text = "Hasła nie są takie same!";
            }
            if(i.Equals(2))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Text = "Taki użytkonik już istnieje!";
            }
        }
    }
}
