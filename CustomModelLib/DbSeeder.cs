using DBLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomModelLib
{
    public static class DbSeeder
    {
        public static OrderContext SeedIfEmpty(this OrderContext db)
        {
            DeleteCompletely(db);
            AssertDatabase(db);
            if (db.Orders.Any()) return db;
            Seed(db);
            db.SaveChanges();
            return db;
        }       

        private static void Seed(OrderContext db)
        {
            Console.WriteLine($"---{MethodBase.GetCurrentMethod().Name}");
            var customerA = new Customer { Name = "Firma Berger", Longitude = 48.3352f, Latitude = 14.5324f };
            var customerB = new Customer { Name = "Fam. Lehner", Longitude = 48.5136f, Latitude = 14.1902f };
            var employeeA = new Employee { Firstname = "Hansi", Lastname = "Huber" };
            var employeeB = new Employee { Firstname = "Susi", Lastname = "Maier" };
            var employeeC = new Employee { Firstname = "Fritzi", Lastname = "Müller" };
            var employeeD = new Employee { Firstname = "Franzi", Lastname = "Hehenberger" };
            var employeeE = new Employee { Firstname = "Pauli", Lastname = "Gruber" };
            var employeeF = new Employee { Firstname = "Elfi", Lastname = "Gerber" };
            var employeeG = new Employee { Firstname = "Maxi", Lastname = "Moser" };
            var productA = new Product { Weight = 43, Price = 19, Description = "Platten A" };
            var productB = new Product { Weight = 46, Price = 22, Description = "Platten C" };
            var productC = new Product { Weight = 52, Price = 31, Description = "Platten B" };
            var productD = new Product { Weight = 2, Price = 10, Description = "Isolierung B" };
            var productE = new Product { Weight = 2, Price = 11, Description = "Isolierung C" };
            var productF = new Product { Weight = 1, Price = 8, Description = "Isolierung D" };
            var productG = new Product { Weight = 3, Price = 12, Description = "Isolierung A" };
            var orderA = new Order { Customer = customerB, OrderDate = new DateTime(2018, 01, 30), Description="Hausisolierung 33"};
            var orderB = new Order { Customer = customerB, OrderDate = new DateTime(2018, 02, 06), Description="Hausisolierung 32"};
            var orderC = new Order { Customer = customerA, OrderDate = new DateTime(2018, 02, 05), Description="Plattenlieferung 1"};
            var orderDetailA = new OrderDetail { Order = orderA, Amount = 30, Product = productC};
            var orderDetailB = new OrderDetail { Order = orderB, Amount = 60, Product = productD };
            var orderDetailC = new OrderDetail { Order = orderB, Amount = 20, Product = productE };
            var orderDetailD = new OrderDetail { Order = orderB, Amount = 20, Product = productF };
            var orderDetailE = new OrderDetail { Order = orderC, Amount = 15, Product = productA };
            var orderDetailF = new OrderDetail { Order = orderC, Amount = 20, Product = productB };
            db.Customers.Add(customerA);
            db.Customers.Add(customerB);
            db.Employees.Add(employeeA);
            db.Employees.Add(employeeB);
            db.Employees.Add(employeeC);
            db.Employees.Add(employeeD);
            db.Employees.Add(employeeE);
            db.Employees.Add(employeeF);
            db.Employees.Add(employeeG);
            db.Products.Add(productA);
            db.Products.Add(productB);
            db.Products.Add(productC);
            db.Products.Add(productD);
            db.Products.Add(productE);
            db.Products.Add(productF);
            db.Products.Add(productG);
            db.Orders.Add(orderA);
            db.Orders.Add(orderB);
            db.Orders.Add(orderC);
            db.OrderDetails.Add(orderDetailA);
            db.OrderDetails.Add(orderDetailB);
            db.OrderDetails.Add(orderDetailC);
            db.OrderDetails.Add(orderDetailD);
            db.OrderDetails.Add(orderDetailE);
            db.OrderDetails.Add(orderDetailF);
        }

        private static void AssertDatabase(OrderContext db)
        {
            if (db.Database.Exists())
            {
                if (db.Database.CompatibleWithModel(true)) return;
                db.Database.Delete();
            }
            db.Database.Create();
        }

        private static void DeleteCompletely(OrderContext db)
        {
            db.Database.Delete();
            db.Database.Create();
        }
    }
}
