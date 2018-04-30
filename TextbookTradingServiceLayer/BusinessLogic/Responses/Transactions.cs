using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class Transactions
    {
        public static Dictionary<string, dynamic> Get(int tId)
        {
            Dictionary<string, dynamic> returnData = new Dictionary<string, dynamic>();

            using (var db = new TBDataModel())
            {
                var transaction = (from t in db.Transactions
                                   where t.Id == tId
                                   select t).FirstOrDefault();
                var innerProduct = transaction.Product;
                ListingDetails lDetails = new ListingDetails();
                lDetails.Author = innerProduct.Author;
                lDetails.Name = innerProduct.Title;
                lDetails.ListPrice = transaction.Price;
                lDetails.ISBN = innerProduct.ISBN;
                lDetails.Negotiable = transaction.Negotiable;
                lDetails.Publisher = innerProduct.Publisher;
                lDetails.UserId = transaction.UserId;

                // Get images
                var images = from b in db.Images
                             where b.ProductId == innerProduct.Id
                             select b;
                List<string> imageB64 = new List<string>();
                foreach(var im in images)
                {
                    imageB64.Add(Convert.ToBase64String(im.ImageData));
                }
                lDetails.Images = imageB64;

                BasicUserData bud = new BasicUserData();

                bud.UserName = transaction.User.UserName;
                bud.PhoneNumber = transaction.User.PhoneNumber;

                returnData.Add("TransactionData", lDetails);
                returnData.Add("UserData", bud);

            }

            return returnData;
        }

        public static string GetUserTransactions(int uId)
        {
            ResponseData r = new ResponseData();
            using (var db = new TBDataModel())
            {
                var transactions = from t in db.Transactions
                                   where t.UserId == uId
                                   && t.Active == 1
                                   select t;
                List<ListingDetails> listingList = new List<ListingDetails>();
                foreach(var item in transactions)
                {
                    ListingDetails l = new ListingDetails();
                    l.Author = item.Product.Author;
                    l.Description = item.Description;
                    l.ISBN = item.Product.ISBN;
                    l.ListPrice = item.Price;
                    l.Name = item.Product.Title;
                    l.Negotiable = item.Negotiable;
                    l.Publisher = item.Product.Publisher;
                    l.UserId = item.UserId;
                    l.TransactionId = item.Id;

                    listingList.Add(l);
                }
                r.Data.Add("Transactions", listingList);
            }
            r.Status = 1;
            r.Schema = "GetUserTransactions";
            return JsonConvert.SerializeObject(r);

        }
    }
}