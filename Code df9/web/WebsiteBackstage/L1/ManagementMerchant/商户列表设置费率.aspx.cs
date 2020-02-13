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
    public partial class 商户列表设置费率 : System.Web.UI.Page
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
            string 编号 = 从URL获取值();
            Response.Redirect("商户列表设置.aspx?Bianhao=" + 编号 + "");
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
                string 一些信息 = "商户ID,充值最低手续费,充值最低余额,提款最低单笔金额,提款最高单笔金额,手续费比率,单笔手续费";

                string 查哪些3第一阶梯 = "第一阶梯起,第一阶梯止,第一阶梯百分比";
                string 查哪些分割3第一阶梯 = ",";
                string 查哪些3第二阶梯 = "第二阶梯起,第二阶梯止,第二阶梯百分比";
                string 查哪些分割3第二阶梯 = ",";
                string 查哪些3第三阶梯 = "第三阶梯起,第三阶梯止,第三阶梯百分比";
                string 查哪些分割3第三阶梯 = ",";
                string 查哪些3第四阶梯 = "第四阶梯起,第四阶梯止,第四阶梯百分比";

                string 哪个表3x = 一些信息 + "," + 查哪些3第一阶梯 + 查哪些分割3第一阶梯 + 查哪些3第二阶梯 + 查哪些分割3第二阶梯 + 查哪些3第三阶梯 + 查哪些分割3第三阶梯 + 查哪些3第四阶梯;

                using (MySqlCommand cmd = new MySqlCommand("SELECT " + 哪个表3x + " FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            //费率设置类
                            this.TextBox_充值最低手续费.Text = dr["充值最低手续费"].ToString();
                            this.TextBox_充值最低余额.Text = dr["充值最低余额"].ToString();
                            this.TextBox_提款最低单笔金额.Text = dr["提款最低单笔金额"].ToString();
                            this.TextBox_提款最高单笔金额.Text = dr["提款最高单笔金额"].ToString();
                            
                            this.TextBox_手续费比率.Text = dr["手续费比率"].ToString();
                            this.TextBox_单笔手续费.Text = dr["单笔手续费"].ToString();

                            this.TextBox_第一阶梯起.Text = dr["第一阶梯起"].ToString();
                            this.TextBox_第一阶梯止.Text = dr["第一阶梯止"].ToString();
                            this.TextBox_第一阶梯百分比.Text = dr["第一阶梯百分比"].ToString();
                            this.TextBox_第二阶梯起.Text = dr["第二阶梯起"].ToString();
                            this.TextBox_第二阶梯止.Text = dr["第二阶梯止"].ToString();
                            this.TextBox_第二阶梯百分比.Text = dr["第二阶梯百分比"].ToString();
                            this.TextBox_第三阶梯起.Text = dr["第三阶梯起"].ToString();
                            this.TextBox_第三阶梯止.Text = dr["第三阶梯止"].ToString();
                            this.TextBox_第三梯百分比.Text = dr["第三阶梯百分比"].ToString();
                            this.TextBox_第四阶梯起.Text = dr["第四阶梯起"].ToString();
                            this.TextBox_第四阶梯止.Text = dr["第四阶梯止"].ToString();
                            this.TextBox_第四阶梯百分比.Text = dr["第四阶梯百分比"].ToString();
                        }
                    }
                }
            }
        }

        private void 更新内容()//更新出去
        {
            if (TextBox_提款最低单笔金额.Text.Length > 0 && 
                TextBox_提款最高单笔金额.Text.Length > 0 && 
                TextBox_手续费比率.Text.Length > 0 &&
                TextBox_单笔手续费.Text.Length > 0 &&
                TextBox_第一阶梯起.Text.Length > 0 &&
                TextBox_第一阶梯止.Text.Length > 0 &&
                TextBox_第一阶梯百分比.Text.Length > 0 &&
                TextBox_第二阶梯起.Text.Length > 0 &&
                TextBox_第二阶梯止.Text.Length > 0 &&
                TextBox_第二阶梯百分比.Text.Length > 0 &&
                TextBox_第三阶梯起.Text.Length > 0 &&
                TextBox_第三阶梯止.Text.Length > 0 &&
                TextBox_第三梯百分比.Text.Length > 0 &&
                TextBox_第四阶梯起.Text.Length > 0 &&
                TextBox_第四阶梯止.Text.Length > 0 &&
                TextBox_第四阶梯百分比.Text.Length > 0
                )
            {
                Button_发出更新.Enabled = false;
                操作更新();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }


        private void 操作更新()
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                string 一些信息 = "充值最低手续费='" + TextBox_充值最低手续费.Text + "',充值最低余额='" + TextBox_充值最低余额.Text + "',提款最低单笔金额='" + TextBox_提款最低单笔金额.Text + "',提款最高单笔金额='" + TextBox_提款最高单笔金额.Text + "',手续费比率='" + TextBox_手续费比率.Text + "',单笔手续费='" + TextBox_单笔手续费.Text + "' ";


                string 查哪些3第一阶梯 = "第一阶梯起='" + TextBox_第一阶梯起.Text + "',第一阶梯止='" + TextBox_第一阶梯止.Text + "',第一阶梯百分比='" + TextBox_第一阶梯百分比.Text + "' ";
                string 查哪些分割3第一阶梯 = ",";
                string 查哪些3第二阶梯 = "第二阶梯起='" + TextBox_第二阶梯起.Text + "',第二阶梯止='" + TextBox_第二阶梯止.Text + "',第二阶梯百分比='" + TextBox_第二阶梯百分比.Text + "' ";
                string 查哪些分割3第二阶梯 = ",";
                string 查哪些3第三阶梯 = "第三阶梯起='" + TextBox_第三阶梯起.Text + "',第三阶梯止='" + TextBox_第三阶梯止.Text + "',第三阶梯百分比='" + TextBox_第三梯百分比.Text + "' ";
                string 查哪些分割3第三阶梯 = ",";
                string 查哪些3第四阶梯 = "第四阶梯起='" + TextBox_第四阶梯起.Text + "',第四阶梯止='" + TextBox_第四阶梯止.Text + "',第四阶梯百分比='" + TextBox_第四阶梯百分比.Text + "' ";

                string 哪个表3x = 查哪些3第一阶梯 + 查哪些分割3第一阶梯 + 查哪些3第二阶梯 + 查哪些分割3第二阶梯 + 查哪些3第三阶梯 + 查哪些分割3第三阶梯 + 查哪些3第四阶梯;



                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户账号 SET " + 一些信息 + "," + 哪个表3x + " WHERE 商户ID=@商户ID ", con))
                {
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