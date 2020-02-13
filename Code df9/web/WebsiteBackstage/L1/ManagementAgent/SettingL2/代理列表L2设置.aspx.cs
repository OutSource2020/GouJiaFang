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
    public partial class 代理列表L2设置 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }

            this.GetCustomer();
            this.获得代理安全列信息();
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
                string 查哪些 = "代理ID,代理名称,状态,时间最后登入,时间注册,所属代理L1";
                string 哪个表 = " table_代理账号等级2 ";
                using (MySqlCommand cmd = new MySqlCommand("SELECT " + 查哪些 + " FROM "+ 哪个表 + " WHERE 代理ID=@代理ID", con))
                {
                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_代理ID.Text = dr["代理ID"].ToString();
                            this.Label_代理名称.Text = dr["代理名称"].ToString();
                            this.Label_状态.Text = dr["状态"].ToString();
                            this.Label_时间最后登入.Text = dr["时间最后登入"].ToString();
                            this.Label_时间注册.Text = dr["时间注册"].ToString();
                            this.Label_所属代理L1.Text = dr["所属代理L1"].ToString();

                        }
                    }
                }
            }
        }

        private void 获得代理安全列信息()//获得数据-安全
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 绑定邮箱,绑定手机,登入错误累计 FROM table_代理账号等级2 WHERE 代理ID=@代理ID", con))
                {
                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_绑定邮箱.Text = dr["绑定邮箱"].ToString();
                            this.Label_绑定手机.Text = dr["绑定手机"].ToString();
                            this.Label_登入错误累计.Text = dr["登入错误累计"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_设置代理信息_Click(object sender, EventArgs e)
        {
            string 编号= 从URL获取值();
            Response.Redirect("代理列表L2设置信息.aspx?Bianhao="+ 编号 + "");
        }

        protected void Button_设置代理账户安全_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("代理列表L2设置安全.aspx?Bianhao=" + 编号 + "");
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("代理列表L2.aspx");
        }
    }
}