using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWebShop.Models;
using ComputerWebShop.ViewModel;
using ComputerWebShop.Repository;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace ComputerWebShop.DAL
{
    public class ProductRatingRespository : IRatingRespository
    {
        private ApplicationDbContext db = null;
        public ProductRatingRespository()
        {
            this.db = new ApplicationDbContext();
        }

        public IEnumerable<ProductsRating> SelectAll()
        {
            return db.ProductsRating.ToList();
        }
        public IEnumerable<ProductsRating> SelectByProductID(int id)
        {
            return db.ProductsRating.Where(m => m.ProductID == id).ToList();
        }
        public void Insert(ProductsRating obj)
        {
            db.ProductsRating.Add(obj);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public bool CanUserVote(String applicationuserid)
        {
            if (db.ProductsRating.Where(m => m.ApplicationUserID == applicationuserid).FirstOrDefault() != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int AverageRating(int productID)
        {

            int count = db.ProductsRating.Where(m => m.ProductID == productID).ToList().Count();
            int sum = db.ProductsRating.Where(m => m.ProductID == productID).Select(m => m.ProductRatings).Sum();
            var result = sum / count;
            return result;
        }
        public int Voters(int ProductID)
        {
            var result = db.ProductsRating.Where(m => m.ProductID == ProductID).Count();
            return result;
        }
        public int UserRating(string applicationuserid, int ProductID)
        {
            var result = db.ProductsRating.Where(m => m.ProductID == ProductID && m.ApplicationUserID == applicationuserid).FirstOrDefault();
            return result.ProductRatings;


        }
        public bool CheckProductExist(int ProductID)
        {
            var result = db.ProductsRating.Where(m => m.ProductID == ProductID).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int GetID(int ProductID, string ApplicationUserID)
        {
            var result = db.ProductsRating.Where(m => m.ProductID == ProductID && m.ApplicationUserID == ApplicationUserID).FirstOrDefault();
            if (result != null)
            {
                return result.ID;
            }
            else
            {
                return 0;
            }
        }




    }
}