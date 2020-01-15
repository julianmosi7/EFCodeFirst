using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("OrderContext")
        {

        }
    }
}
