using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerWebShop.Models
{
    public class Retrievepassword
    {
        public int ID { get; set; }
        public string ConfirmationToken { get; set; }
        public string Email { get; set; }
    }
}