using CustomModelLib;
using DBLibrary;
using MVVM.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OrderViewModelLib
{
    public class OrderViewModel : ObservableObject
    {
        public OrderViewModel() { }        

        private OrderContext db;

        public OrderViewModel(OrderContext db)
        {
            this.db = db;
            Employees = db.Employees.Include("Shipments").AsObservableCollection();
        }

        private ObservableCollection<Employee> employees;

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                RaisePropertyChangedEvent(nameof(Employees));
            }
        }

        private Employee selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                Shipments = db.Shipments.Where(x => x.Employee.Firstname == selectedEmployee.Firstname).AsObservableCollection();
                RaisePropertyChangedEvent(nameof(SelectedEmployee));
            }
        }


        private string enteredEmployee;

        public string EnteredEmployee
        {
            get { return enteredEmployee; }
            set
            {
                enteredEmployee = value;
                ObservableCollection<Employee> emps;
                emps = db.Employees.Where(x => enteredEmployee.Contains(x.Lastname) | x.Lastname.Contains(enteredEmployee)).AsObservableCollection();
                Employees = emps;               
                RaisePropertyChangedEvent(nameof(EnteredEmployee));
            }
        }

        private ObservableCollection<Shipment> shipments;

        public ObservableCollection<Shipment> Shipments
        {
            get { return shipments; }
            set 
            { 
                shipments = value;
                RaisePropertyChangedEvent(nameof(Shipments));
            }
        }

        private DateTime? planDate;

        public DateTime? PlanDate
        {
            get { return planDate; }
            set 
            { 
                planDate = value; 
            }
        }

        private Order selectedOrders;

        public Order SelectedOrders
        {
            get { return selectedOrders; }
            set
            {
                selectedOrders = value;
                Console.WriteLine(selectedOrders.Id);
                RaisePropertyChangedEvent(nameof(SelectedOrders));
            }
        }


        public ICommand AddShipmentToEmployee => new RelayCommand<string>(
            DoAddShipmentToEmployee);
        
        private void DoAddShipmentToEmployee(string obj)
        { 
            Shipment shipment = new Shipment { Employee = selectedEmployee, Orders = new List<Order> { selectedOrders } , DeliverDate = planDate };
           
            Console.WriteLine(selectedOrders.Description);
            Console.WriteLine(shipment.PlanDate);
            db.Shipments.Add(shipment);
            db.SaveChanges();
            Shipments = db.Shipments.Where(x => x.Employee.Lastname == selectedEmployee.Lastname).AsObservableCollection();
        }

    
    }
}
