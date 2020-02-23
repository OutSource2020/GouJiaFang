using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using SqlSugar;
using Sugar.Enties;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{
    public partial class 商户安全白名单更新 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
            }

        }

        private string 从URL获取值()//获得URL传来的地址
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Bianhao"]))
            {
                // Query string value is there so now use it
                //查詢字符串值是那麼現在使用它
                //int thePID = Convert.ToInt32(Request.QueryString["22"]);
                if (System.Text.RegularExpressions.Regex.IsMatch(Request.QueryString["Bianhao"], "^[0-9a-zA-Z]{0,30}$"))
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


        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 编号,商户ID,商户白名单IP,商户白名单备注,状态,时间创建 FROM table_商户白名单管理 WHERE 编号=@编号", con))
                {
                    cmd.Parameters.AddWithValue("@编号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.TextBox_商户白名单IP.Text = dr["商户白名单IP"].ToString();
                            this.TextBox_商户白名单备注.Text = dr["商户白名单备注"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户安全白名单.aspx");
        }

        protected void Button_更新提交_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox_商户白名单IP.Text) || String.IsNullOrEmpty(TextBox_商户白名单备注.Text))
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "必须填写商户白名单IP或者备注");
            }
            else
            {
                Button_更新提交.Enabled = false;

                this.操作更新();
            }


        }

        private void 操作更新()//更新出去
        {
            string 从URL传来值 = 从URL获取值();
            //开始更新
            using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd12 = new MySqlCommand("UPDATE table_商户白名单管理 SET 商户白名单IP=@商户白名单IP , 商户白名单备注=@商户白名单备注 , 状态=@状态 WHERE 编号=@编号 ", con12))
                {
                    cmd12.Parameters.AddWithValue("@商户白名单IP", TextBox_商户白名单IP.Text);
                    cmd12.Parameters.AddWithValue("@商户白名单备注", TextBox_商户白名单备注.Text);
                    cmd12.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    cmd12.Parameters.AddWithValue("@编号", 从URL获取值());
                    con12.Open();
                    cmd12.ExecuteNonQuery();
                    con12.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri, false);
                    Response.Redirect("商户安全白名单.aspx");
                }
            }
        }

        protected void Button_删除_Click(object sender, EventArgs e)
        {
            string 从URL传来值 = 从URL获取值();
            using (SqlSugarClient sqlSugarClient = new DBClient().GetClient())
            {
                sqlSugarClient.Deleteable<table_商户白名单管理>().Where(it => it.编号 == 从URL传来值).ExecuteCommand();
            }
            Response.Redirect("商户安全白名单.aspx");
        }
    }
}