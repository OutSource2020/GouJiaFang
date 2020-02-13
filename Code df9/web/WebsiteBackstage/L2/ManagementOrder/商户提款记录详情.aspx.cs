using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;


namespace web1.WebsiteBackstage.L2.ManagementOrder
{
    public partial class 商户提款记录详情 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //进入时候默认清零
                this.Label_订单号.Text = "";
                this.Label_商户ID.Text = "";
                this.Label_商户名称.Text = "";
                this.Label_类型.Text = "";
                this.Label_出款银行卡名称.Text = "";
                this.Label_出款银行卡卡号.Text = "";
                this.Label_交易方卡号.Text = "";
                this.Label_交易方姓名.Text = "";
                this.Label_交易方银行.Text = "";
                this.Label_交易金额.Text = "";
                this.Label_手续费.Text = "";
                this.Label_创建方式.Text = "";
                this.Label_备注商户写.Text = "";
                this.Label_备注管理写.Text = "";
                this.Label_状态.Text = "";
                this.Label_时间创建.Text = "";
                this.Label_时间完成.Text = "";
                this.Label_时间修改.Text = "";

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
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
                    //LabeL2.Text = "是符合要求字符";

                    //获得传值
                    string URL传来值 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(Request.QueryString["Bianhao"]);
                    return URL传来值;
                }
                else
                {
                    //LabeL2.Text = "是no符合要求字符";

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
                using (MySqlCommand cmd = new MySqlCommand("SELECT 订单号,商户ID,商户名称,类型,出款银行卡名称,出款银行卡卡号,交易方卡号,交易方姓名,交易方银行,交易金额,手续费,创建方式,备注商户写,备注管理写,状态,时间创建,时间完成,时间修改 FROM table_商户明细提款 WHERE 订单号=@订单号", con))
                {
                    cmd.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_订单号.Text = dr["订单号"].ToString();
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.Label_商户名称.Text = dr["商户名称"].ToString();
                            this.Label_类型.Text = dr["类型"].ToString();
                            this.Label_出款银行卡名称.Text = dr["出款银行卡名称"].ToString();
                            this.Label_出款银行卡卡号.Text = dr["出款银行卡卡号"].ToString();
                            this.Label_交易方卡号.Text = dr["交易方卡号"].ToString();
                            this.Label_交易方姓名.Text = dr["交易方姓名"].ToString();
                            this.Label_交易方银行.Text = dr["交易方银行"].ToString();
                            this.Label_交易金额.Text = dr["交易金额"].ToString();
                            this.Label_手续费.Text = dr["手续费"].ToString();
                            this.Label_创建方式.Text = dr["创建方式"].ToString();
                            this.Label_备注商户写.Text = dr["备注商户写"].ToString();
                            this.Label_备注管理写.Text = dr["备注管理写"].ToString();
                            this.Label_状态.Text = dr["状态"].ToString();
                            this.Label_时间创建.Text = dr["时间创建"].ToString();
                            this.Label_时间完成.Text = dr["时间完成"].ToString();
                            this.Label_时间修改.Text = dr["时间修改"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户提款记录.aspx");
        }
    }
}