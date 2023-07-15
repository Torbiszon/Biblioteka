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
            using(var dbcontext = new AppDbContext())
            {
                if(!dbcontext.Users.Any())
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
            }
        }
    }
}
