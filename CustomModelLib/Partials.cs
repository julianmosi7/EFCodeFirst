using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public partial class Order
    {
        public override string ToString()
        {
            return $"{Description} vom {OrderDate?.ToString("dd-MM-yyyy")}";            
        }
    }

    public partial class OrderDetail
    {
        public override string ToString()
        {
            return $"{Amount} {Product.ToString()} zu je {Product.Price}";
        }
    }    

    public partial class Product
    {
        public override string ToString()
        {
            return Description;
        }
    }

    public partial class Employee
    {
        public override string ToString()
        {
            return $"{Lastname} {Firstname}";
        }
    }
}
