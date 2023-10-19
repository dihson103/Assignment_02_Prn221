using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_07_He160021.Model;
using Microsoft.AspNetCore.Authorization;

namespace Assignment02_07_He160021.Pages.Orders
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Assignment02_07_He160021.Model.Prn221Assignment0207Context _context;

        public DetailsModel(Assignment02_07_He160021.Model.Prn221Assignment0207Context context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!;
        public List<OrderDetail> OrderDetails { get; set; } = default!;
        public decimal Total { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(o => o.Customer).FirstOrDefaultAsync(m => m.OrderId == id);
            var orderDetails = await _context.OrderDetails
                .Include(o => o.Product)
                .Where(o => o.OrderId == id)
                .ToListAsync();
            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
                OrderDetails = orderDetails;
                Total = OrderDetails.ConvertAll(x => x.UnitPrice * x.Quantity).Sum();
            }
            return Page();
        }
    }
}
