using System;
using System.Collections.Generic;

namespace ZH2_NY905R.Moduls;

public partial class Customer
{
    public int CustomerPk { get; set; }

    public string FullName { get; set; } = null!;

    public string? Company { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
