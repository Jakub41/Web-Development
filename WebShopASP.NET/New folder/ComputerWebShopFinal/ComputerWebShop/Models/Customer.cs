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

        public int ID  { get; set; }
        public string FirstName { get; set; }
        
        public string LastName{ get; set; }
        public string Address { get; set; }
        public string PhoneNumber{get; set;}
        public  string Gender { get; set; }
        public string City { get; set; }
        
        public string PostalCode { get; set; }
        public string State {get;set;}

        public string Country { get; set; }
        public virtual ApplicationUser user { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }



    }
}