using Assignment02_07_He160021.Model;

namespace Assignment02_07_He160021.Dtos
{
    public class Cart
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Product? Product { get; set; }

        public string GetInfo()
        {
            return Id.ToString() + "," + Number.ToString();
        }
    }
}
