using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementOrder
{
    public partial class 商户充值记录状态更新 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }
            检查状态是否();

        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户充值记录.aspx");
        }

        private string 从URL获取值()//获得URL传来的地址
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Bianhao"]))
            {
                // Query string value is there so now use it
                //查詢字符串值是那麼現在使用它
                //int thePID = Convert.ToInt32(Request.QueryString["22"]);
                if (System.Text.RegularExpressions.Regex.IsMatch(Request.QueryString["Bianhao"], "^[0-9a-zA-Z]{2,30}$"))
                {
                    //Label1.Text = "是符合要求字符";

                    //获得传值
                    string URL传来值 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(Request.QueryString["Bianhao"]);
                    return URL传来值;
                }
                else
                {
                    //Label1.Text = "是no符合要求字符";

                    //URL传来值 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(Request.QueryString["Bianhao"]);
                    Response.Redirect("./404.aspx");

                    return null;
                }
            }

            return null;
        }


        private void 检查状态是否()
        {
            using (MySqlConnection con1 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd1 = new MySqlCommand("SELECT 订单号,充值类型 FROM table_商户明细充值 WHERE 订单号=@订单号", con1))
                {
                    string 从URL传来值 = 从URL获取值();

                    cmd1.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1))
                    {
                        DataTable images = new DataTable();
                        da1.Fill(images);
                        foreach (DataRow dr1 in images.Rows)
                        {

                            string 查询状态是否符合 = dr1["充值类型"].ToString();
                            if (查询状态是否符合 == "充值提款余额")
                            {
                                //string 从URL传来值 = 从URL获取值();
                                Response.Redirect("商户充值记录状态更新号余额.aspx?Bianhao=" + 从URL获取值() + "");
                            }
                            if (查询状态是否符合 == "充值提款手续费")
                            {
                                //string 从URL传来值 = 从URL获取值();
                                Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");
                            }
                        }
                    }
                }
            }
        }

        
    }
}