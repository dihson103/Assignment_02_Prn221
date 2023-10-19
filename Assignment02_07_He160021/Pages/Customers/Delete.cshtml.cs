using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_07_He160021.Model;
using Microsoft.AspNetCore.Authorization;

namespace Assignment02_07_He160021.Pages.Customers
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Assignment02_07_He160021.Model.Prn221Assignment0207Context _context;

        public DeleteModel(Assignment02_07_He160021.Model.Prn221Assignment0207Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                Customer = customer;

                List<Order> orders = await _context.Orders.Where(x => x.CustomerId == customer.CustomerId).ToListAsync();
                foreach(var order in orders)
                {
                    List<OrderDetail> orderDetails = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                    foreach(var detail in orderDetails)
                    {
                        _context.OrderDetails.Remove(detail);
                    }
                    _context.Orders.Remove(order);
                }
                await _context.SaveChangesAsync();

                _context.Customers.Remove(Customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
