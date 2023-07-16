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
    public partial class AddBookWindow : Window
    {
        private readonly AppDbContext _dbcontext;
        private readonly int _id;
        public AddBookWindow(AppDbContext dbcontext, int id)
        {
            InitializeComponent();
            _dbcontext = dbcontext;
            _id = id;
            var authors = _dbcontext.Authors.ToList();
            var list = new List<string>();
            foreach(var author in authors)
            {
                list.Add(author.Fullname);
            }
            authorsList.ItemsSource= list;
            authorsList.Height = title.Height;

        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_dbcontext.Books.FirstOrDefault(o => o.Title.Equals(title.Text)) is null)
            {
                if(int.TryParse(release.Text, out int parsed))
                {
                    if (parsed.ToString().Length == 3 || parsed.ToString().Length == 4)
                    {
                        var author = _dbcontext.Authors.FirstOrDefault(o => o.Fullname.Equals(authorsList.SelectedItem.ToString()));
                        var book = new Book()
                        {
                            Title = title.Text,
                            ReleaseYear = parsed,
                            Author = author
                        };
                        _dbcontext.Books.Add(book);
                        _dbcontext.SaveChanges();
                        Exception(2);
                    }
                    else Exception(1);
                }
                else Exception(1);
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
                exceptions.Text = "Taka książka już istnieje!";
            }
            if (i.Equals(1))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Red;
                exceptions.Text = "Nieprawidłowa data wydania!";
            }
            if (i.Equals(2))
            {
                exceptions.Visibility = Visibility.Visible;
                exceptions.Foreground = Brushes.Green;
                exceptions.Text = "Książa dodanas pomyślnie";
            }
        }
    }
}
