using CustomModelLib;
using DBLibrary;
using OrderViewModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            }catch(Exception ex)
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
            List<Order> orders = db.Orders.ToList();
                        
            foreach (var item in customers.Select(x => x.Name).ToList())
            {
                TreeViewItem child = new TreeViewItem();
                child.Header = item;
                root.Items.Add(child);

                foreach (var item2 in orders.Where(x => x.Customer.Name == item).ToList())
                {
                    if (item2 == null) return;
                    TreeViewItem grandchild = new TreeViewItem();
                    grandchild.Header = item2;
                    child.Items.Add(grandchild);
                }               
                
            }
        }
    }
}
