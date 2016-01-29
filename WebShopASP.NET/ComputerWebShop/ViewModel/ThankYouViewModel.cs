using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWebShop.Models;
namespace ComputerWebShop.ViewModel
{
    public class ThankYouViewModel
    {
        public IList<Cart> cart { get; set; }
        public int GrandSubTotal { get; set; }
        public int GrandTotal { get; set; }
        public int Tax { get; set; }

    }
}