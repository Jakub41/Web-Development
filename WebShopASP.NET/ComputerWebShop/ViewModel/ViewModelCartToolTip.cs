using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerWebShop.ViewModel
{
    public class ViewModelCartToolTip
    {

        public int TotolProducts { get; set; }
        public int Subtotal { get; set; }
        public double Subtotaltax { get { return Subtotal * 0.05; } }
        public double Total { get { return (Subtotal + Subtotaltax) + 10; } }
    }

}