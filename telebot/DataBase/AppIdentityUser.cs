using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace telebot
{
    public class AppIdentityUser : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;

        public List<Category>? Categories { get; set; } = new();
        public List<Link>? Links { get; set; } = new();
        [Key]
        public long? UserId { get; set; }
    }
}
