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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Setting Data Directory...");
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"path    ={path}");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            var db = new OrderContext("OrderContext");
            var viewModel = new OrderViewModel(db);
            DataContext = viewModel;
            AccessDatabase(db);
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
    }
}
