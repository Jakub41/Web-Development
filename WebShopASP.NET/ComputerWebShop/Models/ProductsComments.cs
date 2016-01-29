using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ComputerWebShop.Models
{
    public class ProductsComments
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public virtual Products Products { get; set; }
        [ForeignKey("Products")]
        public int ProductID { get; set; }
    }
}