using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteMerchant.商户账号
{
    public partial class 商户账号 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //獲取cookie
                HttpCookie cookie = Request.Cookies["PPusernameBB"];
                ClassLibrary1.ClassAccount.验证账号商户端();
            }
            GetCustomer();

        }
        
        protected void Button_登入密码_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("商户账号密码登入.aspx");
        }

        protected void Button_登入密码API_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("商户账号密码登入API.aspx");
        }

        protected void Button_支付密码_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("商户账号密码支付.aspx");
        }

        protected void Button_设置绑定邮箱_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("商户账号绑定邮箱.aspx");
        }
       
        private void GetCustomer()//获得数据
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户名称,商户ID,状态,时间注册 FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户名称.Text = dr["商户名称"].ToString();
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.Label_商户状态.Text = dr["状态"].ToString();
                            this.Label_时间注册.Text = dr["时间注册"].ToString();
                        }
                    }
                }
            }
        }


    }
}