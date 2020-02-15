using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web1
{
  public partial class TestForm : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

      
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      var db= (new DBClient()).GetClient();

      var data= db.Queryable<table_商户账号>().InSingle(1);
      TextBox3.Text = 123.ToString(data.商户名称);
    }
  }
}