using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWebShop.Models;
using PagedList;

namespace ComputerWebShop.ViewModel
{
    public class ViewModelProductList
    {

        public  PagedList.IPagedList<Products> Products { get; set; }
        public int PageCount { get; set; }
        public int PageNo { get; set; }
        public int TotalProducts { get; set; }
        public string ProductCategory { get; set; }
    }
}