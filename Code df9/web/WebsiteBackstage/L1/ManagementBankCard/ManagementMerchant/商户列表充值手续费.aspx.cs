using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

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
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_目标商户账号.Text = dr["商户ID"].ToString();

                        }
                    }
                }
            }
        }

        protected void Button_操作充值_Click(object sender, EventArgs e)
        {
            if (TextBox_充值手续费金额.Text.Length > 0 && TextBox_充值手续费备注.Text.Length > 0)
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

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                string 查哪些1 = "商户ID,手续费余额";

                using (MySqlCommand cmd = new MySqlCommand("SELECT " + 查哪些1 + " FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL获取值());
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            string 获得商户ID = dr["商户ID"].ToString();
                            double 转文本框内数值 = System.Convert.ToDouble(TextBox_充值手续费金额.Text);
                            double 获得交易前手续费余额 = double.Parse(dr["手续费余额"].ToString());
                            double 获得交易后手续费余额 = Convert.ToDouble(获得交易前手续费余额) + Convert.ToDouble(转文本框内数值);
                            //注意这个地方好像不能输入小数的充值

                            //向明细手续费表插入充值记录
                            using (MySqlConnection scon2 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                Random rd2 = new Random();
                                string 生成编号2 = "CZSXF" + DateTime.Now.ToString("yyyyMMddHHmmss") + rd2.Next(1000, 9999);
                                string RegisterTime2;
                                RegisterTime2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                string 类型 = "充值手续费";

                                string 充值手续费金额 = TextBox_充值手续费金额.Text;
                                string 充值手续费备注 = TextBox_充值手续费备注.Text;

                                string 要哪些 = "订单号,商户ID,类型,交易金额,手续费收入,交易前手续费余额,交易后手续费余额,备注,时间创建";
                                string 插哪些 = "'" + 生成编号2 + "','" + 获得商户ID + "','" + 类型 + "','" + 转文本框内数值 + "','" + 充值手续费金额 + "','" + 获得交易前手续费余额 + "','" + 获得交易后手续费余额 + "','" + 充值手续费备注 + "','" + RegisterTime2 + "'";

                                string str = "insert into table_商户明细手续费(" + 要哪些 + ") values(" + 插哪些 + ")";

                                scon2.Open();
                                MySqlCommand command = new MySqlCommand();
                                command.Connection = scon2;
                                command.CommandText = str;
                                int obj = command.ExecuteNonQuery();

                                scon2.Close();
                            }



                        }
                    }
                }
            }


            string 手续费余额 = TextBox_充值手续费金额.Text;

            MySqlConnection conn2 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            conn2.Open();
            MySqlCommand scmd2 = new MySqlCommand("update table_商户账号 set 手续费余额 = 手续费余额+'" + 手续费余额 + "'  where 商户ID = '" + 从URL获取值() + "' ", conn2);
            scmd2.ExecuteNonQuery();
            scmd2.Dispose();
            conn2.Close();


            Response.Redirect("商户列表.aspx");
        }
    }
}