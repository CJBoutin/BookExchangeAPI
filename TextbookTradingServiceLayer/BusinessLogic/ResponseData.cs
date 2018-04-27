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
        public Dictionary<string, dynamic> Data { get => data; set => data = value; }

        private Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

    }
}