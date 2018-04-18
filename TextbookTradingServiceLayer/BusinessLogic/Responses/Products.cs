using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class Products
    {
        /// <summary>
        /// Searches the database for the specified criteria. If none are found, the list will be empty.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static List<ListingDetails> SearchTransactions(ProductDetails details)
        {
            List<ListingDetails> tDetails = new List<ListingDetails>();
            List<int> productIds = new List<int>();
            using (var db = new TBDataModel())
            {
                var products = from b in db.Products
                                   where b.ISBN.Contains(details.ISBN)
                                   || b.Title.Contains(details.Title)
                                   || b.Author.Contains(details.Author)
                                   select b;

                foreach(var item in products)
                {
                    productIds.Add(item.Id);
                }

                var transactionList = from t in db.Transactions
                                      where productIds.Any(x => t.ProductId == x)
                                      select t;


                foreach(var item in transactionList)
                {
                    tDetails.Add(new ListingDetails
                    {
                        UserId = item.UserId,
                        Name = item.Product.Title,
                        Author = item.Product.Author,
                        Publisher = item.Product.Publisher,
                        ListPrice = item.Price,
                        Negotiable = item.Negotiable,
                        ISBN = item.Product.ISBN,
                        Description = item.Description
                    });
                }
            }

                return tDetails;
        }
    }
}