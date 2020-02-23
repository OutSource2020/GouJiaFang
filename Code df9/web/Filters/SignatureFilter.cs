using SqlSugar;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using web1.API.Enties;

namespace web1.Filters
{
    public class SignatureFilter : ActionFilterAttribute
    {
        private static long MAX_ERROR_COUNT = 5;

        private SqlSugarClient sqlSugarClient = new DBClient().GetClient();
        private string signKey = "signature";
        private string tsKey = "timeunix";

        private JsonResult GetStandardError(BaseErrors.ERROR_NUMBER errorNumber, string username)
        {
            JsonResult jsonResult = new JsonResult();
            BaseResponse baseResponse = new BaseErrors()[(int)errorNumber];
            baseResponse.Username = username;
            jsonResult.Data = baseResponse;
            return jsonResult;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void VerifyPassword(ActionExecutingContext filterContext, string path, table_商户账号 dbAccount, string password)
        {
            if (dbAccount.商户密码API != password)
            {
                if (dbAccount.登入错误累计 > MAX_ERROR_COUNT)
                {
                    filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1008, dbAccount.商户ID);
                }
                else
                {
                    dbAccount.登入错误累计++;
                    sqlSugarClient.Updateable(dbAccount).UpdateColumns(it => new { it.登入错误累计 }).ExecuteCommand(); ;
                    filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1005, dbAccount.商户ID);
                }
            }
        }

        private void VerifySignature(ActionExecutingContext filterContext, string path, table_商户账号 dbAccount)
        {
            string apiPassword = dbAccount.商户密码API;
            string secret = dbAccount.公共密匙;
            int? timeunix = filterContext.ActionParameters[tsKey] as int?;
            string signature = filterContext.ActionParameters[signKey] as string;
            string source;
            if (path.Contains("AccountInquiry"))
                source = dbAccount.商户ID + apiPassword + timeunix + secret;
            else if (path.Contains("OrderInquire"))
            {
                OrderInquireRequest orderInquireRequest = filterContext.ActionParameters["request"] as OrderInquireRequest;
                source = dbAccount.商户ID + apiPassword + timeunix + orderInquireRequest.OrderNumberMerchant + secret;
            }
            else if (path.Contains("OrderCreate"))
            {
                OrderCreateRequest orderCreateRequest = filterContext.ActionParameters["request"] as OrderCreateRequest;
                source = dbAccount.商户ID + apiPassword + timeunix 
                    + orderCreateRequest.OrderNumberMerchant 
                    + orderCreateRequest.AimsCardNumber
                    + orderCreateRequest.AimsCardName
                    + orderCreateRequest.AimsCardBank
                    + orderCreateRequest.AimsMoney
                    + secret;
            }
            else
            {
                filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1018, dbAccount.商户ID);
                return;
            }
            using (MD5 md5Hash = MD5.Create())
            {
                if (!VerifyMd5Hash(md5Hash, source, signature))
                {
                    if (dbAccount.签名错误累计 > MAX_ERROR_COUNT)
                    {
                        filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1002, dbAccount.商户ID);
                    }
                    else
                    {
                        dbAccount.签名错误累计++;
                        sqlSugarClient.Updateable(dbAccount).UpdateColumns(it => new { it.签名错误累计 }).ExecuteCommand(); ;
                        filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1004, dbAccount.商户ID);
                    }
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod != "POST")
            {
                return;
            }

            BaseRequest account = filterContext.ActionParameters["request"] as BaseRequest;
            string path = filterContext.HttpContext.Request.Path;

            var getByWhere = sqlSugarClient.Queryable<table_商户账号>().Where(it => it.商户ID == account.UserName).ToList();

            // 如果用户名不存在
            if (getByWhere.Count == 0)
            {
                filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1017, account.UserName);
                return;
            }

            // 验证IP白名单
            bool x = ClassLibrary1.ClassAccount.验证商户白名单IP(account.UserName, ClassLibrary1.ClassAccount.来源IP());
            if (!x)
            {
                filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1003, account.UserName);
                return;
            }

            NameValueCollection query = HttpContext.Current.Request.QueryString;
            if (path.Substring(path.Length - 2) == "11")
            {
                // 验证账号密码是否正确
                VerifyPassword(filterContext, path, getByWhere[0], ((AccountRequest)account).UserPassword);
            }
            else
            {
                if (!query.AllKeys.Contains(signKey) || !query.AllKeys.Contains(tsKey))
                {
                    filterContext.Result = GetStandardError(BaseErrors.ERROR_NUMBER.LX1015, account.UserName);
                }
                else
                {
                    // 验证签名是否正确
                    VerifySignature(filterContext, path, getByWhere[0]);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}