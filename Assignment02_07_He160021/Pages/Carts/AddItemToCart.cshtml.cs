using Assignment02_07_He160021.Dtos;
using Assignment02_07_He160021.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment02_07_He160021.Pages.Carts
{
    public class AddItemToCartModel : PageModel
    {
        public string MyCookie { get; set; }

        public void OnPost(IFormCollection form)
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
                newCookie = form["id"] + "," + 1.ToString();
            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("my-cart", newCookie);
            MyCookie = newCookie;
        }
    }
}
