using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SqlSugar;
using Sugar.Enties;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{
    public partial class 商户列表充值手续费 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)/*如果省略这句 , 下面的更新操作将无法完成 , 因为获得的值是不变的*/
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                获得目标账户();
            }
        }


        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户列表.aspx");
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


        private void 获得目标账户()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,手续费余额,提款余额 FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_目标商户账号.Text = dr["商户ID"].ToString();
                            Label_目标商户提款余额.Text = dr["提款余额"].ToString();
                            Label_目标商户手续费余额.Text = dr["手续费余额"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_操作充值_Click(object sender, EventArgs e)
        {
            if (TextBox_充值金额.Text.Length > 0 && TextBox_管理员密码.Text.Length > 0)
            {
                操作充值();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }

        private void 操作充值()
        {
            Button_操作充值.Enabled = false;

            using (SqlSugarClient sqlSugarClient = new DBClient().GetClient())
            {
                var getByWhere = sqlSugarClient.Queryable<table_商户账号>().Where(it => it.商户ID == 从URL获取值()).ToList();
                table_商户账号 商户 = getByWhere[0];
                string Cookie_Password = ClassLibrary1.ClassAccount.cookie解密(HttpContext.Current.Request.Cookies["PPusernameBackstageL1"]["password"]);

                if (TextBox_管理员密码.Text == Cookie_Password)
                {
                    string 充值金额 = TextBox_充值金额.Text;
                    Random rd2 = new Random();
                    DateTime time = DateTime.Now;
                    string 备注 = "管理员扣除";
                    if (RadioButton_目标手续费.Checked)
                    {
                        table_商户明细手续费 fee = new table_商户明细手续费();
                        fee.订单号 = "CZSXF" + DateTime.Now.ToString("yyyyMMddHHmmss") + rd2.Next(1000, 9999);
                        fee.商户ID = Int32.Parse(商户.商户ID);
                        fee.类型 = "充值手续费";
                        fee.交易金额 = Double.Parse(充值金额);
                        fee.手续费收入 = Double.Parse(充值金额);
                        fee.交易前手续费余额 = 商户.手续费余额;
                        fee.交易后手续费余额 = 商户.手续费余额 + Double.Parse(充值金额);
                        fee.备注 = 备注;
                        fee.状态 = "成功";
                        fee.时间创建 = time;
                        sqlSugarClient.Insertable(fee).ExecuteCommand();

                        商户.手续费余额 += Convert.ToDouble(充值金额);
                        sqlSugarClient.Updateable(商户).UpdateColumns(it => new { it.手续费余额 }).ExecuteCommand();
                    }
                    else
                    {
                        table_商户明细余额 balance = new table_商户明细余额();
                        balance.订单号 = "MBON" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                        balance.商户ID = Convert.ToInt32(商户.商户ID);
                        balance.类型 = "充值余额";
                        balance.手续费 = "0";
                        balance.交易金额 = 充值金额;
                        balance.交易前账户余额 = Convert.ToString(商户.提款余额);
                        balance.交易后账户余额 = Convert.ToString(商户.提款余额 + Double.Parse(充值金额));
                        balance.状态 = "成功";
                        balance.时间创建 = time;
                        sqlSugarClient.Insertable(balance).ExecuteCommand();

                        商户.提款余额 += Convert.ToDouble(充值金额);
                        sqlSugarClient.Updateable(商户).UpdateColumns(it => new { it.提款余额 }).ExecuteCommand();
                    }
                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "管理员密码错误");
                    Button_操作充值.Enabled = true;
                    return;
                }
            }
            Response.Redirect("商户列表.aspx");
        }
    }
}