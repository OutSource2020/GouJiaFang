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
    public partial class 后台账号列表设置密码 : System.Web.UI.Page
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

                Button_尝试修改.Enabled = false;
                TextBox_key.Enabled = false;
                TextBox_新密码1.Enabled = false;
                TextBox_新密码2.Enabled = false;
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
            Response.Redirect("后台账号列表设置.aspx?Bianhao=" + 编号 + "");
        }

        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 后台ID FROM table_后台账号 WHERE 后台ID=@后台ID", con))
                {
                    cmd.Parameters.AddWithValue("@后台ID", 从URL传来值 );
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_后台ID.Text = dr["后台ID"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_尝试修改_Click(object sender, EventArgs e)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"]["username"]);

            using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd11 = new MySqlCommand("SELECT 后台ID,keyga,状态 FROM table_后台账号 WHERE 后台ID=@后台ID", con11))
                {
                    cmd11.Parameters.AddWithValue("@后台ID", Cookie_UserName );
                    using (MySqlDataAdapter da11 = new MySqlDataAdapter(cmd11))
                    {
                        DataTable images11 = new DataTable();
                        da11.Fill(images11);
                        foreach (DataRow dr11 in images11.Rows)
                        {
                            string 查询管理ID = dr11["后台ID"].ToString();
                            string 查询密匙 = dr11["keyga"].ToString();
                            string 查询状态 = dr11["状态"].ToString();



                            if (ClassLibrary1.ClassAccount.验证管理L1白名单IP(查询管理ID, ClassLibrary1.ClassAccount.来源IP()) == true)//验证来源IP是否在数据库是白名单IP
                            {
                                if (查询状态 == "启用")
                                {

                                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                                    var result = tfa.ValidateTwoFactorPIN(查询密匙, TextBox_key.Text);

                                    if (result)
                                    {
                                        //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内有效PIN码 " + DateTime.UtcNow.ToString();
                                        //this.lblValidationResult.ForeColor = System.Drawing.Color.Green;


                                        修改密码();
                                    }
                                    else
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "KEY错误");

                                        //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内不有效的PIN码 " + DateTime.UtcNow.ToString();
                                        //this.lblValidationResult.ForeColor = System.Drawing.Color.Red;
                                    }

                                }




                            }
                            else
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "需使用白名单IP 你现在的前IP>> " + ClassLibrary1.ClassAccount.来源IP() + " (-S1003x)");
                            }

                        }
                    }
                }
            }



        }

        private void 修改密码()
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台账号 SET 后台密码=@后台密码 WHERE 后台ID=@后台ID ", con))
                {
                    cmd.Parameters.AddWithValue("@后台密码", TextBox_新密码1.Text);
                    cmd.Parameters.AddWithValue("@后台ID", 从URL传来值);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            string 编号 = 从URL获取值();
            Response.Redirect("后台账号列表设置.aspx?Bianhao=" + 编号 + "");

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