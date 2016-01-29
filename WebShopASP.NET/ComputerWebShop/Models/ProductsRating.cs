using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ComputerWebShop.Models
{
    public class ProductsRating
    {
        public int ID { get; set; }
        public int ProductRatings{ get; set; }

        public virtual ApplicationUser user { get; set; }
        public string ApplicationUserID { get; set; }

        public virtual Products Products { get; set; }
        [ForeignKey("Products")]
        public int ProductID { get; set; }
 
    }
}