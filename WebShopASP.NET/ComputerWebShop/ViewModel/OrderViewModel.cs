using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWebShop.Models;

namespace ComputerWebShop.ViewModel
{
    public class OrderViewModel
    {
        public Orders Orders { get; set; }
        public ICollection<Orders> cOrders { get; set; }
        public Customer Customer { get; set; }
        public string Email { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public IEnumerable<PaymentInfo> cPaymentInfo { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public int Subtotal { get; set; }
        public double Tax { get { return (Subtotal * 5) / 100; } }
        public const int Shippmencharges = 10;
        public double TotalAmount { get { return Tax + Subtotal + Shippmencharges; } }
    }
}