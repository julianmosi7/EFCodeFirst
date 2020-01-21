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
            Console.WriteLine($"---{MethodBase.GetCurrentMethod().Name}");
            AssertDatabase(db);
            if (db.Employees.Any()) return db;
            Seed(db);
            db.SaveChanges();
            return db;
        }

        private static void Seed(OrderContext db)
        {
            Console.WriteLine($"---{MethodBase.GetCurrentMethod().Name}");
            var customerA = new Customer { Name = "Hansi", Longitude = 12, Latitude = 48 };
            var customerB = new Customer { Name = "Susi", Longitude = 13, Latitude = 47 };
            var employeeA = new Employee { Firstname = "Heinzi", Lastname = "Lehner" };
            var employeeB = new Employee { Firstname = "Pauli", Lastname = "Berger" };
            var orderX = new Order { Description = "Auftrag X", Customer = customerA };
            var orderY = new Order { Description = "Auftrag Y", Customer = customerB };
            db.Customers.Add(customerA);
            db.Customers.Add(customerB);
            db.Employees.Add(employeeA);
            db.Employees.Add(employeeB);
            db.Orders.Add(orderX);
            db.Orders.Add(orderY);
        }

        private static void AssertDatabase(OrderContext db)
        {
            Console.WriteLine("---AssertDatabase---");
            bool dbExists = db.Database.Exists();
            if (dbExists)
            {
                Console.WriteLine($"Database exists: {db.Database.Connection.ConnectionString}");
                bool dbStructureOK = db.Database.CompatibleWithModel(true);
                Console.WriteLine($"Structure still the same? {dbStructureOK}");
                if (dbStructureOK) return;

                Console.WriteLine("Delete the database");
                db.Database.Delete();
            }
            Console.WriteLine("Create the database with actual configuration");
            db.Database.Create();
        }
    }
}
