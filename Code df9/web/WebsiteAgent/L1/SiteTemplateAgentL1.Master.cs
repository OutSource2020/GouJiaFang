using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web1.WebsiteAgent.L1
{
    public partial class SiteTemplateAgentL1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    ClassLibrary1.ClassAccount.验证账号商户端();
            //}



            修改label();
        }

        private void 修改label()
        {
            if (ClassLibrary1.ClassAccount.检查代理L1端cookie() == true)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"]["username"]);

                //修改为用户ID
                Label mpLabel = (Label)Page.Master.FindControl("Label_代理ID");
                if (mpLabel != null)
                {
                    mpLabel.Text = Cookie_UserName;
                }
            }
            else
            {
                ClassLibrary1.ClassAccount.弹回登入代理L1页();
            }
        }

    }

}