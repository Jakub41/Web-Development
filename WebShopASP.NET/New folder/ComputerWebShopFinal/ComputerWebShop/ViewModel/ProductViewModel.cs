using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ComputerWebShop.Models;
namespace ComputerWebShop.ViewModel
{
    public class ProductViewModel
    {

        public int ID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "must be atleast 52 character Long", MinimumLength = 52)]
        public string ProdDesc { get; set; }
        [Required]
        public HttpPostedFileBase PrimaryImage { get; set; }
        public int stock { get; set; }
        [Required]
        public int Price { get; set; }
        
        public string CategoryName { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }

    }
}