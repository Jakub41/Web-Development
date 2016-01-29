using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputerWebShop.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public string CartID { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
    }
}