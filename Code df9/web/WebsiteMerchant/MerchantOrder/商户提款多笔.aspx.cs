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
    public partial class 商户提款多笔 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();

                SetInitialRow();
            }
            查询账户信息();
        }

        protected void TextBox_交易方卡号_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                ((TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox3")).Text = ClassLibrary1.ClassBankInfo3a2.BankUtil.getNameOfBank(((TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1")).Text);
            }
        }

        protected void TextBox_交易金额_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                //过滤数字和小数
                ((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text = ClassLibrary1.ClassYZ.过滤数字和小数(((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text);

                ////保留2位小数
                //double dssadsada = double.Parse(((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text);
                //((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text = Math.Round(dssadsada, 2).ToString();
            }

            Label_预计提款金额.Text = 统计订单多少钱();

            Label_预计提款手续费.Text = 统计订单多少手续费();
        }

        protected void Button_刷新预估_Click(object sender, EventArgs e)
        {
            Label_预计提款金额.Text = 统计订单多少钱();

            Label_预计提款手续费.Text = 统计订单多少手续费();

            检查是否符合要求();
        }

        private string 统计订单多少钱()
        {
            double 计算订单总金额 = 0;

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                if (((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text != "")
                {
                    double 交易金额 = double.Parse(((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text);
                    计算订单总金额 = 计算订单总金额 + 交易金额;
                }
            }

            return 计算订单总金额.ToString();
        }

        private string 统计订单多少手续费()
        {
            double 计算订单总手续费 = 0;

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                计算订单总手续费 = 计算订单总手续费 + double.Parse(Label_单笔手续费.Text);
            }

            return 计算订单总手续费.ToString();
        }

        private void 检查是否符合要求()
        {
            if (double.Parse(Label_预计提款金额.Text) > double.Parse(Label_提款余额.Text))
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "注意 你的提款余额不足");
            }
        }

        private void 查询账户信息()
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额,单笔手续费,提款最低单笔金额,提款最高单笔金额 FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            string 提款余额 = dr["提款余额"].ToString();
                            string 手续费余额 = dr["手续费余额"].ToString();
                            string 单笔手续费 = dr["单笔手续费"].ToString();
                            string 提款最低单笔金额 = dr["提款最低单笔金额"].ToString();
                            string 提款最高单笔金额 = dr["提款最高单笔金额"].ToString();

                            Label_提款余额.Text = 提款余额;
                            Label_手续费余额.Text = 手续费余额;
                            Label_单笔手续费.Text = 单笔手续费;
                            Label_账户最低提款金额.Text = 提款最低单笔金额;
                            Label_账户最高提款金额.Text = 提款最高单笔金额;
                        }
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dr = dt.NewRow();
            //dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            Gridview1.DataSource = dt;
            Gridview1.DataBind();
        }
        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox4");
                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox5");

                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    Gridview1.DataSource = dtCurrentTable;
                    Gridview1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox4");
                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox5");

                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        box2.Text = dt.Rows[i]["Column2"].ToString();
                        box3.Text = dt.Rows[i]["Column3"].ToString();
                        box4.Text = dt.Rows[i]["Column4"].ToString();
                        box5.Text = dt.Rows[i]["Column5"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void DeleteRowHandler(object sender, CommandEventArgs e)
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    //for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    //{
                    //    //extract the TextBox values
                    //    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    //    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    //    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                    //    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox4");
                    //    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox5");

                    //    drCurrentRow = dtCurrentTable.NewRow();
                    //    drCurrentRow["RowNumber"] = i + 1;

                    //    dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                    //    dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                    //    dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                    //    dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                    //    dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;

                    //    rowIndex++;
                    //}
                    //dtCurrentTable.Rows.Add(drCurrentRow);
                    //ViewState["CurrentTable"] = dtCurrentTable;

                    //Gridview1.DataSource = dtCurrentTable;
                    //Gridview1.DataBind();






                    GridViewRow row = ((GridViewRow)((LinkButton)sender).Parent.Parent);
                    DataTable dt = new DataTable();
                    DataRow dr = null;
                    dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dt.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dt.Columns.Add(new DataColumn("Column2", typeof(string)));
                    dt.Columns.Add(new DataColumn("Column3", typeof(string)));
                    dt.Columns.Add(new DataColumn("Column4", typeof(string)));
                    dt.Columns.Add(new DataColumn("Column5", typeof(string)));
                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        //dr["RowNumber"] = i;
                        dr[1] = ((TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1")).Text;
                        dr[2] = ((TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox2")).Text;
                        dr[3] = ((TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox3")).Text;
                        dr[4] = ((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text;
                        dr[5] = ((TextBox)Gridview1.Rows[i].Cells[5].FindControl("TextBox5")).Text;
                        dt.Rows.Add(dr);
                    }
                    dt.Rows.RemoveAt(row.RowIndex);
                    ViewState["CurrentTable"] = dt;
                    Gridview1.DataSource = dt;
                    Gridview1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        protected void Button_批量发起提款订单_Click(object sender, EventArgs e)
        {

            //检查支付密码是否正确
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();
            using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd11 = new MySqlCommand("SELECT 商户ID,keyga,支付密码,支付错误累计 FROM table_商户账号 WHERE 商户ID=@商户ID", con11))
                {
                    cmd11.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    using (MySqlDataAdapter da11 = new MySqlDataAdapter(cmd11))
                    {
                        DataTable images11 = new DataTable();
                        da11.Fill(images11);
                        foreach (DataRow dr11 in images11.Rows)
                        {
                            string 商户ID = dr11["商户ID"].ToString();
                            string 密匙 = dr11["keyga"].ToString();
                            string 支付密码 = dr11["支付密码"].ToString();
                            double 支付错误累计 = double.Parse(dr11["支付错误累计"].ToString());


                            if (ClassLibrary1.ClassAccount.验证商户白名单IP(Cookie_UserName, ClassLibrary1.ClassAccount.来源IP()) == true)//验证来源IP是否在数据库是白名单IP
                            {

                                if (支付错误累计 < 6)
                                {
                                    if (支付密码 == TextBox_输入支付密码.Text)//支付密码必须和数据库中一致
                                    {

                                        Button_批量发起提款订单.Enabled = false;//防止重复操作
                                        开始执行();

                                    }
                                    else
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "支付密码或者key密码错误超过限制,需要联系客服处理 (-S1002)");
                                    }


                                }
                                else
                                {
                                    ClassLibrary1.ClassMessage.HinXi(Page, "支付密码或者key密码错误超过限制,需要联系客服处理 (-S1001)");
                                }


                            }
                            else
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "需使用白名单IP下单 或者联系客服处理 你现在的前IP>> " + ClassLibrary1.ClassAccount.来源IP() + " (-S1003)");
                            }




                        }
                    }
                }
            }


        }

        private void 开始执行()
        {
            Button_批量发起提款订单.Enabled = false;//防止重复操作


            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                string 交易方卡号 = ((TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1")).Text;
                string 交易方姓名 = ((TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox2")).Text;
                string 交易方银行 = ((TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox3")).Text;
                string 交易金额 = ((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text;
                string 备注 = ((TextBox)Gridview1.Rows[i].Cells[5].FindControl("TextBox5")).Text;


                if (ClassLibrary1.ClassYZ.IsNumber(交易金额) == true)
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "是数字或者小数");

                    string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

                    //查询账户信息
                    using (MySqlConnection con1 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                    {
                        using (MySqlCommand cmd1 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额,手续费比率,单笔手续费,提款最低单笔金额,提款最高单笔金额 FROM table_商户账号 WHERE 商户ID=@商户ID", con1))
                        {
                            cmd1.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                            using (MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1))
                            {
                                DataTable images = new DataTable();
                                da1.Fill(images);
                                foreach (DataRow dr1 in images.Rows)
                                {
                                    double 提款金额 = double.Parse(((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text);

                                    double 提款余额 = double.Parse(dr1["提款余额"].ToString());
                                    double 手续费余额 = double.Parse(dr1["手续费余额"].ToString());
                                    double 手续费比率 = double.Parse(dr1["手续费比率"].ToString());
                                    double 单笔手续费 = double.Parse(dr1["单笔手续费"].ToString());
                                    double 提款最低单笔金额 = double.Parse(dr1["提款最低单笔金额"].ToString());
                                    double 提款最高单笔金额 = double.Parse(dr1["提款最高单笔金额"].ToString());

                                    if ((提款余额 - 提款金额) >= 0)
                                    {
                                        if (提款最高单笔金额 >= 提款金额)
                                        {
                                            if (提款最低单笔金额 <= 提款金额)
                                            {
                                                if (手续费余额 - 单笔手续费 >= 0)
                                                {

                                                    Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 300));

                                                    double a = double.Parse(交易金额);
                                                    double b = 100;
                                                    double c = double.Parse(dr1["手续费比率"].ToString());
                                                    double d = double.Parse(dr1["单笔手续费"].ToString());
                                                    //double 手续费计算 = (((a / b) * c) + d);
                                                    //double 手续费多少 = Math.Round(手续费计算, 2);

                                                    //提款只收单笔手续费(不扣手续费比率)
                                                    double 手续费计算 = d;

                                                    double 手续费多少 = 手续费计算;

                                                    //扣除 账户余额和手续费
                                                    using (MySqlConnection conmy2 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        using (MySqlCommand cmdmy2 = new MySqlCommand("UPDATE table_商户账号 SET 提款余额=提款余额-'" + 提款金额 + "', 手续费余额= 手续费余额-'" + 手续费多少 + "' where 商户ID='" + Cookie_UserName + "' ", conmy2))
                                                        {
                                                            //cmd12.Parameters.AddWithValue("@提款金额", 提款金额);
                                                            //cmd12.Parameters.AddWithValue("@手续费多少", 手续费多少);
                                                            //cmd12.Parameters.AddWithValue("@商户ID", Cookie_UserName);

                                                            conmy2.Open();
                                                            cmdmy2.ExecuteNonQuery();
                                                            conmy2.Close();

                                                            //Response.Redirect(Request.Url.AbsoluteUri, false);

                                                        }
                                                    }

                                                    //商户提款手续费交易明细
                                                    using (MySqlConnection scon1 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        string 提款手续费_生成编号标头 = "MHFON";
                                                        string 提款手续费_订单号 = 提款手续费_生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                        string 提款手续费_商户ID = Cookie_UserName;
                                                        double 提款手续费_手续费 = 手续费多少;
                                                        double 提款手续费_交易金额 = 提款金额;
                                                        double 提款手续费_交易前手续费余额 = 手续费余额;
                                                        double 提款手续费_交易后手续费余额 = 手续费余额 - 手续费多少;
                                                        string 提款手续费_类型 = "提款";
                                                        string 提款手续费_时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                        string 手续费交易明细哪个表 = " table_商户明细手续费 ";
                                                        string 手续费交易明细写哪些 = "订单号,商户ID,手续费支出,交易金额,交易前手续费余额,交易后手续费余额,类型,时间创建";
                                                        string 手续费交易明细写这些 = "'" + 提款手续费_订单号 + "','" + 提款手续费_商户ID + "','" + 提款手续费_手续费 + "','" + 提款手续费_交易金额 + "','" + 提款手续费_交易前手续费余额 + "','" + 提款手续费_交易后手续费余额 + "','" + 提款手续费_类型 + "','" + 提款手续费_时间创建 + "' ";

                                                        string str = "insert into " + 手续费交易明细哪个表 + "(" + 手续费交易明细写哪些 + ") values(" + 手续费交易明细写这些 + ")";

                                                        scon1.Open();
                                                        MySqlCommand command = new MySqlCommand();
                                                        command.Connection = scon1;
                                                        command.CommandText = str;
                                                        int obj = command.ExecuteNonQuery();

                                                        scon1.Close();
                                                    }

                                                    //商户账户余额交易明细
                                                    using (MySqlConnection scon2 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        string 账户余额_订单号 = "MBON" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                        string 账户余额_类型 = "提款";
                                                        double 账户余额_手续费 = 手续费余额;
                                                        double 账户余额_交易金额 = double.Parse(交易金额);
                                                        double 账户余额_交易前余额 = double.Parse(dr1["提款余额"].ToString());
                                                        double 账户余额_交易后余额 = double.Parse(dr1["提款余额"].ToString()) - double.Parse(交易金额);
                                                        string 账户余额_时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                        string 余额交易明细哪个表 = " table_商户明细余额 ";
                                                        string 余额交易明细写哪些 = "订单号,商户ID,类型,手续费,交易金额,交易前账户余额,交易后账户余额,状态,时间创建";
                                                        string 余额交易明细写这些 = "'" + 账户余额_订单号 + "','" + Cookie_UserName + "','" + 账户余额_类型 + "','" + 手续费多少 + "','" + 账户余额_交易金额 + "','" + 账户余额_交易前余额 + "','" + 账户余额_交易后余额 + "','','" + 账户余额_时间创建 + "' ";

                                                        string str = "insert into " + 余额交易明细哪个表 + "(" + 余额交易明细写哪些 + ") values(" + 余额交易明细写这些 + ")";

                                                        scon2.Open();
                                                        MySqlCommand command = new MySqlCommand();
                                                        command.Connection = scon2;
                                                        command.CommandText = str;
                                                        int obj = command.ExecuteNonQuery();

                                                        scon2.Close();
                                                    }

                                                    //向提款记录表写入信息
                                                    using (MySqlConnection scon3 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        string 生成编号 = "MST" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

                                                        string 创建方式 = "手动";
                                                        string 状态 = "待处理";
                                                        string 类型 = "提款";
                                                        string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                        string 来源IP = ClassLibrary1.ClassAccount.来源IP();

                                                        //string 有哪些 = "订单号,商户ID,卡号,充值金额,充值类型,备注,时间创建,状态 ";
                                                        string 有哪些 = "订单号,商户ID,交易方卡号,交易方姓名,交易方银行,交易金额,手续费,创建方式,备注商户写,状态,类型,时间创建,订单源IP";
                                                        string 收哪些 = "'" + 生成编号 + "','" + Cookie_UserName + "','" + 交易方卡号 + "','" + 交易方姓名 + "','" + 交易方银行 + "','" + 交易金额 + "','" + 手续费多少 + "','" + 创建方式 + "','" + 备注 + "','" + 状态 + "','" + 类型 + "','" + 时间创建 + "','" + 来源IP + "' ";


                                                        string str3 = "insert into table_商户明细提款(" + 有哪些 + ") values(" + 收哪些 + ")";
                                                        scon3.Open();
                                                        MySqlCommand command = new MySqlCommand();
                                                        command.Connection = scon3;
                                                        command.CommandText = str3;
                                                        int obj = command.ExecuteNonQuery();

                                                        scon3.Close();

                                                        //Response.Redirect("./商户提款记录.aspx");
                                                    }

                                                }
                                                else
                                                {
                                                    ClassLibrary1.ClassMessage.HinXi(Page, "手续费余额不足");
                                                }
                                            }
                                            else
                                            {
                                                ClassLibrary1.ClassMessage.HinXi(Page, "目标提款金额 小于账户提款金额限制");
                                            }
                                        }
                                        else
                                        {
                                            ClassLibrary1.ClassMessage.HinXi(Page, "目标提款金额 大于账户提款金额限制");
                                        }
                                    }
                                    else
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "目标提款金额 账户提款余额不足支付");
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "金额 不是数字或者小数");
                }


            }

            Response.Redirect("./商户提款记录.aspx");
        }


        private void 支付错误()
        {
            using (MySqlConnection con11 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd11 = new MySqlCommand("UPDATE table_商户账号 SET 支付错误累计=支付错误累计+1 WHERE 商户ID=@商户ID ", con11))
                {
                    cmd11.Parameters.AddWithValue("@商户ID", TextBox_输入支付密码.Text.Trim());
                    con11.Open();
                    cmd11.ExecuteNonQuery();
                    con11.Close();
                    //this.SaveImage(filePath);
                    //Response.Redirect() //Response.Redirect(Request.Url.AbsoluteUri, false);
                    ClassLibrary1.ClassMessage.HinXi(Page, "支付密码错误");
                }
            }
        }
    }
}