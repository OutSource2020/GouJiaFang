using Newtonsoft.Json;
using SqlSugar;
using Sugar.Enties;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using web1.API.Enties;

namespace web1.WebsiteBackstage.L1.ManagementOrder
{
    public partial class 商户日志_API回调通知 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindDateToGird(int.Parse(DropDownList_选择每页行数.SelectedItem.Value));
        }

        protected void Button_查找_Click(object sender, EventArgs e)
        {
            BindDateToGird(int.Parse(DropDownList_选择每页行数.SelectedItem.Value));
        }

        protected void RadioButton_时间筛选_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender) == RadioButton_时间设置)
            {
                TextBox_开始时间.Enabled = true;
                TextBox_结束时间.Enabled = true;
            }
            else
            {
                TextBox_开始时间.Enabled = false;
                TextBox_结束时间.Enabled = false;
            }
            BindDateToGird(int.Parse(DropDownList_选择每页行数.SelectedItem.Value));
        }

        protected void RadioButton_状态筛选_CheckedChanged(object sender, EventArgs e)
        {
            BindDateToGird(int.Parse(DropDownList_选择每页行数.SelectedItem.Value));
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "修改回调地址")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                Response.Redirect(string.Format("商户回调通知-修改地址.aspx?ID={0}&Redirect={1}", row.Cells[1].Text, "商户回调通知-测试调试.aspx"));
            }
            else if (e.CommandName == "测试发送")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                TestSendCallBack(row.Cells[1].Text, row.Cells[0].Text);
            }
        }

        protected void DropDownList_选择每页行数_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDateToGird(int.Parse(DropDownList_选择每页行数.SelectedItem.Value));
        }

        private string 获得时间()
        {
            string 时间1 = DateTime.Now.ToString("yyyy-MM-dd");
            string 时间2 = DateTime.Now.ToString("yyyy-MM-dd");

            if (RadioButton_时间昨天.Checked)
            {
                时间1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                时间2 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if (RadioButton_时间7天.Checked)
            {
                时间1 = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                时间2 = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (RadioButton_时间本周.Checked)
            {
                时间1 = DateTime.Now.AddDays(Convert.ToDouble((1 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToString("yyyy-MM-dd");
                时间2 = DateTime.Now.AddDays(Convert.ToDouble((7 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToString("yyyy-MM-dd");
            }
            else if (RadioButton_时间本月.Checked)
            {
                时间1 = DateTime.Now.ToString("yyyy-") + DateTime.Now.ToString("MM-") + "01"; //本月第一天
                时间2 = DateTime.Now.ToString("yyyy-") + DateTime.Now.AddMonths(+1).ToString("MM-") + "01";//下个月第一天
            }
            else if (RadioButton_时间设置.Checked)
            {
                时间1 = TextBox_开始时间.Text;
                时间2 = TextBox_结束时间.Text;
            }
            TextBox_开始时间.Text = 时间1;
            TextBox_结束时间.Text = 时间2;
            return " AND (`时间创建` BETWEEN '" + 时间1 + "' AND '" + 时间2 + "" + ClassLibrary1.ClassTimeZD.时分秒更多 + "')";
        }

        public string 获得订单状态()
        {
            string 条件2 = "";

            if (RadioButton_状态待处理.Checked)
            {
                条件2 = " 状态='待处理' ";
            }
            else if (RadioButton_状态成功.Checked)
            {
                条件2 = " 状态='成功' ";
            }
            else if (RadioButton_状态失败.Checked)
            {
                条件2 = " 状态='失败' ";
            }

            if (条件2 == "")
                return 条件2;
            else
                return " AND " + 条件2;
        }

        public string 获得筛选关键字()
        {
            string 条件3 = "";

            string redkey = DropDownList1.SelectedItem.Value;
            if (redkey != "未选择")
            {
                条件3 = " " + DropDownList1.SelectedItem.Value + "='" + TextBox_筛选关键字.Text + "' ";
            }

            if (条件3 == "")
                return 条件3;
            else
                return " AND " + 条件3;
        }

        private string WhereString()
        {
            return "`创建方式` = \"接口\"" + 获得时间() + 获得订单状态() + 获得筛选关键字();
        }

        private void BindDateToGird(int count)
        {
            using (SqlSugarClient db = new DBClient().GetClient())
            {
                // var dt = db.Queryable<table_商户明细提款>().Where(it => it.创建方式 == "接口")
                var dt = db.Queryable<table_商户明细提款>().Where(WhereString())
                 .Select(it =>
                 new
                 {
                     订单号 = it.订单号,
                     商户ID = it.商户ID,
                     商户API订单号 = it.商户API订单号,
                     状态 = it.状态,
                     回调状态 = it.最后一次回调返回的状态,
                     回调地址 = SqlFunc.Subqueryable<table_商户账号>().Where(it2 => it2.商户ID == it.商户ID).Select(it2 => it2.API回调)
                 })
                 .Take(count)
                 .ToDataTable();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private long GetTimeStamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            return (long)System.Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
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

        private static string PostResponse(string url, string postData, out int statusCode)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        private void ShowMessage(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("alert(' ");
            sb.Append(msg);
            sb.Append(" ');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(),
                            "script", sb.ToString());
        }

        private void TestSendCallBack(string id, string orderId)
        {
            using (SqlSugarClient db = new DBClient().GetClient())
            {
                var account = db.Queryable<table_商户账号>().Where(it => it.商户ID == id).First();
                var detail = db.Queryable<table_商户明细提款>().Where(it => it.订单号 == orderId).First();
                CallbackRequest request = new CallbackRequest()
                {
                    Username = account.商户ID,
                    Userpassword = account.商户密码,
                    OrderNumberSite = detail.订单号,
                    OrderNumberMerchant = detail.商户API订单号,
                    OrderType = detail.类型,
                    OrderStatus = detail.状态,
                    OrderTimeCreation = detail.时间创建.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    OrderTimeEnd = detail.时间完成.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    OrderMoney = detail.交易金额.Value.ToString()
                };
                try
                {
                    int statusCode = 0;
                    using (MD5 md5Hash = MD5.Create())
                    {
                        // 商户ID + 商户API密码 + 当前unix时间 + 商户自定义订单号 + 生成订单号 + 类型 + 状态 + 公共密匙(公匙)
                        long ts = GetTimeStamp();
                        string unsign = request.Username + request.Userpassword + ts +
                            request.OrderNumberMerchant + request.OrderNumberSite + request.OrderType + request.OrderStatus + account.公共密匙;
                        string sign = GetMd5Hash(md5Hash, unsign);
                        string url = $"{account.API回调}?timeunix={ts}&signature={sign}";
                        string response = PostResponse(url, JsonConvert.SerializeObject(request), out statusCode);
                        ShowMessage(response);
                    }
                }
                catch (Exception e)
                {
                    ShowMessage(e.Message);
                }
            }
        }
    }
}