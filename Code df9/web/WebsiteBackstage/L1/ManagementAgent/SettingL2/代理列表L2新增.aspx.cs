using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementAgent.SettingL2
{
    public partial class 代理列表L2新增 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("代理列表L2.aspx");
        }

        protected void Button_创建提交_Click(object sender, EventArgs e)
        {
            if (TextBox_代理ID.Text.Length > 1)
            {
                新增();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "未填写完整");
            }

        }

        private bool 判断是否存在相同ID()
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from table_代理账号等级2 where 代理ID=@代理ID", con))
                {
                    cmd.Parameters.AddWithValue("@代理ID", TextBox_代理ID.Text);

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

        private void 新增()
        {
            if (判断是否存在相同ID() == false)
            {
                Button_创建提交.Enabled = false;

                string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                //连接数据库信息============================================================
                string sql = ClassLibrary1.ClassDataControl.conStr1;
                //
                using (MySqlConnection scon = new MySqlConnection(sql))
                {
                    string 代理ID = ClassLibrary1.ClassSecurityZF.FilteSQLStr(TextBox_代理ID.Text);
                    string 代理名称 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(TextBox_代理名称.Text);

                    string str = "insert into table_代理账号等级2(代理ID,代理名称,时间注册,状态,登入错误累计,keyga) values(@代理ID,@代理名称,@时间注册,@状态,@登入错误累计,@keyga)";

                    scon.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Parameters.AddWithValue("@代理ID", 代理ID);
                    command.Parameters.AddWithValue("@代理名称", 代理名称);
                    command.Parameters.AddWithValue("@时间注册", RegisterTime);
                    command.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    command.Parameters.AddWithValue("@登入错误累计", "0");
                    command.Parameters.AddWithValue("@keyga", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));

                    command.Connection = scon;
                    command.CommandText = str;
                    int obj = command.ExecuteNonQuery();

                    scon.Close();
                }
                Response.Redirect("./代理列表L2.aspx");
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "ID已存在");
            }

        }

        protected void Button_随机生成代理ID_Click(object sender, EventArgs e)
        {
            Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 300));
            string 生成编号 = "186" + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

            TextBox_代理ID.Text = 生成编号;
        }

        protected void CheckBox_手动设置商户ID_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_手动设置商户ID.Checked == true)
            {
                TextBox_代理ID.Enabled = true;
            }
            else
            {
                TextBox_代理ID.Enabled = false;
            }
        }


    }
}