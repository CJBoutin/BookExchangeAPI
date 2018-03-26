using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TextbookTradingServiceLayer.BusinessLogic;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Distribute : IDistribute
    {
        public string IsAlive()
        {
            return "Alive";
        }

        /// <summary>
        /// Receives the data the client sends when logging in, and if succesful returns the logged in User's Id.
        /// If unsuccessful, returns an error.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public string Authenticate(LoginDetails details)
        {
            string response = "";
            using (TBDataModel db = new TBDataModel())
            {
                // Check to see if the user-password combination exists

                var uId = (from b in db.Users
                          where 
                          b.PasswordHash == details.PasswordHash
                          && b.UserName == details.UserName
                          select b.Id).FirstOrDefault<int>();

                // if the UserId is -1 then the value doesnt exist
                if (uId == 0)
                {
                    response = JsonConvert.SerializeObject("-1");
                    return response;
                }

                response = JsonConvert.SerializeObject(uId);
            }


                return response;
        }

        public string CreateNewUser(NewUser details)
        {

            string response = New.User(details);
            return response;
        }

        public string NewListing(NewListingDetails details)
        {
            string response = New.Listing(details);

            return response;
        }
    }
}
