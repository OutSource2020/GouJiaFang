using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web1.API.Enties
{
    public class BaseResponse
    {
        public string StatusReplyNumbering { get; set; }
        public string StatusReply { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
    }
}