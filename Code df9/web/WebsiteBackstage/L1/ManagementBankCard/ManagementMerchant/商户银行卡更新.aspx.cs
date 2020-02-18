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
    public partial class 商户银行卡更新 : System.Web.UI.Page
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
            Response.Redirect("./商户银行卡.aspx");
        }


        protected void Button_操作更新传出_Click(object sender, EventArgs e)
        {
            this.操作更新();
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


        private void 操作更新()//更新出去
        {
            Button_操作更新传出.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户银行卡 SET 商户银行名称=@商户银行名称 ,状态=@状态 WHERE 编号=@编号 ", con))
                {
                    cmd.Parameters.AddWithValue("@编号", 从URL传来值);
                    cmd.Parameters.AddWithValue("@商户银行名称", TextBox_商户银行名称.Text);
                    cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("./商户银行卡.aspx");

                }
            }
        }
        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 编号,商户ID,商户银行卡卡号,商户银行名称,商户银行卡主姓名,状态 FROM table_商户银行卡 WHERE 编号=@编号", con))
                {
                    cmd.Parameters.AddWithValue("@编号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_编号.Text = dr["编号"].ToString();
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.Label_商户银行卡卡号.Text = dr["商户银行卡卡号"].ToString();
                            this.TextBox_商户银行名称.Text = dr["商户银行名称"].ToString();
                            this.Label_商户银行卡主姓名.Text = dr["商户银行卡主姓名"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_识别商户银行名称_Click(object sender, EventArgs e)
        {
            TextBox_商户银行名称.Text = ClassLibrary1.ClassBankInfo3a2.BankUtil.getNameOfBank(Label_商户银行卡卡号.Text);
        }
    }
}