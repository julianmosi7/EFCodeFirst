﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string  Lastname { get; set; }
        public virtual List<Shipment> Shipments { get; set; }
    }
}
