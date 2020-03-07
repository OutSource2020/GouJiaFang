using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Diagnostics;
using SqlSugar;
using Sugar.Enties;

namespace web1.WebsiteBackstage.L1.ManagementOrder
{
    public partial class 商户提款记录 : System.Web.UI.Page
    {
        public static string 时间字段 = "时间创建";

        private string shixi()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Label_页面载入时间.Text = "刷新时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //RadioButton_时间今天.Checked = true;
            //RadioButton_状态全部.Checked = true;
            //RadioButton_类型全部.Checked = true;
            //RadioButton_创建方式全部.Checked = true;

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (IsPostBack)
            {
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                GetData();

                PopulateCheckBoxArray();
            }

            下拉获取银行卡();

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkSelect = gvr.FindControl("CheckBox1") as CheckBox;

                if (chkSelect != null)
                {
                    string productID = GridView1.DataKeys[gvr.RowIndex]["订单号"].ToString();

                    if (chkSelect.Checked && !this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Add(productID);
                    }
                    else if (!chkSelect.Checked && this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Remove(productID);
                    }
                }
            }

            BindGrid("where " + 查看勾选了哪些() + " ");

            TextBox_开始时间.Enabled = false;
            TextBox_结束时间.Enabled = false;


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

            勾选循环全部检查隐藏勾选();
            //勾选换颜色();
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
            //lblTime.Text = "Last Refreshed: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //GridView1.DataBind();

            Label1.Text = "载入时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            GridView1.DataBind();
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkSelect = gvr.FindControl("CheckBox1") as CheckBox;

