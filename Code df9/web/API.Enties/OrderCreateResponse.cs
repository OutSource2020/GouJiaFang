using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web1.API.Enties
{
    public class OrderCreateResponse : AccountResponse
    {
        public string OrderNumberSite { get; set; }
        public string OrderNumberMerchant { get; set; }
        public string AimsCardNumber { get; set; }
        public string AimsCardName { get; set; }
        public string AimsCardBank { get; set; }
        public string AimsMoney { get; set; }
    }
}