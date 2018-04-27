using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TextbookTradingServiceLayer.BusinessLogic
{
    public class ResponseData
    {
        public int Status { get; set; }
        public string Schema { get; set; }
        public Dictionary<string, string> Data { get => data; set => data = value; }

        private Dictionary<string, string> data = new Dictionary<string, string>();

    }
}