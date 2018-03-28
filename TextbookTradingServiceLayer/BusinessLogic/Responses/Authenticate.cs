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
            
            string response = "";
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
                        response = JsonConvert.SerializeObject("-1");
                        return response;
                    }

                    // Generate Json Token?


                    response = JsonConvert.SerializeObject(uId);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return response;

        }
    }
}