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

namespace web1.WebsiteMerchant.商户订单
{
    public partial class 商户提款记录导出 : System.Web.UI.Page
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
                ClassLibrary1.ClassAccount.验证账号商户端();
                GetData();

                PopulateCheckBoxArray();
            }


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

            BindGrid(" " + 查看勾选了哪些() + " ");

            //TextBox_开始时间.Enabled = false;
            TextBox_结束时间.Enabled = false;

        }



        protected void Button_查找_Click(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
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
                //TextBox_结束时间.Enabled = true;

                //string 时间1 = DateTime.Now.ToString("yyyy-MM-dd");
                //string 时间2 = DateTime.Now.ToString("yyyy-MM-dd");

                //TextBox_开始时间.Text = 时间1;
                //TextBox_结束时间.Text = 时间2;

                TextBox_结束时间.Text = DateTime.Now.ToString("yyyy-MM-dd"); 

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
                条件4 = "  创建方式='API' ";
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
            if (redkey == "未选择")
            {

            }
            else
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
            return 获得订单状态() + 获得创建方式() + 获得筛选关键字() + 获得筛选端金额() + 获得区间金额() + 获得时间();
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

        protected void RadioButton_状态全部_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态待处理_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态处理中_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态关闭_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态待确认_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态失败_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_状态成功_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void Button_筛选仅按状态_Click(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        //====================================================================================================

        protected void Button_筛选仅按关键字_Click(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型全部_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型出账_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_类型订单提款冲正_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        //====================================================================================================

        //====================================================================================================

        protected void RadioButton_创建方式全部_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_创建方式手动_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
        }

        protected void RadioButton_创建方式API_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(" " + 查看勾选了哪些() + " ");
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



        private void BindGrid(string 时间导入绑定)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            string strQuery = "select 订单号,商户名称,类型,交易方卡号,交易方姓名,交易方银行,交易金额,手续费,创建方式,状态,时间创建,时间完成 from table_商户明细提款 where 商户ID='" + Cookie_UserName + "' and " + 时间导入绑定 + " order by id desc ";
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
            sb.Append("alert('");
            sb.Append(count.ToString());
            sb.Append(" records deleted.');");
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
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



            //double 有几个 = 0;
            //double 有多少钱 = 0;

            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
            //        if (chkRow.Checked)
            //        {
            //            有几个 += 1;
            //            有多少钱 += double.Parse(row.Cells[5].Text);

            //            查询到的数据 = "笔数:" + 有几个 + ",金额:" + 有多少钱 + " ";
            //        }
            //    }
            //}


            //========== 查询数据 结束 ==========

            PopulateCheckBoxArray();//重新获得checkbox的ID



            Response.Clear();
            Response.Charset = "utf8";
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
             "attachment;filename="+ shixi() + ".xls");

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
            //GridView1.HeaderRow.Cells[1].Visible = false;
            //GridView1.HeaderRow.Cells[2].Visible = false;
            //GridView1.HeaderRow.Cells[3].Visible = false;
            //GridView1.HeaderRow.Cells[4].Visible = false;
            //GridView1.HeaderRow.Cells[9].Visible = false;
            ////GridView1.HeaderRow.Cells[10].Visible = false;
            //GridView1.HeaderRow.Cells[11].Visible = false;
            //GridView1.HeaderRow.Cells[12].Visible = false;
            ////GridView1.HeaderRow.Cells[13].Visible = false;
            //GridView1.HeaderRow.Cells[14].Visible = false;
            //GridView1.HeaderRow.Cells[15].Visible = false;

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

                            //row.Cells[1].Visible = false;
                            //row.Cells[2].Visible = false;
                            //row.Cells[3].Visible = false;
                            //row.Cells[4].Visible = false;
                            //row.Cells[9].Visible = false;
                            ////row.Cells[10].Visible = false;
                            //row.Cells[11].Visible = false;
                            //row.Cells[12].Visible = false;
                            ////row.Cells[13].Visible = false;
                            //row.Cells[14].Visible = false;
                            //row.Cells[15].Visible = false;
                            row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

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
            
            Response.Output.Write(sw.ToString()+ 查询到的数据);
            Response.End();
            
        }

        protected void btnExportAll_Click(object sender, EventArgs e)
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
                //有几个 += 1;
                //有多少钱 += double.Parse(GridView1.Rows[i].Cells[5].Text);


                GridViewRow row = GridView1.Rows[i];
                row.BackColor = System.Drawing.Color.White;
                row.Cells[0].Visible = false;
                row.Attributes.Add("class", "textmode");
                if (i % 2 != 0)
                {
                    row.Cells[1].Style.Add("background-color", "#C2D69B");
                    row.Cells[2].Style.Add("background-color", "#C2D69B");
                    row.Cells[3].Style.Add("background-color", "#C2D69B");
                    //row.Cells[14].Style.Add("background-color", "#C2D69B");
                    //row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                }
            }
            GridView1.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);

            //string 查询到的数据 = "笔数:" + 有几个 + ",金额:" + 有多少钱 + " ";
            string 查询到的数据 = " ";

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

                e.Row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

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



    }
}

//========== 原 勾选不掉 ==========
//if (ViewState["CheckBoxArray"] != null)
//{
//    ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
//    string checkAllIndex = "chkAll-" + GridView1.PageIndex;

//    if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
//    {
//        CheckBox chkAll = (CheckBox)GridView1.HeaderRow.Cells[0].FindControl("chkAll");
//        chkAll.Checked = true;
//    }
//    for (int i = 0; i < GridView1.Rows.Count; i++)
//    {

//        if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
//        {
//            if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
//            {
//                CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
//                chk.Checked = true;
//                GridView1.Rows[i].Attributes.Add("style", "background-color:aqua");
//            }
//            else
//            {
//                int CheckBoxIndex = GridView1.PageSize * (GridView1.PageIndex) + (i + 1);
//                if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
//                {
//                    CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
//                    chk.Checked = true;
//                    GridView1.Rows[i].Attributes.Add("style", "background-color:aqua");
//                }
//            }
//        }
//    }
//}
//========== 原 勾选不掉 ==========