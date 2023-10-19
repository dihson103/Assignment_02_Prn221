using Assignment02_07_He160021.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment02_07_He160021.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly Prn221Assignment0207Context _context;

        public RegisterModel(Prn221Assignment0207Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public void OnGet()
        {
            Customer = new Customer() { Type = 0 };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || _context.Customers == null || Customer == null)
            {
                return Page();
            }
            if (IsUsernameExist(Customer.Username))
            {
                ModelState.AddModelError("Error", "Username is already exist!");
                return Page();
            }
            else
            {
                _context.Customers.Add(Customer);
                _context.SaveChanges();
                string message = "Register success. Please login to order product.";
                return RedirectToPage("/Login", new {message});
            }
        }

        private bool IsUsernameExist(string username)
        {
            Customer? customer = _context.Customers.SingleOrDefault(x => x.Username == username);
            return customer != null;
        }
    }
}
