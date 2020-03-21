using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using Google.Authenticator;

namespace web1.WebsiteBackstage.L1.Login
{
    public partial class 登入 : System.Web.UI.Page
    {
        string 现在时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckBox_保存验证.Enabled = false;
            CheckBox_保存验证.Checked = true;
        }

        protected void Button_登入_Click(object sender, EventArgs e)
        {
            //ClassLibrary1.ClassMessage.HinXi(Page, "执行验证码通过授权");
            //用户登入============================================================
            if (TextBox_账号.Text == "" || TextBox_密码.Text == "")
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "输入用户名和密码!");
            }
            else
            {
                if (TextBox_账号.Text.Length > 1 &&
                    TextBox_账号.Text.Length < 30 &&
                    TextBox_密码.Text.Length > 1 &&
                    TextBox_密码.Text.Length < 30 &&
                    TextBox_输入员工号.Text.Length > 1 &&
                    TextBox_输入员工号.Text.Length < 10)
                {


                    using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                    {
                        using (MySqlCommand cmd11 = new MySqlCommand("SELECT 后台ID,keyga,状态 FROM table_后台账号 WHERE 后台ID=@后台ID", con11))
                        {
                            cmd11.Parameters.AddWithValue("@后台ID", TextBox_账号.Text);
                            using (MySqlDataAdapter da11 = new MySqlDataAdapter(cmd11))
                            {
                                DataTable images11 = new DataTable();
                                da11.Fill(images11);

                                 if (images11.Rows.Count<1)
                                 {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "账号不存在!");
                                        return;
                                 }
                                foreach (DataRow dr11 in images11.Rows)
                                {
                                    string 查询管理ID = dr11["后台ID"].ToString();
                                    string 查询密匙 = dr11["keyga"].ToString();
                                    string 查询状态 = dr11["状态"].ToString();



                                    #if DEBUG
                                    if (true)
                                    #else
                                    if (ClassLibrary1.ClassAccount.验证管理L1白名单IP(查询管理ID, ClassLibrary1.ClassAccount.来源IP()) == true)//验证来源IP是否在数据库是白名单IP
                                    #endif
                                    {
                                        if (查询状态 == "启用")
                                        {

                                            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                                            var result = tfa.ValidateTwoFactorPIN(查询密匙, TextBox_输入员工号.Text);

#if DEBUG
                                            result = true;
#endif
                                            if (result)
                                            {
                                                //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内有效PIN码 " + DateTime.UtcNow.ToString();
                                                //this.lblValidationResult.ForeColor = System.Drawing.Color.Green;




                                                string 后台ID = TextBox_账号.Text.Trim();
                                                string 后台密码 = TextBox_密码.Text.Trim();

                                                MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                                                conn.Open();
                                                MySqlCommand cmd = new MySqlCommand("select * from table_后台账号 where 后台ID=@后台ID and 后台密码=@后台密码 ", conn);
                                                cmd.Parameters.AddWithValue("@后台ID", 后台ID);
                                                cmd.Parameters.AddWithValue("@后台密码", 后台密码);

                                                MySqlDataReader sdr = cmd.ExecuteReader();
                                                sdr.Read();
                                                if (sdr.HasRows)
                                                {
                                                    ClassLibrary1.ClassMessage.HinXi(Page, "登錄成功!");

                                                    最后登录时间(现在时间);//更改账户最后登录时间

                                                    if (this.CheckBox_保存验证.Checked == true)//判斷是否選擇了記住用戶名和密碼的複選框
                                                    {
                                                        string username = ClassLibrary1.ClassAccount.cookie加密(后台ID);//用户名
                                                        string password = ClassLibrary1.ClassAccount.cookie加密(后台密码);//密码
                                                        string nowtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                        //判斷客戶端瀏覽器是否存在該Cookie 存在就先清除

                                                        if (Request.Cookies["PPusernameBackstageL1"] != null && Request.Cookies["PPusernameBackstageL1"] != null)
                                                        {
                                                            Response.Cookies["PPusernameBackstageL1"].Expires = System.DateTime.Now.AddSeconds(-1);//Expires過期時間
                                                            Response.Cookies["PPusernameBackstageL1"].Expires = System.DateTime.Now.AddSeconds(-1);
                                                        }
                                                        //向客戶端瀏覽器加入Cookie (用戶名和密碼 最好是使用MD5加密)
                                                        HttpCookie hcUserName1 = new HttpCookie("PPusernameBackstageL1");
                                                        hcUserName1.Expires = System.DateTime.Now.AddDays(1);
                                                        hcUserName1.Values["username"] = username;
                                                        hcUserName1.Values["password"] = password;
                                                        hcUserName1.Values["nowtime"] = nowtime;
                                                        //hcUserName1.Domain = "TEAWIFI.com";
                                                        //HttpCookie hcPassword1 = new HttpCookie("PPusernameBB");
                                                        //hcPassword1.Expires = System.DateTime.Now.AddDays(7);
                                                        //hcPassword1.Value = password;
                                                        Response.Cookies.Add(hcUserName1);
                                                        //Response.Cookies.Add(hcPassword1);




                                                        //插入前参数
                                                        //---生成编号
                                                        Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 300));
                                                        string 生成编号 = "BLLL1" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                        //---操作名称
                                                        string 操作 = "登入后台账户";
                                                        //---获取登入IP地址
                                                        string ipaddress;
                                                        ipaddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                                        if (ipaddress == "" || ipaddress == null)
                                                            ipaddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                                                        //---获取时间
                                                        string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                        //开始插入登入日志
                                                        using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                        {
                                                            string 哪个表 = "table_后台日志管理";
                                                            string 写哪些 = "编号,后台ID,keyga,keyga验证,操作,时间操作,IP";
                                                            string 写这些 = "@编号,@后台ID,@keyga,@keyga验证,@操作,@时间操作,@IP";

                                                            string str = "insert into " + 哪个表 + "(" + 写哪些 + ") values(" + 写这些 + ")";

                                                            scon.Open();
                                                            MySqlCommand command = new MySqlCommand();

                                                            command.Parameters.AddWithValue("@编号", 生成编号);
                                                            command.Parameters.AddWithValue("@后台ID", 后台ID);
                                                            command.Parameters.AddWithValue("@keyga", 查询密匙);
                                                            command.Parameters.AddWithValue("@keyga验证", TextBox_输入员工号.Text);
                                                            command.Parameters.AddWithValue("@操作", 操作);
                                                            command.Parameters.AddWithValue("@时间操作", RegisterTime);
                                                            command.Parameters.AddWithValue("@IP", ipaddress);

                                                            command.Connection = scon;
                                                            command.CommandText = str;
                                                            int obj = command.ExecuteNonQuery();

                                                            scon.Close();
                                                        }







                                                        Response.Redirect("~/WebsiteBackstage/L1/ControlCenter/管理控制台.aspx");  //刷新頁面
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    ClassLibrary1.ClassMessage.HinXi(Page, "用户名或者密码错误");
                                                }

                                                conn.Close();




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
                                        ClassLibrary1.ClassMessage.HinXi(Page, "需使用白名单IP登入 或者联系客服处理 你现在的前IP>> " + ClassLibrary1.ClassAccount.来源IP() + " (-S1003x)");
                                    }

                                }
                            }
                        }
                    }


                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "账号密码是否符合格式?");
                }





            }
        }
        
        private void 最后登录时间(string 写入最后登录时间)
        {
            using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd11 = new MySqlCommand("UPDATE table_后台账号 SET 时间最后登入=@时间最后登入 WHERE 后台ID=@后台ID ", con11))
                {
                    cmd11.Parameters.AddWithValue("@后台ID", TextBox_账号.Text.Trim());
                    cmd11.Parameters.AddWithValue("@时间最后登入", 写入最后登录时间);
                    con11.Open();
                    cmd11.ExecuteNonQuery();
                    con11.Close();
                }
            }
        }







    }
}