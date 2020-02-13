using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Google.Authenticator;

namespace web1
{
    public partial class GoogleAuthenticator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var result = tfa.ValidateTwoFactorPIN(TextBox3.Text, this.txtCode.Text);

            if (result)
            {
                this.lblValidationResult.Text = this.txtCode.Text + " is a valid PIN at UTC time " + DateTime.UtcNow.ToString();
                this.lblValidationResult.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                this.lblValidationResult.Text = this.txtCode.Text + " is not a valid PIN at UTC time " + DateTime.UtcNow.ToString();
                this.lblValidationResult.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode(TextBox1.Text, TextBox2.Text, TextBox3.Text, false, 2);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;

            this.imgQrCode.ImageUrl = qrCodeImageUrl;
            this.lblManualSetupCode.Text = manualEntrySetupCode;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox3.Text = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        }
    }
}