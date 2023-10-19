using Assignment02_07_He160021.Dtos;

namespace Assignment02_07_He160021.Utils
{
    public class CartUltil
    {
        public static List<Cart> GetCartInfo(string cookie)
        {
            List<Cart> carts = new List<Cart>();
            string[] strings = cookie.Split("|");
            foreach (string s in strings)
            {
                string[] c = s.Split(",");
                Cart cart = new Cart()
                {
                    Id = int.Parse(c[0]),
                    Number = int.Parse(c[1]),
                };
                carts.Add(cart);
            }
            return carts;
        }

        private static string GetCookie(List<Cart> carts)
        {
            if (carts.Count == 0) return "";
            string cookie = "";
            carts.ForEach(cart =>
            {
                cookie += cart.GetInfo() + "|";
            });
            return cookie.Remove(cookie.Length -1);
        }

        public static string AddItemToCart(Cart cart, string cookie)
        {
            List<Cart> carts = GetCartInfo(cookie);
            bool IsItemExist = false;
            foreach(Cart c in carts)
            {
                if(c.Id == cart.Id)
                {
                    c.Number += cart.Number;
                    IsItemExist = true;
                    break;
                }
            }
            if(!IsItemExist)
            {
                carts.Add(cart);    
            }
            return GetCookie(carts);
        }

        public static string RemoveItem(Cart cart, string cookie)
        {
            List<Cart> carts = GetCartInfo(cookie);
            foreach(Cart c in carts)
            {
                if(c.Id == cart.Id)
                {
                    carts.Remove(c);
                    break;
                }
            }
            return GetCookie(carts);
        }

        public static string ChangeNumber(Cart cart, string cookie)
        {
            List<Cart> carts = GetCartInfo(cookie);
            foreach (Cart c in carts)
            {
                if (c.Id == cart.Id)
                {
                    c.Number = cart.Number;
                    break;
                }
            }
            return GetCookie(carts);
        }
    }
}
