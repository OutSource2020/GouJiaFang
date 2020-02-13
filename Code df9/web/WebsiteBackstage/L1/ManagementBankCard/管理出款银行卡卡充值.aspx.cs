using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementBankCard
{
    public partial class 管理出款银行卡卡充值 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }
            this.GetCustomer();
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("管理出款银行卡.aspx");
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
                using (MySqlCommand cmd = new MySqlCommand("SELECT 出款银行卡名称,出款银行卡卡号 FROM table_后台出款银行卡管理 WHERE 出款银行卡卡号=@出款银行卡卡号", con))
                {
                    cmd.Parameters.AddWithValue("@出款银行卡卡号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            Label_目标目标名称.Text = dr["出款银行卡名称"].ToString();
                            Label_目标银行卡卡号.Text= dr["出款银行卡卡号"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button_确认充值_Click(object sender, EventArgs e)
        {
            if (TextBox_目标银行卡充值金额.Text.Length > 0 && TextBox_备注.Text.Length > 0)
            {
                操作充值();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }

        protected void 操作充值()
        {
            Button_确认充值.Enabled = false;

            string 从URL传来值 = 从URL获取值();

            //流水插入
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 出款银行卡名称,出款银行卡卡号,出款银行卡余额 FROM table_后台出款银行卡管理 WHERE 出款银行卡卡号=@出款银行卡卡号", con))
                {
                    cmd.Parameters.AddWithValue("@出款银行卡卡号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            string 出款银行卡名称 = dr["出款银行卡名称"].ToString();
                            string 银行卡卡号 = dr["出款银行卡卡号"].ToString();
                            double 银行卡余额 = double.Parse(dr["出款银行卡余额"].ToString());
                            double 银行卡余额充值后 = double.Parse(dr["出款银行卡余额"].ToString())+ double.Parse(TextBox_目标银行卡充值金额.Text);

                            using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                //插入 出款银行卡流水
                                string 生成编号 = "BOPBCR" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                string 订单状态 = "完成";
                                string 类型 = "出款银行卡充值";
                                string 目标银行卡充值金额 = TextBox_目标银行卡充值金额.Text;
                                string 备注 = TextBox_备注.Text;

                                string 指定值 = "订单号,类型,收入,余额,出款银行卡卡号,出款银行卡名称,备注,状态,时间创建";
                                string 发出值 = "'"+ 生成编号 + "','"+ 类型 + "','"+ 目标银行卡充值金额 + "','"+ 银行卡余额充值后 + "','"+ 银行卡卡号 + "','"+ 出款银行卡名称 + "','"+ 备注 + "','"+ 订单状态 + "','"+RegisterTime+"' ";

                                string str = "insert into table_后台出款银行卡流水(" + 指定值 + ") values(" + 发出值 + ")";

                                scon.Open();
                                MySqlCommand command = new MySqlCommand();
                                command.Connection = scon;
                                command.CommandText = str;
                                int obj = command.ExecuteNonQuery();

                                scon.Close();
                            }



                        }
                    }
                }
            }

            //更新余额
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                string 目标银行卡充值金额 = TextBox_目标银行卡充值金额.Text;

                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_后台出款银行卡管理 SET 出款银行卡余额=出款银行卡余额+"+ 目标银行卡充值金额 + " WHERE 出款银行卡卡号=@出款银行卡卡号 ", con))
                {
                    cmd.Parameters.AddWithValue("@出款银行卡卡号", 从URL传来值);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            Response.Redirect("./管理出款银行卡.aspx");
        }
    }
}