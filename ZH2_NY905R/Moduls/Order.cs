using System;
using System.Collections.Generic;

namespace ZH2_NY905R.Moduls;

public partial class Order
{
    public int OrderPk { get; set; }

    public int CustomerFk { get; set; }

    public int ProductFk { get; set; }

    public int Quantity { get; set; }

    public virtual Customer CustomerFkNavigation { get; set; } = null!;

    public virtual Product ProductFkNavigation { get; set; } = null!;
}
