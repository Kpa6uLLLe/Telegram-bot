using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace tgBOT.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppIdentityUser, IdentityRole, string>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Link> Links { get; set; }
        public string DbPath { get; set; }

        public ApplicationDbContext()
        {
            DbPath = "Server=WIN-I8JP04LM8ST\\SQLEXPRESS;Database=tgBOTUserData;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            DbPath = "Server=WIN-I8JP04LM8ST\\SQLEXPRESS;Database=tgBOTUserData;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<AppIdentityUser>()
                .HasAlternateKey(u => u.UserId);

            modelBuilder.Entity<Category>()
                .HasAlternateKey(c => new {c.Id,c.Name})
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
            modelBuilder.Entity<AppIdentityUser>(b =>
            {
                b.ToTable("Users");
                b.Property(e => e.Id).HasColumnName("LocalId");
                b.Property(e => e.NormalizedUserName).HasColumnName("Nickname");
                b.Property(e => e.PasswordHash).HasColumnName("Password");
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(DbPath);
    }
    public class Link
    {
        public string Url { get; set; }
        public Category Category { get; set; }
        public AppIdentityUser User { get; set; }
        [Key]
        public long Id { get; set; }

    }
    public class Category
    {
        public long Id { get; set; }
        public string? Name { get; set; } = "";
        public List<Link> Links { get; set; }

        public AppIdentityUser User { get; set; }



    }
}