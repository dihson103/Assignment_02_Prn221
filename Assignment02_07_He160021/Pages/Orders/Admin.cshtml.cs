﻿using System;
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
    [Authorize(Policy = "Admin")]
    public class AdminModel : PageModel
    {
        private readonly Assignment02_07_He160021.Model.Prn221Assignment0207Context _context;

        public AdminModel(Assignment02_07_He160021.Model.Prn221Assignment0207Context context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
            }
        }
    }
}