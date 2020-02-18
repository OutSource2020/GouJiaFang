using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{
    public partial class 商户通知设置 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }
            查询状态();
        }

        private void 查询状态()
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 短信通知,提款验证码验证 FROM table_后台商户账号 where 总开关=11", con))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label1.Text = dr["短信通知"].ToString();
                            this.Label2.Text = dr["提款验证码验证"].ToString();
                            //this.imgCustomer.ImageUrl = "~/images/" + dr["CustomerId"].ToString() + ".jpg";
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台商户账号 SET 短信通知='yes' WHERE 总开关=11", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri, false);
                    Response.Redirect("./商户通知设置.aspx");

                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台商户账号 SET 短信通知='no' WHERE 总开关=11", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri, false);
                    Response.Redirect("./商户通知设置.aspx");

                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台商户账号 SET 提款验证码验证='yes' WHERE 总开关=11", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri, false);
                    Response.Redirect("./商户通知设置.aspx");

                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台商户账号 SET 提款验证码验证='no' WHERE 总开关=11", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //Response.Redirect(Request.Url.AbsoluteUri, false);
                    Response.Redirect("./商户通知设置.aspx");

                }
            }
        }
    }
}