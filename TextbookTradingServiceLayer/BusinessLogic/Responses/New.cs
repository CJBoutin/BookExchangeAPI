using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class New
    {
        /// <summary>
        /// Creates a new user based on the passed in 'details' parameter
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static string User(NewUser details)
        {
            ResponseData r = new ResponseData();
            using (var db = new TBDataModel())
            {
                // Check to see if the username already exists. If it does, return an error
                var usernameId = (from b in db.Users
                                 where b.UserName == details.UserName
                                 select b.Id).FirstOrDefault();
                // If this evaluates to true, then the username is already in use
                if(usernameId != 0)
                {
                    Dictionary<string, string> error = new Dictionary<string, string>();
                    error.Add("Error", "Username already exists.");
                    error.Add("ErrorCode", "1");
                    return JsonConvert.SerializeObject(error);
                }

                // If the username is okay, add data to db
                User newUser = new User();
                newUser.PasswordHash = details.PasswordHash;
                newUser.PhoneNumber = details.PhoneNumber;
                newUser.UserName = details.UserName;
                newUser.EmailAddress = details.EmailAddress;

                db.Users.Add(newUser);
                // Save user Changes
                db.SaveChanges();

                r.Status = 1;
                r.Schema = "NewUser";
                r.Data.Add("UserId", newUser.Id.ToString());
                r.Data.Add("UserName", newUser.UserName);
            }

            return JsonConvert.SerializeObject(r);
        }

        /// <summary>
        /// Creates a new listing based on user input
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static string Listing(ListingDetails details)
        {
            string response = "";
            using (var db = new TBDataModel())
            {
                int pId = (from b in db.Products
                          where b.ISBN == details.ISBN
                          && b.Title == details.Name
                          select b.Id).FirstOrDefault();

                // If this product doesnt exist
                if(pId==0)
                {
                    var newProd = new Product();
                    newProd.Author = details.Author;
                    newProd.ISBN = details.ISBN;
                    newProd.Publisher = details.Publisher;
                    newProd.Title = details.Name;
                    newProd.UserId = details.UserId;

                    db.Products.Add(newProd);

                    db.SaveChanges();

                    pId = newProd.Id;
                }
                // Make new listing
                var newListing = new Transaction();               

                newListing.Active = 1;
                newListing.DateCreated = DateTime.UtcNow;
                newListing.DateModified = DateTime.UtcNow;
                newListing.Description = details.Description;
                newListing.Price = details.ListPrice;
                newListing.ProductId = pId;
                newListing.UserId = details.UserId;

                db.Transactions.Add(newListing);

                // Add the images uploaded to the database
                foreach (var im in details.Images)
                {
                    var image = new Image();
                    image.ImageData = Convert.FromBase64String(im);
                    image.ProductId = pId;
                    image.UserId = details.UserId;

                    db.Images.Add(image);
                }

                db.SaveChanges();

                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("NewListingId", newListing.Id.ToString());
                response = JsonConvert.SerializeObject(r);
            }
                return response;
        }
    }
}