using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TextbookTradingApp.BusinessLogic
{
    public class RequestSender
    {
#if DEBUG
        //private string _apiDomain = @"http://localhost:64581/Distribute.svc/";
        private string _apiDomain = @"http://boutinvm.eastus.cloudapp.azure.com/Distribute.svc/";

#else
        private string _apiDomain = @"http://boutinvm.eastus.cloudapp.azure.com/Distribute.svc/";
#endif
        public string SendPost(string UriTemplate, string JsonData)
        {
            string fulluri = _apiDomain + UriTemplate;
            // sends a post to the API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fulluri);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var sw = new StreamWriter(request.GetRequestStreamAsync().Result))
            {
                sw.Write(JsonData);
                sw.Flush();
            }

            var response = request.GetResponseAsync().Result;
            using (var sReader = new StreamReader(response.GetResponseStream()))
            {
                return sReader.ReadToEnd();
            }
        }

        public string SendGet(string UriTemplate)
        {
            string fulluri = _apiDomain + UriTemplate;
            // sends a post to the API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fulluri);
            request.Method = "GET";

            request.UseDefaultCredentials = true;

            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            return readStream.ReadToEnd();

        }
    }
}
