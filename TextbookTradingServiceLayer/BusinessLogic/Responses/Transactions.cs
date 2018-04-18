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
    }
}