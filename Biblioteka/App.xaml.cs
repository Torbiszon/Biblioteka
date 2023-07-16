using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using (var dbcontext = new AppDbContext())
            {
                if (!dbcontext.Users.Any())
                {
                    var user = new User()
                    {
                        Username = "test",
                        Password = "test",
                        Firstname = "test",
                        Lastname = "testowy",
                        LibraryId = Guid.NewGuid().ToString()
                    };
                    var employee = new Employee()
                    {
                        Username = "emp",
                        Password = "emp",
                        Firstname = "emp",
                        Lastname = "empowy",
                        Role = new Role()
                        {
                            RoleType = RoleType.Moderator
                        }

                    };
                    var admin = new Employee()
                    {
                        Username = "admin",
                        Password = "admin",
                        Firstname = "admin",
                        Lastname = "adminowy",
                        Role = new Role()
                        {
                            RoleType = RoleType.Admin
                        }

                    };
                    dbcontext.Users.Add(user);
                    dbcontext.Users.Add(employee);
                    dbcontext.Users.Add(admin);
                    dbcontext.SaveChanges();
                }
                if (!dbcontext.Authors.Any())
                {
                    var author = new Author("Henryk", "Sienkiewicz");
                    dbcontext.Authors.Add(author);
                    dbcontext.SaveChanges();
                }
                if (!dbcontext.Books.Any())
                {
                    var author = dbcontext.Authors.FirstOrDefault(x => x.Fullname.Equals("Henryk Sienkiewicz"));
                    var book1 = new Book()
                    {
                        Title = "Krzyżacy",
                        ReleaseYear = 1900,
                        Author = author
                    };
                    var book2 = new Book()
                    {
                        Title = "Na polu chwały",
                        ReleaseYear = 1906,
                        Author = author
                    };
                    var book3 = new Book()
                    {
                        Title = "Quo vadis",
                        ReleaseYear = 1896,
                        Author = author
                    };
                    dbcontext.Books.Add(book1);
                    dbcontext.Books.Add(book2);
                    dbcontext.Books.Add(book3);
                    dbcontext.SaveChanges();
                }
            }
        }
    }
}
