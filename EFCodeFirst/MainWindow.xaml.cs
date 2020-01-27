using CustomModelLib;
using DBLibrary;
using OrderViewModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EFCodeFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private OrderContext db;
        private OrderViewModel viewModel;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionString();
            db = new OrderContext("OrderContext").SeedIfEmpty();
            viewModel = new OrderViewModel(db);
            db.Configuration.ProxyCreationEnabled = false;
            DataContext = viewModel;
            AccessDatabase(db);
            LoadTree();
        }

        private void ConnectionString()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        public void AccessDatabase(OrderContext db)
        {
            try
            {
                int nr = db.Employees.Count();
                Console.WriteLine($"Nr Employees = {nr}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadTree()
        {
            TreeViewItem root = new TreeViewItem();
            root.Header = "Kunden";
            treeView.Items.Add(root);
            List<Customer> customers = db.Customers.ToList();
            foreach (var item in customers.Select(x => x.Name).ToList())
            {
                TreeViewItem child = new TreeViewItem();
                child.Header = item;
                root.Items.Add(child);

                foreach (var item2 in db.Orders.Where(x => x.Customer.Name == item).ToList())
                {
                    TreeViewItem grandchild = new TreeViewItem();
                    grandchild.Header = item2;
                    child.Items.Add(grandchild);

                    foreach (var item3 in db.OrderDetails.Where(x => x.Order.Description == item2.Description).ToList())
                    {
                        TreeViewItem furthergrandchild = new TreeViewItem();
                        furthergrandchild.Header = item3;
                        grandchild.Items.Add(furthergrandchild);
                    }
                }
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listBoxItem = sender as ListBoxItem;
            var item = listBoxItem?.Content as string;
            Console.WriteLine(item);
            if (item == null) return;
            viewModel.EnteredEmployee = item;
        }
    }
}

