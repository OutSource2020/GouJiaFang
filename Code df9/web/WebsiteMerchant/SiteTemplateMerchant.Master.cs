using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web1.WebsiteMerchant
{
    public partial class SiteTemplateMerchant : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            修改label();
        }


        private void 修改label()
        {
            if (ClassLibrary1.ClassAccount.检查商户端cookie() == true)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

                //修改为用户ID
                Label mpLabel = (Label)Page.Master.FindControl("Label_商户ID");
                if (mpLabel != null)
                {
                    mpLabel.Text = Cookie_UserName;
                }
            }
            else
            {
                ClassLibrary1.ClassAccount.弹回登入商户页();
            }
        }



    }
}