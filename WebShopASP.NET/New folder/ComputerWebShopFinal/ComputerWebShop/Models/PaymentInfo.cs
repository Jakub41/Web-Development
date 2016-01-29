using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ComputerWebShop.Models;
namespace ComputerWebShop.Models
{
    public class PaymentInfo
    {
        public int ID{ get; set; }
        public String CartNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string SecurityCode { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

    }
}