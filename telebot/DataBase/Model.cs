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

        public DbSet<Link> Links { get; set; }
        public string DbPath { get; set; }

        public ULinksContext()
        {
            AppSettings settings = new AppSettings();
            DbPath = settings.GetDBPath();

        }
        public ULinksContext(DbContextOptions options)
        {
            AppSettings settings = new AppSettings();
            DbPath = settings.GetDBPath();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(DbPath);
    }
    public class Link
    {
        public string Url { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
        [Key]
        public long Id { get; set; }

    }
    public class Category
    {
        [Key]
        public string Name { get; set; } = "";
        
        public User User { get; set; }


    }
    public class User
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<Category> Categories { get; set; } = new();

        [Key]
        public long LocalId { get; set; }
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }

    }
    }
