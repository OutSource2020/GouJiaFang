using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{

    /*
    邮箱账号 huijupay168@163.com  
    密码 zxc23456
     */
    public partial class 商户列表设置安全密码 : System.Web.UI.Page
    {
        string 邮箱主机地址 = "smtp.163.com";
        string 邮箱主机地址端口 = "25";
        string 邮箱用户名 = "huijupay168@163.com";
        string 邮箱密码 = "zxc1234567";
        string 邮箱发件人 = "huijupay168@163.com";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
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
            Response.Redirect("商户列表设置安全.aspx?Bianhao=" + 编号 + "");
        }

        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_发送商户账号密码和支付密码_Click(object sender, EventArgs e)
        {
            Button_发送商户账号密码和支付密码.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            //查找数据库中的密码
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 绑定邮箱,商户密码,支付密码,商户名称 FROM table_商户账号 WHERE 商户ID=@商户ID ", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            string 发给谁 = dr["绑定邮箱"].ToString();
                            string 商户密码 = dr["商户密码"].ToString();
                            string 支付密码 = dr["支付密码"].ToString();
                            string 商户名称 = dr["商户名称"].ToString();

                            string 换行 = System.Environment.NewLine + "<br>";

                            string 邮件标题 = "平台通知";
                            // string 邮件内容 = "你的ID=" + 从URL传来值 + "" + 换行 + " 登入密码=" + 商户密码 + "" + 换行 + " 支付密码=" + 支付密码 + "" + 换行 + "  尽快登录修改密码!";
                            string 邮件内容 = string.Format(
                                "【{0}用户】您已在平台成功获取支付密码，登录后请尽快更换密码。{1}{1}账户名为：{1}{1}{2}{1}初始登录密码为：{1}{1}{3}{1}初始支付密码为：{1}{1}{4}{1}操作后台登录网址：",
                                商户名称, 换行, 从URL传来值, 商户密码, 支付密码)
                                + " http://huijuzf.com/" + 换行 + 换行 + "此信息非常重要，请妥善保存，不要向任何人透露。如需要重置，请联系客服。";
                            string 从哪发 = 邮箱发件人;

                            MailMessage msg = new MailMessage();
                            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                            try
                            {
                                msg.Subject = 邮件标题;//主题
                                msg.Body = 邮件内容;//添加郵件正文部分
                                msg.From = new MailAddress(从哪发);//合法的郵件地址 从哪
                                msg.To.Add(发给谁);//合法的郵件地址 发到
                                msg.IsBodyHtml = true;
                                client.Host = 邮箱主机地址;
                                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(邮箱用户名, 邮箱密码);
                                client.Port = int.Parse(邮箱主机地址端口);
                                client.EnableSsl = true;
                                client.UseDefaultCredentials = false;
                                client.Credentials = basicauthenticationinfo;
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.Send(msg);
                            }
                            catch (Exception ex)
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "" + ex + "");
                                //log.Error(ex.Message);
                            }


                        }
                    }
                }
            }
        }

        protected void Button_重置商户账号密码和支付密码_Click(object sender, EventArgs e)
        {
            Button_重置商户账号密码和支付密码.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,绑定邮箱,商户名称 FROM table_商户账号 WHERE 商户ID=@商户ID ", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            //重置密码
                            //随机生成
                            string 生成登入密码 = Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100000, 999999));
                            string 生成支付密码 = Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100000, 999999));

                            string 换行 = System.Environment.NewLine + "<br>";

                            string 邮件标题 = "平台通知";
                            string 发给谁 = dr["绑定邮箱"].ToString();
                            string 从哪发 = 邮箱发件人;
                            string 商户名称 = dr["商户名称"].ToString();

                            // string 邮件内容 = "你的ID=" + 从URL传来值 + "" + 换行 + " 登入密码=" + 生成登入密码 + "" + 换行 + " 支付密码=" + 生成支付密码 + "" + 换行 + "  尽快登录修改密码!";
                            string 邮件内容 = string.Format(
                                "【{0}用户】您已在平台成功重置支付密码，登录后请尽快更换密码。{1}{1}账户名为：{1}{1}{2}{1}初始登录密码为：{1}{1}{3}{1}初始支付密码为：{1}{1}{4}{1}操作后台登录网址：",
                                商户名称, 换行, 从URL传来值, 生成登入密码, 生成支付密码)
                                + " http://huijuzf.com/" + 换行 + 换行 + "此信息非常重要，请妥善保存，不要向任何人透露。如需要重置，请联系客服。";


                            //更新 重置密码
                            using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd12 = new MySqlCommand("UPDATE table_商户账号 SET 商户密码=@商户密码 , 支付密码=@支付密码 WHERE 商户ID=@商户ID ", con12))
                                {
                                    cmd12.Parameters.AddWithValue("@商户密码", 生成登入密码);
                                    cmd12.Parameters.AddWithValue("@支付密码", 生成支付密码);
                                    cmd12.Parameters.AddWithValue("@商户ID", 从URL传来值);

                                    con12.Open();
                                    cmd12.ExecuteNonQuery();
                                    con12.Close();

                                    MailMessage msg = new MailMessage();
                                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                                    try
                                    {
                                        msg.Subject = 邮件标题;//主题
                                        msg.Body = 邮件内容;//添加郵件正文部分
                                        msg.From = new MailAddress(从哪发);//合法的郵件地址 从哪
                                        msg.To.Add(发给谁);//合法的郵件地址 发到
                                        msg.IsBodyHtml = true;
                                        client.Host = 邮箱主机地址;
                                        System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(邮箱用户名, 邮箱密码);
                                        client.Port = int.Parse(邮箱主机地址端口);
                                        client.EnableSsl = true;
                                        client.UseDefaultCredentials = false;
                                        client.Credentials = basicauthenticationinfo;
                                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        client.Send(msg);
                                    }
                                    catch (Exception ex)
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "" + ex + "");
                                        //log.Error(ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Button_发送商户账号密码API_Click(object sender, EventArgs e)
        {
            Button_发送商户账号密码API.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            //查找数据库中的密码
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 绑定邮箱,商户密码API,商户名称,公共密匙 FROM table_商户账号 WHERE 商户ID=@商户ID ", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            string 发给谁 = dr["绑定邮箱"].ToString();
                            string 商户密码 = dr["商户密码API"].ToString();
                            string 商户名称 = dr["商户名称"].ToString();
                            string 商户密匙 = dr["公共密匙"].ToString();

                            string 换行 = System.Environment.NewLine + "<br>";

                            string 邮件标题 = "平台通知";
                            // string 邮件内容 = "你的ID=" + 从URL传来值 + "" + 换行 + " 商户密码API=" + 商户密码 + "  尽快登录修改 API密码!";
                            string 邮件内容 = string.Format(
                                "【{0}用户】您已在平台获取账户密码API，登录后请尽快更换密码。{1}账户名为：{1}{1}{2}{1}商户密码API为：{1}{1}{3}{1}商户公共密匙为：{1}{1}{4}{1}操作后台登录网址：",
                                商户名称, 换行, 从URL传来值, 商户密码, 商户密匙)
                                + " http://huijuzf.com/" + 换行 + 换行 + "此信息非常重要，请妥善保存，不要向任何人透露。如需要重置，请联系客服。";

                            string 从哪发 = 邮箱发件人;

                            MailMessage msg = new MailMessage();
                            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                            try
                            {
                                msg.Subject = 邮件标题;//主题
                                msg.Body = 邮件内容;//添加郵件正文部分
                                msg.From = new MailAddress(从哪发);//合法的郵件地址 从哪
                                msg.To.Add(发给谁);//合法的郵件地址 发到
                                msg.IsBodyHtml = true;
                                client.Host = 邮箱主机地址;
                                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(邮箱用户名, 邮箱密码);
                                client.Port = int.Parse(邮箱主机地址端口);
                                client.EnableSsl = true;
                                client.UseDefaultCredentials = false;
                                client.Credentials = basicauthenticationinfo;
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.Send(msg);
                            }
                            catch (Exception ex)
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "" + ex + "");
                                //log.Error(ex.Message);
                            }


                        }
                    }
                }
            }


        }

        protected void Button_重置商户账号密码API_Click(object sender, EventArgs e)
        {
            Button_重置商户账号密码API.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,绑定邮箱,商户名称 FROM table_商户账号 WHERE 商户ID=@商户ID ", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            //重置密码
                            //随机生成
                            string 生成商户密码API = Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100000, 999999));
                            string 生成商户密匙 = Guid.NewGuid().ToString("N");

                            string 换行 = System.Environment.NewLine + "<br>";

                            string 邮件标题 = "平台通知";
                            string 商户名称 = dr["商户名称"].ToString();
                            string 从哪发 = 邮箱发件人;
                            string 发给谁 = dr["绑定邮箱"].ToString();

                            // string 邮件内容 = "你的ID=" + 从URL传来值 + "" + 换行 + " 商户密码API=" + 生成商户密码API + "  尽快登录修改 API密码!";
                            string 邮件内容 = string.Format(
                                "【{0}用户】您已在平台重置账户密码API，登录后请尽快更换密码。{1}账户名为：{1}{1}{2}{1}商户密码API为：{1}{1}{3}{1}商户公共密匙为：{1}{1}{4}{1}操作后台登录网址：",
                                商户名称, 换行, 从URL传来值, 生成商户密码API, 生成商户密匙)
                                + " http://huijuzf.com/" + 换行 + 换行 + "此信息非常重要，请妥善保存，不要向任何人透露。如需要重置，请联系客服。";

                            //更新 重置密码
                            using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd12 = new MySqlCommand("UPDATE table_商户账号 SET 商户密码API=@商户密码API, 公共密匙=@商户密匙 WHERE 商户ID=@商户ID ", con12))
                                {
                                    cmd12.Parameters.AddWithValue("@商户密码API", 生成商户密码API);
                                    cmd12.Parameters.AddWithValue("@商户ID", 从URL传来值);
                                    cmd12.Parameters.AddWithValue("@商户密匙", 生成商户密匙);

                                    con12.Open();
                                    cmd12.ExecuteNonQuery();
                                    con12.Close();
                                    MailMessage msg = new MailMessage();
                                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                                    try
                                    {
                                        msg.Subject = 邮件标题;//主题
                                        msg.Body = 邮件内容;//添加郵件正文部分
                                        msg.From = new MailAddress(从哪发);//合法的郵件地址 从哪
                                        msg.To.Add(发给谁);//合法的郵件地址 发到
                                        msg.IsBodyHtml = true;
                                        client.Host = 邮箱主机地址;
                                        System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(邮箱用户名, 邮箱密码);
                                        client.Port = int.Parse(邮箱主机地址端口);
                                        client.EnableSsl = true;
                                        client.UseDefaultCredentials = false;
                                        client.Credentials = basicauthenticationinfo;
                                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        client.Send(msg);
                                    }
                                    catch (Exception ex)
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "" + ex + "");
                                        //log.Error(ex.Message);
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}