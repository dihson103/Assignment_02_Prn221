
using Assignment02_07_He160021.Dtos;

namespace Assignment02_07_He160021.Utils
{
    public class CartUtil
    {

        public static List<Cart> GetCartInfo(string cookie)
        {
            List<Cart> carts = new List<Cart>();
            string[] arr= cookie.Split('|');
            foreach (string s in arr)
            {
                string[] p = s.Split(",");
                Cart cart = new Cart()
                {
                    Id = int.Parse(p[0]),
                    Number = int.Parse(p[1]),
                };
                carts.Add(cart);
            }
            return carts;
        }

        public static string GetCookieString(List<Cart> carts)
        {
            string cookie = "";
            foreach (Cart cart in carts)
            {
                cookie += cart.GetCart() + "|";
            }
            return cookie.Remove(cookie.Length - 1).Trim();
        }

        public static string AddToCart(Cart cart, string cookie)
        {
            List<Cart> carts = GetCartInfo(cookie);
            bool isItemExist = false;
            foreach (Cart c in carts)
            {
                if(cart.Id == c.Id)
                {
                    c.Number += cart.Number;
                    isItemExist = true;
                    break;
                }
            }
            if (!isItemExist)
            {
                carts.Add(cart);
            }
            return GetCookieString(carts);
        }

        public static string RemoveItem(Cart cart, string cookie)
        {
            List<Cart> carts = GetCartInfo(cookie);
            foreach (Cart c in carts)
            {
                if (cart.Id == c.Id)
                {
                    carts.Remove(c);
                    break;
                }
            }
            return GetCookieString(carts);
        }

    }
}
