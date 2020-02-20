using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web1.WebsiteMerchant.MerchantOrder
{
  public partial class 商户提款引导 : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button_文本导入_Click(object sender, EventArgs e)
    {
      Response.Redirect("商户充值记录.aspx");
    }
    protected void Button_文档导入_Click(object sender, EventArgs e)
    {
      Response.Redirect("商户充值记录.aspx");
    }
    protected void Button_手动输入_Click(object sender, EventArgs e)
    {
      Response.Redirect("商户充值记录.aspx");
    }
  }
}