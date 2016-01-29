using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWebShop.Models;

namespace ComputerWebShop.ViewModel
{
    public class ViewModelDetails
    {

        public ICollection<Products> Products { get; set; }
        public Products Product { get; set; }
        public ProductsComments ProductsComments { get; set; }
        public IEnumerable<ProductsComments> cProductsComments { get; set; } 
    
    }
}