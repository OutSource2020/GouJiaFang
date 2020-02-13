using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteMerchant.商户订单
{
    public partial class 商户充值记录 : System.Web.UI.Page
    {
        public static string 时间字段 = "时间创建";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();
            }

            BindGrid(" " + 查看勾选了哪些() + " ");
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

        protected void Button_查找_Click(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void Button_商户充值_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户充值.aspx");
        }

        protected void TimerTick(object sender, EventArgs e)
        {
            Label_刷新时间.Text = "载入时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            GridView1.DataBind();
        }

        private void 页面自动刷新()
        {
            int 获得刷新秒数 = Convert.ToInt32("10" + "000");
            Timer_自动刷新.Interval = 获得刷新秒数;
            Timer_自动刷新.Enabled = true;//開始計時

            //Timer_自动刷新.Enabled = false;//先關閉計時
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

        public string 获得类型()
        {
            string 中间加和3 = "";
            string 条件3 = "";

            if (RadioButton_类型全部.Checked)
            {
                条件3 = null;
            }
            if (RadioButton_类型充值提款手续费.Checked)
            {
                条件3 = "  充值类型='充值提款手续费' ";
            }
            if (RadioButton_类型充值提款余额.Checked)
            {
                条件3 = "  充值类型='充值提款余额' ";
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

        public string 查看勾选了哪些()
        {
            return  获得类型() + 获得时间();
        }

        //====================================================================================================
        protected void RadioButton_时间今天_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间昨天_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间7天_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间本周_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间本月_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_时间设置_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        //====================================================================================================

        protected void RadioButton_类型全部_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型充值提款手续费_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型充值提款余额_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        private void BindGrid(string 时间导入绑定)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection connC = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                connC.Open();

                MySqlCommand cmd1 = new MySqlCommand("select COUNT(订单号) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and 商户ID='" + Cookie_UserName + "' and " + 时间导入绑定 + " ", connC);
                object obj1 = cmd1.ExecuteScalar();
                if (obj1 != null)
                {
                    Label_充值提款余额笔数.Text = obj1.ToString();
                }

                MySqlCommand cmd2 = new MySqlCommand("select COUNT(订单号) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and 商户ID='" + Cookie_UserName + "' and " + 时间导入绑定 + " ", connC);
                object obj2 = cmd2.ExecuteScalar();
                if (obj2 != null)
                {
                    Label_充值手续费笔数.Text = obj2.ToString();
                }

                MySqlCommand cmd3 = new MySqlCommand("select sum(充值金额) from table_商户明细充值 where 充值类型='充值提款余额' and 状态='成功' and 商户ID='" + Cookie_UserName + "' and " + 时间导入绑定 + " ", connC);
                object obj3 = cmd3.ExecuteScalar();
                if (obj3 != null)
                {
                    Label_充值提款余额总数.Text = obj3.ToString();
                }

                MySqlCommand cmd4 = new MySqlCommand("select sum(充值金额) from table_商户明细充值 where 充值类型='充值提款手续费' and 状态='成功' and 商户ID='" + Cookie_UserName + "' and " + 时间导入绑定 + " ", connC);
                object obj4 = cmd4.ExecuteScalar();
                if (obj4 != null)
                {
                    Label_充值手续费金额总数.Text = obj4.ToString();
                }

                connC.Close();
            }

            using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd11 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额,手续费比率 FROM table_商户账号 WHERE 商户ID=@商户ID", con11))
                {
                    cmd11.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    using (MySqlDataAdapter da11 = new MySqlDataAdapter(cmd11))
                    {
                        DataTable images11 = new DataTable();
                        da11.Fill(images11);
                        foreach (DataRow dr11 in images11.Rows)
                        {
                            this.Label_账户提款余额.Text = dr11["提款余额"].ToString();
                            this.Label_账户手续费余额.Text = dr11["手续费余额"].ToString();
                            this.Label_账户手续费比率.Text = dr11["手续费比率"].ToString();
                        }
                    }
                }
            }


            string strQuery = "select 订单号,商户ID,商户银行卡卡号,商户充值目标姓名,商户充值目标卡号,商户充值目标银行,充值类型,充值金额,状态,时间创建 from table_商户明细充值 where 商户ID='" + Cookie_UserName + "' and " + 时间导入绑定 + " order by 时间创建 desc  LIMIT " + 分页() + " ";
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
            #endregion

        }
        protected void DropDownList_选择每页行数_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageSize = Convert.ToInt32(DropDownList_选择每页行数.SelectedItem.Value);
            GridView1.DataBind();
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
                double jieguo = 50 * 获得;
                double jieguo2 = 50;
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
                double jieguo = 50 * 获得;
                double jieguo2 = jieguo + 50;
                return "" + jieguo + "-" + jieguo2 + "";
            }
            else
            {
                return "";
            }
        }


    }
}
