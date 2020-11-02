using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class Order : INotifyPropertyChanged
    {
        public double Subtotal { get; set; }
        public double HTS { get; set; }
        public double Total { get; set; }

        public string Catagory { get; set; }
        public string OrderName { get; internal set; }
        public double Price { get; internal set; }


        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }
    }
}
