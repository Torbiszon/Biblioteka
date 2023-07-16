using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class AppDbContext : DbContext
    {
        public DbSet<BaseUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=myDb;trusted_connection=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Employee>();

            modelBuilder.Entity<User>()
                .HasMany(o => o.BorrowedBooks)
                .WithMany(o => o.Users);
            modelBuilder.Entity<Employee>()
                .HasOne(o => o.Role)
                .WithMany(o => o.Users)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Author>()
                .HasMany(o=>o.Books)
                .WithOne(o=>o.Author)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
