using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using Google.Authenticator;

namespace web1.WebsiteMerchant.商户账号
{
    public partial class 商户账号密码登入API : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();
                chaxun();
            }

            
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户账号.aspx");
        }

        private void chaxun()
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
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

        protected void Button_更新密码_Click(object sender, EventArgs e)
        {
            

            if (TextBox_商户旧登入密码.Text.Length > 0 && TextBox_商户新登入密码.Text.Length > 0 && TextBox_商户新登入密码再次确认.Text.Length > 0)
            {
                if (TextBox_商户新登入密码.Text == TextBox_商户新登入密码再次确认.Text)
                {
                    using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                    {
                        string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

                        using (MySqlCommand cmd11 = new MySqlCommand("SELECT 商户ID,keyga FROM table_商户账号 WHERE 商户ID=@商户ID", con11))
                        {

                            cmd11.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                            using (MySqlDataAdapter da11 = new MySqlDataAdapter(cmd11))
                            {
                                DataTable images11 = new DataTable();
                                da11.Fill(images11);
                                foreach (DataRow dr11 in images11.Rows)
                                {
                                    string 商户ID = dr11["商户ID"].ToString();
                                    string 密匙 = dr11["keyga"].ToString();

                                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                                    var result = tfa.ValidateTwoFactorPIN(密匙, TextBox_KEY.Text);

                                    if (result)
                                    {
                                        //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内有效PIN码 " + DateTime.UtcNow.ToString();
                                        //this.lblValidationResult.ForeColor = System.Drawing.Color.Green;
                                        
                                        操作更新();
                                    }
                                    else
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "用户名或者密码错误或者没有这个员工");

                                        //this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内不有效的PIN码 " + DateTime.UtcNow.ToString();
                                        //this.lblValidationResult.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                            }
                        }
                    }



                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "两次输入的新密码不同 检查是否输入错误");
                }
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }


        private void 操作更新()
        {
            Button_更新密码.Enabled = false;

            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,商户密码API FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            string 商户密码旧 = dr["商户密码API"].ToString();

                            if (商户密码旧 == TextBox_商户旧登入密码.Text)
                            {
                                Button_更新密码.Enabled = false;

                                using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand cmd12 = new MySqlCommand("UPDATE table_商户账号 SET 商户密码API=@商户密码API WHERE 商户ID=@商户ID ", con12))
                                    {
                                        cmd12.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                                        cmd12.Parameters.AddWithValue("@商户密码API", TextBox_商户新登入密码.Text);

                                        con12.Open();
                                        cmd12.ExecuteNonQuery();
                                        con12.Close();

                                        //Response.Redirect(Request.Url.AbsoluteUri, false);
                                        Response.Redirect("商户账号.aspx");

                                    }
                                }

                            }
                            else
                            {
                                //ClassLibrary1.ClassMessage.HinXi(Page, "旧密码不对");
                                密码错误();
                            }

                        }
                    }
                }
            }
        }

        private void 密码错误()
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd11 = new MySqlCommand("UPDATE table_商户账号 SET 登入错误累计=登入错误累计+1 WHERE 商户ID=@商户ID ", con11))
                {
                    cmd11.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    con11.Open();
                    cmd11.ExecuteNonQuery();
                    con11.Close();
                    //this.SaveImage(filePath);
                    //Response.Redirect() //Response.Redirect(Request.Url.AbsoluteUri, false);
                    ClassLibrary1.ClassMessage.HinXi(Page, "旧密码不对");
                }
            }
        }
    }
}