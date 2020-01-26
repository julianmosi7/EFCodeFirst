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
       

    }
}