                if (chkSelect != null)
                {
                    string productID = GridView1.DataKeys[gvr.RowIndex]["订单号"].ToString();

                    if (chkSelect.Checked && !this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Add(productID);
                    }
                    else if (!chkSelect.Checked && this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Remove(productID);
                    }
                }
            }


            勾选循环全部检查隐藏勾选();
            //勾选换颜色();
        }



        protected void Button_查找_Click(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void Button_检查重复_Click(object sender, EventArgs e)
        {
            ClassLibrary1.ClassMessage.HinXi(Page, " " + 循环判断是否重复() + " ");
        }

        private void 勾选换颜色()
        {
            CheckBox cb;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                if (cb.Checked == true)
                {
                    GridView1.Rows[i].Attributes.Add("style", "background-color:aqua");
                }

            }
        }

        private void 勾选循环全部检查隐藏勾选()
        {
            CheckBox cb;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                if (cb.Enabled == false)
                {
                    cb.Checked = false;
                }

            }
        }

        private void 勾选循环全部勾上()
        {
            CheckBox cb;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                if (cb.Enabled != false)
                {
                    cb.Checked = true;
                }

            }
        }

        private void 勾选循环全部取消()
        {
            CheckBox cb;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                if (cb.Enabled != false)
                {
                    cb.Checked = false;
                }

            }
        }

        private bool 判断是否是x分钟内相同卡号(string 分钟数, string 传来卡号, string 传来金额)
        {
            //连接数据库
            ClassLibrary1.ClassDataControl.OpenConnection1();
            string 卡号 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(传来卡号);
            //创建SQL语句
            string selStr = "select 交易方卡号 from table_商户明细提款 where 交易方卡号='" + 卡号 + "' and 交易金额='" + 传来金额 + "' and 时间创建 >= NOW() - INTERVAL " + 分钟数 + " MINUTE";
            //创建数据适配器
            MySqlDataAdapter da = new MySqlDataAdapter(selStr, ClassLibrary1.ClassDataControl.con1);
            //创建满足条件的数据集
            DataSet ds = new DataSet();
            da.Fill(ds);
            //如果数据集不为空 , 则用户名已经存在
            if (ds.Tables[0].Rows.Count >= 2)
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "已經存在");
                return true;
            }
            else
            {
                return false;
            }

        }

        private string 循环判断是否重复()
        {
            string 返回 = "检查重复: 20分钟内 ";

            int count = 0;
            SetData();
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            count = arr.Count;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (arr.Contains(GridView1.DataKeys[i].Value))
                {
                    //DeleteRecord(GridView1.DataKeys[i].Value.ToString());

                    string 获得金额 = GridView1.Rows[i].Cells[5].Text;
                    string 获得卡号 = GridView1.Rows[i].Cells[6].Text;

                    if (判断是否是x分钟内相同卡号("30", 获得卡号, 获得金额) == true)
                    {
                        string 本次内容 = 返回 + "|| 重复卡号: <" + 获得卡号 + "> 重复金额:<" + 获得金额 + "> || ";


                        返回 = 本次内容;
                    }

                    arr.Remove(GridView1.DataKeys[i].Value);
                }
            }
            ViewState["SelectedRecords"] = arr;
            hfCount.Value = "0";
            GridView1.AllowPaging = true;
            BindGrid("");
            ShowMessage(count);



            return 返回;
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

        public string 获得订单状态()
        {
            string 中间加和2 = "";
            string 条件2 = "";

            if (RadioButton_状态全部.Checked)
            {
                条件2 = null;
            }
            if (RadioButton_状态待处理.Checked)
            {
                条件2 = "  状态='待处理' ";
            }
            if (RadioButton_状态成功.Checked)
            {
                条件2 = "  状态='成功' ";
            }
            if (RadioButton_状态失败.Checked)
            {
                条件2 = "  状态='失败' ";
            }

            if (条件2 == null)
            {

            }
            else
            {
                中间加和2 = " and ";
            }

            return 条件2 + 中间加和2;
        }

        public string 获得类型()
        {
            string 中间加和3 = "";
            string 条件3 = "";

            if (RadioButton_类型全部.Checked)
            {
                条件3 = null;
            }
            if (RadioButton_类型出账.Checked)
            {
                条件3 = "  类型='出账' ";
            }
            if (RadioButton_类型订单提款冲正.Checked)
            {
                条件3 = "  类型='订单提款冲正' ";
            }


            if (条件3 == null)
            {

            }
            else
            {
                中间加和3 = " and ";
            }

            return 条件3 + 中间加和3;
        }

        public string 获得创建方式()
        {
            string 中间加和4 = "";
            string 条件4 = "";


            if (RadioButton_创建方式全部.Checked)
            {
                条件4 = null;
            }
            if (RadioButton_创建方式手动.Checked)
            {
                条件4 = "  创建方式='手动' ";
            }
            if (RadioButton_创建方式API.Checked)
            {
                条件4 = "  创建方式='接口' ";
            }
            if (RadioButton_创建方式文档导入.Checked)
            {
                条件4 = "  创建方式='文档导入' ";
            }
            if (RadioButton_创建方式文本导入.Checked)
            {
                条件4 = "  创建方式='文本导入' ";
            }



            if (条件4 == null)
            {

            }
            else
            {
                中间加和4 = " and ";
            }

            return 条件4 + 中间加和4;
        }

        public string 获得筛选关键字()
        {
            string 中间加和5 = "";
            string 条件5 = "";


            string redkey = DropDownList1.SelectedItem.Value;
            if (redkey != "未选择")
            {
                条件5 = " " + DropDownList1.SelectedItem.Value + "='" + TextBox_筛选关键字.Text + "' ";
                中间加和5 = " and ";
            }

            return 条件5 + 中间加和5;
        }



        public string 获得筛选端金额()
        {
            string 中间加和5 = "";
            string 条件5 = "";


            if (DropDownList_端金额.SelectedItem.Value == "未选择")
            {

            }
            else
            {
                if (TextBox_筛选端金额.Text != "")
                {
                    string 下拉表转 = DropDownList_端金额.SelectedItem.Text;
                    string 下拉表内容 = null;

                    if (下拉表转.Contains("金额小于"))
                    {
                        下拉表内容 = " 交易金额<='" + TextBox_筛选端金额.Text + "'";
                    }
                    if (下拉表转.Contains("金额等于"))
                    {
                        下拉表内容 = " 交易金额='" + TextBox_筛选端金额.Text + "'";
                    }
                    if (下拉表转.Contains("金额大于"))
                    {
                        下拉表内容 = " 交易金额>='" + TextBox_筛选端金额.Text + "'";
                    }


                    条件5 = 下拉表内容;
                    中间加和5 = " and ";
                }
                else
                {

                }

            }

            return 条件5 + 中间加和5;

        }

        public string 获得区间金额()
        {
            string 区间条件参数 = "";
            string 区间金额起始 = "";
            string 区间金额结束 = "";
            string 金额字段 = "交易金额";

            if (IsNumberic(TextBox_区间金额起始.Text) == true)
            {
                区间金额起始 = TextBox_区间金额起始.Text;
            }
            else
            {
                TextBox_区间金额起始.Text = "0";
                区间金额起始 = "0";
            }

            if (IsNumberic(TextBox_区间金额结束.Text) == true)
            {
                区间金额结束 = TextBox_区间金额结束.Text;
            }
            else
            {
                TextBox_区间金额结束.Text = "100";
                区间金额结束 = "100";
            }

            区间条件参数 = "  (" + 金额字段 + " between '" + 区间金额起始 + "'  and '" + 区间金额结束 + "') and";


            return 区间条件参数;
        }


        public string 查看勾选了哪些()
        {
            return 获得订单状态() + 获得类型() + 获得创建方式() + 获得筛选关键字() + 获得筛选端金额() + 获得区间金额() + 获得时间();
        }


        //====================================================================================================
        protected void RadioButton_时间今天_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间昨天_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间7天_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间本周_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间本月_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间设置_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        //====================================================================================================

        protected void RadioButton_状态全部_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态待处理_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态处理中_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态关闭_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态待确认_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态失败_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态成功_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void Button_筛选仅按状态_Click(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        //====================================================================================================

        protected void Button_筛选仅按关键字_Click(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型全部_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型出账_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型订单提款冲正_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        //====================================================================================================

        protected void RadioButton_创建方式全部_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_创建方式手动_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_创建方式API_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        private DataTable GetDataCL(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        private void BindGridForBatchOperator()
        {
            string strQuery = "select 订单号,商户ID,出款银行卡名称,出款银行卡卡号,交易方姓名,交易方卡号,交易方银行,交易金额,时间创建,时间完成,创建方式,状态,操作员,后台处理批次ID组,商户API订单号 FROM table_商户明细提款 " + " order by 后台处理批次ID组 desc  LIMIT " + 分页() + " ";
            DataTable dt = new DataTable();
            String strConnString = ClassLibrary1.ClassDataControl.conStr1;
            MySqlConnection con = new MySqlConnection(strConnString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(strQuery);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        private void BindGrid(string 时间导入绑定)
        {
            string strQuery = "select 订单号,商户ID,出款银行卡名称,出款银行卡卡号,交易方姓名,交易方卡号,交易方银行,交易金额,时间创建,时间完成,创建方式,状态,操作员,后台处理批次ID组,商户API订单号 FROM table_商户明细提款 " + 时间导入绑定 + " order by id desc  LIMIT " + 分页() + " ";
            DataTable dt = new DataTable();
            String strConnString = ClassLibrary1.ClassDataControl.conStr1;
            MySqlConnection con = new MySqlConnection(strConnString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(strQuery);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }


            //select* FROM table_商户明细提款 where 时间创建 between '2019-1-1 00:00:00'  and '2019-2-1 00:00:00';

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();

                MySqlCommand cmd1 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 状态='成功' and " + 查看勾选了哪些() + " ", connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_交易笔数_成功.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 状态='失败' and " + 查看勾选了哪些() + " ", connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_交易笔数_失败.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 状态='待处理' and " + 查看勾选了哪些() + " ", connC);
                object obj3 = cmd3.ExecuteScalar();
                if (obj3 != null)
                {
                    Label_交易笔数_待处理.Text = obj3.ToString();
                }

                MySqlCommand cmd5 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where 状态='订单提款冲正' and " + 查看勾选了哪些() + " ", connC);
                object obj5 = cmd5.ExecuteScalar();
                if (obj5 != null)
                {
                    Label_交易笔数_冲正.Text = obj5.ToString();
                }

                MySqlCommand cmd6 = new MySqlCommand("select COUNT(*) from table_商户明细提款 where " + 查看勾选了哪些() + " ", connC);
                object obj6 = cmd6.ExecuteScalar();
                if (obj6 != null)
                {
                    Label_交易笔数_合计.Text = obj6.ToString();
                }



                MySqlCommand cmd21 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 状态='成功' and " + 查看勾选了哪些() + " ", connC);
                object obj21 = cmd21.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_交易总金额_成功.Text = obj21.ToString();
                }

                MySqlCommand cmd22 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 状态='失败' and " + 查看勾选了哪些() + " ", connC);
                object obj22 = cmd22.ExecuteScalar();
                if (obj22 != null)
                {
                    Label_交易总金额_失败.Text = obj22.ToString();
                }

                MySqlCommand cmd23 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 状态='待处理' and " + 查看勾选了哪些() + " ", connC);
                object obj23 = cmd23.ExecuteScalar();
                if (obj23 != null)
                {
                    Label_交易总金额_待处理.Text = obj23.ToString();
                }

                MySqlCommand cmd25 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where 状态='订单提款冲正' and " + 查看勾选了哪些() + " ", connC);
                object obj25 = cmd25.ExecuteScalar();
                if (obj25 != null)
                {
                    Label_交易总金额_冲正.Text = obj25.ToString();
                }

                MySqlCommand cmd26 = new MySqlCommand("select sum(交易金额) from table_商户明细提款 where " + 查看勾选了哪些() + " ", connC);
                object obj26 = cmd26.ExecuteScalar();
                if (obj26 != null)
                {
                    Label_交易总金额_合计.Text = obj26.ToString();
                }




                MySqlCommand cmd31 = new MySqlCommand("select sum(手续费) from table_商户明细提款 where 状态='成功' and " + 查看勾选了哪些() + " ", connC);
                object obj31 = cmd31.ExecuteScalar();
                if (obj31 != null)
                {
                    Label_手续费总金额_成功.Text = obj31.ToString();
                }

                MySqlCommand cmd32 = new MySqlCommand("select sum(手续费) from table_商户明细提款 where 状态='失败' and " + 查看勾选了哪些() + " ", connC);
                object obj32 = cmd32.ExecuteScalar();
                if (obj32 != null)
                {
                    Label_手续费总金额_失败.Text = obj32.ToString();
                }

                MySqlCommand cmd33 = new MySqlCommand("select sum(手续费) from table_商户明细提款 where 状态='待处理' and " + 查看勾选了哪些() + " ", connC);
                object obj33 = cmd33.ExecuteScalar();
                if (obj33 != null)
                {
                    Label_手续费总金额_待处理.Text = obj33.ToString();
                }

                MySqlCommand cmd35 = new MySqlCommand("select sum(手续费) from table_商户明细提款 where 状态='订单提款冲正' and " + 查看勾选了哪些() + " ", connC);
                object obj35 = cmd35.ExecuteScalar();
                if (obj35 != null)
                {
                    Label_手续费总金额_冲正.Text = obj35.ToString();
                }

                MySqlCommand cmd36 = new MySqlCommand("select sum(手续费) from table_商户明细提款 where " + 查看勾选了哪些() + " ", connC);
                object obj36 = cmd36.ExecuteScalar();
                if (obj36 != null)
                {
                    Label_手续费总金额_合计.Text = obj36.ToString();
                }

                connC.Close();
            }
        }

        private void 下拉获取银行卡()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 出款银行卡名称,出款银行卡卡号 from table_后台出款银行卡管理 where 状态='启用' ";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_后台出款银行卡管理");
            myconn.Close();
            // DropDownList_选择银行卡.Items.Clear();
            DropDownList_选择银行卡.DataSource = ds.Tables[0].DefaultView;
            DropDownList_选择银行卡.DataTextField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡名称"].ToString();
            DropDownList_选择银行卡.DataValueField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡卡号"].ToString();
            DropDownList_选择银行卡.DataBind();

            myconn.Close();
        }


        //protected void OnPaging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GridView1.DataBind();
        //    SetData();
        //}

        private void GetData()
        {
            ArrayList arr;
            if (ViewState["SelectedRecords"] != null)
                arr = (ArrayList)ViewState["SelectedRecords"];
            else
                arr = new ArrayList();
            CheckBox chkAll = (CheckBox)GridView1.HeaderRow
                                .Cells[0].FindControl("chkAll");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(GridView1.DataKeys[i].Value))
                    {
                        arr.Add(GridView1.DataKeys[i].Value);
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i]
                                       .Cells[0].FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(GridView1.DataKeys[i].Value))
                        {
                            arr.Add(GridView1.DataKeys[i].Value);
                        }
                    }
                    else
                    {
                        if (arr.Contains(GridView1.DataKeys[i].Value))
                        {
                            arr.Remove(GridView1.DataKeys[i].Value);
                        }
                    }
                }
            }
            ViewState["SelectedRecords"] = arr;
        }

        private void SetData()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)GridView1.HeaderRow.Cells[0].FindControl("chkAll");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(GridView1.DataKeys[i].Value);
                    if (!chk.Checked)
                        chkAll.Checked = false;
                    else
                        currentCount++;
                }
            }
            hfCount.Value = (arr.Count - currentCount).ToString();
        }

        private long GetTimeStamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            return (long)System.Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DBClient db = new DBClient();
            var dbCilent= db.GetClient();
            int calCount = 0; 
            //判定 DropDownList_选择银行卡 是否空
            if (String.IsNullOrEmpty(DropDownList_选择银行卡.SelectedValue))
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "出款银行卡还未设置或者未启用");
            }
            else
            {




                long OperatorId = GetTimeStamp();

                int count = 0;
                SetData();
                GridView1.AllowPaging = false;
                GridView1.DataBind();
                ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
                count = arr.Count;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (arr.Contains(GridView1.DataKeys[i].Value))
                    {
                        //DeleteRecord(GridView1.DataKeys[i].Value.ToString());



                        //判定 DropDownList_选择银行卡 是否空
                        if (String.IsNullOrEmpty(DropDownList_选择银行卡.SelectedValue))
                        {
                            ClassLibrary1.ClassMessage.HinXi(Page, "出款银行卡还未设置或者未启用");
                        }
                        else
                        {
                            //ClassLibrary1.ClassMessage.HinXi(Page, "出款银行卡");


                            if (String.IsNullOrEmpty(TextBox_备注.Text))
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "必须填写备注");
                            }
                            else
                            {

                                //先检查状态是否待处理
                                using (MySqlConnection conCHA = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand cmdCHA = new MySqlCommand("SELECT 状态 FROM table_商户明细提款 WHERE 订单号=@订单号", conCHA))
                                    {
                                        cmdCHA.Parameters.AddWithValue("@订单号", GridView1.Rows[i].Cells[1].Text);
                                        using (MySqlDataAdapter daCHA = new MySqlDataAdapter(cmdCHA))
                                        {
                                            DataTable imagesCHA = new DataTable();
                                            daCHA.Fill(imagesCHA);
                                            foreach (DataRow drCHA in imagesCHA.Rows)
                                            {
                                                if (drCHA["状态"].ToString() == "待处理")
                                                {
                                                    //开始更新




                                                    string 设置订单的状态 = DropDownList_下拉框1.SelectedItem.Value;
                                                    if (设置订单的状态 == "成功")
                                                    {
                                                        string 从URL传来值 = GridView1.Rows[i].Cells[1].Text;

                                                        //查询出款银行卡余额是否足够支付
                                                        using (MySqlConnection con查询出款银行卡 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                        {
                                                            using (MySqlCommand cmd查询出款银行卡 = new MySqlCommand("SELECT 出款银行卡卡号,出款银行卡余额,出款银行卡每日限额,出款银行卡最小交易金额 FROM table_后台出款银行卡管理 WHERE 出款银行卡卡号=@出款银行卡卡号", con查询出款银行卡))
                                                            {
                                                                cmd查询出款银行卡.Parameters.AddWithValue("@出款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
                                                                using (MySqlDataAdapter da查询出款银行卡 = new MySqlDataAdapter(cmd查询出款银行卡))
                                                                {
                                                                    DataTable images查询出款银行卡 = new DataTable();
                                                                    da查询出款银行卡.Fill(images查询出款银行卡);
                                                                    foreach (DataRow dr查询出款银行卡 in images查询出款银行卡.Rows)
                                                                    {
                                                                        //1.扣除出款银行卡内的余额
                                                                        //2.出款银行卡的流水记录
                                                                        //3.设置本单状态成功


                                                                        double 出款银行卡余额 = double.Parse(dr查询出款银行卡["出款银行卡余额"].ToString());
                                                                        double 银行卡每日限额 = double.Parse(dr查询出款银行卡["出款银行卡每日限额"].ToString());
                                                                        double 银行卡最小交易限额 = double.Parse(dr查询出款银行卡["出款银行卡最小交易金额"].ToString());

                                                                        double 本单交易金额 = double.Parse(GridView1.Rows[i].Cells[5].Text);

                                                                        if (银行卡最小交易限额 <= 本单交易金额)
                                                                        {
                                                                            if ((出款银行卡余额 - 本单交易金额) >= 0)
                                                                            {
                                                                                //1.设置订单状态 成功
                                                                                string 时间完成1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                        var 时间完成debug = Convert.ToDateTime(时间完成1);

                                        //隔离级别序列化
                                       // dbCilent.Ado.ExecuteCommand("set session transaction isolation level serializable;");

                                        // 重写的这垃圾代码（接盘的算你幸运，算我受不了帮你写一下）
                                        var result = dbCilent.Ado.UseTran(() =>
                                        {
                                        
                                          //table_商户明细提款   更新状态
                                          dbCilent.Updateable<table_商户明细提款>().SetColumns(it => 
                                          new table_商户明细提款()
                                          {
                                            备注管理写 = TextBox_备注.Text, 
                                            状态 = DropDownList_下拉框1.SelectedItem.Value,
                                            时间完成= Convert.ToDateTime( 时间完成1),
                                            出款银行卡名称= DropDownList_选择银行卡.SelectedItem.Text,
                                            出款银行卡卡号= DropDownList_选择银行卡.SelectedItem.Value,
                                            操作员= ClassLibrary1.ClassAccount.检查管理L1端cookie2(),
                                           // 订单号=从URL传来值,
                                            后台处理批次ID组=OperatorId
                                          })
                                          .Where(it => it.订单号 == 从URL传来值).ExecuteCommand();

                                          //查询 
                                          var data = dbCilent.Queryable<table_商户明细提款>()
                                          .Where(it => it.订单号== 从URL传来值)
                                          .Select(f => new { f.订单号, f.商户ID, f.交易金额, f.状态 })
                                          .First();
                                          //扣余额
                                          dbCilent.Ado.ExecuteCommand( "UPDATE table_后台出款银行卡管理 SET 出款银行卡余额 = 出款银行卡余额 - " + data.交易金额.ToString() + " WHERE 出款银行卡卡号 ='" + DropDownList_选择银行卡.SelectedItem.Value + "';");

                                          string 生成编号1 = "BOPBCP" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                          string 时间创建1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                          
                                          string 类型 = "订单提款出款";
                                          string 状态 = "成功";
                                          double 余额1 = Convert.ToDouble(出款银行卡余额) - Convert.ToDouble(data.交易金额);
                                          table_后台出款银行卡流水 outCardHistory = new table_后台出款银行卡流水
                                          {
                                            订单号 = 生成编号1,
                                            商户ID =Convert.ToInt32( data.商户ID),
                                            余额 = 余额1,
                                            类型 = 类型,
                                            状态 = 状态,
                                            时间创建 = Convert.ToDateTime(时间创建1),
                                            时间交易 = Convert.ToDateTime(时间创建1),
                                            支出 = Convert.ToDouble(data.交易金额),
                                            出款银行卡卡号 = DropDownList_选择银行卡.SelectedItem.Value,
                                            出款银行卡名称 = DropDownList_选择银行卡.SelectedItem.Text
                                          };
                                          // 插入出款流水
                                          var t2=  dbCilent.Insertable(outCardHistory).ExecuteCommand();
                                        });
                                        

                                      if(result.IsSuccess){
                                          calCount++;


                                        }
                                          
                                          
                            

                                                                                //Response.Redirect("商户提款记录.aspx");
                                                                            }
                                                                            else
                                                                            {
                                                                                ClassLibrary1.ClassMessage.HinXi(Page, "出款银行卡余额不足");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            ClassLibrary1.ClassMessage.HinXi(Page, "本单金额小于出款银行卡限制的 最小交易金额");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }


                                                    }
                                                    if (设置订单的状态 == "失败")
                                                    {
                                                        string 从URL传来值 = GridView1.Rows[i].Cells[1].Text;

                                                        //查询订单信息
                                                        using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                        {
                                                            using (MySqlCommand cmd11 = new MySqlCommand("SELECT 订单号,商户ID,交易方姓名,交易方卡号,交易方银行,交易金额,手续费,状态 FROM table_商户明细提款 WHERE 订单号=@订单号", con11))
                                                            {
                                                                cmd11.Parameters.AddWithValue("@订单号", 从URL传来值);
                                                                using (MySqlDataAdapter da11 = new MySqlDataAdapter(cmd11))
                                                                {
                                                                    DataTable images11 = new DataTable();
                                                                    da11.Fill(images11);
                                                                    foreach (DataRow dr11 in images11.Rows)
                                                                    {
                                                                        string 商户ID = dr11["商户ID"].ToString();
                                                                        string 交易金额 = dr11["交易金额"].ToString();
                                                                        string 手续费 = dr11["手续费"].ToString();

                                                                        //查询账户信息
                                                                        using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                                        {
                                                                            using (MySqlCommand cmd12 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额,手续费比率,单笔手续费,提款最低单笔金额,提款最高单笔金额 FROM table_商户账号 WHERE 商户ID=@商户ID", con12))
                                                                            {
                                                                                cmd12.Parameters.AddWithValue("@商户ID", 商户ID);
                                                                                using (MySqlDataAdapter da12 = new MySqlDataAdapter(cmd12))
                                                                                {
                                                                                    DataTable images12 = new DataTable();
                                                                                    da12.Fill(images12);
                                                                                    foreach (DataRow dr12 in images12.Rows)
                                                                                    {
                                                                                        double 提款余额 = double.Parse(dr12["提款余额"].ToString());
                                                                                        double 手续费余额 = double.Parse(dr12["手续费余额"].ToString());
                                                                                        double 手续费比率 = double.Parse(dr12["手续费比率"].ToString());
                                                                                        double 单笔手续费 = double.Parse(dr12["单笔手续费"].ToString());
                                                                                        double 提款最低单笔金额 = double.Parse(dr12["提款最低单笔金额"].ToString());
                                                                                        double 提款最高单笔金额 = double.Parse(dr12["提款最高单笔金额"].ToString());

                                                                                        //直接设置设置订单状态
                                                                                        string 时间完成 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                                                        Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 300));

                                                                                        //1.退回余额和手续费
                                                                                        //2.插 商户余额交易明细
                                                                                        //3.插 商户手续费交易明细
                                                                                        //4.向提款记录表更新信息-失败

                                                                                        //1 退回 账户余额和手续费
                                                                                        using (MySqlConnection conmy2 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                                                        {

                                                                                            using (MySqlCommand cmdmy2 = new MySqlCommand("UPDATE table_商户账号 SET 提款余额=提款余额+" + 交易金额 + " , 手续费余额= 手续费余额+" + 手续费 + " where 商户ID=@商户ID ", conmy2))
                                                                                            {
                                                                                                cmdmy2.Parameters.AddWithValue("@商户ID", 商户ID);

                                                                                                conmy2.Open();
                                                                                                cmdmy2.ExecuteNonQuery();
                                                                                                conmy2.Close();

                                                                                            }
                                                                                        }


                                                                                        //2 商户提款手续费交易明细
                                                                                        using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                                                        {
                                                                                            string 提款手续费_生成编号标头 = "MHFOR";
                                                                                            string 提款手续费_订单号 = 提款手续费_生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                                                            string 提款手续费_商户ID = 商户ID;
                                                                                            double 提款手续费_手续费 = double.Parse(手续费);
                                                                                            double 提款手续费_交易金额 = double.Parse(交易金额);
                                                                                            double 提款手续费_交易前手续费余额 = 手续费余额;
                                                                                            double 提款手续费_交易后手续费余额 = 手续费余额 + System.Convert.ToDouble(手续费);
                                                                                            string 提款手续费_类型 = "订单出款退款";
                                                                                            string 提款手续费_时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                                                            string 手续费交易明细哪个表 = " table_商户明细手续费 ";
                                                                                            string 手续费交易明细写哪些 = "订单号,商户ID,手续费支出,交易金额,交易前手续费余额,交易后手续费余额,类型,时间创建";
                                                                                            string 手续费交易明细写这些 = "@订单号,@商户ID,@手续费支出,@交易金额,@交易前手续费余额,@交易后手续费余额,@类型,@时间创建 ";

                                                                                            string str = "insert into " + 手续费交易明细哪个表 + "(" + 手续费交易明细写哪些 + ") values(" + 手续费交易明细写这些 + ")";

                                                                                            scon.Open();
                                                                                            MySqlCommand command = new MySqlCommand();

                                                                                            command.Parameters.AddWithValue("@订单号", 提款手续费_订单号);
                                                                                            command.Parameters.AddWithValue("@商户ID", 提款手续费_商户ID);
                                                                                            command.Parameters.AddWithValue("@手续费支出", 提款手续费_手续费);
                                                                                            command.Parameters.AddWithValue("@交易金额", 提款手续费_交易金额);
                                                                                            command.Parameters.AddWithValue("@交易前手续费余额", 提款手续费_交易前手续费余额);
                                                                                            command.Parameters.AddWithValue("@交易后手续费余额", 提款手续费_交易后手续费余额);
                                                                                            command.Parameters.AddWithValue("@类型", 提款手续费_类型);
                                                                                            command.Parameters.AddWithValue("@时间创建", 提款手续费_时间创建);

                                                                                            command.Connection = scon;
                                                                                            command.CommandText = str;
                                                                                            int obj = command.ExecuteNonQuery();

                                                                                            scon.Close();
                                                                                        }


                                                                                        //3 商户账户余额交易明细
                                                                                        using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                                                        {
                                                                                            string 账户余额_订单号 = "MOPOR" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                                                            string 账户余额_类型 = "订单出款退款";
                                                                                            double 账户余额_手续费 = 手续费余额;
                                                                                            double 账户余额_交易金额 = double.Parse(dr12["提款余额"].ToString());
                                                                                            double 账户余额_交易前余额 = double.Parse(dr12["提款余额"].ToString());
                                                                                            double 账户余额_交易后余额 = double.Parse(dr12["提款余额"].ToString()) + double.Parse(dr11["交易金额"].ToString());
                                                                                            string 账户余额_时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                                                            string 余额交易明细哪个表 = " table_商户明细余额 ";
                                                                                            string 余额交易明细写哪些 = "订单号,商户ID,类型,交易金额,交易前账户余额,交易后账户余额,状态,时间创建";
                                                                                            string 余额交易明细写这些 = "@订单号,@商户ID,@类型,@交易金额,@交易前账户余额,@交易后账户余额,@状态,@时间创建 ";

                                                                                            string str = "insert into " + 余额交易明细哪个表 + "(" + 余额交易明细写哪些 + ") values(" + 余额交易明细写这些 + ")";

                                                                                            scon.Open();
                                                                                            MySqlCommand command = new MySqlCommand();

                                                                                            command.Parameters.AddWithValue("@订单号", 账户余额_订单号);
                                                                                            command.Parameters.AddWithValue("@商户ID", 商户ID);
                                                                                            command.Parameters.AddWithValue("@类型", 账户余额_类型);
                                                                                            command.Parameters.AddWithValue("@交易金额", 账户余额_交易金额);
                                                                                            command.Parameters.AddWithValue("@交易前账户余额", 账户余额_交易前余额);
                                                                                            command.Parameters.AddWithValue("@交易后账户余额", 账户余额_交易后余额);
                                                                                            command.Parameters.AddWithValue("@状态", "");
                                                                                            command.Parameters.AddWithValue("@时间创建", 账户余额_时间创建);

                                                                                            command.Connection = scon;
                                                                                            command.CommandText = str;
                                                                                            int obj = command.ExecuteNonQuery();

                                                                                            scon.Close();
                                                                                        }

                                                                                        //4 向提款记录表更新信息
                                                                                        using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                                                        {
                                                                                            using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户明细提款 SET 备注管理写=@备注管理写 , 状态=@状态,时间完成=@时间完成 , 操作员=@操作员  WHERE 订单号=@订单号 ", con))
                                                                                            {
                                                                                                cmd.Parameters.AddWithValue("@备注管理写", TextBox_备注.Text);
                                                                                                cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                                                                                                cmd.Parameters.AddWithValue("@时间完成", 时间完成);
                                                                                                cmd.Parameters.AddWithValue("@操作员", ClassLibrary1.ClassAccount.检查管理L1端cookie2());
                                                                                                cmd.Parameters.AddWithValue("@订单号", 从URL传来值);

                                                                                                con.Open();
                                                                                                cmd.ExecuteNonQuery();
                                                                                                con.Close();
                                                                                            }
                                                                                        }


                                                                                        //Response.Redirect("商户提款记录.aspx");

                                                                                    }
                                                                                }
                                                                            }
                                                                        }


                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }



                                                }
                                                else
                                                {
                                                    ClassLibrary1.ClassMessage.HinXi(Page, "订单已经处理过");
                                                    //Response.Redirect("代理列表L1.aspx");
                                                }

                                            }
                                        }
                                    }
                                }






                            }
                        }













                        arr.Remove(GridView1.DataKeys[i].Value);
                    }
                }
                ViewState["SelectedRecords"] = arr;
                hfCount.Value = "0";
                GridView1.AllowPaging = true;
                //BindGrid("");
                BindGridForBatchOperator();
                ShowMessage(calCount);


                //Response.Redirect("商户提款记录.aspx");
                //========== 执行完批量操作后消除勾选 开始 ==========
                CheckBox cb;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                    cb.Checked = false;

                }
                //========== 执行完批量操作后消除勾选 结束 ==========


                勾选循环全部取消();
            }


        }







        private void DeleteRecord(string CustomerID)
        {
            //string constr = ConfigurationManager
            //            .ConnectionStrings["conString"].ConnectionString;
            //string query = "delete from TestCustomers where CustomerID=@CustomerID";
            //MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            //MySqlCommand cmd = new MySqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();


            //MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            //MySqlCommand com = new MySqlCommand("UPDATE table_商户明细提款 SET 状态= '" + DropDownList_状态.SelectedItem.Value + "', 出款银行卡卡号= '" + DropDownList_银行卡.SelectedItem.Value + "', 备注管理写='" + TextBox_备注.Text + "'  where 订单号=@ID", con);
            //com.Parameters.AddWithValue("@ID", CustomerID);
            //con.Open();
            //com.ExecuteNonQuery();
            //con.Close();




        }

        private void ShowMessage(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("alert(' 批量操作有 ");
            sb.Append(count.ToString());
            sb.Append(" 笔 成功');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(),
                            "script", sb.ToString());
        }






        /// <summary>
        /// 翻页操作
        /// 在GridView当前索引正在更改时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            #region 方法1 废弃

            //txtName.Text = "翻页操作";
            //int index = e.NewPageIndex+1;
            //DataSet ds = SplitDataSet(index);
            //DataTable dt = ds.Tables[0];
            ////DataGrid1.DataSource = dt;
            ////DataGrid1.DataBind();
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            //ClearDisplay();
            //GridView1.AllowCustomPaging = false;         

            #endregion

            #region 方法2

            //GridView1.PageIndex = e.NewPageIndex;
            //GetCustomers();

            #endregion

            #region 方法3

            #region 前台

            //<PagerTemplate>
            //    当前第:
            //     <%--//((GridView)Container.NamingContainer)就是为了得到当前的控件--%>
            //    <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
            //    页/共:
            //    <%--//得到分页页面的总数--%>
            //    <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
            //    页
            //    <%--//如果该分页是首分页，那么该连接就不会显示了.同时对应了自带识别的命令参数CommandArgument--%>
            //    <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
            //        Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
            //    <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
            //        CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
            //    <%--//如果该分页是尾页，那么该连接就不会显示了--%>
            //    <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
            //        Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
            //    <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
            //        Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
            //    转到第
            //    <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />页
            //    <%--//这里将CommandArgument即使点击该按钮e.newIndex 值为3 --%>
            //    <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
            //        CommandName="Page" Text="GO" />
            //</PagerTemplate>

            #endregion

            // 得到该控件
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (e.NewPageIndex == -3)
            {
                //点击了Go按钮
                TextBox txtNewPageIndex = null;

                //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
                GridViewRow pagerRow = theGrid.BottomPagerRow;

                if (pagerRow != null)
                {
                    //得到text控件
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (txtNewPageIndex != null)
                {
                    //得到索引
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
                }
            }
            else
            {
                //点击了其他的按钮
                newPageIndex = e.NewPageIndex;
            }
            //防止新索引溢出
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            //得到新的值
            theGrid.PageIndex = newPageIndex;

            //重新绑定
            //GetCustomers();
            GridView1.PageSize = Convert.ToInt32(DropDownList_选择每页行数.SelectedItem.Value);
            GridView1.DataBind();






            //GridView1.PageIndex = e.NewPageIndex;
            //GridView1.DataBind();

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkSelect = gvr.FindControl("CheckBox1") as CheckBox;

                if (chkSelect != null)
                {
                    string productID = GridView1.DataKeys[gvr.RowIndex]["订单号"].ToString();

                    if (chkSelect.Checked && !this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Add(productID);
                    }
                    else if (!chkSelect.Checked && this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Remove(productID.ToString());
                    }
                }
            }



            #endregion

        }
        protected void DropDownList_选择每页行数_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageSize = Convert.ToInt32(DropDownList_选择每页行数.SelectedItem.Value);
            GridView1.DataBind();
        }



        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        //protected void OnPaging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GridView1.DataBind();

        //}

        private void PopulateCheckBoxArray()
        {
            ArrayList CheckBoxArray;
            if (ViewState["CheckBoxArray"] != null)
            {
                CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            }
            else
            {
                CheckBoxArray = new ArrayList();
            }

            int CheckBoxIndex;
            bool CheckAllWasChecked = false;
            CheckBox chkAll = (CheckBox)GridView1.HeaderRow.Cells[0].FindControl("chkAll");
            string checkAllIndex = "chkAll-" + GridView1.PageIndex;
            if (chkAll.Checked)
            {
                if (CheckBoxArray.IndexOf(checkAllIndex) == -1)
                {
                    CheckBoxArray.Add(checkAllIndex);
                }
            }
            else
            {
                if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                {
                    CheckBoxArray.Remove(checkAllIndex);
                    CheckAllWasChecked = true;
                }
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                    CheckBoxIndex = GridView1.PageSize * GridView1.PageIndex + (i + 1);
                    if (chk.Checked)
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) == -1 && !CheckAllWasChecked)
                        {
                            CheckBoxArray.Add(CheckBoxIndex);
                        }
                    }
                    else
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1 || CheckAllWasChecked)
                        {
                            CheckBoxArray.Remove(CheckBoxIndex);
                        }
                    }
                }
            }
            ViewState["CheckBoxArray"] = CheckBoxArray;
        }

        private void OriginSelect()
        {
            //========== 查询数据 开始 ==========
            string 查询到的数据 = "";

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkSelect = gvr.FindControl("CheckBox1") as CheckBox;

                if (chkSelect != null)
                {
                    string productID = GridView1.DataKeys[gvr.RowIndex]["订单号"].ToString();

                    if (chkSelect.Checked && !this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Add(productID);
                    }
                    else if (!chkSelect.Checked && this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Remove(productID);
                    }
                }
            }



            double 有几个 = 0;
            double 有多少钱 = 0;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        有几个 += 1;
                        有多少钱 += double.Parse(row.Cells[5].Text);

                        查询到的数据 = "笔数:" + 有几个 + ",金额:" + 有多少钱 + " ";
                    }
                }
            }


            //========== 查询数据 结束 ==========

            PopulateCheckBoxArray();//重新获得checkbox的ID



            Response.Clear();
            Response.Charset = "utf8";
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
             "attachment;filename=" + shixi() + ".xls");

            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.ContentEncoding = Encoding.Unicode;

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView1.AllowPaging = false;
            GridView1.DataBind();

            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");

            GridView1.HeaderRow.Cells[0].Visible = false;
            GridView1.HeaderRow.Cells[1].Visible = false;
            GridView1.HeaderRow.Cells[2].Visible = false;
            GridView1.HeaderRow.Cells[3].Visible = false;
            GridView1.HeaderRow.Cells[4].Visible = false;
            //GridView1.HeaderRow.Cells[5].Visible = false;
            //GridView1.HeaderRow.Cells[6].Visible = false;
            //GridView1.HeaderRow.Cells[7].Visible = false;
            //GridView1.HeaderRow.Cells[8].Visible = false;
            GridView1.HeaderRow.Cells[9].Visible = false;
            GridView1.HeaderRow.Cells[10].Visible = false;
            GridView1.HeaderRow.Cells[11].Visible = false;
            GridView1.HeaderRow.Cells[12].Visible = false;
            GridView1.HeaderRow.Cells[13].Visible = false;
            GridView1.HeaderRow.Cells[14].Visible = false;
            GridView1.HeaderRow.Cells[15].Visible = false;

            if (ViewState["CheckBoxArray"] != null)
            {
                ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                string checkAllIndex = "chkAll-" + GridView1.PageIndex;
                int rowIdx = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    row.Visible = false;

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (CheckBoxArray.IndexOf(i + 1) != -1)
                        {
                            row.Visible = true;
                            row.BackColor = System.Drawing.Color.White;
                            row.Cells[0].Visible = false;

                            row.Cells[1].Visible = false;
                            row.Cells[2].Visible = false;
                            row.Cells[3].Visible = false;
                            row.Cells[4].Visible = false;
                            //row.Cells[5].Visible = false;
                            //row.Cells[6].Visible = false;
                            //row.Cells[7].Visible = false;
                            //row.Cells[8].Visible = false;
                            row.Cells[9].Visible = false;
                            row.Cells[10].Visible = false;
                            row.Cells[11].Visible = false;
                            row.Cells[12].Visible = false;
                            row.Cells[13].Visible = false;
                            row.Cells[14].Visible = false;
                            row.Cells[15].Visible = false;

                            row.Attributes.Add("class", "textmode");
                            if (rowIdx % 2 != 0)
                            {

                            }
                            rowIdx++;
                        }
                    }
                }
            }
            GridView1.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);

            Response.Output.Write(sw.ToString() + 查询到的数据);
            Response.End();

        }

        private void OriginAll()
        {
            double 有几个 = 0;
            double 有多少钱 = 0;


            Response.Clear();
            Response.Charset = "utf8";
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
             "attachment;filename=" + shixi() + ".xls");

            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.ContentEncoding = Encoding.Unicode;

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView1.AllowPaging = false;
            GridView1.DataBind();

            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
            GridView1.HeaderRow.Cells[0].Visible = false;
            GridView1.HeaderRow.Cells[1].Style.Add("background-color", "green");
            GridView1.HeaderRow.Cells[2].Style.Add("background-color", "green");
            GridView1.HeaderRow.Cells[3].Style.Add("background-color", "green");
            GridView1.HeaderRow.Cells[4].Style.Add("background-color", "green");

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                有几个 += 1;
                有多少钱 += double.Parse(GridView1.Rows[i].Cells[5].Text);


                GridViewRow row = GridView1.Rows[i];
                row.BackColor = System.Drawing.Color.White;
                row.Cells[0].Visible = false;
                row.Attributes.Add("class", "textmode");
                if (i % 2 != 0)
                {
                    row.Cells[1].Style.Add("background-color", "#C2D69B");
                    row.Cells[2].Style.Add("background-color", "#C2D69B");
                    row.Cells[3].Style.Add("background-color", "#C2D69B");
                    row.Cells[14].Style.Add("background-color", "#C2D69B");
                }
            }
            GridView1.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);

            string 查询到的数据 = "笔数:" + 有几个 + ",金额:" + 有多少钱 + " ";

            Response.Output.Write(sw.ToString() + 查询到的数据);
            Response.Flush();
            Response.End();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

                e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

                CheckBox chkSelect = e.Row.FindControl("CheckBox1") as CheckBox;

                if (chkSelect != null)
                {
                    string productID = GridView1.DataKeys[e.Row.RowIndex]["订单号"].ToString();

                    chkSelect.Checked = this.ProductIDs.Contains(productID);
                }
            }


            //GridViewRow gvr = e.Row;

            //if (gvr.RowType == DataControlRowType.DataRow)
            //{
            //    CheckBox chkSelect = gvr.FindControl("CheckBox1") as CheckBox;

            //    if (chkSelect != null)
            //    {
            //        string productID = GridView1.DataKeys[gvr.RowIndex]["订单号"].ToString();

            //        chkSelect.Checked = this.ProductIDs.Contains(productID);
            //    }
            //}

        }

        protected void Button_全选_Click(object sender, EventArgs e)
        {
            勾选循环全部勾上();
        }

        protected void Button_全选取消_Click(object sender, EventArgs e)
        {
            勾选循环全部取消();
        }

        protected void Button_统计_Click(object sender, EventArgs e)
        {
            string 查询到的数据 = "";

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chkSelect = gvr.FindControl("CheckBox1") as CheckBox;

                if (chkSelect != null)
                {
                    string productID = GridView1.DataKeys[gvr.RowIndex]["订单号"].ToString();

                    if (chkSelect.Checked && !this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Add(productID);
                    }
                    else if (!chkSelect.Checked && this.ProductIDs.Contains(productID))
                    {
                        this.ProductIDs.Remove(productID);
                    }
                }
            }



            double 有几个 = 0;
            double 有多少钱 = 0;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        有几个 += 1;
                        有多少钱 += double.Parse(row.Cells[5].Text);

                        查询到的数据 = "笔数:" + 有几个 + ",金额:" + 有多少钱 + " ";
                    }
                }
            }

            ClassLibrary1.ClassMessage.HinXi(Page, 查询到的数据);
        }




        private List<string> ProductIDs
        {
            get
            {
                if (this.ViewState["ProductIDs"] == null)
                {
                    this.ViewState["ProductIDs"] = new List<string>();
                }

                return this.ViewState["ProductIDs"] as List<string>;
            }
        }





        protected void Button_分页_Click(object sender, EventArgs e)
        {
            Label_现在是第几页.Text = "现在显示" + 获得第几页() + "条";
        }

        public string 分页()
        {
            if (IsNumberic(TextBox_分页页数.Text) == true)
            {
                double 获得 = double.Parse(TextBox_分页页数.Text) - 1;
                double jieguo = double.Parse(DropDownList_选择每页行数.SelectedItem.Value) * 获得;//条数*文本框获得数
                double jieguo2 = double.Parse(DropDownList_选择每页行数.SelectedItem.Value);//结果2获得结果1的数加上选择框数
                return "" + jieguo + "," + jieguo2 + "";
            }
            else
            {
                TextBox_分页页数.Text = "1";
                return "0,50";
            }
        }

        private bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string 获得第几页()
        {
            if (IsNumberic(TextBox_分页页数.Text) == true)
            {
                double 获得 = double.Parse(TextBox_分页页数.Text) - 1;
                double jieguo = double.Parse(DropDownList_选择每页行数.SelectedItem.Value) * 获得;//条数*文本框获得数
                double jieguo2 = jieguo + double.Parse(DropDownList_选择每页行数.SelectedItem.Value);//结果2获得结果1的数加上选择框数
                return "" + jieguo + "-" + jieguo2 + "";
            }
            else
            {
                return "";
            }
        }

        protected void RadioButton_创建方式文本导入_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_创建方式文档导入_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_创建方式文本输入_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid("where " + 查看勾选了哪些() + " ");
        }

        private void ExportGird<T>(bool all, string name, string[] headers, Action<bool, DataTable , Action<DataRow, T, int>> data , Action<DataRow, T, int> action)
        {
            DataTable dt = new DataTable();
            foreach (string head in headers)
            {
                dt.Columns.Add(head, typeof(string));
            }

            DataRow dr = dt.NewRow();
            for (int i = 0; i < headers.Length; ++i)
            {
                dr[i] = headers[i];
            }
            dt.Rows.Add(dr);

            data.Invoke(all, dt, action);

            IWorkbook wb = new HSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Sheet1");
            ICreationHelper cH = wb.GetCreationHelper();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i);
                for (int j = 0; j < headers.Length; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(cH.CreateRichTextString(dt.Rows[i].ItemArray[j].ToString()));
                }
                sheet.AutoSizeColumn(i);
            }
            string fileName = name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ".xls";
            Response.ClearContent();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition",
                              "attachment; filename=" + fileName + ";");
            wb.Write(Response.OutputStream);
            Response.Flush();
            Response.End();
        }

        private void DataFromView(bool all, DataTable dt, Action<DataRow, GridViewRow, int> action)
        {
            if (ViewState["CheckBoxArray"] == null)
                return;
            ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            int index = 1;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                if (row.RowType != DataControlRowType.DataRow)
                    continue;
                if (!all)
                    if (CheckBoxArray.IndexOf(i + 1) == -1)
                        continue;
                if (row.Cells[12].Text == "成功")
                    continue;
                DataRow dr = dt.NewRow();
                action.Invoke(dr, row, index);
                dt.Rows.Add(dr);
                index++;
            }
        }

        private void Export_招商银行(bool all)
        {
            string[] headers = { "收款账户列", "收款户名列", "转账金额列", "备注列", "收款银行列", "收款银行支行列", "收款省/直辖市列", "收款市县列", "转出账号/卡", "转账模式" };
            ExportGird<GridViewRow>(all, "招商银行", headers, DataFromView, (dr, row, index) =>
            {
                dr[0] = row.Cells[6].Text;
                dr[1] = row.Cells[7].Text;
                dr[2] = row.Cells[5].Text;
                dr[4] = row.Cells[8].Text;
                dr[9] = "实时";
            });
        }

        private void Export_光大银行(bool all)
        {
            string[] headers = { "序号", "付款账号类型(0:企业账号,1:个人账号)", "付款账号", "付款户名", "收款人账号", "收款户名", "交易金额",
                "收款行行号", "收款行名称", "用途" };
            ExportGird<GridViewRow>(all, "光大银行", headers, DataFromView, (dr, row, index) =>
            {
                dr[0] = index.ToString();
                dr[1] = "1";
                dr[4] = row.Cells[6].Text;
                dr[5] = row.Cells[7].Text;
                dr[6] = row.Cells[5].Text;
                dr[8] = row.Cells[8].Text;
            });
        }

        private void Export_平安银行(bool all)
        {
            string[] headers = { "金额", "收款人账号", "收款人名称", "收款账号开户行名称", "收款方所在省", "收款方所在市县", "转账类型", "汇款用途" };
            ExportGird<GridViewRow>(all, "平安银行", headers, DataFromView, (dr, row, index) =>
            {
                dr[0] = row.Cells[5].Text;
                dr[1] = row.Cells[6].Text;
                dr[2] = row.Cells[7].Text;
                dr[3] = row.Cells[8].Text;
                dr[6] = (new[] { "行内转账", "异地跨行" })[("平安银行" == dr[3]) ? 0 : 1];
            });
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (RadioButton_默认模板.Checked)
                OriginSelect();
            else if (RadioButton_招商银行.Checked)
                Export_招商银行(false);
            else if (RadioButton_光大银行.Checked)
                Export_光大银行(false);
            else if (RadioButton_平安银行.Checked)
                Export_平安银行(false);
        }

        protected void btnExportAll_Click(object sender, EventArgs e)
        {
            if (RadioButton_默认模板.Checked)
                OriginAll();
            else if (RadioButton_招商银行.Checked)
                Export_招商银行(true);
            else if (RadioButton_光大银行.Checked)
                Export_光大银行(true);
            else if (RadioButton_平安银行.Checked)
                Export_平安银行(true);
        }

        private void DataFromDatabase(bool all, DataTable dt, Action<DataRow, table_商户明细提款, int> action)
        {
            using (SqlSugarClient sqlSugarClient = new DBClient().GetClient())
            {
                var NewerList = sqlSugarClient.Queryable<table_商户明细提款>().
                    Where(it => it.后台处理批次ID组 == SqlFunc.Subqueryable<table_商户明细提款>()
                    .Max(s => s.后台处理批次ID组)).ToList();
                foreach (table_商户明细提款 record in NewerList)
                {
                    DataRow dr = dt.NewRow();
                    action(dr, record, NewerList.IndexOf(record) + 1);
                    dt.Rows.Add(dr);
                }
            }

        }

        private void ExportAllData()
        {
            string[] headers = {"订单号", "商户ID", "出款银行卡名称", "出款银行卡卡号", "交易金额", "交易方卡号", "交易方姓名", "交易方银行", "创建时间", 
                "完成时间", "创建方式", "订单状态", "后台处理批次ID组", "操作员" };
            ExportGird<table_商户明细提款>(true, "最新后台处理批次", headers, DataFromDatabase, (dr, record, index) =>
            {
                dr[0] = record.订单号;
                dr[1] = record.商户ID;
                dr[2] = record.出款银行卡名称;
                dr[3] = record.出款银行卡卡号;
                dr[4] = record.交易金额;
                dr[5] = record.交易方卡号;
                dr[6] = record.交易方姓名;
                dr[7] = record.交易方银行;
                dr[8] = record.时间创建.Value.ToString("yyyy-MM-dd HH:mm:ss");
                dr[9] = record.时间完成.Value.ToString("yyyy-MM-dd HH:mm:ss");
                dr[10] = record.创建方式;
                dr[11] = record.状态;
                dr[12] = record.后台处理批次ID组;
                dr[13] = record.操作员;
            });
        }

        private void ExportGuangDa()
        {
            string[] headers = { "序号", "付款账号类型(0:企业账号,1:个人账号)", "付款账号", "付款户名", "收款人账号", "收款户名", "交易金额",
                "收款行行号", "收款行名称", "用途" };
            ExportGird<table_商户明细提款>(true, "最新后台处理批次_光大银行", headers, DataFromDatabase, (dr, record, index) =>
            {
                dr[0] = index.ToString();
                dr[1] = "1";
                dr[2] = record.出款银行卡卡号;
                dr[3] = record.操作员;
                dr[4] = record.交易方卡号;
                dr[5] = record.交易方姓名;
                dr[6] = record.交易金额;
                dr[8] = record.交易方银行;
            });
        }

        private void ExportPingAn()
        {
            string[] headers = { "金额", "收款人账号", "收款人名称", "收款账号开户行名称", "收款方所在省", "收款方所在市县", "转账类型", "汇款用途" };
            ExportGird<table_商户明细提款>(true, "最新后台处理批次_平安银行", headers, DataFromDatabase, (dr, record, index) =>
            {
                dr[0] = record.交易金额;
                dr[1] = record.交易方卡号;
                dr[2] = record.交易方姓名;
                dr[3] = record.交易方银行;
                dr[6] = (new[] { "行内转账", "异地跨行" })[("平安银行" == dr[3]) ? 0 : 1];
            });
        }

        private void ExportZhaoShang()
        {
            string[] headers = { "收款账户列", "收款户名列", "转账金额列", "备注列", "收款银行列", "收款银行支行列", "收款省/直辖市列", 
                "收款市县列", "转出账号/卡", "转账模式" };
            ExportGird<table_商户明细提款>(true, "最新后台处理批次_招商银行", headers, DataFromDatabase, (dr, record, index) =>
            {
                dr[0] = record.交易方卡号;
                dr[1] = record.交易方姓名;
                dr[2] = record.交易金额;
                dr[4] = record.交易方银行;
                dr[8] = record.出款银行卡卡号;
                dr[9] = "实时";
            });
        }

        protected void Button_导出最新后台处理批次ID组_Click(object sender, EventArgs e)
        {
            if (RadioButton_批次默认模板.Checked)
                ExportAllData();
            else if (RadioButton_批次光大银行.Checked)
                ExportGuangDa();
            else if (RadioButton_批次平安银行.Checked)
                ExportPingAn();
            else if (RadioButton_批次招商银行.Checked)
                ExportZhaoShang();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BindGridForBatchOperator();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string strQuery = "select 订单号,商户ID,出款银行卡名称,出款银行卡卡号,交易方姓名,交易方卡号,交易方银行,交易金额,时间创建,时间完成,创建方式,状态,操作员,后台处理批次ID组 FROM table_商户明细提款 " + " order by 时间完成 desc  LIMIT " + 分页() + " ";
            DataTable dt = new DataTable();
            String strConnString = ClassLibrary1.ClassDataControl.conStr1;
            MySqlConnection con = new MySqlConnection(strConnString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(strQuery);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
    }
}