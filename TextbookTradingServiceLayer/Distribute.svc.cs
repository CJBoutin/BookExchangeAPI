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
            return AuthenticateUser.Auth(details);
        }

        public string CreateNewUser(NewUser details)
        {
            string response;
            try
            {

                response = New.User(details);
            }catch(Exception e)
            {
                return e.Message;
            }
            return response;
        }

        public string NewListing(NewListingDetails details)
        {
            string response;

            try
            {
                response = New.Listing(details);
            }
            catch(Exception e)
            {
                return e.Message;
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
                return e.Message;
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
                return e.Message;
            }

        }
    }
}
