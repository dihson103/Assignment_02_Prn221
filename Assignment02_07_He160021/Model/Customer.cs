using System;
using System.Collections.Generic;

namespace Assignment02_07_He160021.Model;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int Type { get; set; }

    public string Username { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
