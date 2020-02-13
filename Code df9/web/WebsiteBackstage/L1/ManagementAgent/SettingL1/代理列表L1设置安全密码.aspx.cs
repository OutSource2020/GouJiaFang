using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace web1.WebsiteBackstage.L1.ManagementAgent.SettingL1
{
    public partial class 代理列表L1设置安全密码 : System.Web.UI.Page
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

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("代理列表L1设置安全.aspx?Bianhao=" + 编号 + "");
        }


        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 代理ID,代理密码 FROM table_代理账号等级1 WHERE 代理ID=@代理ID", con))
                {
                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_代理ID.Text = dr["代理ID"].ToString();
                            this.TextBox_代理密码.Text = dr["代理密码"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_操作更新_Click(object sender, EventArgs e)
        {
            
            if(TextBox_代理密码.Text.Length>1)
            {
                Button_操作更新.Enabled = false;
                更新开始();
            }
        }

        private void 更新开始()
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 代理ID,绑定邮箱 FROM table_代理账号等级1 WHERE 代理ID=@代理ID ", con))
                {
                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {


                            //更新 重置密码
                            using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd12 = new MySqlCommand("UPDATE table_代理账号等级1 SET 代理密码=@代理密码 WHERE 代理ID=@代理ID ", con12))
                                {
                                    cmd12.Parameters.AddWithValue("@代理密码", TextBox_代理密码.Text);
                                    cmd12.Parameters.AddWithValue("@代理ID", 从URL传来值);

                                    con12.Open();
                                    cmd12.ExecuteNonQuery();
                                    con12.Close();


                                    string 编号 = 从URL获取值();
                                    Response.Redirect("代理列表L1设置安全.aspx?Bianhao=" + 编号 + "");
                                }
                            }



                        }
                    }
                }
            }



        }
    }
}