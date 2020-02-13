using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementAgent.SettingL2
{
    public partial class 代理列表L2设置信息 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();

                获得所属代理();
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("代理列表L2设置.aspx?Bianhao=" + 编号 + "");
        }


        protected void CustomerUpdate(object sender, EventArgs e)
        {
            this.更新内容();
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
                using (MySqlCommand cmd = new MySqlCommand("SELECT 代理ID,代理名称,状态,时间最后登入,时间注册 FROM table_代理账号等级2 WHERE 代理ID=@代理ID", con))
                {
                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_代理ID.Text = dr["代理ID"].ToString();
                            this.TextBox_代理名称.Text = dr["代理名称"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();
                            this.Label_时间最后登入.Text = dr["时间最后登入"].ToString();
                            this.Label_时间注册.Text = dr["时间注册"].ToString();
                        }
                    }
                }
            }
        }

        private void 获得所属代理()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 代理ID,代理名称 from table_代理账号等级1";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_代理账号等级1");
            myconn.Close();
            DropDownList_所属代理.DataSource = ds.Tables[0].DefaultView;
            DropDownList_所属代理.DataTextField = ds.Tables["table_代理账号等级1"].Columns["代理名称"].ToString();
            DropDownList_所属代理.DataValueField = ds.Tables["table_代理账号等级1"].Columns["代理ID"].ToString();
            DropDownList_所属代理.DataBind();
            DropDownList_所属代理.Items.Insert(0, new ListItem("请选择", "未选择"));

            myconn.Close();
        }

        private void 更新内容()//更新出去
        {
            if (TextBox_代理名称.Text.Length > 0)
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
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_代理账号等级2 SET 代理名称=@代理名称 , 状态=@状态 ,所属代理L1=@所属代理L1  WHERE 代理ID=@代理ID ", con))
                {
                    cmd.Parameters.AddWithValue("@代理名称", TextBox_代理名称.Text);
                    cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@所属代理L1", DropDownList_所属代理.SelectedItem.Value);

                    cmd.Parameters.AddWithValue("@代理ID", 从URL传来值);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string 编号 = 从URL获取值();
                    Response.Redirect("代理列表L2设置.aspx?Bianhao=" + 编号 + "");
                    

                }
            }
        }
    }
}