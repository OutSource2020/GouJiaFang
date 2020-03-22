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
using SqlSugar;
using Sugar.Enties;

namespace web1.WebsiteMerchant.商户订单
{
    public partial class 商户提款 : System.Web.UI.Page
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

        protected void TextBox_交易金额_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                ((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text = ClassLibrary1.ClassYZ.过滤数字和小数(((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text);
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
            SetPreviousData();
        }

        protected void Button_确认订单发起_Click(object sender, EventArgs e)
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

                                        Button_确认订单发起.Enabled = false;//防止重复操作
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

        private long GetTimeStamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            return (long)System.Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }
        private void 开始执行()
        {
            Button_确认订单发起.Enabled = false;//防止重复操作
            long OperatorId = GetTimeStamp();
            int Sindex = 0;
            using (SqlSugarClient dbClient = new DBClient().GetClient())
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string 交易方卡号 = ((TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1")).Text;
                    string 交易方姓名 = ((TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox2")).Text;
                    string 交易方银行 = ((TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox3")).Text;
                    string 交易金额 = ((TextBox)Gridview1.Rows[i].Cells[4].FindControl("TextBox4")).Text;
                    string 备注 = ((TextBox)Gridview1.Rows[i].Cells[5].FindControl("TextBox5")).Text;

                    if (!ClassLibrary1.ClassYZ.IsNumber(交易金额))
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "金额 不是数字或者小数 忽略该笔继续执行");
                        continue;
                    }

                    string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();
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

                    dbClient.Ado.UseTran(() =>
                    {
                        DateTime now = DateTime.Now;

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
                }
            }
            Response.Redirect("./商户提款记录.aspx");
        }

        private bool ValidateGoogleCode()
        {
            string UserName = null;
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

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("../MerchantOverview/商户首页.aspx");
        }

        protected void Button_识别银行卡名称_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                ((TextBox)Gridview1.Rows[i].Cells[3].FindControl("TextBox3")).Text = ClassLibrary1.ClassBankInfo3a2.BankUtil.getNameOfBank(((TextBox)Gridview1.Rows[i].Cells[1].FindControl("TextBox1")).Text);
            }
        }
    }
}