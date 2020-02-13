using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteMerchant.商户首页
{
    public partial class 商户首页 : System.Web.UI.Page
    {
        public static string 时间字段 = "时间创建";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();
            }

            Label_商户ID.Text= ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["username"]);

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
            return 获得时间();
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
            查询商户充值(查看勾选了哪些() );
            查询商户提款(查看勾选了哪些() );
            今日提款详情(查看勾选了哪些() );
            提款数据统计(查看勾选了哪些() );
        }

        private void 查询商户充值(string 时间导入绑定)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();

                MySqlCommand cmd11 = new MySqlCommand("select COUNT(*) from table_商户明细充值 where 商户ID='" + Cookie_UserName + "' and 充值类型='充值提款余额' and 状态 = '成功' and " + 时间导入绑定 + " ", connC);
                object obj11 = cmd11.ExecuteScalar();
                if (obj11 != null)
                {
                    Label_记录充值_充值笔数今天.Text = obj11.ToString();
                }

                MySqlCommand cmd12 = new MySqlCommand("select COUNT(*) from table_商户明细充值 where 商户ID='" + Cookie_UserName + "' and 充值类型='充值提款余额' and 状态 = '成功' and " + 时间导入绑定 + " ", connC);
                object obj12 = cmd12.ExecuteScalar();
                if (obj12 != null)
                {
                    Label_记录充值_充值笔数今天成功.Text = obj12.ToString();
                }

                MySqlCommand cmd13 = new MySqlCommand("select sum(充值金额) from table_商户明细充值 where 商户ID='" + Cookie_UserName + "' and 充值类型='充值提款余额' and 状态 = '成功' and " + 时间导入绑定 + " ", connC);
                object obj13 = cmd13.ExecuteScalar();
                if (obj13 != null)
                {
                    Label_记录充值_充值金额成功.Text = obj13.ToString();
                }

                MySqlCommand cmd14 = new MySqlCommand("select COUNT(*) from table_商户明细充值 where 商户ID='" + Cookie_UserName + "' and 充值类型='充值提款余额' and 状态 = '成功' and " + 时间导入绑定 + " ", connC);
                object obj14 = cmd14.ExecuteScalar();
                if (obj14 != null)
                {
                    Label_记录充值_充值金额成功7天.Text = obj14.ToString();
                }

                connC.Close();
            }
        }

        private void 查询商户提款(string 时间导入绑定)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();

                MySqlCommand cmd21 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "' and 类型='提款' and 状态='成功' and " + 时间导入绑定 + " ", connC);
                object obj21 = cmd21.ExecuteScalar();
                if (obj21 != null)
                {
                    Label_记录提款_提款笔数今天.Text = obj21.ToString();
                }

                MySqlCommand cmd22 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "' and 类型='提款'  and 状态='成功' and " + 时间导入绑定 + " ", connC);
                object obj22 = cmd22.ExecuteScalar();
                if (obj22 != null)
                {
                    Label_记录提款_提款笔数今天成功.Text = obj22.ToString();
                }

                MySqlCommand cmd23 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "' and 类型='提款'  and 状态='成功' and " + 时间导入绑定 + " ", connC);
                object obj23 = cmd23.ExecuteScalar();
                if (obj23 != null)
                {
                    Label_记录提款_提款金额成功.Text = obj23.ToString();
                }

                MySqlCommand cmd24 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "' and 类型='提款'  and 状态='成功' and " + 时间导入绑定 + " ", connC);
                object obj24 = cmd24.ExecuteScalar();
                if (obj24 != null)
                {
                    Label_记录提款_提款金额成功7天.Text = obj24.ToString();
                }

                connC.Close();
            }
        }

        private void 今日提款详情(string 时间导入绑定)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();
                MySqlCommand cmd31 = new MySqlCommand("select 提款余额 from table_商户账号 where 商户ID='" + Cookie_UserName + "' ", connC);
                object obj31 = cmd31.ExecuteScalar();
                if (obj31 != null)
                {
                    Label_今日提款详情_账户余额.Text = obj31.ToString();
                }

                MySqlCommand cmd32 = new MySqlCommand("select 手续费余额 from table_商户账号 where 商户ID='" + Cookie_UserName + "' ", connC);
                object obj32 = cmd32.ExecuteScalar();
                if (obj32 != null)
                {
                    Label_今日提款详情_手续费余额.Text = obj32.ToString();
                }

                connC.Close();
            }
        }



        private void 提款数据统计(string 时间导入绑定 )
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();



                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "'and 类型='提款' and 状态='待处理' and " + 时间导入绑定 + " ", connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_交易笔数_待处理.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "'and 类型='提款' and 状态='成功' and " + 时间导入绑定 + " ", connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_交易笔数_成功.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "'and 类型='提款' and 状态='失败' and " + 时间导入绑定 + " ", connC);
                object obj3 = cmd3.ExecuteScalar();
                if (obj3 != null)
                {
                    Label_交易笔数_失败.Text = obj3.ToString();
                }

                MySqlCommand cmd4 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "'and 类型='提款'  and " + 时间导入绑定 + " ", connC);
                object obj4 = cmd4.ExecuteScalar();
                if (obj4 != null)
                {
                    Label_交易笔数_合计.Text = obj4.ToString();
                }




                MySqlCommand cmd21 = new MySqlCommand("select COUNT(交易金额) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "'and 类型='提款'  and 状态='待处理' and " + 时间导入绑定 + " ", connC);
                object obj21 = cmd21.ExecuteScalar();
                if (obj21 != null)
                {
                    Label_交易总金额_待处理.Text = obj21.ToString();
                }

                MySqlCommand cmd22 = new MySqlCommand("select COUNT(交易金额) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "'and 类型='提款' and 状态='成功' and " + 时间导入绑定 + " ", connC);
                object obj22 = cmd2.ExecuteScalar();
                if (obj22 != null)
                {
                    Label_交易总金额_成功.Text = obj22.ToString();
                }

                MySqlCommand cmd23 = new MySqlCommand("select COUNT(交易金额) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "' and 类型='提款' and 状态='失败' and " + 时间导入绑定 + " ", connC);
                object obj23 = cmd3.ExecuteScalar();
                if (obj23 != null)
                {
                    Label_交易总金额_失败.Text = obj23.ToString();
                }

                MySqlCommand cmd24 = new MySqlCommand("select COUNT(交易金额) from table_商户明细提款 where 商户ID='" + Cookie_UserName + "' and 类型='提款' and " + 时间导入绑定 + " ", connC);
                object obj24 = cmd24.ExecuteScalar();
                if (obj24 != null)
                {
                    Label_交易总金额_合计.Text = obj24.ToString();
                }




                MySqlCommand cmd31 = new MySqlCommand("select COUNT(*) from table_商户明细手续费 where 商户ID='" + Cookie_UserName + "'and 类型='提款'  and 状态='待处理' and " + 时间导入绑定 + " ", connC);
                object obj31 = cmd31.ExecuteScalar();
                if (obj31 != null)
                {
                    Label_手续费总金额_待处理.Text = obj31.ToString();
                }

                MySqlCommand cmd32 = new MySqlCommand("select COUNT(*) from table_商户明细手续费 where 商户ID='" + Cookie_UserName + "'and 类型='提款'  and 状态='成功' and " + 时间导入绑定 + " ", connC);
                object obj32 = cmd32.ExecuteScalar();
                if (obj32 != null)
                {
                    Label_手续费总金额_成功.Text = obj32.ToString();
                }

                MySqlCommand cmd33 = new MySqlCommand("select COUNT(*) from table_商户明细手续费 where 商户ID='" + Cookie_UserName + "'and 类型='提款'  and 状态='失败' and " + 时间导入绑定 + " ", connC);
                object obj33 = cmd33.ExecuteScalar();
                if (obj33 != null)
                {
                    Label_手续费总金额_失败.Text = obj33.ToString();
                }

                MySqlCommand cmd34 = new MySqlCommand("select COUNT(*) from table_商户明细手续费 where 商户ID='" + Cookie_UserName + "' and 类型='提款' and " + 时间导入绑定 + " ", connC);
                object obj34 = cmd34.ExecuteScalar();
                if (obj34 != null)
                {
                    Label_手续费总金额_合计.Text = obj34.ToString();
                }


                connC.Close();
            }
        }


    }

}