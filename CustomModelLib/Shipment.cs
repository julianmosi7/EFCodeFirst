using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public class Shipment
    {
        public int Id { get; set; }
        public int SequenceNr { get; set; }
        public DateTime? PlanDate { get; set; }
        public DateTime? DeliverDate { get; set; }
        public virtual List<Employee> Employees { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
