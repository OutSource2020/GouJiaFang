using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementBackstage
{
    public partial class 后台账号列表设置 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }

            this.GetCustomer();

            string 从URL传来值 = 从URL获取值();
            if (判断是否为分级等级L2(从URL传来值) == true)
            {

            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "不属于L2账号");
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

                string 哪个表 = " table_后台账号 ";
                string 准备查哪些 = "后台ID,后台账号名称,状态,时间最后登入,时间注册  ";

                using (MySqlCommand cmd = new MySqlCommand("SELECT " + 准备查哪些 + " FROM "+ 哪个表 + " WHERE 后台ID=@后台ID", con))
                {
                    cmd.Parameters.AddWithValue("@后台ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_后台ID.Text = dr["后台ID"].ToString();
                            this.Label_后台名称.Text = dr["后台账号名称"].ToString();
                            this.Label_状态.Text = dr["状态"].ToString();
                            this.Label_时间最后登入.Text = dr["时间最后登入"].ToString();
                            this.Label_时间注册.Text = dr["时间注册"].ToString();
                        }
                    }
                }
            }
        }


        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("后台账号列表.aspx");
        }

        protected void Button_设置密码_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("后台账号列表设置密码.aspx?Bianhao=" + 编号 + "");
        }

        protected void Button_设置验证器_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("后台账号列表设置QR.aspx?Bianhao=" + 编号 + "");
        }

        protected void Button_设置账号信息_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("后台账号列表设置账号信息.aspx?Bianhao=" + 编号 + "");
        }

        private bool 判断是否为分级等级L2(string 传入账号)
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("select 后台ID,后台账号分级 from table_后台账号 where 后台ID=@后台ID and 后台账号分级='L2' ", con))
                {
                    cmd.Parameters.AddWithValue("@后台ID", 传入账号);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    object o = cmd.ExecuteScalar();
                    if (o != null)
                    {
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        return false;
                    }
                    //con.Close();
                }
            }
        }


    }
}