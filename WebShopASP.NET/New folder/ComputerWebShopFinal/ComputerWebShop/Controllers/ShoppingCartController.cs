using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWebShop.Models;
using ComputerWebShop.ViewModel;
using ComputerWebShop.Services;
namespace ComputerWebShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public bool ProductExist(int ProductID) 
        {
            string CartID = Session["CartID"].ToString();
            var result = db.Cart.Where(m => m.cartID == CartID && m.ProductID == ProductID).FirstOrDefault();
            if (result != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        //public ActionResult InsertCart(int ProductID) 
        //{
           
        //}

        public ActionResult ShowCart(int? ProductID)
        {
            if (User.IsInRole("Admin")) 
            {
                RedirectToAction("Index", "Home");
            }
                if (ProductID != null || Session["CartID"] != null)
                {
                    CartViewModel objCarViewModel = new CartViewModel();
                    if (Session["CartID"] == null)
                    {
                        Session.Timeout = 60;
                        EmailService objEmailService = new EmailService();
                        Session["CartID"] = objEmailService.CreateConfirmationToken();
                        int ProdID = Convert.ToInt16(ProductID);
                        int Price = db.Products.Find(ProductID).Price;

                        Cart objCart = new Cart { ProductID = ProdID, Quantity = 1, cartID = Session["CartID"].ToString(), Price = Price };


                        db.Cart.Add(objCart);
                        db.SaveChanges();

                    }
                    else
                    {
                        if (ProductID != null)
                        {
                            if (ProductExist(Convert.ToInt32(ProductID)))
                            {


                            }
                            else
                            {
                                int ProdID = Convert.ToInt16(ProductID);
                                int Price = db.Products.Find(ProductID).Price;
                                Cart objCart = new Cart { ProductID = ProdID, Quantity = 1, cartID = Session["CartID"].ToString(), Price = Price };


                                db.Cart.Add(objCart);
                                db.SaveChanges();
                            }
                        }
                        else
                        {

                        }

                    }
                    if (Session["CartID"] != null)
                    {

                        string CartID = Session["CartID"].ToString();
                        objCarViewModel.Carts = db.Cart.Include("Products").Where(m => m.cartID == CartID).ToList();
                        objCarViewModel.Subtotal = db.Cart.Where(m => m.cartID == CartID).Select(g => g.Price).Sum();


                        foreach (var item in objCarViewModel.Carts)
                        {

                        }

                    }
                    return View(objCarViewModel);
                }
                else
                {
                    ViewBag.Empty = true;
                    return View();

                }
            
            
        }
        public ActionResult DeleteItem(int ID) 
        { 
        //string CartID = Session["CartID"];
        //var result = db.Cart.Where(m => m.ProductID == ProductID && m.cartID == CartID).FirstOrDefault();
            CartViewModel objCartViewModel = new CartViewModel();
            Cart cart = db.Cart.Where(m => m.ID == ID).FirstOrDefault();
            db.Cart.Remove(cart);
            db.SaveChanges();
            string CartID = Session["CartID"].ToString();
            objCartViewModel.Carts = db.Cart.Include("Products").Where(m => m.cartID == CartID).ToList();
            
            if (objCartViewModel.Carts.FirstOrDefault() == null)
            {
                Session.Remove("CartID");
                return RedirectToAction("ShowCart");
            }
            else
            {
               int subtotal = db.Cart.Where(m => m.cartID == CartID).Select(g => g.Price).Sum();
                objCartViewModel.Subtotal = Convert.ToInt16(subtotal); 
                return PartialView("CartPartial", objCartViewModel);
            }
        }
        [HttpPost]
        public ActionResult ShowCart(FormCollection formCollection) 
        {

            int quantity = Convert.ToInt16( formCollection["quantity"]);
            int productid = Convert.ToInt16(formCollection["productid"]);
           
            string CartID = Session["CartID"].ToString();
            var ProdPrice = db.Products.Find(productid).Price;
            var result = db.Cart.Where(m => m.ProductID == productid && m.cartID == CartID).FirstOrDefault();
            result.Quantity = quantity;
            result.Price = quantity * ProdPrice;
            db.SaveChanges();
            return RedirectToAction("ShowCart");
            
        
        }
        //
        // GET: /ShoppingCart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ShoppingCart/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ShoppingCart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ShoppingCart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
