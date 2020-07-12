using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web1.API.Enties
{
    public class BaseErrors
    {
        public enum ERROR_NUMBER
        {
            LX1000 = 0,
            LX1001,
            LX1002,
            LX1003,
            LX1004,
            LX1005,
            LX1006,
            LX1007,
            LX1008,
            LX1009,
            LX1010,
            LX1011,
            LX1012,
            LX1013,
            LX1014,
            LX1015,
            LX1016,
            LX1017,
            LX1018,
            LX1019,
            LX1020
        }

        private static Dictionary<string, string> standardError = new Dictionary<string, string>
        {
            { "LX1000", "操作成功" },
            { "LX1001", "没有查询到" },
            { "LX1002", "签名错误超过限制" },
            { "LX1003", "需使用白名单IP下单" },
            { "LX1004", "API签名错误" },
            { "LX1005", "密码錯誤" },
            { "LX1006", "API时间超时" },
            { "LX1007", "查询不到订单号" },
            { "LX1008", "密码错误超过限制" },
            { "LX1009", "传来的订单单号已經存在" },
            { "LX1010", "手续费余额不足" },
            { "LX1011", "目标提款金额 小于账户提款金额限制" },
            { "LX1012", "目标提款金额 大于账户提款金额限制" },
            { "LX1013", "目标提款金额 账户提款余额不足支付" },
            { "LX1014", "金额 不是数字或者小数" },
            { "LX1015", "参数缺失" },
            { "LX1016", "未知错误 : {0}" },
            { "LX1017", "账号不存在" },
            { "LX1018", "不支持的操作" },
            { "LX1019", "订单处理失败" },
            { "LX1020", "订单重复" }
        };

        private static BaseResponse[] baseResponses = new BaseResponse[standardError.Count];

        public BaseResponse this[int i]
        {
            get { return baseResponses[i]; }
        }

        public BaseErrors()
        {
            int i = 0;
            foreach (KeyValuePair<string, string> item in standardError)
            {
                BaseResponse baseResponse = new BaseResponse();
                baseResponse.StatusReply = item.Value;
                baseResponse.StatusReplyNumbering = item.Key;
                baseResponses[i] = baseResponse;
                i++;
            }
        }
    }
}