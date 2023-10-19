using Assignment02_07_He160021.Dtos;
using Assignment02_07_He160021.Model;
using Assignment02_07_He160021.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Assignment02_07_He160021.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Prn221Assignment0207Context _context;

        public IndexModel(ILogger<IndexModel> logger, Prn221Assignment0207Context context)
        {
            _logger = logger;
            _context = context;
        }

        private List<string> SearchBys = new List<string>() { "ID", "Product Name", "Unit Price" };

        public List<Product> Products { get; set; }
        [BindProperty]
        public string FilterBy { get; set; }
        [BindProperty]
        public string SearchValue { get; set; }

        public void OnGet()
        {
            Products = _context.Products.ToList();
            ViewData["SearchBy"] = new SelectList(SearchBys);
        }

        public void OnPost()
        {
            if(SearchValue == null)
            {
                Products = _context.Products.ToList();
            }
            else
            {
                switch(FilterBy)
                {
                    case "ID":
                        Products = _context.Products
                            .Where(product => product.ProductId.ToString().Equals(SearchValue))
                            .ToList();
                        break;
                    case "Product Name":
                        Products = _context.Products
                            .Where(product => product.ProductName.Contains(SearchValue))
                            .ToList();
                        break;
                    case "Unit Price":
                        Products = _context.Products
                            .Where(product => product.UnitPrice <= decimal.Parse(SearchValue))
                            .ToList();
                        break;
                }
            }
            ViewData["SearchBy"] = new SelectList(SearchBys);
        }

        public IActionResult OnPostAddItem(IFormCollection form)
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                return RedirectToPage("/Login");
            }
            string? cookie = Request.Cookies["my-cart"];
            if(cookie == null)
            {
                cookie = form["id"] + ",1";
            }
            else
            {
                Cart cart = new Cart()
                {
                    Id = int.Parse(form["id"]),
                    Number = 1
                };
                cookie = CartUltil.AddItemToCart(cart, cookie);

            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("my-cart", cookie, options);
            return RedirectToPage("/Index");
        }
    }
}
