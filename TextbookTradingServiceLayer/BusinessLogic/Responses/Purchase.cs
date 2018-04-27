using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class Purchase
    {
        public static string Accept(AcceptPurchase details)
        {
            ResponseData r = new ResponseData();
            // Seller marked a sale as complete
            // API sets the transaction as inactive
            using (var db = new TBDataModel())
            {
                var chosenBid = (from b in db.Bids
                            where b.Id == details.BidId
                            select b).FirstOrDefault();


                // If the sale doesnt exist, then it doesnt exist
                if(chosenBid.Transaction == null || chosenBid.Transaction.Active == 0)
                {
                    throw new Exception("Transaction does not exist.");
                }

                // if the sale exists, set the sale to inactive to mark as complete
                chosenBid.Transaction.Active = 0;
                chosenBid.Transaction.DateModified = DateTime.UtcNow;

                // set Accepted flag to true the bids associated with the item
                // But only if there are no others
                if(db.Bids.Any(x => x.Accepted == 1 && x.TransactionId == chosenBid.TransactionId))
                {
                    // if a bid has already been accepted
                    throw new Exception("There is already a bid accepted for this transaction.");
                }
                chosenBid.Accepted = 1;

                db.SaveChanges();
                r.Status = 1;
                r.Schema = "AcceptPurchase";
                r.Data.Add("Purchase Status", "Accepted");
            }

            return JsonConvert.SerializeObject(r);
        }
    }
}