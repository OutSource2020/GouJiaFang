using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web1.WebsiteMerchant.商户账号
{
    public partial class 登出账号 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();
            }

            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();
            Label_商户ID.Text = Cookie_UserName;
        }

        protected void Button_立即登出账号_Click(object sender, EventArgs e)
        {
            //判斷客戶端瀏覽器是否存在該Cookie 存在就先清除

            if (Request.Cookies["PPusernameBB"] != null)
            {
                Response.Cookies["PPusernameBB"].Expires = System.DateTime.Now.AddSeconds(-1);//Expires過期時間
            }

            Response.Redirect("/WebsiteMerchant/Login/登入.aspx");
        }
    }
}