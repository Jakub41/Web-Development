using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ComputerWebShop.Models
{
    public class Cart
    {

        public int ID { get; set; }

        public string cartID { get; set; }
        public virtual Products Products { get; set; }
        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        public int TotalPrice
        {
            get { return Quantity * Price; }
        }
    }
}