using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWebShop.ViewModel;
using ComputerWebShop.Models;
using  Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace ComputerWebShop.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View();
        }
        
        [ChildActionOnly]
        public ActionResult CategoryList() 
        {

            return PartialView(db.Category.ToList().Distinct());
        }
        [ChildActionOnly]
        public ActionResult BrandList(int CategoryID) 
        {
            var result1  = (from u in db.Products
                           where u.CategoryID == CategoryID
                           select new  ViewModelGetBrand{ Brand= u.Brand,CategoryID= u.CategoryID}).Distinct();
            //var result = db.Products.Where(m => m.CategoryID == CategoryID).Select(m => m.Brand).ToList();
            
            return PartialView(result1);
        }
        [ChildActionOnly]
        public ActionResult Cart()
        {
            ViewModelCartToolTip objViewModelCartToolTip = new ViewModelCartToolTip();
            if(Session["CartID"]!=null)
            {
                string CartID = Session["CartID"].ToString();
                  objViewModelCartToolTip.TotolProducts =   db.Cart.Where(m => m.cartID == CartID).Count();
                  objViewModelCartToolTip.Subtotal    = db.Cart.Where(m=>m.cartID==CartID).Select(g => g.Price).Sum();
                
            }
            return PartialView(objViewModelCartToolTip);

        }
        public ActionResult Slide() 
        {
            return PartialView(db.Products.Take(1).Single());
        }
        public ActionResult TopThree() 
        {

            return PartialView(db.Products.Take(3).OrderByDescending(m => m.DateAdded).ToList());
        }
        public ActionResult BottomThree()
        {
            return PartialView(db.Products.Take(3).OrderBy(m=>m.DateAdded).ToList());
        }
        public ActionResult Apple()
        {
            return PartialView(db.Products.Where(m=>m.Brand=="Apple").Take(2).OrderByDescending(m => m.DateAdded).ToList());
        }
        public ActionResult SidePanel() 
        {
            return PartialView(db.Products.Take(1));
        }
        public ActionResult Footer() 
        {
            return PartialView(db.Products.ToList());
        }


        public ActionResult GetName() 
        {
            if (Request.IsAuthenticated)
            {
                var applicationuserid = User.Identity.GetUserId();

                ViewBag.Name = (from b in db.Customer
                                where b.ApplicationUserID == applicationuserid
                                select b.FirstName).FirstOrDefault();
            }
            return PartialView();
        
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LoginPartial()
        {
            ViewModelRetrieveName obj = new ViewModelRetrieveName() { Name = "hussan" };
            return PartialView(obj);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}