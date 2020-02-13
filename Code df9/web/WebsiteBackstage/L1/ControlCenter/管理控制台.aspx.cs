using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using System.Media;

namespace web1.WebsiteBackstage.L1.ControlCenter
{
    public partial class 管理控制台 : System.Web.UI.Page
    {
        public static string 时间字段 = "时间创建";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();

                Response.Write("<embed height='0' width='0' src='http://adminx.pppayment.com/MessageAudio/6809.wav' />");

            }

            查询商户充值余额(查看勾选了哪些());
            查询商户充值手续费(查看勾选了哪些());
            查询商户提款(查看勾选了哪些());

            获取计数(" " + 查看勾选了哪些() + " ");
            
            判断响起提示音();

            //页面自动刷新();
            Label_刷新时间.Text = "载入时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (CheckBox_刷新自动勾选.Checked == true)
            {
                //Timer1.Interval = 1000;
                //Timer1.Enabled = true;//開始計時

                Timer_自动刷新.Enabled = false;//先關閉計時
                int 获得刷新秒数 = Convert.ToInt32(TextBox_刷新秒数.Text + "000");
                Timer_自动刷新.Interval = 获得刷新秒数;
                Timer_自动刷新.Enabled = true;//開始計時
            }
            if (CheckBox_刷新自动勾选.Checked == false)
            {
                Timer_自动刷新.Enabled = false;//先關閉計時
            }

        }

        protected void CheckBox_刷新自动勾选_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_刷新自动勾选.Checked == true)
            {
                //Timer1.Interval = 1000;
                //Timer1.Enabled = true;//開始計時

                Timer_自动刷新.Enabled = false;//先關閉計時
                int 获得刷新秒数 = Convert.ToInt32(TextBox_刷新秒数.Text + "000");
                Timer_自动刷新.Interval = 获得刷新秒数;
                Timer_自动刷新.Enabled = true;//開始計時
            }
            if (CheckBox_刷新自动勾选.Checked == false)
            {
                Timer_自动刷新.Enabled = false;//先關閉計時
            }
        }

        protected void TimerTick(object sender, EventArgs e)
        {
            Label_刷新时间.Text = "载入时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            获取计数(" " + 查看勾选了哪些() + " ");

            判断响起提示音();
        }

        protected void Button_查找_Click(object sender, EventArgs e)
        {
            获取计数(" " + 查看勾选了哪些() + " ");
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
            获取计数(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间昨天_CheckedChanged(object sender, EventArgs e)
        {
            获取计数(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间7天_CheckedChanged(object sender, EventArgs e)
        {
            获取计数(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间本周_CheckedChanged(object sender, EventArgs e)
        {
            获取计数(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间本月_CheckedChanged(object sender, EventArgs e)
        {
            获取计数(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间设置_CheckedChanged(object sender, EventArgs e)
        {
            获取计数(" " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        private void 查询商户提款(string 这里填时间)
        {
            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();

                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 状态='成功' and " + 这里填时间, connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_商户提款交易笔数.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 状态='成功' and " + 这里填时间, connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_商户提款提款金额.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select sum(手续费) from table_商户明细提款 where 状态='成功' and " + 这里填时间, connC);
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
            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();
                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and " + 这里填时间, connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_交易手续费笔数.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select sum(充值金额) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and " + 这里填时间, connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_充值手续费金额.Text = obj2.ToString();
                }

                //MySqlCommand cmd3 = new MySqlCommand("select sum(产生手续费) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and " + 这里填时间, connC);
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
            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();
                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and " + 这里填时间, connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_交易余额笔数.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select sum(充值金额) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and " + 这里填时间, connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_充值余额金额.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select sum(产生手续费) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and " + 这里填时间, connC);
                object obj3 = cmd3.ExecuteScalar();
                if (obj3 != null)
                {
                    Label_充值余额产生手续费.Text = obj3.ToString();
                }

                connC.Close();
            }
        }


        private void 获取计数(string 时间导入绑定)
        {

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();

                MySqlCommand cmd1 = new MySqlCommand("select count(订单号) from table_商户明细充值 where 状态='待处理' and " + 时间导入绑定, connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_充值订单.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select count(订单号) from table_商户明细提款 where 状态='待处理' and " + 时间导入绑定, connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_提款订单.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select count(编号) from table_商户银行卡 where 状态='待审核' and " + 时间导入绑定, connC);
                object obj3 = cmd3.ExecuteScalar();
                if (obj3 != null)
                {
                    Label_银行卡待审核.Text = obj3.ToString();
                }

                connC.Close();
            }

        }

        public void 判断响起提示音()
        {
            int 充值订单 = int.Parse(Label_充值订单.Text);
            int 提款订单 = int.Parse(Label_提款订单.Text);
            int 银行卡待审核 = int.Parse(Label_银行卡待审核.Text);

            //方法1
            //SoundPlayer s = new SoundPlayer();
            //s.SoundLocation = Server.ToString("http://127.0.0.1/pumpit.mp3");
            //s.PlaySync();

            //方法2
            //Response.Write("<embed src = 'http://127.0.0.1/pumpit.mp3' width = '0' height = '0' id = 'music' autostart = 'true'></embed>");

            //方法3
            //如果要播mp3要先在project 裡加reference(wav檔不用)，方法是在你的程式碼所在的project(專案)名稱上按右鍵然後add>>reference 接著勾這個圖裡的東西也就是COM >> Windows Media Player
            //WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            //wplayer.URL = @"http://adminx.pppayment.com/MessageAudio/6809.wav";
            //wplayer.controls.play();

            //方法4
            //Response.Write("<embed src = 'http://adminx.pppayment.com/MessageAudio/6809.wav' width = '0' height = '0' id = 'music' autostart = 'true'></embed>");


            //如果新短消息数量 大于0
            if (充值订单 >= 1)
            {
                if (CheckBox_提示音开关.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "xalert()", "<script>xalert()</script>", false);
                }
            }

            //如果新短消息数量 大于0
            if (提款订单 >= 1)
            {
                if (CheckBox_提示音开关.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "xalert()", "<script>xalert()</script>", false);
                }

            }

            //如果新短消息数量 大于0
            if (银行卡待审核 >= 1)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "xalert()", "<script>xalert()</script>", false);
            }

        }


    }
}
