using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using Google.Authenticator;

namespace web1.WebsiteMerchant.Login
{
    public partial class 登入 : System.Web.UI.Page
    {
        string 现在时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
                    TextBox_回答.Text.Length > 1 && 
                    TextBox_回答.Text.Length < 10)
                {
                    using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                    {
                        using (MySqlCommand cmd11 = new MySqlCommand("SELECT 商户ID,keyga,登入错误累计,状态 FROM table_商户账号 WHERE 商户ID=@商户ID", con11))
                        {
                            cmd11.Parameters.AddWithValue("@商户ID", TextBox_账号.Text);
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
                                    string 商户ID = dr11["商户ID"].ToString();
                                    string 密匙 = dr11["keyga"].ToString();
                                    double 登入错误累计 = double.Parse(dr11["登入错误累计"].ToString());
                                    string 状态 = dr11["状态"].ToString();



                                    if (ClassLibrary1.ClassAccount.验证商户白名单IP(商户ID, ClassLibrary1.ClassAccount.来源IP()) == true)//验证来源IP是否在数据库是白名单IP
                                    {


                                        if (状态 == "启用")
                                        {
                                            if (登入错误累计 < 6)
                                            {

                                                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                                                var result = tfa.ValidateTwoFactorPIN(密匙, TextBox_回答.Text);

#if DEBUG
                                                result = true;
#endif

                                                if (result)
                                                {
                                                    //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内有效PIN码 " + DateTime.UtcNow.ToString();
                                                    //this.lblValidationResult.ForeColor = System.Drawing.Color.Green;

                                                    string 商户IDID = TextBox_账号.Text.Trim();
                                                    string 商户ID密码 = TextBox_密码.Text.Trim();

                                                    MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                                                    conn.Open();
                                                    MySqlCommand cmd = new MySqlCommand("select * from table_商户账号 where 商户ID=@商户ID and 商户密码=@商户密码 ", conn);

                                                    cmd.Parameters.AddWithValue("@商户ID", 商户IDID);
                                                    cmd.Parameters.AddWithValue("@商户密码", 商户ID密码);

                                                    MySqlDataReader sdr = cmd.ExecuteReader();
                                                    sdr.Read();
                                                    if (sdr.HasRows)
                                                    {
                                                        ClassLibrary1.ClassMessage.HinXi(Page, "登錄成功!");

                                                        最后登录时间(现在时间);//更改账户最后登录时间

                                                        if (this.CheckBox_保存验证.Checked == true)//判斷是否選擇了記住用戶名和密碼的複選框
                                                        {
                                                            string username = ClassLibrary1.ClassAccount.cookie加密(商户IDID);//用户名
                                                            string password = ClassLibrary1.ClassAccount.cookie加密(商户ID密码);//密码
                                                            string 时间最后登录 = ClassLibrary1.ClassAccount.cookie加密(现在时间);//密码
                                                            string nowtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                            //判斷客戶端瀏覽器是否存在該Cookie 存在就先清除

                                                            if (Request.Cookies["PPusernameMerchant"] != null && Request.Cookies["PPusernameMerchant"] != null)
                                                            {
                                                                Response.Cookies["PPusernameMerchant"].Expires = System.DateTime.Now.AddSeconds(-1);//Expires過期時間
                                                                Response.Cookies["PPusernameMerchant"].Expires = System.DateTime.Now.AddSeconds(-1);
                                                            }
                                                            //向客戶端瀏覽器加入Cookie (用戶名和密碼 最好是使用MD5加密)
                                                            HttpCookie hcUserName1 = new HttpCookie("PPusernameMerchant");
                                                            hcUserName1.Expires = System.DateTime.Now.AddDays(1);
                                                            hcUserName1.Values["username"] = username;
                                                            hcUserName1.Values["password"] = password;
                                                            hcUserName1.Values["ginx"] = 时间最后登录;
                                                            hcUserName1.Values["nowtime"] = nowtime;
                                                            //hcUserName1.Domain = "TEAWIFI.com";
                                                            //HttpCookie hcPassword1 = new HttpCookie("PPusernameMerchant");
                                                            //hcPassword1.Expires = System.DateTime.Now.AddDays(7);
                                                            //hcPassword1.Value = password;
                                                            Response.Cookies.Add(hcUserName1);
                                                            //Response.Cookies.Add(hcPassword1);




                                                            //插入前参数----------------------------------------------------------------------------------------------------
                                                            //---生成编号
                                                            Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 300));
                                                            string 生成编号 = "MLL" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                            //---操作名称
                                                            string 操作 = "登入商户账户";
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
                                                                string 哪个表 = "table_商户日志管理";
                                                                string 写哪些 = "编号,商户ID,keyga,keyga验证,操作,时间操作,IP";
                                                                string 写这些 = "@编号,@商户ID,@keyga,@keyga验证,@操作,@时间操作,@IP";

                                                                string str = "insert into " + 哪个表 + "(" + 写哪些 + ") values(" + 写这些 + ")";

                                                                scon.Open();
                                                                MySqlCommand command = new MySqlCommand();

                                                                command.Parameters.AddWithValue("@编号", 生成编号);
                                                                command.Parameters.AddWithValue("@商户ID", 商户ID);
                                                                command.Parameters.AddWithValue("@keyga", 密匙);
                                                                command.Parameters.AddWithValue("@keyga验证", TextBox_回答.Text);
                                                                command.Parameters.AddWithValue("@操作", 操作);
                                                                command.Parameters.AddWithValue("@时间操作", RegisterTime);
                                                                command.Parameters.AddWithValue("@IP", ipaddress);

                                                                command.Connection = scon;
                                                                command.CommandText = str;
                                                                int obj = command.ExecuteNonQuery();

                                                                scon.Close();
                                                            }
                                                            //----------------------------------------------------------------------------------------------------

                                                            Response.Redirect("~/WebsiteMerchant/MerchantOverview/商户首页.aspx");  //刷新頁面
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                    else
                                                    {
                                                        //ClassLibrary1.ClassMessage.HinXi(Page, "用户名或者密码错误");
                                                        登录错误();

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
                                            else
                                            {
                                                ClassLibrary1.ClassMessage.HinXi(Page, "账户锁定,联系客服查询");
                                            }


                                        }
                                        else
                                        {
                                            ClassLibrary1.ClassMessage.HinXi(Page, "账号未启用");
                                        }


                                    }
                                    else
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "需使用白名单IP登入 或者联系客服处理 你现在的前IP>> " + ClassLibrary1.ClassAccount.来源IP() + " (-S1003)");
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
                using (MySqlCommand cmd11 = new MySqlCommand("UPDATE table_商户账号 SET 时间最后登入=@时间最后登入 WHERE 商户ID=@商户ID ", con11))
                {
                    cmd11.Parameters.AddWithValue("@商户ID", TextBox_账号.Text.Trim());
                    cmd11.Parameters.AddWithValue("@时间最后登入", 写入最后登录时间);
                    con11.Open();
                    cmd11.ExecuteNonQuery();
                    con11.Close();
                }
            }
        }

        private void 登录错误()
        {
            using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd11 = new MySqlCommand("UPDATE table_商户账号 SET 登入错误累计=登入错误累计+1 WHERE 商户ID=@商户ID ", con11))
                {
                    cmd11.Parameters.AddWithValue("@商户ID", TextBox_账号.Text.Trim());
                    con11.Open();
                    cmd11.ExecuteNonQuery();
                    con11.Close();
                    ClassLibrary1.ClassMessage.HinXi(Page, "用户名或者密码错误");
                }
            }
        }

    }
}