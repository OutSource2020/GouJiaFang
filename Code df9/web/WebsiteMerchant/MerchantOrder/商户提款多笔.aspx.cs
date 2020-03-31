using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using Google.Authenticator;
using System.Threading;
using System.Data.OleDb;
using System.IO;
using ExcelDataReader;
using System.Text.RegularExpressions;
using Sugar.Enties;
using SqlSugar;

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
            string Cookie_UserName = null;
            //檢測cookie
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"] != null)
                Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["username"]);
            if (Cookie_UserName != null)
                using (var db = (new DBClient()).GetClient())
                {
                    var data = db.Queryable<Sugar.Enties.table_商户账号>().Where(it => it.商户ID == Cookie_UserName).First();
                    if (data.手动提款状态 == false)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "你没有手动提款的权限,请找管理员核实");
                        Thread.Sleep(1000 * 3);
                        System.Web.HttpContext.Current.Response.Redirect("/WebsiteMerchant/MerchantOverview/商户首页.aspx");
                    }
                }
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

            if (!ValidateGoogleCode())
                return;

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
                                        if (Session["TimeOut"] == null || GetTimeStamp() - (long)Session["TimeOut"] > 60 * 1000)
                                        {
                                            Session.Add("TimeOut", GetTimeStamp());
                                            Button_批量发起提款订单.Enabled = false;//防止重复操作
                                            开始执行();
                                        }
                                        else
                                        {
                                            ClassLibrary1.ClassMessage.HinXi(Page, "1分钟之内不能发起重复订单");
                                            // Response.Redirect("../MerchantOverview/商户首页.aspx");
                                        }
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

        private long GetTimeStamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            return (long)System.Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

        private void 开始执行()
        {
            Button_批量发起提款订单.Enabled = false;//防止重复操作
            long OperatorId = GetTimeStamp();
            int Sindex = 0;
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();
            using (SqlSugarClient dbClient = new DBClient().GetClient())
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string 交易方卡号 = ((TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1")).Text;
                    string 交易方姓名 = ((TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox2")).Text;
                    string 交易方银行 = ((TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox3")).Text;
                    string 交易金额 = ((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text;
                    string 备注 = ((TextBox)Gridview1.Rows[i].Cells[5].FindControl("TextBox5")).Text;

                    DateTime now = DateTime.Now;

                    if (!ClassLibrary1.ClassYZ.IsNumber(交易金额))
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "金额 不是数字或者小数 忽略该笔继续执行");
                        continue;
                    }

                    // 这个地方有可能失灵，需要测试
                    int count = 0;

                    dbClient.Ado.UseTran(() => { }); // select 之前保证一次 commit，即使什么都不做
                    dbClient.Ado.UseTran(() =>
                    {
                        count = dbClient.Queryable<table_商户明细提款>().Where(it => it.交易方卡号 == 交易方卡号
                        && it.交易方姓名 == 交易方姓名
                        && it.交易金额.Value.ToString() == 交易金额
                        && DateTime.Now < it.时间创建.Value.AddMinutes(10)).Count();
                    });

                    if (count > 0)
                        continue;

                    dbClient.Ado.UseTran(() => { }); // select 之前保证一次 commit，即使什么都不做
                    table_商户账号 record = null;
                    dbClient.Ado.UseTran(() =>
                    {
                        record = dbClient.Queryable<table_商户账号>().Where(it => it.商户ID == Cookie_UserName).First();
                    });
                    if (record == null)
                        return;
                    double 提款金额 = double.Parse(交易金额);

                    double 提款余额 = record.提款余额.Value;
                    double 手续费余额 = record.手续费余额.Value;
                    double 手续费比率 = record.手续费比率.Value;
                    double 单笔手续费 = record.单笔手续费.Value;
                    double 提款最低单笔金额 = double.Parse(record.提款最低单笔金额);
                    double 提款最高单笔金额 = double.Parse(record.提款最高单笔金额);
                    if (提款余额 - 提款金额 < 0)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "目标提款金额 账户提款余额不足支付");
                        return;
                    }
                    if (手续费余额 - 单笔手续费 < 0)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "手续费余额不足");
                        return;
                    }

                    if (提款最高单笔金额 - 提款金额 < 0)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "目标提款金额 大于账户提款金额限制 忽略该笔继续执行");
                        continue;
                    }
                    if (提款最低单笔金额 - 提款金额 > 0)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "目标提款金额 小于账户提款金额限制 忽略该笔继续执行");
                        continue;
                    }

                    var result = dbClient.Ado.UseTran(() =>
                    {
                        dbClient.Ado.ExecuteCommand("UPDATE `table_商户账号` SET `提款余额` = `提款余额` - '" + 提款金额 + "', " +
                            "`手续费余额` = `手续费余额` - '" + 单笔手续费 + "' WHERE `商户ID` = '" + record.商户ID + "';");

                        string 类型 = "提款";

                        table_商户明细手续费 fee = new table_商户明细手续费()
                        {
                            订单号 = "MHFON" + now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)),
                            商户ID = int.Parse(record.商户ID),
                            手续费支出 = 单笔手续费,
                            交易金额 = 提款金额,
                            交易前手续费余额 = 手续费余额,
                            交易后手续费余额 = 手续费余额 - 单笔手续费,
                            类型 = 类型,
                            时间创建 = now
                        };

                        dbClient.Insertable(fee).ExecuteCommand();

                        table_商户明细余额 balance = new table_商户明细余额()
                        {
                            订单号 = "MBON" + now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)),
                            商户ID = int.Parse(record.商户ID),
                            类型 = 类型,
                            手续费 = 单笔手续费.ToString(),
                            交易金额 = 交易金额,
                            交易前账户余额 = 提款余额.ToString(),
                            交易后账户余额 = (提款余额 - 提款金额).ToString(),
                            时间创建 = now
                        };

                        dbClient.Insertable(balance).ExecuteCommand();

                        table_商户明细提款 detail = new table_商户明细提款()
                        {
                            订单号 = "MST" + now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)),
                            商户ID = record.商户ID,
                            交易方卡号 = 交易方卡号,
                            交易方姓名 = 交易方姓名,
                            交易方银行 = 交易方银行,
                            交易金额 = 提款金额,
                            手续费 = 单笔手续费,
                            创建方式 = "手动",
                            备注商户写 = 备注,
                            状态 = "待处理",
                            类型 = 类型,
                            时间创建 = now,
                            订单源IP = ClassLibrary1.ClassAccount.来源IP(),
                            商户提交批次ID组 = OperatorId,
                            商户提交序号 = Sindex++
                        };

                        dbClient.Insertable(detail).ExecuteCommand();
                    });
                    if (!result.IsSuccess)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "网络开小差，请重试");
                    }
                    Thread.Sleep(1000);
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


        private bool ValidateGoogleCode()
        {
            // 验证google  验证码
            string UserName = null;
            //檢測cookie
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"] != null)
                UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["username"]);
            if (UserName != null)
                using (var db = (new DBClient()).GetClient())
                {
                    var data = db.Queryable<Sugar.Enties.table_商户账号>().Where(it => it.商户ID == UserName).First();
                    if (data.二步验证状态 == true)
                    {
                        if (TextGoogleValidate.Text.Length != 6)
                        {
                            ClassLibrary1.ClassMessage.HinXi(Page, "验证码不和规范");
                            return false;
                        }
                        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

                        var result = tfa.ValidateTwoFactorPIN(data.keyga, TextGoogleValidate.Text);
                        if (!result)
                        {
                            ClassLibrary1.ClassMessage.HinXi(Page, "验证码错误");
                            return false;
                        }
                    }
                }
            return true;
        }

        protected void Button_提款订单文本导入_Click(object sender, EventArgs e)
        {
            if (UploadTxt.FileName == "")
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "文本文件导入不存在");
                return;
            }

            var a = Path.GetExtension(UploadTxt.FileName);

            if (Path.GetExtension(UploadTxt.FileName) != ".txt")
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "文本格式不对");
                return;
            }


            if (UploadTxt.PostedFile.ContentLength > (4 * 1024 * 1024))
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "文本文件过大不能导入,小于4m");
                return;
            }
            else
            {

                //string rootPath2 = Request.ApplicationPath;
                //string rootPath1 = Server.MapPath("~/");
                string rootPath = Path.Combine(HttpRuntime.AppDomainAppPath.ToString(), "UploadFile");

                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);

                try
                {
                    var path = Path.Combine(rootPath, UploadTxt.FileName);
                    UploadTxt.SaveAs(path);

                    var ExcelData = TXTToDS(path);
                    if (ExcelData == null)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "文本导入失败");
                        return;
                    }
                    // 操作数据源

                    for (int index = 0; index < ExcelData["交易方卡号"].Count(); index++)
                    {
                        AddNewRowToGrid2(
                          ExcelData["交易方卡号"][index],
                          ExcelData["交易方姓名"][index],
                          ExcelData["交易方银行"][index],
                          ExcelData["交易金额"][index],
                          ExcelData["备注"][index]);

                    }

                }
                catch (Exception ex)
                {

                    ClassLibrary1.ClassMessage.HinXi(Page, "文本导入失败,请你的文本 文档符合本页面表格排版要求(相同),并且必须符合表格所要求的,头行列 交易方卡号	交易方姓名	交易方银行	交易金额，	备注 字段必须有");
                    System.IO.File.Delete(Path.Combine(rootPath, UploadTxt.FileName));
                }

                创建方式_Text.Text = "文本导入";
                System.IO.File.Delete(Path.Combine(rootPath, UploadTxt.FileName));

                ClassLibrary1.ClassMessage.HinXi(Page, "文本导入成功");

            }
        }


        protected void Button_提款订单文档导入_Click(object sender, EventArgs e)
        {

            if (UploadExcel.FileName == "")
            {

                ClassLibrary1.ClassMessage.HinXi(Page, "excel文件导入不存在");
                return;
            }

            var a = Path.GetExtension(UploadExcel.FileName);

            if (Path.GetExtension(UploadExcel.FileName) != ".xlsx" && Path.GetExtension(UploadExcel.FileName) != ".xls")
                ClassLibrary1.ClassMessage.HinXi(Page, "excel格式不对");

            //选择上传的图片
            if (UploadExcel.PostedFile.ContentLength > (4 * 1024 * 1024))
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "excel文件过大不能导入");
            }
            else
            {
                //string rootPath2 = Request.ApplicationPath;
                //string rootPath1 = Server.MapPath("~/");
                string rootPath = Path.Combine(HttpRuntime.AppDomainAppPath.ToString(), "UploadFile");

                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);

                try
                {
                    var path = Path.Combine(rootPath, UploadExcel.FileName);
                    UploadExcel.SaveAs(path);

                    var ExcelData = ExcelToDS(path);
                    if (ExcelData == null)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "excel导入失败");
                        return;
                    }
                    // 操作数据源

                    for (int index = 0; index < ExcelData["交易方卡号"].Count(); index++)
                    {
                        AddNewRowToGrid2(
                          ExcelData["交易方卡号"][index],
                          ExcelData["交易方姓名"][index],
                          ExcelData["交易方银行"][index],
                          ExcelData["交易金额"][index],
                          ExcelData["备注"][index]);

                    }

                }
                catch (Exception ex)
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "excel导入失败,请你的excel 文档符合本页面表格排版要求(相同),并且必须符合表格所要求的,头行列 交易方卡号	交易方姓名	交易方银行	交易金额，	备注 字段必须有");
                    System.IO.File.Delete(Path.Combine(rootPath, UploadExcel.FileName));
                }
                创建方式_Text.Text = "文档导入";

                System.IO.File.Delete(Path.Combine(rootPath, UploadExcel.FileName));
                ClassLibrary1.ClassMessage.HinXi(Page, "excel导入成功");
            }
        }

        private void AddNewRowToGrid2(string 交易方卡号, string 交易方姓名, string 交易方银行, string 交易金额, string 备注)
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

                        dtCurrentTable.Rows[i - 1]["Column1"] = 交易方卡号;
                        dtCurrentTable.Rows[i - 1]["Column2"] = 交易方姓名;
                        dtCurrentTable.Rows[i - 1]["Column3"] = 交易方银行;
                        dtCurrentTable.Rows[i - 1]["Column4"] = 交易金额;
                        dtCurrentTable.Rows[i - 1]["Column5"] = 备注;

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


        public Dictionary<string, List<string>> ExcelToDS(string path)
        {
            Dictionary<string, List<string>> DataTable = null;


            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var s = reader.Name;
                    do { while (reader.Read()) { } } while (reader.NextResult());

                    var headers = new List<string>();
                    DataTable = new Dictionary<string, List<string>>();

                    var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,

                            ReadHeaderRow = rowReader =>
                            {
                                for (var i = 0; i < rowReader.FieldCount; i++)
                                {
                                    headers.Add(Convert.ToString(rowReader.GetValue(i)));
                                    DataTable.Add(headers[i], new List<string>());
                                }
                            },
                            FilterRow = (rowReader) =>
                            {
                                for (var i = 0; i < rowReader.FieldCount; i++)
                                    DataTable[headers[i]].Add(Convert.ToString(rowReader.GetValue(i)));
                                return true;
                            },
                            FilterColumn = (columnReader, columnIndex) =>
                            {
                                return true;
                            }
                        }
                    });
                }
            }
            return DataTable;
        }

        public static Dictionary<string, List<string>> TXTToDS(string path)
        {
            Dictionary<string, List<string>> DataTable = null;
            string[] headArr = null;
            string strReadLine = null;
            DataTable = new Dictionary<string, List<string>>();
            try
            {
                StreamReader srReadFile = new StreamReader(path);
                int i = 0;
                // 读取流直至文件末尾结束
                while (!srReadFile.EndOfStream)
                {
                    if (i == 0)
                    {
                        i++;
                        var headReadLine = srReadFile.ReadLine();
                        headReadLine = Regex.Replace(headReadLine, @"\t+", @" ");
                        headReadLine = Regex.Replace(headReadLine, @"\s+", @" ");

                        //string[] striparr = modified.Split(new string[] { "\\s" }, StringSplitOptions.None);

                        headArr = headReadLine.Split(' ');
                        foreach (string ele in headArr)
                        {
                            DataTable.Add(ele, new List<string>());
                        }
                    }
                    else
                    {
                        strReadLine = srReadFile.ReadLine(); //读取每行数据
                        strReadLine = Regex.Replace(strReadLine, @"\t+", @" ");
                        strReadLine = Regex.Replace(strReadLine, @"\s+", @" ");
                        var strArr = strReadLine.Split(' ');

                        int index = 0;
                        foreach (string ele in strArr)
                        {
                            DataTable[headArr[index]].Add(ele);
                            index++;
                        }

                    }

                }

                // 关闭读取流文件
                srReadFile.Close();

            }
            catch (Exception e)
            {
                DataTable = null;
            }

            return DataTable;
        }

    }
}