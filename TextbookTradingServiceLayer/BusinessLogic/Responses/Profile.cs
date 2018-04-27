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
            ResponseData r = new ResponseData();

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

                r.Status = 1;
                r.Schema = "GetProfile";
                r.Data.Add("UserName", userData.UserName);
                r.Data.Add("FirstName", userData.FirstName);
                r.Data.Add("LastName", userData.LastName);
                r.Data.Add("PhoneNumber", userData.PhoneNumber);
                r.Data.Add("Rating", userData.Rating.ToString());
            }

                return JsonConvert.SerializeObject(r);
        }

        public static string Update(UpdateUser details)
        {
            ResponseData r = new ResponseData();

            using (var db = new TBDataModel())
            {
                // Get the user we want to edit
                var uToChange = (from b in db.Users
                                 where b.Id == details.UIdToChange
                                 select b).FirstOrDefault();

                // If the hashed password equals the stored hash, then we are good to update info
                if (uToChange.PasswordHash == details.PasswordHash)
                {
                    if (details.NewPwHash != null && details.NewPwHash != "")
                        uToChange.PasswordHash = details.NewPwHash;
                }


                if (details.EmailAddress != null && details.EmailAddress != "")
                uToChange.EmailAddress = details.EmailAddress;

                if (details.PhoneNumber != null && details.PhoneNumber != "")
                    uToChange.PhoneNumber = details.PhoneNumber;

                if (details.FirstName != null && details.FirstName != "")
                    uToChange.FirstName = details.FirstName;

                if (details.LastName != null && details.LastName != "")
                    uToChange.LastName = details.LastName;

                db.SaveChanges();
            }

            r.Status = 1;
            r.Data.Add("Result", "Operation Complete");
            return JsonConvert.SerializeObject(r);
        }
    }
}