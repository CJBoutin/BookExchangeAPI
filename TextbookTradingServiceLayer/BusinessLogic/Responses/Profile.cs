using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class Profile
    {
        public static string Get(int profileId)
        {
            Dictionary<string, string> rData = new Dictionary<string, string>();

            using (var db = new TBDataModel())
            {
                var userData = (from b in db.Users
                               where b.Id == profileId
                               select b).FirstOrDefault();

                // if UserData is 0, there was no corresponding userId
                if(userData.Id == 0)
                {
                    throw new Exception("No user exists with that Id.");
                }

                rData.Add("UserName", userData.UserName);
                rData.Add("PhoneNumber", userData.PhoneNumber);
                rData.Add("Rating", userData.Rating.ToString());
            }

                return JsonConvert.SerializeObject(rData);
        }

        public static string Update(UpdateUser details)
        {
            Dictionary<string, string> rData = new Dictionary<string, string>();

            using (var db = new TBDataModel())
            {
                // Get the user we want to edit
                var uToChange = (from b in db.Users
                                 where b.Id == details.UIdToChange
                                 select b).FirstOrDefault();

                uToChange.EmailAddress = details.EmailAddress;
                uToChange.PasswordHash = details.PasswordHash;
                uToChange.PhoneNumber = details.PhoneNumber;

                db.SaveChanges();
            }

            rData.Add("Status", "1");
            rData.Add("Result", "Operation Complete");
            return JsonConvert.SerializeObject(rData);
        }
    }
}