using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment02_07_He160021.Model;
using Microsoft.AspNetCore.Authorization;

namespace Assignment02_07_He160021.Pages.Customers
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Assignment02_07_He160021.Model.Prn221Assignment0207Context _context;

        public CreateModel(Assignment02_07_He160021.Model.Prn221Assignment0207Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Customers == null || Customer == null)
            {
                return Page();
            }

            if(IsUsernameExist(Customer.Username))
            {
                ModelState.AddModelError("error", "Username is already exist.");
                return Page();
            }
            else if(Customer.Type != 1 && Customer.Type != 0)
            {
                ModelState.AddModelError("error", "Type should be 1 or 0");
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool IsUsernameExist(string username)
        {
            Customer? customer = _context.Customers.SingleOrDefault(x => x.Username == username);
            return customer != null;
        }
    }
}
