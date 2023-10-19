using System;
using System.Collections.Generic;

namespace Assignment02_07_He160021.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }
    
    public DateTime OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public int? Freight { get; set; }

    public string ShipAddress { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
