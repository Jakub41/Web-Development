using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerWebShop.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string CategoryName { get; set; }
        public virtual ICollection<Products> Products { get; set; }


    }
}