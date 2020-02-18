using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web1.API.Enties
{
    public class OrderInquireResponse : BaseResponse
    {
        public string OrderNumberSite { get; set; }
        public string OrderNumberMerchant { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string OrderTimeCreation { get; set; }
        public string OrderTimeEnd { get; set; }
    }
}