using Assignment02_07_He160021.Dtos;
using Assignment02_07_He160021.Model;
using Assignment02_07_He160021.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace Assignment02_07_He160021.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly Prn221Assignment0207Context _context;

        public CartModel(Prn221Assignment0207Context context)
        {
            _context = context;
        }

        public List<Cart> Carts { get; set; }
        public decimal TotalPrice { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string Phone { get; set; }

        public void OnGet()
        {
            List<Cart> list = GetListItemInCart();
            Carts = list;
            TotalPrice = list.ConvertAll(x => x.Number * x.Product.UnitPrice).Sum();

            Customer? customer = GetUserLoggedIn();
            Address = customer.Address;
            Phone = customer.Phone;
        }

        public IActionResult OnPostRemovItem(IFormCollection form)
        {
            Cart cart = new Cart() { Id = int.Parse(form["id"]) };
            string? cookie = Request.Cookies["my-cart"];
            string newCookie = CartUltil.RemoveItem(cart, cookie);
            if(string.IsNullOrEmpty(newCookie))
            {
                Response.Cookies.Delete("my-cart");
            }
            else
            {
                UpdateCookie(newCookie);
            }
            return RedirectToPage("/Carts/Cart");
        }

        public IActionResult OnPostChangeNumber(IFormCollection form)
        {
            Cart cart = new Cart() 
            {
                Id = int.Parse(form["id"]),
                Number = int.Parse(form["number"])
            };
            if(cart.Number <= 0)
            {
                string message = "Numbers of product should more than 0.";
                return RedirectToPage("/Carts/Cart", new { message });
            }
            if(!CheckNumberInStore(cart))
            {
                string message = "Numbers of this product is not enough.";
                return RedirectToPage("/Carts/Cart", new { message });
            }
            string? cookie = Request.Cookies["my-cart"];
            string newCookie = CartUltil.ChangeNumber(cart, cookie);
            UpdateCookie(newCookie);
            return RedirectToPage("/Carts/Cart");
        }

        public IActionResult OnPostCheckOut()
        {
            Customer? customer = GetUserLoggedIn();

            if (customer != null)
            {
                Order order = new Order()
                {
                    CustomerId = customer.CustomerId,
                    OrderDate = DateTime.Now,
                    ShipAddress = customer.Address
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                int addedOrderId = order.OrderId;

                List<Cart> list = GetListItemInCart();

                foreach (Cart cart in list)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = addedOrderId,
                        ProductId = cart.Id,
                        UnitPrice = cart.Product.UnitPrice,
                        Quantity = cart.Number
                    };
                    Product product = cart.Product;
                    product.QuantityPerUnit = product.QuantityPerUnit - cart.Number;
                    _context.OrderDetails.Add(orderDetail);
                    _context.Attach(product).State = EntityState.Modified;
                }

                _context.SaveChanges();

                Response.Cookies.Delete("my-cart");
                string message = "Order success!";
                return RedirectToPage("/Index", new {message});
            }
            return Page();
        }

        private bool CheckNumberInStore(Cart cart)
        {
            Product? product = _context.Products.SingleOrDefault(x => x.ProductId == cart.Id);
            return product.QuantityPerUnit >= cart.Number;
        }

        private void UpdateCookie(string cookie)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("my-cart", cookie, options);
        }

        private List<Cart> GetListItemInCart()
        {
            List<Cart> list = new List<Cart>();
            string? cookie = Request.Cookies["my-cart"];
            if (cookie != null)
            {
                list = CartUltil.GetCartInfo(cookie);
                foreach (var cart in list)
                {
                    cart.Product = _context.Products.FirstOrDefault(x => x.ProductId == cart.Id);
                }
            }
            return list;
        }

        private Customer? GetUserLoggedIn()
        {
            var user = HttpContext.User;
            var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _context.Customers.SingleOrDefault(x => x.Username == username);
        }
    }
}
