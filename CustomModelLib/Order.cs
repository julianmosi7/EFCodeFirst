using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? OrderDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}
