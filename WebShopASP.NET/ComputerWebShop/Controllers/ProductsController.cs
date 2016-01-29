using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ComputerWebShop.Models;
using ComputerWebShop.ViewModel;
using PagedList;

namespace ComputerWebShop.Controllers
{
    public class ProductsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Products/
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
           
                var products = db.Products.Include(p => p.Category);
                return View(products.ToList());
            

            
        }

        //public ActionResult Orders() 
        //{
        //    ViewBag.panel = "productsbought";
        //    string applicationuserid = User.Identity.GetUserId();
        //    int CustID = db.Customer.Where(m => m.ApplicationUserID == applicationuserid).FirstOrDefault().ID;
        //    OrderViewModel objOrderViewModel = new OrderViewModel();
        //    objOrderViewModel.cOrders = db.Orders.Where(m => m.CustomerID == CustID).ToList();
        //    return View();
        //}
        //List All Products
       public void ChangeCurrency()
       {
           HttpCookie cookie = new HttpCookie("Danish");
           cookie.Value = "Da-dk";
           cookie.Expires = DateTime.Now.AddYears(1);
           Response.Cookies.Add(cookie);

       }
      

      
        public ActionResult ListProducts(int? CategoryID,string Brand,int? page,string search) 
        {
            ViewModelProductList objViewModelProductList = new ViewModelProductList();
            int pagesize = 3;
            int pagenumber = (page ?? 1);
            objViewModelProductList.PageCount = pagesize;
            objViewModelProductList.PageNo = pagenumber;
            if (CategoryID != null) 
            {

            }
            if (CategoryID != null&& Brand !=null) 
            {
               objViewModelProductList.Products= db.Products.Where(m => m.CategoryID == CategoryID && m.Brand == Brand).OrderBy(m=>m.ID).ToPagedList(pagenumber,pagesize);
               objViewModelProductList.TotalProducts = objViewModelProductList.Products.Count;
               objViewModelProductList.ProductCategory = db.Category.Where(m => m.ID == CategoryID).Single().CategoryName;

                return View(objViewModelProductList);
            }
            else if (CategoryID != null && Brand == null)
            {
                objViewModelProductList.Products = db.Products.Where(m => m.CategoryID == CategoryID).OrderBy(m=>m.ID).ToPagedList(pagenumber, pagesize);
                objViewModelProductList.ProductCategory = db.Category.Where(m => m.ID == CategoryID).Single().CategoryName;

                objViewModelProductList.TotalProducts = objViewModelProductList.Products.Count;

                return View(objViewModelProductList);
            }
            else if (!String.IsNullOrEmpty(search))
            {
                objViewModelProductList.Products = db.Products.Where(m=>m.ProductName.Contains(search)||m.ProductCategory.Contains(search)).OrderBy(m => m.ID).ToPagedList(pagenumber, pagesize);

                objViewModelProductList.TotalProducts = objViewModelProductList.Products.Count;

                return View(objViewModelProductList);
            }
            else
            {
                objViewModelProductList.Products = db.Products.OrderBy(m=>m.ID).ToPagedList(pagenumber, pagesize);
                objViewModelProductList.TotalProducts = objViewModelProductList.Products.Count;

                return View(objViewModelProductList);
            }

        }
        // Partial

        public ActionResult PriceDesc(int PageCount, int PageNo) 
        {
            System.Threading.Thread.Sleep(1000);
           
            ViewModelProductList objViewModelProductList = new ViewModelProductList();
            objViewModelProductList.PageCount = PageCount;
            objViewModelProductList.PageNo = PageNo;
            objViewModelProductList.Products = db.Products.OrderByDescending(m => m.Price).ToPagedList( Convert.ToInt32( PageNo), Convert.ToInt32(PageCount));
           return PartialView("PartialListProducts",objViewModelProductList);
        }
        public ActionResult Latest(int PageCount, int PageNo)
        {
            System.Threading.Thread.Sleep(1000);

            ViewModelProductList objViewModelProductList = new ViewModelProductList();
            objViewModelProductList.PageCount = PageCount;
            objViewModelProductList.PageNo = PageNo;
            objViewModelProductList.Products = db.Products.OrderByDescending(m=>m.DateAdded).ToPagedList(Convert.ToInt32(PageNo), Convert.ToInt32(PageCount));
            return PartialView("PartialListProducts", objViewModelProductList);
        }
        // GET: /Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objViewModelDetails = new ViewModelDetails();
           objViewModelDetails.Product    = db.Products.Find(id);
            if (objViewModelDetails == null)
            {
                return HttpNotFound();
            }
            string brand = objViewModelDetails.Product.Brand;
            objViewModelDetails.Products = db.Products.Where(m => m.Brand == brand).ToList();
            objViewModelDetails.cProductsComments = db.ProductsComments.Where(m => m.ProductID == id).ToList();
            return View(objViewModelDetails);
        }
        [HttpPost]
        public ActionResult GiveComments(ViewModelDetails ViewModelDetails) 
        {
            if(ModelState.IsValid){
                     var applicationuserID   = User.Identity.GetUserId();
                     ViewModelDetails.ProductsComments.Name = db.Customer.Where(m => m.ApplicationUserID == applicationuserID).FirstOrDefault().FirstName;
                     ViewModelDetails.ProductsComments.Date = DateTime.Now;
                     int id = ViewModelDetails.Product.ID;
                     ViewModelDetails.ProductsComments.ProductID = id;
                db.ProductsComments.Add(ViewModelDetails.ProductsComments);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new {ID = ViewModelDetails.Product.ID });
        }
        //Get:  /Products/AddCategory
       
        // GET: /Products/Create
       
        
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
          public ActionResult CheckValidation() 
        {
           
            return View();
        }
      
    }
}
