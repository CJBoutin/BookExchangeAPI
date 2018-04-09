using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextbookTradingServiceLayer.EntityFramework;

namespace TextbookTradingServiceLayer.BusinessLogic.Responses
{
    public static class AuthenticateUser
    {
        public static string Auth(LoginDetails details)
        {

            Dictionary<string, string> rDict = new Dictionary<string, string>();
            try
            {
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
                        rDict.Add("Status", "0");
                        rDict.Add("UserId", "Null");
                        return JsonConvert.SerializeObject(rDict);
                    }

                    // Generate Json Token?

                    rDict.Add("Status", "1");
                    rDict.Add("UserId", uId.ToString());
                    return JsonConvert.SerializeObject(rDict);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}