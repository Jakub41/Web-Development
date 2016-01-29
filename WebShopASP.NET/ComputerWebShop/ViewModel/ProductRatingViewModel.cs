using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ComputerWebShop.Models;

namespace ComputerWebShop.ViewModel
{
    public class ProductRatingViewModel
    {


        public int ID { get; set; }
        public int ProductRatings { get; set; }
        public bool CanUserVote { get; set; }
        public int UserRating { get; set; }
        public int AverageRating { get; set; }
        public int Voters { get; set; }

        public int ProductID { get; set; }
    }
}