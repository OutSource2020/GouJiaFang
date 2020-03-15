using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web1.API.Enties
{
    public class OrderCreateRequest : OrderInquireRequest
    {
        public string AimsCardNumber { get; set; }
        public string AimsCardName { get; set; }
        public string AimsCardBank { get; set; }
        public string AimsMoney { get; set; }
        public string CallBack { get; set; }
    }
}