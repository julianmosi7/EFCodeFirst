using CustomModelLib;
using DBLibrary;
using MVVM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OrderViewModelLib
{
    public class OrderViewModel : ObservableObject
    {
        public OrderViewModel()
        {

        }

        private OrderContext db;

        public OrderViewModel(OrderContext db)
        {
            this.db = db;
        }

        private TreeViewItem GetTreeViewItem(MouseButtonEventArgs e)
        {
            var ele = e.OriginalSource as DependencyObject;
            while(ele != null && !(ele is TreeViewItem))
            {
                Console.WriteLine($"   check click on {ele.GetType().Name}");
                ele = VisualTreeHelper.GetParent(ele);
            }
            if (ele == null) return null;
            return ele as TreeViewItem;
        }

        private string selectedEmployee;

        public string SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                Employees = db.Employees
                    .Where(x => x.Firstname.StartsWith(selectedEmployee.First().ToString()))
                    .ToList();
                Console.WriteLine(db.Employees.Where(x => x.Firstname.StartsWith(selectedEmployee.First().ToString())).ToList());
            }
        }


        private List<Employee> employees;

        public List<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                RaisePropertyChangedEvent(nameof(Employees));
            }
        }

    }
}
