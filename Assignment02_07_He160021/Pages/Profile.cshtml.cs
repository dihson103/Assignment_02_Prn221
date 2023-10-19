using Assignment02_07_He160021.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignment02_07_He160021.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly Prn221Assignment0207Context _context;

        public ProfileModel(Prn221Assignment0207Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public void OnGet()
        {
            var user = HttpContext.User;
            var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Customer = _context.Customers.SingleOrDefault(x => x.Username == username);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(Customer).State = EntityState.Modified;
            _context.SaveChanges();
            ModelState.AddModelError("Error", "Update success.");
            return Page();
        }
    }
}
