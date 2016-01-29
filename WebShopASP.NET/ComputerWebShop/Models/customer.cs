using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ComputerWebShop.Models
{
    public class Customer
    {

        
        public int ID { get; set; }
        [Display(Name="FirstName",ResourceType=typeof(Resources.Resources))]
       
        public string FirstName { get; set; }
         [Display(Name = "LastName", ResourceType = typeof(Resources.Resources))]
        public string LastName { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        public string Address { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resources))]
        public string PhoneNumber { get; set; }
        [Display(Name = "Gender", ResourceType = typeof(Resources.Resources))]
        public string Gender { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resources.Resources))]
        public string City { get; set; }

        [Display(Name = "PostalCode", ResourceType = typeof(Resources.Resources))]
        public string PostalCode { get; set; }
        [Display(Name = "State", ResourceType = typeof(Resources.Resources))]
        public string State { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resources.Resources))]
        public string Country { get; set; }
        public virtual ApplicationUser user { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }



    }
}