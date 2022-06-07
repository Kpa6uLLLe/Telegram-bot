using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace tgBOT.Data
{
    public class AppIdentityUser : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Nickname { get; set; } = string.Empty;
        public long? BotAPIUserId { get; set; }
    }
}
