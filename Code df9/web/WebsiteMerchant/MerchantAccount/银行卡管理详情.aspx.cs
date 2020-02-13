using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteMerchant.商户账号
{
    public partial class 银行卡管理详情 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();
                this.GetCustomer();

            }

            检查是否已经隐藏();
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("银行卡管理.aspx");
        }


        protected void Button_操作更新传出_Click(object sender, EventArgs e)
        {
            this.操作隐藏();
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


        private void 操作隐藏()//更新出去
        {
            Button_操作更新传出.Enabled = false;

            string 从URL传来值 = 从URL获取值();
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户银行卡 SET 商户银行卡卡标记=@商户银行卡卡标记 , 状态=@状态 WHERE 编号=@编号 ", con))
                {
                    cmd.Parameters.AddWithValue("@编号", 从URL传来值);
                    cmd.Parameters.AddWithValue("@状态", "停用");
                    cmd.Parameters.AddWithValue("@商户银行卡卡标记", "隐藏");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("./银行卡管理.aspx");

                }
            }
        }

        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 编号,商户ID,商户银行卡卡号,商户银行名称,商户银行卡主姓名,商户银行卡卡标记 FROM table_商户银行卡 WHERE 商户ID=@商户ID and 编号=@编号", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    cmd.Parameters.AddWithValue("@编号", 从URL传来值);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            if (dr["商户银行卡卡标记"].ToString() == "显示")
                            {
                                this.Label_编号.Text = dr["编号"].ToString();
                                this.Label_商户ID.Text = dr["商户ID"].ToString();
                                this.Label_银行卡卡号.Text = dr["商户银行卡卡号"].ToString();
                                this.Label_银行名称.Text = dr["商户银行名称"].ToString();
                                this.Label_银行卡主姓名.Text = dr["商户银行卡主姓名"].ToString();
                            }
                            else
                            {
                                Response.Redirect("./银行卡管理.aspx");
                            }


                        }
                    }
                }
            }
        }

        private void 检查是否已经隐藏()//获得数据
        {
            string 从URL传来值 = 从URL获取值();
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 编号,商户ID,商户银行卡卡号,商户银行名称,商户银行卡主姓名,商户银行卡卡标记 FROM table_商户银行卡 WHERE 商户ID=@商户ID and 编号=@编号", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    cmd.Parameters.AddWithValue("@编号", 从URL传来值);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            if (dr["商户银行卡卡标记"].ToString() == "显示")
                            {

                            }
                            else
                            {
                                Response.Redirect("./银行卡管理.aspx");
                            }


                        }
                    }
                }
            }
        }


    }
}