using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerWebShop.Models;
using ComputerWebShop.ViewModel;
namespace ComputerWebShop.Repository
{
     interface IRatingRespository
    {
        IEnumerable<ProductsRating> SelectAll();
        IEnumerable<ProductsRating> SelectByProductID(int id);
        void Insert(ProductsRating obj);
        void Save();
        int UserRating(string applicationuserid,int ProductID);
        bool CheckProductExist(int ProductID);
        bool CanUserVote(String applicationuserid);
        int AverageRating(int productsID);
        int Voters(int ProductID);
        int GetID(int ProductID,string applicationuserid  );
        
    }
}