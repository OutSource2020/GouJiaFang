using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web1.API.Enties;

namespace web1.Filters
{
    public class MyExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            JsonResult jsonResult = new JsonResult();
            BaseResponse baseResponse = new BaseErrors()[(int)BaseErrors.ERROR_NUMBER.LX1016];
            baseResponse.Username = null;
            baseResponse.StatusReply = string.Format(baseResponse.StatusReply, filterContext.Exception.Message);
            jsonResult.Data = baseResponse;
            filterContext.Result = jsonResult;
            base.OnException(filterContext);
        }
    }
}