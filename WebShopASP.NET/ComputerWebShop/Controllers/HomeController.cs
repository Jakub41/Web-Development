using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWebShop.ViewModel;
using ComputerWebShop.Models;
using ComputerWebShop.Helper;
using Microsoft.AspNet.Identity;
using System.Globalization;

using System.Data.Entity;

namespace ComputerWebShop.Controllers
{
    public class HomeController : BaseController
    {
       ApplicationDbContext db = new ApplicationDbContext();

        // Quick Search AutoComplelete

       
       
        public ActionResult TestAction()
        {

            ProductRatingViewModel objProductRatingViewModel = new ProductRatingViewModel();


            return View();
        }
       
        public JsonResult GetProducts(string term)
        {
            List<string> products = db.Products.Where(s => s.ProductName.StartsWith(term))
                .Select(x => x.ProductName).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetCurrency(FormCollection FormCollection)
        {
            string Currency = FormCollection["Currency"].ToString();

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["Currency"];
            if (cookie != null)
                cookie.Value = Currency;   // update cookie value
            else
            {

                cookie = new HttpCookie("Currency");
                cookie.Value = Currency;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        } 
        public ActionResult SetCulture(FormCollection FormCollection)
        {
            // Validate input
            string culture = FormCollection["Language"].ToString();
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
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
            var result1 = (from u in db.Products
                           where u.CategoryID == CategoryID
                           select new ViewModelGetBrand { Brand = u.Brand, CategoryID = u.CategoryID }).Distinct();
            //var result = db.Products.Where(m => m.CategoryID == CategoryID).Select(m => m.Brand).ToList();

            return PartialView(result1);
        }
        [ChildActionOnly]
        public ActionResult Cart()
        {
            ViewModelCartToolTip objViewModelCartToolTip = new ViewModelCartToolTip();
            if (Session["CartID"] != null)
            {
                string CartID = Session["CartID"].ToString();
                objViewModelCartToolTip.TotolProducts = db.Cart.Where(m => m.cartID == CartID).Count();
                objViewModelCartToolTip.Subtotal = db.Cart.Where(m => m.cartID == CartID).Select(g => g.Price).Sum();

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
            return PartialView(db.Products.Take(3).OrderBy(m => m.DateAdded).ToList());
        }
        public ActionResult Apple()
        {
            return PartialView(db.Products.Where(m => m.Brand == "Apple").Take(2).OrderByDescending(m => m.DateAdded).ToList());
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
            ViewModelRetrieveName obj = new ViewModelRetrieveName() { Name = "Test" };
            return PartialView(obj);
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}