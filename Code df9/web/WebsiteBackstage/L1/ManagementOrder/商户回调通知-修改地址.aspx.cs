using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SqlSugar;
using Sugar.Enties;

namespace web1.WebsiteBackstage.L1.ManagementOrder
{
    public partial class 商户回调通知_修改地址 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                Label_目标商户账号.Text = GetDataFromUrl("ID");
                using (SqlSugarClient db = new DBClient().GetClient())
                {
                    Label_原始地址.Text = db.Queryable<table_商户账号>().Where(it => it.商户ID == GetDataFromUrl("ID")).Select(it => it.API回调).First();
                }
            }
        }


        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetDataFromUrl("Redirect"));
        }


        private string GetDataFromUrl(string key)
        {
            if (!String.IsNullOrEmpty(Request.QueryString[key]))
            {
                return ClassLibrary1.ClassSecurityZF.FilteSQLStr(Request.QueryString[key]);
            }
            return null;
        }

        protected void Button_操作更新_Click(object sender, EventArgs e)
        {
            string Cookie_Password = ClassLibrary1.ClassAccount.cookie解密(HttpContext.Current.Request.Cookies["PPusernameBackstageL1"]["password"]);
            if (TextBox_管理员密码.Text != Cookie_Password)
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "管理员密码错误");
                return;
            }
            using (SqlSugarClient db = new DBClient().GetClient())
            {
                db.Updateable<table_商户账号>().Where(it => it.商户ID == GetDataFromUrl("ID"))
                .SetColumns(it => it.API回调 == TextBox_地址.Text)
                .ExecuteCommand();
            }
            Response.Redirect(GetDataFromUrl("Redirect"));
        }
    }
}