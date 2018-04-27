using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class Bid
    {
        public static string Make(BidData details)
        {
            ResponseData r = new ResponseData();
            using (var db = new TBDataModel())
            {
                // using the bid details
                // Add new bid to database
                EntityFramework.Bid newBid = new EntityFramework.Bid();
                newBid.ProposedPrice = details.ProposedPrice;
                newBid.TransactionId = details.TransactionId;
                newBid.UserId = details.PurchaserId;
                newBid.Accepted = 0;

                db.Bids.Add(newBid);
                db.SaveChanges();

                r.Status = 1;
                r.Schema = "NewBid";
                r.Data.Add("BidId", newBid.Id.ToString());

            }

            return JsonConvert.SerializeObject(r);
        }

        public static string Get(int transactionId)
        {
            ResponseData r = new ResponseData();
            using (var db = new TBDataModel())
            {
                // Get all bids associated with this transaction
                var bids = from b in db.Bids
                           where b.TransactionId == transactionId
                           select b;

                // Return this list of bids
                var bList = new List<BidFlash>();
                foreach(var item in bids)
                {
                    bList.Add(new BidFlash { Id = item.Id, ProposedPrice = item.ProposedPrice });
                }
                //var bidJson = JsonConvert.SerializeObject(bList);

                r.Status = 1;
                r.Schema = "GetBids";
                r.Data.Add("BidList", bList);
            }

            return JsonConvert.SerializeObject(r);
        }
    }

    class BidFlash
    {
        public int Id { get; set; }
        public int ProposedPrice { get; set; }
    }
}