using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace telebot
{
    public class ULinksContext : IdentityDbContext<AppIdentityUser, IdentityRole, string>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<User> Users { get; set; }
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
        public ULinksContext(DbContextOptions<ULinksContext> options)
        {
            AppSettings settings = new AppSettings();
            DbPath = settings.GetDBPath();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppIdentityUser>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
                .HasAlternateKey(c => new { c.Id, c.Name })
                .HasName($"AK_Id_Name");
            modelBuilder.Entity<Link>()
                .HasOne(l => l.Category)
                .WithMany(c => c.Links)
                .HasPrincipalKey(c => c.Name);



            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
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
        public long Id { get; set; }
        public string? Name { get; set; } = "";
        public List<Link> Links { get; set; }

        public User User { get; set; }



    }
    public class User
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Nickname { get; set; } = string.Empty;
        public List<Category>? Categories { get; set; } = new();
        public List<Link>? Links { get; set; } = new();

        [Key]
        public long? UserId { get; set; }
    }
}

