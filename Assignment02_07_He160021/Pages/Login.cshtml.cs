using Assignment02_07_He160021.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Assignment02_07_He160021.Pages
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }

    public class LoginModel : PageModel
    {
        private readonly Prn221Assignment0207Context _context;

        public LoginModel(Prn221Assignment0207Context context)
        {
            _context = context;
        }

        [BindProperty]
        public UserRequest UserRequest { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Customer? customer = _context.Customers.SingleOrDefault(
                    x => x.Username == UserRequest.Username && x.Password == UserRequest.Password
                    );
                if (customer == null)
                {
                    ModelState.AddModelError("Error", "Wrong username or password.");
                    return Page();
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, customer.Username),
                        new Claim("Role", customer.Type.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                            );

                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}
