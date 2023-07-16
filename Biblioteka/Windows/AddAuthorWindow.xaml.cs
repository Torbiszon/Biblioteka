using Azure.Identity;
using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    public partial class AddAuthorWindow : Window
    {
        private readonly AppDbContext _dbcontext;
        private readonly int _id;
        public AddAuthorWindow(AppDbContext dbcontext, int id)
        {
            InitializeComponent();
            _dbcontext = dbcontext;
            _id = id;
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var _fullname = $"{firstname.Text[0].ToString().ToUpper()}{firstname.Text.Substring(1).ToLower()} {lastname.Text[0].ToString().ToUpper()}{lastname.Text.Substring(1).ToLower()}";
            if (_dbcontext.Authors.FirstOrDefault(o => o.Fullname.Equals(_fullname)) is null)
            {
                var author = new Author(firstname.Text, lastname.Text);
                _dbcontext.Authors.Add(author);
                _dbcontext.SaveChanges();
                Exception(1);
            }
            else Exception(0);
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var window = new MenuWindow(_dbcontext, _id);
            window.Show();
            this.Close();
        }        
        private void Exception(int i)
        {
            if (i.Equals(0))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Red;
                exceptions.Text = "Taki autor już istnieje!";
            }
            if (i.Equals(1))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Green;
                exceptions.Text = "Autor dodany pomyślnie";
            }
        }
    }
}
