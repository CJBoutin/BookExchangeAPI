using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class Purchase
    {
        public static string Accept(AcceptPurchase details)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            // Seller marked a sale as complete
            // API sets the transaction as inactive
            using (var db = new TBDataModel())
            {
                var sale = (from b in db.Transactions
                            where b.Id == details.BidId
                            && b.UserId == details.SellerId
                            select b).FirstOrDefault();

                // If the sale doesnt exist, then it doesnt exist
                if(sale == null)
                {
                    throw new Exception("Transaction does not exist.");
                }

                // if the sale exists, set the sale to inactive to mark as complete
                sale.Active = 0;
                sale.DateModified = DateTime.UtcNow;

                db.SaveChanges();

                response.Add("TransactionId", sale.Id.ToString());
            }

            return JsonConvert.SerializeObject(response);
        }
    }
}