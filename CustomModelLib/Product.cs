using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public partial class Product
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
