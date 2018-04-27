using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TextbookTradingServiceLayer.BusinessLogic;
using TextbookTradingServiceLayer.BusinessLogic.Responses;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Distribute : IDistribute
    {
        public string IsAlive()
        {
            string availableServices = @"Supported Methods:";
            availableServices += "boutinvm.eastus.cloudapp.azure.com/Distribute.svc/Authenticate ";
            availableServices += "boutinvm.eastus.cloudapp.azure.com/Distribute.svc/NewUser ";
            availableServices += "boutinvm.eastus.cloudapp.azure.com/Distribute.svc/NewListing";
            return availableServices;
        }

        /// <summary>
        /// Receives the data the client sends when logging in, and if succesful returns the logged in User's Id.
        /// If unsuccessful, returns an error.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public string Authenticate(LoginDetails details)
        {
            try
            {

                return AuthenticateUser.Auth(details);
            }
            catch(Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);

            }
        }

        public string CreateNewUser(NewUser details)
        {
            string response;
            try
            {

                response = New.User(details);
            }catch(Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);
            }
            return response;
        }

        public string NewListing(ListingDetails details)
        {
            string response;

            try
            {
                response = New.Listing(details);
            }
            catch(Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);
            }
            return response;
        }

        public string GetProfile(int x)
        {
            try
            {
                return Profile.Get(x);
            }
            catch (Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);
            }

        }
        public string AcceptPurchase(AcceptPurchase details)
        {
            try
            {
                return Purchase.Accept(details);
            }
            catch (Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);
            }

        }

        public string SearchTransactions(ProductDetails details)
        {
            try
            {
                return JsonConvert.SerializeObject(Products.SearchTransactions(details));
            }
            catch(Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);
            }
        }

        public string GetTransactionDetails(int transactionId)
        {
            try
            {
                // Test This
                return JsonConvert.SerializeObject(Transactions.Get(transactionId));
            }
            catch(Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);
            }
        }

        public string UpdateProfile(UpdateUser details)
        {
            try
            {
                return Profile.Update(details);
            }
            catch(Exception e)
            {
                Dictionary<string, string> errorResult = new Dictionary<string, string>();
                errorResult.Add("Status", "0");
                errorResult.Add("Error Object", JsonConvert.SerializeObject(e));
                return JsonConvert.SerializeObject(errorResult);
            }
        }

        public string MakeBid(BidData details)
        {
            try
            {
                return BusinessLogic.Responses.Bid.Make(details);
            }
            catch(Exception e)
            {
                ResponseData r = new ResponseData();
                r.Status = 0;
                r.Schema = "Error";
                r.Data.Add("Error Message", e.Message);
                return JsonConvert.SerializeObject(r);
            }
        }

        public string GetBid(int transactionId)
        {
            try
            {
                return BusinessLogic.Responses.Bid.Get(transactionId);
            }
            catch(Exception e)
            {
                ResponseData r = new ResponseData();
                r.Status = 0;
                r.Schema = "Error";
                r.Data.Add("Error Message", e.Message);
                return JsonConvert.SerializeObject(r);
            }
        }

    }
}
