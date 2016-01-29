using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ComputerWebShop.Models
{
    public class Products
    {
            public int ID { get; set; }
            public string ProductName { get; set; }
            public string ProductCategory { get; set; }
            public string Brand { get; set; }
            public DateTime DateAdded { get; set; }
            [StringLength(500, ErrorMessage = "must be atleast 52 character Long", MinimumLength = 52)]
            public string ProdDesc { get; set; }
            public string PrimaryImage { get; set; }
            public int stock { get; set; }
            [DataType(DataType.Currency)]
            public int Price { get; set; }
            public virtual Category Category { get; set; }
            [ForeignKey("Category")]
            public int CategoryID { get; set; }
            public virtual ICollection<Cart> Cart { get; set; }
            public virtual ICollection<ProductsComments> ProductsComments { get; set; }



    }
}