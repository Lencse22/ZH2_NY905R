using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZH2_NY905R
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string? Company { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int OrderPrice { get; set; }

    }
}
