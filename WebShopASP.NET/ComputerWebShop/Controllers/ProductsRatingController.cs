using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWebShop.Models;
using ComputerWebShop.ViewModel;
using ComputerWebShop.Repository;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using ComputerWebShop.DAL;

namespace ComputerWebShop.Controllers
{
    public class ProductsRatingController : BaseController
    {
        private IRatingRespository repository = null;
        //
        // GET: /ProductsRating/
        public ProductsRatingController()
        {
            this.repository = new ProductRatingRespository();
        }

        [ChildActionOnly]
        public ActionResult ShowRating(int ProductID)
        {

            ProductRatingViewModel objProductRatingViewModel = new ProductRatingViewModel();
            if (repository.CheckProductExist(ProductID) == true)
            {
                objProductRatingViewModel.AverageRating = repository.AverageRating(ProductID);
                objProductRatingViewModel.Voters = repository.Voters(ProductID);
                if (Request.Cookies["rating" + repository.GetID(ProductID, User.Identity.GetUserId())] != null)
                {
                    objProductRatingViewModel.CanUserVote = false;
                    objProductRatingViewModel.UserRating = repository.UserRating(User.Identity.GetUserId(), ProductID);
                }
                else
                {
                    objProductRatingViewModel.CanUserVote = true;
                }

            }

            else
            {
                objProductRatingViewModel.CanUserVote = true;

            }

            return View(objProductRatingViewModel);

        }
        
        public ActionResult OnlyDisplayRating(int ProductID)
        {
            ProductRatingViewModel objProductRatingViewModel = new ProductRatingViewModel();
            if (repository.CheckProductExist(ProductID) == true)
            {
                objProductRatingViewModel.AverageRating = repository.AverageRating(ProductID);
            }
            else
            {
                objProductRatingViewModel.AverageRating = 0;
            }
            return View(objProductRatingViewModel);
        }
        [HttpPost]
        public JsonResult SaveRating(ProductRatingViewModel objProductRatingViewModel)
        {
            //int number = 
            //int ProductID = Convert.ToInt32(FormCollection["ProductID"]);

            ProductsRating objProductsRating = new ProductsRating();
            objProductsRating.ProductID = objProductRatingViewModel.ProductID;
            objProductsRating.ProductRatings = objProductRatingViewModel.ProductRatings;

            objProductsRating.ApplicationUserID = User.Identity.GetUserId();
            repository.Insert(objProductsRating);
            repository.Save();

            HttpCookie cookie = new HttpCookie("rating" + repository.GetID(objProductRatingViewModel.ProductID, User.Identity.GetUserId()));
            cookie.Value = DateTime.Now.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);


            objProductRatingViewModel.AverageRating = repository.AverageRating(objProductRatingViewModel.ProductID);
            objProductRatingViewModel.Voters = repository.Voters(objProductRatingViewModel.ProductID);

            return Json(objProductRatingViewModel);

        }
    }
}