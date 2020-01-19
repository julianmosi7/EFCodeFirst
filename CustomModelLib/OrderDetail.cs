using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
