﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web1.API.Enties
{
    public class AccountRequest : BaseRequest
    {
        public string UserPassword { get; set; }
    }
}