using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using Google.Authenticator;

namespace web1.WebsiteBackstage.L1.ManagementBackstage
{
    public partial class 后台账号列表新增 : System.Web.UI.Page
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
            Response.Redirect("商户列表.aspx");
        }

        protected void Button_新增后台账号_Click(object sender, EventArgs e)
        {
            if (TextBox_账号信息_后台账号ID.Text.Length > 0 && 

                TextBox_账号名称.Text.Length > 0
                )
            {
                

                if (TextBox_账号信息_后台账号ID.Text.Length > 2)
                {
                    if(判断是否存在相同ID()==false)
                    {
                        //ClassLibrary1.ClassMessage.HinXi(Page, "ID不存在");
                        


                        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                        var result = tfa.ValidateTwoFactorPIN(获取密匙(), TextBox_验证密匙.Text);

                        if (result)
                        {
                            //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内有效PIN码 " + DateTime.UtcNow.ToString();
                            //this.lblValidationResult.ForeColor = System.Drawing.Color.Green;

                            操作新增();
                        }
                        else
                        {
                            ClassLibrary1.ClassMessage.HinXi(Page, "KEY错误");

                            //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内不有效的PIN码 " + DateTime.UtcNow.ToString();
                            //this.lblValidationResult.ForeColor = System.Drawing.Color.Red;
                        }


                    }
                    else
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "ID已存在");
                    }

                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "ID是否符合格式? (空或者使用非数字)");
                }
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }

        private bool 判断是否存在相同ID()
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("select 后台ID from table_后台账号 where 后台ID=@后台ID", con))
                {
                    cmd.Parameters.AddWithValue("@后台ID", TextBox_账号信息_后台账号ID.Text);

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

        private string 获取密匙()
        {
            string 密匙 = "";
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查管理L1端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 后台ID,keyga FROM table_后台账号 WHERE 后台ID=@后台ID", con))
                {
                    cmd.Parameters.AddWithValue("@后台ID", Cookie_UserName);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            密匙 = dr["keyga"].ToString();
                        }
                    }
                }
            }

            return 密匙;
        }

        private void 操作新增()
        {
            //Button_新增商户.Enabled = false;

            string 时间注册 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string 验证器google = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            string 写表 = "table_后台账号";
            string 写头 = "后台ID,后台密码,keyga,后台账号名称,后台账号分级,状态,时间注册";
            string 写值 = "@后台ID,@后台密码,@keyga,@后台账号名称,@后台账号分级,@状态,@时间注册";

            string _query = "INSERT INTO " + 写表 + "(" + 写头 + ") values (" + 写值 + ")";
            using (MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@后台ID", TextBox_账号信息_后台账号ID.Text);
                    comm.Parameters.AddWithValue("@后台密码", TextBox_后台账号密码.Text);
                    comm.Parameters.AddWithValue("@keyga", 验证器google);
                    comm.Parameters.AddWithValue("@后台账号名称", TextBox_账号名称.Text);
                    comm.Parameters.AddWithValue("@后台账号分级", "L2");
                    comm.Parameters.AddWithValue("@状态", DropDownList_状态.SelectedItem.Value);
                    comm.Parameters.AddWithValue("@时间注册", 时间注册);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();

                        conn.Close();

                        Response.Redirect("./后台账号列表.aspx");
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

        protected void Button_随机生成后台账号ID_Click(object sender, EventArgs e)
        {
            Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1,100, 300));
            string 生成编号 = "111"+ Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

            TextBox_账号信息_后台账号ID.Text = 生成编号;
        }

        protected void CheckBox_手动设置后台账号ID_CheckedChanged(object sender, EventArgs e)
        {
            if(CheckBox_手动设置后台账号ID.Checked==true)
            {
                TextBox_账号信息_后台账号ID.Enabled = true;
            }
            else
            {
                TextBox_账号信息_后台账号ID.Enabled = false;
            }
        }
    }
}