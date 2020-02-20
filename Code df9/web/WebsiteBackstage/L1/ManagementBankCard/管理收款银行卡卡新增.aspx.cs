using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace web1.WebsiteBackstage.L1.ManagementBankCard
{
    public partial class 管理收款银行卡卡新增 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }
        }

        protected void TextBox_交易方卡号_TextChanged(object sender, EventArgs e)
        {
            TextBox_收款银行名称.Text = ClassLibrary1.ClassBankInfo3a2.BankUtil.getNameOfBank(TextBox_收款银行卡卡号.Text);
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("管理收款银行卡.aspx");
        }

        protected void Button_操作新增收款银行卡_Click(object sender, EventArgs e)
        {
            if (TextBox_收款银行卡名称.Text.Length > 0 && TextBox_收款银行卡卡号.Text.Length > 0 && TextBox_收款银行名称.Text.Length > 0 )
            {
                操作新增();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }

        }

        private bool 检查同商户号中是否有相同IP(string 传入卡号, string 传入名称)
        {
            string query = "select * from table_后台收款银行卡管理 where 收款银行卡卡号=@收款银行卡卡号 or 收款银行卡名称=@收款银行卡名称 ";

            using (var connection = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@收款银行卡卡号", 传入卡号);
                command.Parameters.AddWithValue("@收款银行卡名称", 传入名称);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 1)
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "这个 商户的白名单列中 已存在IP");
                    return true;
                }
                else
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "这个 商户的白名单列中 没有这个IP");
                    return false;
                }
            }

        }

        private void 操作新增()
        {
            if (检查同商户号中是否有相同IP(TextBox_收款银行卡卡号.Text, TextBox_收款银行卡名称.Text) ==false)
            {
                Button_操作新增收款银行卡.Enabled = false;

                string 生成编号 = "BRPBCM" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                string 收款银行卡名称 = TextBox_收款银行卡名称.Text;
                string 收款银行卡卡号 = TextBox_收款银行卡卡号.Text;
                string 收款银行名称 = TextBox_收款银行名称.Text;
                double 最初余额 = 0;
                string 收款银行卡主姓名 = TextBox_收款银行卡主姓名.Text;
                string 收款银行卡主电话 = TextBox_收款银行卡主电话.Text;

                string 显示标记 = DropDownList_显示标记.SelectedItem.Value;
                string 状态 = DropDownList_收款银行卡状态.SelectedItem.Value;
        string 手续卡 = DropDownList_手续卡.SelectedItem.Value;
        string 金额卡 = DropDownList_金额卡.SelectedItem.Value;
        string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string 写表 = "table_后台收款银行卡管理";
                string 写头 = "编号,收款银行卡名称,收款银行卡卡号,收款银行名称,收款银行卡余额,收款银行卡主姓名,收款银行卡主电话,显示标记,状态,时间创建,手续卡,金额卡";
                string 写值 = "@编号,@收款银行卡名称,@收款银行卡卡号,@收款银行名称,@收款银行卡余额,@收款银行卡主姓名,@收款银行卡主电话,@显示标记,@状态,@时间创建,@手续卡,@金额卡";

                string _query = "INSERT INTO " + 写表 + "(" + 写头 + ") values (" + 写值 + ")";
                using (MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                {
                    using (MySqlCommand comm = new MySqlCommand())
                    {
                        comm.Connection = conn;
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = _query;
                        comm.Parameters.AddWithValue("@编号", 生成编号);
                        comm.Parameters.AddWithValue("@收款银行卡名称", 收款银行卡名称);
                        comm.Parameters.AddWithValue("@收款银行卡卡号", 收款银行卡卡号);
                        comm.Parameters.AddWithValue("@收款银行名称", 收款银行名称);
                        comm.Parameters.AddWithValue("@收款银行卡余额", 最初余额);
                        comm.Parameters.AddWithValue("@收款银行卡主姓名", 收款银行卡主姓名);
                        comm.Parameters.AddWithValue("@收款银行卡主电话", 收款银行卡主电话);
                        comm.Parameters.AddWithValue("@显示标记", 显示标记);
                        comm.Parameters.AddWithValue("@状态", 状态);
                        comm.Parameters.AddWithValue("@时间创建", 时间创建);
                        comm.Parameters.AddWithValue("@手续卡", 手续卡);
                        comm.Parameters.AddWithValue("@金额卡", 金额卡);
            try
                        {
                            conn.Open();
                            comm.ExecuteNonQuery();

                            Response.Redirect("./管理收款银行卡.aspx");
                        }
                        catch (MySqlException ex)
                        {
                            // other codes here
                            // do something with the exception
                            // don't swallow it.

                            ClassLibrary1.ClassMessage.HinXi(Page, "错误 " + ex + " ");
                        }
                    }
                }
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "银行卡卡号或者银行卡名称已經存在 ");
            }


        }

        protected void Button_返回_Click1(object sender, EventArgs e)
        {
            Response.Redirect("管理收款银行卡.aspx");
        }
    }
}