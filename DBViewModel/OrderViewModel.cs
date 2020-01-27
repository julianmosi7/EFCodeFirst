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
    public class OrderViewModel : ObservableCollection
    {
        public OrderViewModel() { }        

        private OrderContext db;

        public OrderViewModel(OrderContext db)
        {
            this.db = db;
            Employees = db.Employees.AsObservableCollection();
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
                emps = db.Employees.Where(x => x.Lastname.StartsWith(enteredEmployee)).AsObservableCollection();
                Employees = emps;               
                RaisePropertyChangedEvent(nameof(EnteredEmployee));
            }
        }

    }
}
