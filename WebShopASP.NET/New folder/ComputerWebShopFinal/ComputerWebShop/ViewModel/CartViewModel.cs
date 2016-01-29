using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWebShop.Models;
namespace ComputerWebShop.ViewModel
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public Products Product { get; set; }
        public int Subtotal { get; set; }
        public const int ShippementCharges = 100;
        public int ProductPrice { get; set; }
        public double Tax { get  {return (Subtotal*5)/100;} }
        public const int shippingcharges = 10;
        public double TotalAmount { get{return Tax+Subtotal+shippingcharges ;}  }

    }
    
}