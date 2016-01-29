using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ComputerWebShop.Models;
using ComputerWebShop.Services;
using ComputerWebShop.ViewModel;

namespace ComputerWebShop.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            
        }
       
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login

       


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView();
        }

        public ActionResult Dashboard(string panel) 
        {
            ViewBag.panel = "edit";
            OrderViewModel objOrderViewModel = new OrderViewModel(); 

            if (panel != null) 
            {
                ViewBag.panel = panel;
                if (panel == "paymentinfo") 
                {
                    string applicationuserid1 = User.Identity.GetUserId();
                    int CustID = db.Customer.Where(m => m.ApplicationUserID == applicationuserid1).FirstOrDefault().ID;
                    objOrderViewModel.PaymentInfo = db.PaymentInfo.Where(m => m.CustomerID == CustID).FirstOrDefault();
                }
                if (panel == "ListPaymentInfo") 
                {
                    objOrderViewModel.cPaymentInfo = db.PaymentInfo.ToList();
 
                }
            }
            string applicationuserid = User.Identity.GetUserId();
            objOrderViewModel.Customer= db.Customer.Where(m => m.ApplicationUserID == applicationuserid).FirstOrDefault();
            
            return View(objOrderViewModel);
        }
        public ActionResult EditCustomerDetails() 
        {
            OrderViewModel objOrderViewModel = new OrderViewModel(); 
            ViewBag.panel ="edit";
            string applicationuserid = User.Identity.GetUserId();
            objOrderViewModel.Customer = db.Customer.Where(m => m.ApplicationUserID == applicationuserid).FirstOrDefault();
            return PartialView("PartialDashboard", objOrderViewModel);
            
            
        }
        [HttpPost]
        public ActionResult EditCustomerDetails(FormCollection FormCollection)
        {

          

                var result = db.Customer.Find(Convert.ToInt16(FormCollection["ID"]));
                result.FirstName = FormCollection["firstName"].ToString();
                result.LastName = FormCollection["lastName"].ToString();
                result.Address = FormCollection["Address"].ToString();
                result.Gender = FormCollection["PhoneNumber"].ToString();
                result.PhoneNumber = FormCollection["Customer.Gender"].ToString();
                result.PostalCode = FormCollection["PostalCode"].ToString();
                result.State = FormCollection["City"].ToString();
                result.City = FormCollection["State"].ToString();
                result.Country = FormCollection["Country"].ToString();
                db.SaveChanges();
                return RedirectToAction("Dashboard", new { panel = "edit" });
            
           
        }
        public ActionResult UpdatePayment()
        {
            
            ViewBag.panel = "paymentinfo";

            return PartialView("PartialDashboard");
        }
        [HttpPost]
        public ActionResult UpdatePayment(FormCollection FormCollection)
        {
                var ApplicationUserID  =    User.Identity.GetUserId();
                var ID  = db.Customer.Where(m => m.ApplicationUserID == ApplicationUserID).FirstOrDefault().ID;
            PaymentInfo  PaymentInfo = new PaymentInfo();
                 PaymentInfo.CartNumber = FormCollection["CardNumber"].ToString();
                PaymentInfo.SecurityCode = FormCollection["SecurityCode"].ToString();
                PaymentInfo.ExpiryDate = FormCollection["cardExpiry"].ToString();
                PaymentInfo.CustomerID = ID;
                db.PaymentInfo.Add(PaymentInfo);

            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        //List Payement Options 
        public ActionResult ListPaymentOption() 
        {
            
            OrderViewModel objOrderViewModel = new OrderViewModel();    
           objOrderViewModel.cPaymentInfo=  db.PaymentInfo.ToList();
            ViewBag.panel = "ListPaymentInfo";
            return PartialView("PartialDashboard",objOrderViewModel);
        }
        public ActionResult ProductsBought()
        {
            ViewBag.panel = "productsbought";
            string applicationuserid = User.Identity.GetUserId();
            int CustID = db.Customer.Where(m => m.ApplicationUserID == applicationuserid).FirstOrDefault().ID;
            OrderViewModel objOrderViewModel = new OrderViewModel();
            objOrderViewModel.cOrders = db.Orders.Where(m => m.CustomerID == CustID).ToList();
            // objOrderViewModel.Cart = db.Cart.Include("Products").Where(m => m.cartID == objOrderViewModel.Orders.CartID).ToList();

            return PartialView("PartialDashboard", objOrderViewModel);
        }
        [ChildActionOnlyAttribute]
        public ActionResult RenderProducts(string CartID)
        {
            Cart oCart = new Cart();
            var Productslist = db.Cart.Include("Products").Where(m => m.cartID == CartID).ToList();
            return PartialView(Productslist.ToList());
        }
        [AllowAnonymous]
        public ActionResult EnterEmail(string message) 
        {
            if (message == "successfull")
            {
                ViewBag.sent = message;
            }
            if (message == "notexist") 
            {
                ViewBag.sent = message;
            }
           return  View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult EnterEmail(FormCollection FormCollection )
        {

            EmailService objEmailService = new EmailService();
            string confirmationtoken = objEmailService.CreateConfirmationToken();
            string Email = FormCollection["Email"].ToString();
            var checkExist = db.Users.Where(m => m.UserName == Email).FirstOrDefault();
            if (checkExist != null)
            {
                Retrievepassword objRetrievepassword = new Retrievepassword() { ConfirmationToken = confirmationtoken ,Email=Email };
                db.Retrievepassword.Add(objRetrievepassword);
                db.SaveChanges();


                objEmailService.SendEmailConfirmation(Email, confirmationtoken);
                return RedirectToAction("EnterEmail", new { message = "successfull" });
            }
            else 
            {
                return RedirectToAction("EnterEmail", new { message = "notexist" });
            }
        }
        [AllowAnonymous]
        public ActionResult ChangePassword(String ID)
            {
            
            if (ID != null)
            {
                
                var result = db.Retrievepassword.Where(m => m.ConfirmationToken == ID).FirstOrDefault();
                if (result != null)
                {
                    if (Request.IsAuthenticated)
                    {
                        AuthenticationManager.SignOut();
                    }
                    ManageUserViewModel objManageUserViewModel = new ManageUserViewModel() { ConfirmationToken=ID };
                    return View(objManageUserViewModel);
                }
                else 
                {
                    ViewBag.expired = "Link has Expired";
                      return RedirectToAction("Login","Account");
                }
            }
            {
                return RedirectToAction("Index","Home");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
           
            
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    var checkexist = db.Retrievepassword.Where(m => m.ConfirmationToken == model.ConfirmationToken).FirstOrDefault();
                    var userid = db.Users.Where(m => m.UserName == checkexist.Email).FirstOrDefault();
                    IdentityResult result = await UserManager.RemovePasswordAsync(userid.Id);
                    result = await UserManager.AddPasswordAsync(userid.Id,model.NewPassword);
                    if (result.Succeeded)
                    {
                        Retrievepassword Retrievepassword = db.Retrievepassword.Where(m=>m.ConfirmationToken==model.ConfirmationToken).FirstOrDefault();
                        db.Retrievepassword.Remove(Retrievepassword);
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            

            // If we got this far, something failed, redisplay form
            return View(model);
        }
         

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                   

                        await SignInAsync(user, model.RememberMe);

                        if (UserManager.IsInRole(user.Id, "Admin"))
                        {
                            return RedirectToAction("Index","Admin");
                        }
                        if (user.EmailConfirmed)
                        {
                        return RedirectToLocal(returnUrl);
                    }
                    else {
                        return RedirectToAction("ConfirmAccount","Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        
        }
        private bool ConfirmAccount1(string confirmationToken)
        {
            
            ApplicationUser user = db.Users.SingleOrDefault(u => u.ConfirmationToken == confirmationToken);
            if (user != null)
            {
                user.EmailConfirmed = true;
                user.ConfirmationToken = "000000000000";
                DbSet<ApplicationUser> dbSet = db.Set<ApplicationUser>();
                dbSet.Attach(user);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            return false;
        }
        public ActionResult RegisterConfirmation(string Id)
        {
            if (ConfirmAccount1(Id))
            {
                return View();
            }
            else
            {
                ViewBag.Error = "Link has expired";
                return View();
                
            }
        }
        //ConfirmAccount
        [Authorize]
        public ActionResult ConfirmAccount(string emailsent)
        {
            if (emailsent=="successfull")
            {

                ViewBag.EmailSent = "Email Sent Sucessfully";
            }
            return View();
        }
        [Authorize]
        public async Task<ActionResult> ResendEmail() 
        {
            EmailService objEmailService = new EmailService();
            string applicationUserId = User.Identity.GetUserId();
            var result = UserManager.FindById(applicationUserId);
            string confirmationToken = objEmailService.CreateConfirmationToken();
            ApplicationUser user = db.Users.SingleOrDefault(u => u.Id == applicationUserId);
            if (user != null)
            {
                user.ConfirmationToken=confirmationToken;
                DbSet<ApplicationUser> dbSet = db.Set<ApplicationUser>();
                dbSet.Attach(user);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                objEmailService.SendEmailConfirmation(result.Email, "", confirmationToken);
            }


            return RedirectToAction("ConfirmAccount", new {emailsent ="successfull" });
            
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                EmailService objEmailService = new EmailService();
                string ConfirmationToken = objEmailService.CreateConfirmationToken();
                var user = new ApplicationUser() {Email=model.Email,  UserName = model.Email,ConfirmationToken= ConfirmationToken   };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    objEmailService.SendEmailConfirmation(model.Email, model.Name, ConfirmationToken);
                    Customer objCustomer = new Customer { FirstName=model.Name,LastName=model.LastName
                        ,Address= model.Address,
                        Gender = model.Gender,
                        PhoneNumber = model.PhoneNumber,
                        City = model.City,
                       Country =model.Country
                       ,State = model.State

                        ,ApplicationUserID= user.Id
                        ,PostalCode=model.PostalCode
                    };
                   
                    db.Customer.Add(objCustomer);
                    db.SaveChanges();
                       var CustomerID =  db.Customer.Where(m=>m.ApplicationUserID==user.Id).FirstOrDefault().ID;
                       PaymentInfo objPaymentInfo = new PaymentInfo() { CustomerID = CustomerID };
                         db.PaymentInfo.Add(objPaymentInfo);
                         db.SaveChanges();
                         await this.UserManager.AddToRoleAsync(user.Id, "Customers");

                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("ConfirmAccount", "Account");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }
        public ActionResult Edit()
        {
            string UserID = User.Identity.GetUserId();
            int id = db.Customer.Where(m=>m.ApplicationUserID == UserID  ).FirstOrDefault().ID;
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}