using System;
using System.Collections.Generic;

namespace Assignment02_07_He160021.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public int? SupplierId { get; set; }

    public int? CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public int QuantityPerUnit { get; set; }

    public decimal UnitPrice { get; set; }

    public string ProductImage { get; set; } = null!;

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
