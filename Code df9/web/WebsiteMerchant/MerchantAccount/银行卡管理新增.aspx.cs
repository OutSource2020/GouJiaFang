using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace web1.WebsiteMerchant.商户账号
{
    public partial class 银行卡管理新增 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();
            }
        }

        protected void Button_银行卡提交_Click(object sender, EventArgs e)
        {
            
            if (TextBox_商户银行卡卡号.Text.Length > 0 && TextBox_商户银行名称.Text.Length > 0 && TextBox_商户银行卡主姓名.Text.Length > 0 )
            {
                操作新增();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }

        private bool 检查是否有相同商户银行卡()
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("select 商户ID,商户银行卡卡号 from table_商户银行卡 where 商户ID=@商户ID and 商户银行卡卡号=@商户银行卡卡号 and 商户银行卡卡标记='显示' ", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", ClassLibrary1.ClassAccount.检查商户端cookie2() );
                    cmd.Parameters.AddWithValue("@商户银行卡卡号", TextBox_商户银行卡卡号.Text);
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

        private void 操作新增()
        {
            if (检查是否有相同商户银行卡() == false)
            {
                Button_银行卡提交.Enabled = false;

                string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 300));
                string 生成编号 = "MBCN" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                string 状态第一次 = "待审核";

                string 哪个表 = "table_商户银行卡";
                string 哪些内容 = "编号,商户ID,商户银行卡卡号,商户银行名称,商户银行卡主姓名,状态,时间创建,商户银行卡卡标记";
                string 哪些数据 = "@编号,@商户ID,@商户银行卡卡号,@商户银行名称,@商户银行卡主姓名,@状态,@时间创建,@商户银行卡卡标记";


                using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                {
                    string str = "insert into " + 哪个表 + "(" + 哪些内容 + ") values(" + 哪些数据 + ")";

                    scon.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Parameters.AddWithValue("@编号", 生成编号);
                    command.Parameters.AddWithValue("@商户ID", ClassLibrary1.ClassAccount.检查商户端cookie2());
                    command.Parameters.AddWithValue("@商户银行卡卡号", TextBox_商户银行卡卡号.Text);
                    command.Parameters.AddWithValue("@商户银行名称", TextBox_商户银行名称.Text);
                    command.Parameters.AddWithValue("@商户银行卡主姓名", TextBox_商户银行卡主姓名.Text);
                    command.Parameters.AddWithValue("@状态", 状态第一次);
                    command.Parameters.AddWithValue("@时间创建", RegisterTime);
                    command.Parameters.AddWithValue("@商户银行卡卡标记", "显示");

                    command.Connection = scon;
                    command.CommandText = str;
                    int obj = command.ExecuteNonQuery();
                }

                Response.Redirect("./银行卡管理.aspx");
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "银行卡卡号已經存在");
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("./银行卡管理.aspx");
        }

        protected void TextBox_交易方卡号_TextChanged(object sender, EventArgs e)
        {
            TextBox_商户银行名称.Text = ClassLibrary1.ClassBankInfo3a2.BankUtil.getNameOfBank(TextBox_商户银行卡卡号.Text);
        }
    }
}