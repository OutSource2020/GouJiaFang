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
    public partial class 商户列表设置 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }

            this.GetCustomer();
            this.获得商户安全列信息();
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
                string 查哪些1 = "商户ID,商户名称,提款余额,手续费余额,手续费收款方式,状态,时间最后登入,时间注册";
                string 查哪些分割1 = ",";
                string 查哪些2 = "所属管理L2,所属代理L1,所属代理L2,登入错误累计,支付错误累计,提款最低单笔金额,提款最高单笔金额,手续费比率,单笔手续费";
                string 查哪些分割2 = ",";
                string 查哪些3第一阶梯 = "第一阶梯起,第一阶梯止,第一阶梯百分比";
                string 查哪些分割3第一阶梯 = ",";
                string 查哪些3第二阶梯 = "第二阶梯起,第二阶梯止,第二阶梯百分比";
                string 查哪些分割3第二阶梯 = ",";
                string 查哪些3第三阶梯 = "第三阶梯起,第三阶梯止,第三阶梯百分比";
                string 查哪些分割3第三阶梯 = ",";
                string 查哪些3第四阶梯 = "第四阶梯起,第四阶梯止,第四阶梯百分比";

                string 哪个表3x = 查哪些3第一阶梯 + 查哪些分割3第一阶梯 + 查哪些3第二阶梯 + 查哪些分割3第二阶梯 + 查哪些3第三阶梯 + 查哪些分割3第三阶梯 + 查哪些3第四阶梯;
                string 合查 = 查哪些1 + 查哪些分割1 + 查哪些2 + 查哪些分割2 + 哪个表3x;

                string 哪个表 = " table_商户账号 ";
                using (MySqlCommand cmd = new MySqlCommand("SELECT " + 合查 + " FROM "+ 哪个表 + " WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.Label_商户名称.Text = dr["商户名称"].ToString();
                            this.Label_提款余额.Text = dr["提款余额"].ToString();
                            this.Label_手续费余额.Text = dr["手续费余额"].ToString();
                            this.Label_手续费收款方式.Text = dr["手续费收款方式"].ToString();
                            this.Label_状态.Text = dr["状态"].ToString();
                            this.Label_时间最后登入.Text = dr["时间最后登入"].ToString();
                            this.Label_时间注册.Text = dr["时间注册"].ToString();
                            this.Label_所属管理L2.Text = dr["所属管理L2"].ToString();
                            this.Label_所属代理L1.Text = dr["所属代理L1"].ToString();
                            this.Label_所属代理L2.Text = dr["所属代理L2"].ToString();
                            this.Label_提款最低单笔金额.Text = dr["提款最低单笔金额"].ToString();
                            this.Label_提款最高单笔金额.Text = dr["提款最高单笔金额"].ToString();
                            this.Label_手续费比率.Text = dr["手续费比率"].ToString();
                            this.Label_单笔手续费.Text = dr["单笔手续费"].ToString();

                            //费率设置
                            this.Label_第一阶梯起.Text = dr["第一阶梯起"].ToString();
                            this.Label_第一阶梯止.Text = dr["第一阶梯止"].ToString();
                            this.Label_第一阶梯百分比.Text = dr["第一阶梯百分比"].ToString();

                            this.Label_第二阶梯起.Text = dr["第二阶梯起"].ToString();
                            this.Label_第二阶梯止.Text = dr["第二阶梯止"].ToString();
                            this.Label_第二阶梯百分比.Text = dr["第二阶梯百分比"].ToString();

                            this.Label_第三阶梯起.Text = dr["第三阶梯起"].ToString();
                            this.Label_第三阶梯止.Text = dr["第三阶梯止"].ToString();
                            this.Label_第三阶梯百分比.Text = dr["第三阶梯百分比"].ToString();

                            this.Label_第四阶梯起.Text = dr["第四阶梯起"].ToString();
                            this.Label_第四阶梯止.Text = dr["第四阶梯止"].ToString();
                            this.Label_第四阶梯百分比.Text = dr["第四阶梯百分比"].ToString();
                        }
                    }
                }
            }
        }

        private void 获得商户安全列信息()//获得数据-安全
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,绑定邮箱,绑定手机,登入错误累计,支付错误累计 FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_绑定邮箱.Text = dr["绑定邮箱"].ToString();
                            this.Label_绑定手机.Text = dr["绑定手机"].ToString();
                            this.Label_登入错误累计.Text = dr["登入错误累计"].ToString();
                            this.Label_支付错误累计.Text = dr["支付错误累计"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_设置商户信息_Click(object sender, EventArgs e)
        {
            string 编号= 从URL获取值();
            Response.Redirect("商户列表设置信息.aspx?Bianhao="+ 编号 + "");
        }

        protected void Button_设置商户费率_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("商户列表设置费率.aspx?Bianhao=" + 编号 + "");
        }

        protected void Button_设置商户安全_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("商户列表设置安全.aspx?Bianhao=" + 编号 + "");
        }

        protected void Button_充值商户手续费_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("商户列表充值手续费.aspx?Bianhao=" + 编号 + "");
        }

        protected void Button_设置商户账户安全_Click(object sender, EventArgs e)
        {
            string 编号 = 从URL获取值();
            Response.Redirect("商户列表设置安全.aspx?Bianhao=" + 编号 + "");
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户列表.aspx");
        }
    }
}