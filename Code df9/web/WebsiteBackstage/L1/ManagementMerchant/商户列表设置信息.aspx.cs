using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{
    public partial class 商户列表设置信息 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();

                获得所属管理L2();
                获得所属代理L1();
                获得所属代理L2();
                this.GetCustomer();
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("商户列表设置.aspx?Bianhao=" + 编号 + "");
        }


        protected void CustomerUpdate(object sender, EventArgs e)
        {
            this.更新内容();
        }

        private void 获得所属管理L2()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 后台ID,后台账号名称 from table_后台账号";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_后台账号");
            myconn.Close();
            DropDownList_所属管理L2.DataSource = ds.Tables[0].DefaultView;
            DropDownList_所属管理L2.DataTextField = ds.Tables["table_后台账号"].Columns["后台账号名称"].ToString();
            DropDownList_所属管理L2.DataValueField = ds.Tables["table_后台账号"].Columns["后台ID"].ToString();
            DropDownList_所属管理L2.DataBind();
            DropDownList_所属管理L2.Items.Insert(0, new ListItem("请选择", "未选择"));

            myconn.Close();
        }

        private void 获得所属代理L1()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 代理ID,代理名称 from table_代理账号等级1";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_代理账号等级1");
            myconn.Close();
            DropDownList_所属代理L1.DataSource = ds.Tables[0].DefaultView;
            DropDownList_所属代理L1.DataTextField = ds.Tables["table_代理账号等级1"].Columns["代理名称"].ToString();
            DropDownList_所属代理L1.DataValueField = ds.Tables["table_代理账号等级1"].Columns["代理ID"].ToString();
            DropDownList_所属代理L1.DataBind();
            DropDownList_所属代理L1.Items.Insert(0, new ListItem("请选择", "未选择"));

            myconn.Close();
        }

        private void 获得所属代理L2()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 代理ID,代理名称 from table_代理账号等级2";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_代理账号等级2");
            myconn.Close();
            DropDownList_所属代理L2.DataSource = ds.Tables[0].DefaultView;
            DropDownList_所属代理L2.DataTextField = ds.Tables["table_代理账号等级2"].Columns["代理名称"].ToString();
            DropDownList_所属代理L2.DataValueField = ds.Tables["table_代理账号等级2"].Columns["代理ID"].ToString();
            DropDownList_所属代理L2.DataBind();
            DropDownList_所属代理L2.Items.Insert(0, new ListItem("请选择", "未选择"));

            myconn.Close();
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

        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,商户名称,状态,时间最后登入,时间注册,所属管理L2,所属代理L1,所属代理L2 FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.TextBox_商户名称.Text = dr["商户名称"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();
                            this.Label_时间最后登入.Text = dr["时间最后登入"].ToString();
                            this.Label_时间注册.Text = dr["时间注册"].ToString();
                            this.DropDownList_所属管理L2.SelectedValue = dr["所属管理L2"].ToString();
                            this.DropDownList_所属代理L1.SelectedValue = dr["所属代理L1"].ToString();
                            this.DropDownList_所属代理L2.SelectedValue = dr["所属代理L2"].ToString();
                        }
                    }
                }
            }
        }

        private void 更新内容()//更新出去
        {
            if (TextBox_商户名称.Text.Length > 0 )
            {
                操作更新();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }

            
        }

        private void 操作更新()
        {
            Button_发出更新.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户账号 SET 商户名称=@商户名称 , 状态=@状态 , 所属管理L2=@所属管理L2 ,所属代理L1=@所属代理L1 ,所属代理L2=@所属代理L2 WHERE 商户ID=@商户ID ", con))
                {
                    cmd.Parameters.AddWithValue("@商户名称", TextBox_商户名称.Text);
                    cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@所属管理L2", DropDownList_所属管理L2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@所属代理L1", DropDownList_所属代理L1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@所属代理L2", DropDownList_所属代理L2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string 编号 = 从URL获取值();
                    Response.Redirect("商户列表设置.aspx?Bianhao=" + 编号 + "");
                    

                }
            }
        }
    }
}