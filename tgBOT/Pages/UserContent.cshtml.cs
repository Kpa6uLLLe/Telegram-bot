using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tgBOT.Data;

namespace tgBOT.Pages
{
    public class TableModel : PageModel
    {
        private readonly ILogger<TableModel> _logger;
        ApplicationDbContext _context;
        public List<Link> UserLinks { get; private set; } = new();
        public List<Category> UserCategories { get; private set; } = new();
        public List<User> Users { get; private set; } = new();
        public TableModel(ApplicationDbContext db)
        {
            _context = db;
        }
        public void OnGet()
        {
            Users = _context.Users.ToList();
            UserLinks = _context.Links.ToList();
            UserCategories = _context.Categories.ToList();
            
        }
    }
}