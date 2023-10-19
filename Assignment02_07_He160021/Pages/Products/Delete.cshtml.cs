using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_07_He160021.Model;
using Microsoft.AspNetCore.Authorization;

namespace Assignment02_07_He160021.Pages.Products
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
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                List<OrderDetail> orderDetails = await _context.OrderDetails.Where(m => m.ProductId == product.ProductId).ToListAsync();
                foreach(var order in orderDetails)
                {
                    var o = _context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                    _context.OrderDetails.Remove(order);
                    _context.Orders.Remove(o);
                }
                await _context.SaveChangesAsync();

                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
