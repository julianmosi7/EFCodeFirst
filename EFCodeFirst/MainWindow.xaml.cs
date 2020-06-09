using CustomModelLib;
using DBLibrary;
using MVVM.Tools;
using OrderViewModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            if (e.ChangedButton != MouseButton.Left) return;
            var ele = listBox.InputHitTest(e.GetPosition(listBox));
            var listboxItem = GetListBoxItem(ele as DependencyObject);
            if (listboxItem == null) return;
            string item = listboxItem.Content.ToString();

            viewModel.EnteredEmployee = item;
        }

        private ListBoxItem GetListBoxItem(DependencyObject ele)
        {
            while(ele != null && !(ele is ListBoxItem))
            {
                ele = VisualTreeHelper.GetParent(ele);
            }
            return ele as ListBoxItem;
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        { 
            var ele = e.OriginalSource as DependencyObject;
            var treeViewItem = GetTreeViewItem(ele as DependencyObject);
            if (treeViewItem.Header.GetType().Name.StartsWith("Order"))
            {
                Order item = treeViewItem.Header as Order;
                viewModel.SelectedOrders = db.Orders.FirstOrDefault(x => x.Id == item.Id);
            }
            
        }

        private TreeViewItem GetTreeViewItem(DependencyObject ele)
        {
            while(ele != null && !(ele is TreeViewItem))
            {
                ele = VisualTreeHelper.GetParent(ele);
            }
            return ele as TreeViewItem;
        }

    }
}

