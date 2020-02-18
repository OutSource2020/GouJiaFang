using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using Google.Authenticator;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{
    public partial class 商户列表设置安全QR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
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

        protected void Button_验证_Click(object sender, EventArgs e)
        {
            string 从URL传来值 = 从URL获取值();
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,keyga FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            string 商户ID = dr["商户ID"].ToString();
                            string 密匙 = dr["keyga"].ToString();

                            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                            var result = tfa.ValidateTwoFactorPIN(密匙, this.txtCode.Text);

                            if (result)
                            {
                                this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内有效PIN码 " + DateTime.UtcNow.ToString();
                                this.lblValidationResult.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                this.lblValidationResult.Text = this.txtCode.Text + " 是UTC时间内不有效的PIN码 " + DateTime.UtcNow.ToString();
                                this.lblValidationResult.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            
        }

        protected void Button_重置生成新的_Click(object sender, EventArgs e)
        {
            Button_重置生成新的.Enabled = false;

            string 从URL传来值 = 从URL获取值();
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户账号 SET keyga=@keyga WHERE 商户ID=@商户ID ", con))
                {
                    cmd.Parameters.AddWithValue("@keyga", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    //Response.Redirect(Request.Url.AbsoluteUri, false);
                }
            }

        }

        protected void Button_读取账号生成二维码_Click(object sender, EventArgs e)
        {
            Button_读取账号生成二维码.Enabled = false;

            string 从URL传来值 = 从URL获取值();
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,keyga FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            string 商户ID = dr["商户ID"].ToString();
                            string 密匙= dr["keyga"].ToString();

                            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                            var setupInfo = tfa.GenerateSetupCode("PPMQRCode", 商户ID, 密匙, false, 2);

                            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                            string manualEntrySetupCode = setupInfo.ManualEntryKey;

                            this.imgQrCode.ImageUrl = qrCodeImageUrl;
                            this.lblManualSetupCode.Text = manualEntrySetupCode;
                        }
                    }
                }
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("商户列表设置安全.aspx?Bianhao=" + 编号 + "");
        }
    }
}