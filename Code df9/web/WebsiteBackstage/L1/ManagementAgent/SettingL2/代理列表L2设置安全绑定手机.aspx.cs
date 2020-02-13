using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementAgent.SettingL2
{
    public partial class 代理列表L2设置安全绑定手机 : System.Web.UI.Page
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

        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 代理ID,绑定手机 FROM table_代理账号等级2 WHERE 代理ID=@代理ID", con))
                {
                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_代理ID.Text = dr["代理ID"].ToString();
                            this.TextBox_绑定手机.Text = dr["绑定手机"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("代理列表L2设置安全.aspx?Bianhao=" + 编号 + "");
        }


        protected void CustomerUpdate(object sender, EventArgs e)
        {
            this.更新内容();
        }


        private void 更新内容()//更新出去
        {
            if (TextBox_绑定手机.Text.Length > 0)
            {
                操作更新();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }

            
        }

        private void 操作更新()
        {
            Button_发出更新.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_代理账号等级2 SET 绑定手机=@绑定手机 WHERE 代理ID=@代理ID ", con))
                {
                    cmd.Parameters.AddWithValue("@绑定手机", TextBox_绑定手机.Text);
                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string 编号 = 从URL获取值();
                    Response.Redirect("代理列表L2设置安全.aspx?Bianhao=" + 编号 + "");


                }
            }
        }
    }
}