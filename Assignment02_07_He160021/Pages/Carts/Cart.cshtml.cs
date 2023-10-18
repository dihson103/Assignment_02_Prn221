using Assignment02_07_He160021.Dtos;
using Assignment02_07_He160021.Model;
using Assignment02_07_He160021.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment02_07_He160021.Pages
{
    public class CartModel : PageModel
    {
        private readonly Prn221Assignment0207Context _context;

        public CartModel(Prn221Assignment0207Context context)
        {
            _context = context;
        }

        public List<Cart> Items { get; set; }

        public void OnGet()
        {
            string? cookie = Request.Cookies["my-cart"];
            if (cookie != null)
            {
                List<Cart> carts = CartUtil.GetCartInfo(cookie);
                carts.ForEach(cart =>
                {
                    cart.Product = _context.Products.SingleOrDefault(c => c.ProductId == cart.Id);
                });
                Items = carts;
            }
        }

        public IActionResult OnAddItem(IFormCollection form)
        {
            string? cookie = Request.Cookies["my-cart"];
            string newCookie;
            if (cookie != null)
            {
                Cart cart = new Cart()
                {
                    Id = int.Parse(form["id"]),
                    Number = 1
                };
                newCookie = CartUtil.AddToCart(cart, cookie);
            }
            else
            {
                newCookie = form["id"] + "," + 1;
            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("my-cart", newCookie);
            return Page();
        }
    }
}
