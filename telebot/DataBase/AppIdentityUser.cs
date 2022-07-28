using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace telebot
{
    public class AppIdentityUser : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Nickname { get; set; } = string.Empty;
        public long? UserId { get; set; }
    }
}
