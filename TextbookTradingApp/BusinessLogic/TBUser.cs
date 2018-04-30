using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TextbookTradingApp.BusinessLogic
{
    public static class TBUser
    {
        public static string Create(string username, string password, string phonenumber, string emailaddress)
        {
            Dictionary<string, string> uJson = new Dictionary<string, string>();
            uJson.Add("UserName", username);
            string hash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hash = Hash.GetMd5Hash(md5Hash, password);
            }

            // encoded contains the hash you are wanting
            uJson.Add("PasswordHash", hash);
            uJson.Add("PhoneNumber", phonenumber);
            uJson.Add("EmailAddress", emailaddress);

            return JsonConvert.SerializeObject(uJson);
        }


        // Verify a hash against a string.

    }
}
