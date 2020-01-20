using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string  Lastname { get; set; }
        public virtual List<Order> Shipments { get; set; }
    }
}
