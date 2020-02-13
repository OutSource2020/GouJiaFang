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
    public partial class 后台账号列表设置账号信息 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();

                this.GetCustomer();
            }

            string 从URL传来值 = 从URL获取值();
            if (判断是否为分级等级L2(从URL传来值) == true)
            {

            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "不属于L2账号");

                Button_发出更新.Enabled = false;
                TextBox_后台账号名称.Enabled = false;
                DropDownList_下拉框1.Enabled = false;
            }


        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("后台账号列表设置.aspx?Bianhao=" + 编号 + "");
        }


        protected void CustomerUpdate(object sender, EventArgs e)
        {
            this.更新内容();
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
                using (MySqlCommand cmd = new MySqlCommand("SELECT 后台ID,后台账号名称,状态,时间最后登入,时间注册 FROM table_后台账号 WHERE 后台ID=@后台ID", con))
                {
                    cmd.Parameters.AddWithValue("@后台ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_后台ID.Text = dr["后台ID"].ToString();
                            this.TextBox_后台账号名称.Text = dr["后台账号名称"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();
                            this.Label_时间最后登入.Text = dr["时间最后登入"].ToString();
                            this.Label_时间注册.Text = dr["时间注册"].ToString();
                        }
                    }
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

        private void 更新内容()//更新出去
        {
            if (TextBox_后台账号名称.Text.Length > 1 )
            {

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                var result = tfa.ValidateTwoFactorPIN(获取密匙(), TextBox_验证密匙.Text);

                if (result)
                {
                    //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内有效PIN码 " + DateTime.UtcNow.ToString();
                    //this.lblValidationResult.ForeColor = System.Drawing.Color.Green;


                    操作更新();
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
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }

            
        }

        private void 操作更新()
        {
            Button_发出更新.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台账号 SET 后台账号名称=@后台账号名称 , 状态=@状态 WHERE 后台ID=@后台ID ", con))
                {
                    cmd.Parameters.AddWithValue("@后台账号名称", TextBox_后台账号名称.Text);
                    cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@后台ID", 从URL传来值);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string 编号 = 从URL获取值();
                    Response.Redirect("后台账号列表设置.aspx?Bianhao=" + 编号 + "");
                    

                }
            }
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