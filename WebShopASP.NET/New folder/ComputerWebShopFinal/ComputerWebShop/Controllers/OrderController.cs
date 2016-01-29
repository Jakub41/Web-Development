using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ComputerWebShop.Models;
using ComputerWebShop.ViewModel;
using Microsoft.AspNet.Identity;
using ComputerWebShop.Services;
namespace ComputerWebShop.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        ApplicationDbContext db = new ApplicationDbContext();
        
        [Authorize]
        public ActionResult CheckOut(string message)
        {
            if (User.IsInRole("Admin")) 
            {
                RedirectToAction("Index", "Home");
            }
           
            if (message != null) 
            {
                ViewBag.message = message;
            }
            OrderViewModel objOrderViewModel = new OrderViewModel();
            if (Session["CartID"] != null)
            {
               string CartID = Session["CartID"].ToString();
                String ApplicationUserID  =  User.Identity.GetUserId();
                 

                objOrderViewModel.Email = User.Identity.GetUserName();
                objOrderViewModel.Customer = db.Customer.Where(m => m.ApplicationUserID == ApplicationUserID).FirstOrDefault();
               objOrderViewModel.PaymentInfo = db.PaymentInfo.Where(m => m.CustomerID == objOrderViewModel.Customer.ID).FirstOrDefault();
               objOrderViewModel.Cart = db.Cart.Include("Products").Where(m => m.cartID == CartID).ToList();
               int subtotal = db.Cart.Where(m => m.cartID == CartID).Select(g => g.Price).Sum();
               objOrderViewModel.Subtotal = subtotal;

               ViewBag.CreditCard = new SelectList(db.PaymentInfo, "ID", "CartNumber");

                return View(objOrderViewModel);
            }
            else 
            {
           return RedirectToAction("Index","Home");
            
            }

        }
        [HttpPost]
        public ActionResult Checkout(FormCollection formCollection)
        {
            // string name = formCollection["txtname"].ToString();
            String ApplicationUserID = User.Identity.GetUserId();

            var result = db.Customer.Where(m => m.ApplicationUserID == ApplicationUserID).FirstOrDefault();
            var result1 = db.PaymentInfo.Where(m=>m.CustomerID== result.ID).FirstOrDefault();
            if (result.FirstName == "" || result.LastName == "" || result.Address == "" || result.PhoneNumber == ""   || result.City == "" || result.State == "" || result.Country == ""
           ||  result1.CartNumber==""|| result1.SecurityCode=="" ) 
            {
              return  RedirectToAction("CheckOut", new  { message ="Please fill all fields before submitting the form" });
            }
            else{
            EmailService oEmailService = new EmailService();
            string CartID = Session["CartID"].ToString();

            int CustomerID = db.Customer.Where(m => m.ApplicationUserID == ApplicationUserID).FirstOrDefault().ID;
           
            
            Orders oOrders = new Orders() { CustomerID = CustomerID, CartID = CartID ,Date=DateTime.Now};
              

            db.Orders.Add(oOrders);
            db.SaveChanges();
            var items = db.Cart.Include("Products").Where(m => m.cartID == CartID).ToList();
            oEmailService.SendEmailConfirmation(User.Identity.GetUserName());
            return RedirectToAction("ThankYou");
            }
        }
        [HttpPost]
        public ActionResult CustomerInfo(OrderViewModel OrderViewModel)
        {
         //   string name = formCollection["txtname"].ToString();
            if (ModelState.IsValid)
            {
                int CustomerID = OrderViewModel.Customer.ID;
                var result = db.Customer.Find(CustomerID);
                result.FirstName = OrderViewModel.Customer.FirstName;
                result.LastName = OrderViewModel.Customer.LastName;
                result.Address = OrderViewModel.Customer.Address;
                result.PhoneNumber = OrderViewModel.Customer.PhoneNumber;
                result.City = OrderViewModel.Customer.City;
                result.State = OrderViewModel.Customer.State;
                result.Country = OrderViewModel.Customer.Country;
                db.SaveChanges();
            }
            return PartialView();
        }
        [HttpPost]
        public ActionResult PaymentInfo(FormCollection formCollection)
        {
            int ID = Convert.ToInt16(formCollection["Payment_ID"]);
            var result = db.PaymentInfo.Find(ID);
            result.CartNumber = formCollection["card_number"].ToString();
            result.ExpiryDate = formCollection["cardExpiry"].ToString();
            result.SecurityCode = formCollection["ccv"].ToString();
            
                   
            db.SaveChanges();
            return PartialView();
        }
        
       

        public ActionResult ThankYou()
        {
           
            if (Session["CartID"] != null)
            {
                var CartID = Session["CartID"].ToString();
                var result = db.Cart.Where(m => m.cartID == CartID).ToList();
            ViewBag.Total =db.Cart.Where(m => m.cartID == CartID).Select(g => g.Price).Sum();


                Session.Remove("CartID");
                return View(result.ToList());
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
        
        }

	}
}