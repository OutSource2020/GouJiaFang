using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using SqlSugar;
using Sugar.Enties;

namespace web1.WebsiteBackstage.L1.ManagementBankCard
{
    public partial class 管理收款银行卡卡更新 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("管理收款银行卡.aspx");
        }

        protected void Button_更新作业_Click(object sender, EventArgs e)
        {
            if (TextBox_收款银行名称.Text.Length > 0)
            {
                操作更新();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
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

        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 编号,收款银行卡名称,收款银行卡卡号,收款银行名称,收款银行卡余额,收款银行卡主姓名,收款银行卡主电话,显示标记,状态,手续卡,金额卡 FROM table_后台收款银行卡管理 WHERE 收款银行卡卡号=@收款银行卡卡号", con))
                {
                    cmd.Parameters.AddWithValue("@收款银行卡卡号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.lblCustomerID.Text = dr["编号"].ToString();
                            this.Label_收款银行卡名称.Text = dr["收款银行卡名称"].ToString();
                            this.Label_收款银行卡卡号.Text = dr["收款银行卡卡号"].ToString();
                            this.TextBox_收款银行名称.Text = dr["收款银行名称"].ToString();
                            this.Label_收款银行卡余额.Text = dr["收款银行卡余额"].ToString();
                            this.TextBox_收款银行卡主姓名.Text = dr["收款银行卡主姓名"].ToString();
                            this.TextBox_收款银行卡主电话.Text = dr["收款银行卡主电话"].ToString();
                            this.DropDownList_显示标记.SelectedValue = dr["显示标记"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();
                            this.DropDownList_手续卡.Text = dr["手续卡"].ToString();
                            this.DropDownList_金额卡.Text = dr["金额卡"].ToString();


                        }
                    }
                }
            }
        }

        private void 操作更新()
        {
            Button_更新作业.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                string 收款银行卡名称 = TextBox_收款银行名称.Text;

                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台收款银行卡管理 SET 收款银行名称=@收款银行名称 , 收款银行卡主姓名=@收款银行卡主姓名 , 收款银行卡主电话=@收款银行卡主电话 , 显示标记=@显示标记 , 状态=@状态,手续卡=@手续卡,金额卡=@金额卡 WHERE 收款银行卡卡号=@收款银行卡卡号 ", con))
                {
                    cmd.Parameters.AddWithValue("@收款银行名称", 收款银行卡名称);
                    cmd.Parameters.AddWithValue("@收款银行卡主姓名", TextBox_收款银行卡主姓名.Text);
                    cmd.Parameters.AddWithValue("@收款银行卡主电话", TextBox_收款银行卡主电话.Text);
                    cmd.Parameters.AddWithValue("@显示标记", DropDownList_显示标记.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@收款银行卡卡号", 从URL传来值);
                    cmd.Parameters.AddWithValue("@手续卡", DropDownList_手续卡.SelectedValue);
                    cmd.Parameters.AddWithValue("@金额卡", DropDownList_金额卡.SelectedValue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //this.SaveImage(filePath);
                    //Response.Redirect(Request.Url.AbsoluteUri, false);

                    con.Close();

                    Response.Redirect("./管理收款银行卡.aspx");

                }
            }
        }

        protected void Button_识别收款银行名称_Click(object sender, EventArgs e)
        {
            TextBox_收款银行名称.Text = ClassLibrary1.ClassBankInfo3a2.BankUtil.getNameOfBank(Label_收款银行卡卡号.Text);
        }

        protected void Button_删除_Click(object sender, EventArgs e)
        {
            using (SqlSugarClient sqlSugarClient = new DBClient().GetClient())
            {
                if (sqlSugarClient.Deleteable<table_后台收款银行卡管理>().With(SqlWith.RowLock).Where(it => it.编号 == lblCustomerID.Text).ExecuteCommand() > 0)
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "删除成功");
                    Response.Redirect("./管理收款银行卡.aspx");
                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "删除失败");
                }
            }
        }
    }
}