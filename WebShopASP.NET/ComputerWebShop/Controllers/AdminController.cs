using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWebShop.ViewModel;
using ComputerWebShop.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;


namespace ComputerWebShop.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public UserManager<ApplicationUser> UserManager { get; private set; }
         public AdminController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            
        }
       
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            OrderViewModel objOrderViewModel = new OrderViewModel();
            objOrderViewModel.cOrders = db.Orders.OrderByDescending(m=>m.OrderID).ToList();
            return View(objOrderViewModel);
        }
        //[ChildActionOnlyAttribute]
        //public ActionResult RenderOrders(string CartID)
        //{

        //    OrderViewModel objOrderViewModel = new OrderViewModel();
        //   objOrderViewModel.Cart= db.Cart.Include("Products").Where(m => m.cartID == CartID).ToList();
        //   int subtotal= db.Cart.Where(m => m.cartID == CartID).Select(g => g.Price).Sum();
        //   objOrderViewModel.Subtotal = Convert.ToInt16(subtotal);
        //    return PartialView(objOrderViewModel);
        //}

        public ActionResult CheckOrders(string CartID) 
        {
            
            OrderViewModel objOrderViewModel = new OrderViewModel();
            Cart objCart = new Cart();             
            var result = db.Cart.Include("Products").Where(m => m.cartID == CartID ).ToList();
            var CustomerID = db.Orders.Where(m => m.CartID == CartID).FirstOrDefault().CustomerID;
            ViewBag.Customer = db.Customer.Find(CustomerID);
            ViewBag.Total =db.Cart.Where(m => m.cartID == CartID).Select(g => g.Price).Sum();

            return View(result.ToList());
        }

        public ActionResult ListProducts() 
        {
            return View(db.Products.ToList());
        }
        public ActionResult Create() 
        {
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "CategoryName");

            return View();
        
        }
        public ActionResult Edit(int id)
        {
            
            var result = db.Products.Find(id);
            Products products = new Products()
            {
                ProductName = result.ProductName,
                Brand = result.Brand,
                DateAdded = DateTime.Now,
                ProdDesc = result.ProdDesc,
                ProductCategory = result.ProductCategory,
                stock = result.stock,
                    
                Price = result.Price,
            };
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "CategoryName",products.CategoryID);
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products products)
        {
          products.PrimaryImage = db.Products.Where(m=>m.ID== products.ID).FirstOrDefault().PrimaryImage;
            if (ModelState.IsValid)
            {
                var result = db.Products.Find(products.ID);
                result.ProductName = products.ProductName;
                result.Brand = products.Brand;
                result.ProductCategory = products.ProductCategory;
                result.ProdDesc = products.ProdDesc;
                result.PrimaryImage = products.PrimaryImage;
                result.stock = products.stock;
                result.Price = products.Price;
                result.CategoryID = products.CategoryID;
                db.SaveChanges();
                return RedirectToAction("ListProducts","Admin");
            }
            ViewBag.CategoryID = new SelectList(db.Category, "ID", "CategoryName", products.CategoryID);
            return View(products);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel products)
        {
            if (ModelState.IsValid)
            {


            var ValidImageTyps = new string[]
                {
                "image/gif",
                "image/jpeg",
                "image/pjqpeg",
                "image/png"
                };

                if(!ValidImageTyps.Contains(products.PrimaryImage.ContentType))
                {
                   ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image");

                   
                }

                var ProductCategory = db.Category.Find(products.CategoryID);

            
                  Products Products = new Products()
                {
                    ProductName = products.ProductName,
                    Brand = products.Brand,
                    DateAdded = DateTime.Now,
                    ProdDesc = products.ProdDesc,
                    stock = products.stock,
                    Price = products.Price,
                    ProductCategory = ProductCategory.CategoryName,
                    
                    CategoryID = products.CategoryID
                };

                if(products.PrimaryImage!=null&& products.PrimaryImage.ContentLength>0)
                {
                    var UploadDir = "/Content/images";
                        var imagePath = Path.Combine(Server.MapPath(UploadDir),products.PrimaryImage.FileName);
                    var imageUrl = Path.Combine(@"/Content/images/",products.PrimaryImage.FileName);
                    products.PrimaryImage.SaveAs(imagePath);
                    Products.PrimaryImage = imageUrl;
                    
                }


                db.Products.Add(Products);
                db.SaveChanges();
                return RedirectToAction("ListProducts");
            }

            ViewBag.CategoryID = new SelectList(db.Category, "ID", "CategoryName", products.CategoryID);
            return View(products);
        }
        public ActionResult Delete(int? id)
        {
            
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            var checknull1 = db.Cart.Where(m => m.ProductID == id).FirstOrDefault();
            if (checknull1 != null)
            {
                var Item = db.Cart.Where(m => m.ProductID == id).ToList();
                foreach (var item in Item)
                {
                    db.Cart.Remove(item);
                }
            }
            var checknull = db.ProductsComments.Where(m => m.ProductID == id).FirstOrDefault();
            if (checknull != null)
            {
                var Item2 = db.ProductsComments.Where(m => m.ProductID == id).ToList();
                foreach (var item in Item2)
                {
                    db.ProductsComments.Remove(item);
                }
            }
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("ListProducts");
        }

        public ActionResult AddAdmin() 
        {


            return View();
        
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAdmin(AddAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() {Email=model.Email, UserName = model.Email,EmailConfirmed=true,ConfirmationToken="000000"};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                   
                    db.SaveChanges();

                    await this.UserManager.AddToRoleAsync(user.Id, "Admin");
                   
                    return RedirectToAction("AdminList", "Admin");
                }
              
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult AdminList(string Status)
        {
            if (Status != null) 
            {
                ViewBag.Message = Status;
            }
            
            var users = from u in db.Users

                        from ur in u.Roles

                        join r in db.Roles on ur.RoleId equals r.Id
                        select new AddAdminViewModel
                        {

                            ID = u.Id,
                            Email = u.Email,
                            RoleType = r.Name,

                        };

            return View(users);
        }
        public ActionResult DeleteAdmin(string ID)
        {
            try
            {
                if (ID != null)
                {

                    var userAdmin = db.Users.Find(ID);
                    var userCustomer = db.Users.Find(ID);
                    UserManager.RemoveFromRole(ID,"Admin");
                    UserManager.RemoveFromRole(ID, "Customers");

                    db.Users.Remove(userAdmin);
                    db.Users.Remove(userCustomer);



                    db.SaveChanges();

                }
                return RedirectToAction("AdminList", new { status = "Successfully Deleted" });
            }
            catch (Exception err)
            {

                return RedirectToAction("AdminList", new { status = "Error While Deleting" });
            }

        }
        public ActionResult DeleteUser()
        {
            var users = from u in db.Users
                        where u.Roles.Any(r => r.RoleId == "2")
                        select new
                        {
                            u.Email,
                            u.Id
                        }
                        ;

            List<ViewModelUser> ListViewModelUser = new List<ViewModelUser>();
            foreach (var item in users)
            {
                ViewModelUser objViewModelUser = new ViewModelUser();
                objViewModelUser.UserID = item.Id;
                objViewModelUser.Email = item.Email;
                ListViewModelUser.Add(objViewModelUser);
            }
            return View(ListViewModelUser);
        }
        [HttpGet]
        public ActionResult DeleteUserConfirm(string ID)
        {

            //   db.Roles.Remove(db.Roles.Where(m => m.Id == ID).Single());
            db.Customer.Remove(db.Customer.Where(m => m.ApplicationUserID == ID).Single());
            db.Users.Remove(db.Users.Where(usr => usr.Id == ID).Single());

            db.SaveChanges();

            return RedirectToAction("DeleteUser");
        }

        // Create Category
        public ActionResult CreateCategory()
        {
            
            return View();
        }
        public ActionResult ListCategory() 
        {

            return View(db.Category.ToList());
        }
        //Post: /Category/SaveData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category Category)
        {
            db.Category.Add(Category);
            db.SaveChanges();
            return RedirectToActionPermanent("ListCategory");
        }
	}
}