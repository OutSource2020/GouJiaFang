using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteAgent.L1.Overview
{
    public partial class 代理首页L1 : System.Web.UI.Page
    {
        public static string 时间字段 = "时间创建";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.检查代理L1端cookie();
            }

            显示();

        }


        public string 获得时间()
        {
            string 条件1 = "";

            if (RadioButton_时间今天.Checked)
            {
                TextBox_开始时间.Enabled = false;
                TextBox_结束时间.Enabled = false;

                string 时间1 = DateTime.Now.ToString("yyyy-MM-dd");
                string 时间2 = DateTime.Now.ToString("yyyy-MM-dd");

                TextBox_开始时间.Text = 时间1;
                TextBox_结束时间.Text = 时间2;

                条件1 = "  (" + 时间字段 + " between '" + 时间1 + "'  and '" + 时间2 + "" + ClassLibrary1.ClassTimeZD.时分秒更多 + "')";
            }
            if (RadioButton_时间昨天.Checked)
            {
                TextBox_开始时间.Enabled = false;
                TextBox_结束时间.Enabled = false;

                string 时间1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                string 时间2 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

                TextBox_开始时间.Text = 时间1;
                TextBox_结束时间.Text = 时间2;

                条件1 = "  (" + 时间字段 + " between '" + 时间1 + "'  and '" + 时间2 + "" + ClassLibrary1.ClassTimeZD.时分秒更多 + "')";

            }
            if (RadioButton_时间7天.Checked)
            {
                TextBox_开始时间.Enabled = false;
                TextBox_结束时间.Enabled = false;

                string 时间1 = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                string 时间2 = DateTime.Now.ToString("yyyy-MM-dd");

                TextBox_开始时间.Text = 时间1;
                TextBox_结束时间.Text = 时间2;

                条件1 = "  (" + 时间字段 + " between '" + 时间1 + "'  and '" + 时间2 + "" + ClassLibrary1.ClassTimeZD.时分秒更多 + "')";

            }
            if (RadioButton_时间本周.Checked)
            {
                TextBox_开始时间.Enabled = false;
                TextBox_结束时间.Enabled = false;

                string 时间1 = DateTime.Now.AddDays(Convert.ToDouble((1 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToString("yyyy-MM-dd");
                string 时间2 = DateTime.Now.AddDays(Convert.ToDouble((7 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToString("yyyy-MM-dd");

                TextBox_开始时间.Text = 时间1;
                TextBox_结束时间.Text = 时间2;

                条件1 = "  (" + 时间字段 + " between '" + 时间1 + "'  and '" + 时间2 + "" + ClassLibrary1.ClassTimeZD.时分秒更多 + "')";

            }
            if (RadioButton_时间本月.Checked)
            {
                TextBox_开始时间.Enabled = false;
                TextBox_结束时间.Enabled = false;

                string 时间1 = DateTime.Now.ToString("yyyy-") + DateTime.Now.ToString("MM-") + "01"; //本月第一天
                //DateTime date2 = DateTime.Now.AddMonths(-1).Date.AddDays(1 - DateTime.Now.Day);
                //Response.Write(date2.ToString());
                string 时间2 = DateTime.Now.ToString("yyyy-") + DateTime.Now.AddMonths(+1).ToString("MM-") + "01";//下个月第一天

                TextBox_开始时间.Text = 时间1;
                TextBox_结束时间.Text = 时间2;

                条件1 = "  (" + 时间字段 + " between '" + 时间1 + "'  and '" + 时间2 + "" + ClassLibrary1.ClassTimeZD.时分秒更多 + "')";
            }
            if (RadioButton_时间设置.Checked)
            {
                TextBox_开始时间.Enabled = true;
                TextBox_结束时间.Enabled = true;

                //string 时间1 = DateTime.Now.ToString("yyyy-MM-dd");
                //string 时间2 = DateTime.Now.ToString("yyyy-MM-dd");

                //TextBox_开始时间.Text = 时间1;
                //TextBox_结束时间.Text = 时间2;

                条件1 = "  (" + 时间字段 + " between '" + TextBox_开始时间.Text + "'  and '" + TextBox_结束时间.Text + "" + ClassLibrary1.ClassTimeZD.时分秒更多 + "')";
            }


            return 条件1;

        }


        public string 查看勾选了哪些()
        {
            return  获得时间();
        }

        //====================================================================================================
        protected void RadioButton_时间今天_CheckedChanged(object sender, EventArgs e)
        {
            显示();
        }

        protected void RadioButton_时间昨天_CheckedChanged(object sender, EventArgs e)
        {
            显示();
        }

        protected void RadioButton_时间7天_CheckedChanged(object sender, EventArgs e)
        {
            显示();
        }

        protected void RadioButton_时间本周_CheckedChanged(object sender, EventArgs e)
        {
            显示();
        }

        protected void RadioButton_时间本月_CheckedChanged(object sender, EventArgs e)
        {
            显示();
        }

        protected void RadioButton_时间设置_CheckedChanged(object sender, EventArgs e)
        {
            显示();
        }

        //====================================================================================================

        private void 显示()
        {
            查询商户充值余额(查看勾选了哪些());
            查询商户充值手续费(查看勾选了哪些());
            查询商户提款(查看勾选了哪些());
        }

        private void 查询商户提款(string 这里填时间)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查代理L1端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();

                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_商户提款交易笔数.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_商户提款提款金额.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select sum(手续费) from table_商户明细提款 where 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj3 = cmd3.ExecuteScalar();
                if (obj3 != null)
                {
                    Label_商户提款手续费金额.Text = obj3.ToString();
                }

                connC.Close();
            }
        }

        private void 查询商户充值手续费(string 这里填时间)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查代理L1端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();
                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_交易手续费笔数.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select sum(充值金额) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_充值手续费金额.Text = obj2.ToString();
                }

                //MySqlCommand cmd3 = new MySqlCommand("select sum(产生手续费) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                //object obj3 = cmd3.ExecuteScalar();
                //if (obj3 != null)
                //{
                //    Label_商户充值手续费金额.Text = obj3.ToString();
                //}

                connC.Close();
            }
        }

        private void 查询商户充值余额(string 这里填时间)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查代理L1端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();
                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_交易余额笔数.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select sum(充值金额) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_充值余额金额.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select sum(产生手续费) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and  商户ID in (select 商户ID from table_商户账号 where 所属代理L1='" + Cookie_UserName + "')  and " + 这里填时间, connC);
                object obj3 = cmd3.ExecuteScalar();
                if (obj3 != null)
                {
                    Label_充值余额产生手续费.Text = obj3.ToString();
                }

                connC.Close();
            }
        }
    }
}