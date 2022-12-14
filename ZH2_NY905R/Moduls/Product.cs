using System;
using System.Collections.Generic;

namespace ZH2_NY905R.Moduls;

public partial class Product
{
    public int ProductPk { get; set; }

    public string Name { get; set; } = null!;

    public string UnitName { get; set; } = null!;

    public int UnitPrice { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
