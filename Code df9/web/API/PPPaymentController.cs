using SqlSugar;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using web1.API.Enties;

namespace web1.API
{
    public class PPPaymentController : Controller
    {
        private SqlSugarClient sqlSugarClient = new DBClient().GetClient();
        private BaseResponse baseSuccess = new BaseErrors()[(int)BaseErrors.ERROR_NUMBER.LX1000];

        private JsonResult GetStandardError(BaseErrors.ERROR_NUMBER errorNumber, string username, string password)
        {
            JsonResult jsonResult = new JsonResult();
            BaseResponse baseResponse = new BaseErrors()[(int)errorNumber];
            baseResponse.Username = username;
            baseResponse.Userpassword = password;
            jsonResult.Data = baseResponse;
            return jsonResult;
        }

        public static TChild AutoCopy<TParent, TChild>(TParent parent) where TChild : TParent, new()
        {
            TChild child = new TChild();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;
        }

        [HttpPost]
        public ActionResult AccountInquiry(int? timeunix, string signature, AccountRequest request)
        {
            var getByWhere = sqlSugarClient.Queryable<table_商户账号>().Where(it => it.商户ID == request.UserName).ToList();
            AccountResponse accountResponse = AutoCopy<BaseResponse, AccountResponse>(baseSuccess);

            accountResponse.InquireAccountDeposit = Convert.ToString(getByWhere[0].提款余额);
            accountResponse.InquireAccountHandlingFee = Convert.ToString(getByWhere[0].手续费余额);
            accountResponse.InquireTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            accountResponse.Username = request.UserName;
            accountResponse.Userpassword = request.UserPassword;
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = accountResponse;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult OrderInquire(int? timeunix, string signature, OrderInquireRequest request)
        {
            var getByWhere = sqlSugarClient.Queryable<table_商户明细提款>().Where(it => it.商户ID == request.UserName && it.商户API订单号 == request.OrderNumberMerchant).ToList();
            if (getByWhere.Count > 0)
            {
                OrderInquireResponse orderInquireResponse = AutoCopy<BaseResponse, OrderInquireResponse>(baseSuccess);
                orderInquireResponse.OrderNumberMerchant = request.OrderNumberMerchant;
                orderInquireResponse.OrderNumberSite = getByWhere[0].订单号;
                orderInquireResponse.OrderTimeCreation = getByWhere[0].时间创建.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if (getByWhere[0].时间完成.HasValue)
                    orderInquireResponse.OrderTimeEnd = getByWhere[0].时间完成.Value.ToString("yyyy-MM-dd HH:mm:ss");
                orderInquireResponse.OrderType = getByWhere[0].类型;
                orderInquireResponse.OrderStatus = getByWhere[0].状态;
                orderInquireResponse.Username = request.UserName;
                orderInquireResponse.Userpassword = request.UserPassword;
                JsonResult jsonResult = new JsonResult();
                jsonResult.Data = orderInquireResponse;
                return jsonResult;
            }
            else
            {
                return GetStandardError(BaseErrors.ERROR_NUMBER.LX1007, request.UserName, request.UserPassword);
            }
        }

        [HttpPost]
        public ActionResult OrderCreate(int? timeunix, string signature, OrderCreateRequest request)
        {
            var getByWhere = sqlSugarClient.Queryable<table_商户账号>().Where(it => it.商户ID == request.UserName).ToList();
            table_商户账号 account = getByWhere[0];

            if (Convert.ToDouble(account.手续费余额) - Convert.ToDouble(account.单笔手续费) < 0)
            {
                return GetStandardError(BaseErrors.ERROR_NUMBER.LX1010, request.UserName, request.UserPassword);
            }
            if (Convert.ToDouble(request.AimsMoney) - Convert.ToDouble(account.提款最低单笔金额) < 0)
            {
                return GetStandardError(BaseErrors.ERROR_NUMBER.LX1011, request.UserName, request.UserPassword);
            }
            if (Convert.ToDouble(request.AimsMoney) - Convert.ToDouble(account.提款最高单笔金额) > 0)
            {
                return GetStandardError(BaseErrors.ERROR_NUMBER.LX1012, request.UserName, request.UserPassword);
            }
            if (account.提款余额.Value - Convert.ToDouble(request.AimsMoney) < 0)
            {
                return GetStandardError(BaseErrors.ERROR_NUMBER.LX1013, request.UserName, request.UserPassword);
            }

            string 状态 = "待处理";
            string 类型 = "提款";
            DateTime 时间创建 = DateTime.Now;
            // string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            double preFee = Convert.ToDouble(account.手续费余额);
            double preBalance = account.提款余额.Value;

            account.提款余额 -= Convert.ToDouble(request.AimsMoney);
            account.手续费余额 -= Convert.ToDouble(account.单笔手续费);
            sqlSugarClient.Updateable(account).UpdateColumns(it => new { it.提款余额, it.手续费余额 }).ExecuteCommand();

            table_商户明细手续费 fee = new table_商户明细手续费();
            fee.订单号 = "MHFON" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
            fee.商户ID = Convert.ToInt32(account.商户ID);
            fee.手续费支出 = account.单笔手续费;
            fee.交易金额 = Convert.ToDouble(request.AimsMoney);
            fee.交易前手续费余额 = preFee;
            fee.交易后手续费余额 = account.手续费余额;
            fee.类型 = 类型;
            fee.状态 = 状态;
            fee.时间创建 = 时间创建;
            sqlSugarClient.Insertable<table_商户明细手续费>(fee).ExecuteCommand();

            table_商户明细余额 balance = new table_商户明细余额();
            balance.订单号 = "MBON" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
            balance.商户ID = Convert.ToInt32(account.商户ID);
            balance.类型 = 类型;
            balance.手续费 = Convert.ToString(account.单笔手续费);
            balance.交易金额 = request.AimsMoney;
            balance.交易前账户余额 = Convert.ToString(preBalance);
            balance.交易后账户余额 = Convert.ToString(account.提款余额);
            balance.状态 = 状态;
            balance.时间创建 = 时间创建;
            sqlSugarClient.Insertable<table_商户明细余额>(balance).ExecuteCommand();

            table_商户明细提款 detail = new table_商户明细提款();
            detail.订单号 = "MST" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
            detail.商户ID = account.商户ID;
            detail.交易方卡号 = request.AimsCardNumber;
            detail.交易方姓名 = request.AimsCardName;
            detail.交易方银行 = request.AimsCardBank;
            detail.商户API订单号 = request.OrderNumberMerchant;
            detail.交易金额 = Convert.ToDouble(request.AimsMoney);
            detail.手续费 = account.单笔手续费;
            detail.创建方式 = "接口";
            detail.备注商户写 = "";
            detail.状态 = 状态;
            detail.类型 = 类型;
            detail.时间创建 = 时间创建;
            detail.订单源IP = ClassLibrary1.ClassAccount.来源IP();
            sqlSugarClient.Insertable<table_商户明细提款>(detail).ExecuteCommand();

            OrderCreateResponse orderCreateResponse = AutoCopy<BaseResponse, OrderCreateResponse>(baseSuccess);
            orderCreateResponse.Username = request.UserName;
            orderCreateResponse.Userpassword = request.UserPassword;
            orderCreateResponse.OrderNumberMerchant = request.OrderNumberMerchant;
            orderCreateResponse.OrderNumberSite = detail.订单号;
            orderCreateResponse.AimsCardBank = request.AimsCardBank;
            orderCreateResponse.AimsCardName = request.AimsCardName;
            orderCreateResponse.AimsCardNumber = request.AimsCardNumber;
            orderCreateResponse.AimsMoney = request.AimsMoney;

            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = orderCreateResponse;
            return jsonResult;
        }
    }
}