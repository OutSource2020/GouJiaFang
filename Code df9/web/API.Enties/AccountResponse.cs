using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web1.API.Enties
{
    public class AccountResponse : BaseResponse
    {
        public string InquireAccountDeposit { get; set; }
        public string InquireAccountHandlingFee { get; set; }
        public string InquireTime { get; set; }
    }
}