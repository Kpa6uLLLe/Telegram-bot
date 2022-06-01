using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace telebot
{
    public class ULinksContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public string DbPath { get; set;}

        public ULinksContext()
        {

        }
        public ULinksContext(DbContextOptions options)
        {
            AppSettings settings = new AppSettings();
            DbPath = settings.GetDBPath();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(DbPath);
    }
    public class Category
    {
        [Key]
        public string Name { get; set; } = "";
        public List<string> LinkList { get; } = new();
        
        public User User { get; set; }


    }
    public class User
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<Category> Categories { get; set; } = new();
        [Key]
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }

    }
    }
