using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
//using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L2.ManagementOrder
{
    public partial class 商户提款记录状态更新 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
                下拉获取银行卡();
            }
            检查状态是否();
           
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户提款记录.aspx");
        }

        private void 检查状态是否()
        {
            using (MySqlConnection con1 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd1 = new MySqlCommand("SELECT 订单号,商户ID,类型,状态 FROM table_商户明细提款 WHERE 订单号=@订单号", con1))
                {
                    string 从URL传来值 = 从URL获取值();

                    cmd1.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1))
                    {
                        DataTable images = new DataTable();
                        da1.Fill(images);
                        foreach (DataRow dr1 in images.Rows)
                        {
                            string redkey = dr1["状态"].ToString();
                            if (redkey == "待处理")
                            {
                                Button_启动冲正.Enabled = false;
                            }
                            if (redkey == "成功")
                            {
                                Button_启动冲正.Enabled = true;
                                DropDownList_下拉框1.Enabled = false;
                                DropDownList_选择银行卡.Enabled = false;
                                TextBox_备注.Enabled = false;
                                Button_变更状态.Enabled = false;
                            }
                            if (redkey == "失败")
                            {
                                Button_启动冲正.Enabled = false;
                                DropDownList_下拉框1.Enabled = false;
                                DropDownList_选择银行卡.Enabled = false;
                                TextBox_备注.Enabled = false;
                                Button_变更状态.Enabled = false;
                            }
                            if (redkey == "冲正")
                            {
                                Button_启动冲正.Enabled = false;
                                Button_变更状态.Enabled = false;
                                ClassLibrary1.ClassMessage.HinXi(Page, "已经是冲正");
                                Response.Redirect("商户提款记录.aspx");
                            }

                        }
                    }
                }
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
            DropDownList_选择银行卡.DataSource = ds.Tables[0].DefaultView;
            DropDownList_选择银行卡.DataTextField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡名称"].ToString();
            DropDownList_选择银行卡.DataValueField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡卡号"].ToString();
            DropDownList_选择银行卡.DataBind();

            myconn.Close();
        }

        protected void Button_变更状态_Click(object sender, EventArgs e)
        {
            if (TextBox_备注.Text.Length > 0)
            {
                //判定 DropDownList_选择银行卡 是否空
                if (String.IsNullOrEmpty(DropDownList_选择银行卡.SelectedValue))
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "出款银行卡还未设置或者未启用");
                }
                else
                {

                    //先检查状态是否待处理
                    using (MySqlConnection conCHA = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                    {
                        using (MySqlCommand cmdCHA = new MySqlCommand("SELECT 状态 FROM table_商户明细提款 WHERE 订单号=@订单号", conCHA))
                        {
                            cmdCHA.Parameters.AddWithValue("@订单号", 从URL获取值());
                            using (MySqlDataAdapter daCHA = new MySqlDataAdapter(cmdCHA))
                            {
                                DataTable imagesCHA = new DataTable();
                                daCHA.Fill(imagesCHA);
                                foreach (DataRow drCHA in imagesCHA.Rows)
                                {
                                    if (drCHA["状态"].ToString() == "待处理")
                                    {
                                        //开始更新

                                        this.操作更新();


                                    }
                                    else
                                    {
                                        ClassLibrary1.ClassMessage.HinXi(Page, "订单已经处理过");
                                        //Response.Redirect("代理列表L2.aspx");
                                    }

                                }
                            }
                        }
                    }


                }

                
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "备注未填写");
            }
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
                    //LabeL2.Text = "是符合要求字符";

                    //获得传值
                    string URL传来值 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(Request.QueryString["Bianhao"]);
                    return URL传来值;
                }
                else
                {
                    //LabeL2.Text = "是no符合要求字符";

                    //URL传来值 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(Request.QueryString["Bianhao"]);
                    Response.Redirect("./404.aspx");

                    return null;
                }
            }

            return null;
        }

        private void 操作更新()//更新出去
        {
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
                    Button_变更状态.Enabled = false;
                    操作开始();
                }
            }
        }

        private void 操作开始()
        {
            string 设置订单的状态 = DropDownList_下拉框1.SelectedItem.Value;
            if (设置订单的状态 == "成功")
            {
                string 从URL传来值 = 从URL获取值();

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

                                double 本单交易金额 = double.Parse(Label_金额.Text);

                                if (银行卡最小交易限额 <= 本单交易金额)
                                {
                                    if ((出款银行卡余额 - 本单交易金额) >= 0)
                                    {
                                        //1.设置订单状态 成功
                                        string 时间完成 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                        using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                        {
                                            using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户明细提款 SET 备注管理写=@备注管理写 , 状态=@状态 ,时间完成=@时间完成 , 出款银行卡名称=@出款银行卡名称 , 出款银行卡卡号=@出款银行卡卡号 , 操作员=@操作员 WHERE 订单号=@订单号 ", con))
                                            {
                                                cmd.Parameters.AddWithValue("@备注管理写", TextBox_备注.Text);
                                                cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                                                cmd.Parameters.AddWithValue("@时间完成", 时间完成);
                                                cmd.Parameters.AddWithValue("@出款银行卡名称", DropDownList_选择银行卡.SelectedItem.Text);
                                                cmd.Parameters.AddWithValue("@出款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
                                                cmd.Parameters.AddWithValue("@操作员", ClassLibrary1.ClassAccount.检查管理L2端cookie2());
                                                cmd.Parameters.AddWithValue("@订单号", 从URL获取值());

                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }

                                        //2.1扣除出款银行卡内的余额
                                        using (MySqlConnection con20 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                        {
                                            using (MySqlCommand cmd20 = new MySqlCommand("SELECT 订单号,商户ID,交易金额,状态 FROM table_商户明细提款 WHERE 订单号=@订单号", con20))
                                            {
                                                cmd20.Parameters.AddWithValue("@订单号", 从URL传来值);
                                                using (MySqlDataAdapter da20 = new MySqlDataAdapter(cmd20))
                                                {
                                                    DataTable images20 = new DataTable();
                                                    da20.Fill(images20);
                                                    foreach (DataRow dr20 in images20.Rows)
                                                    {
                                                        //2.2扣除出款银行卡内的余额
                                                        string 商户ID = dr20["商户ID"].ToString();
                                                        string 本次支出 = dr20["交易金额"].ToString();

                                                        using (MySqlConnection con21 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                        {
                                                            using (MySqlCommand cmd21 = new MySqlCommand("UPDATE table_后台出款银行卡管理 SET 出款银行卡余额=出款银行卡余额-"+ 本次支出 + " WHERE 出款银行卡卡号=@出款银行卡卡号 ", con21))
                                                            {
                                                                cmd21.Parameters.AddWithValue("@出款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);

                                                                con21.Open();
                                                                cmd21.ExecuteNonQuery();
                                                                con21.Close();
                                                            }
                                                        }

                                                        //3.插入 出款银行卡流水明细 本单交易 本次转出费用的记录
                                                        //3.1查询出款终端管理的 原余额

                                                        double 余额 = System.Convert.ToDouble(出款银行卡余额) - System.Convert.ToDouble(本次支出);

                                                        //3.2插入 出款银行卡流水明细
                                                        using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                        {
                                                            Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 300));
                                                            string 生成编号1 = "" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                            string 时间创建1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                            string 类型 = "订单提款出款";
                                                            string 状态 = "成功";

                                                            string 哪个表1 = "table_后台出款银行卡流水";
                                                            string 写哪些1 = "订单号,商户ID,支出,余额,出款银行卡卡号,出款银行卡名称,类型,状态,时间创建";
                                                            string 写这些1 = "@订单号,@商户ID,@支出,@余额,@出款银行卡卡号,@出款银行卡名称,@类型,@状态,@时间创建";

                                                            string str = "insert into " + 哪个表1 + "(" + 写哪些1 + ") values(" + 写这些1 + ")";

                                                            scon.Open();
                                                            MySqlCommand command = new MySqlCommand();

                                                            command.Parameters.AddWithValue("@订单号", 生成编号1);
                                                            command.Parameters.AddWithValue("@商户ID", 商户ID);
                                                            command.Parameters.AddWithValue("@支出", 本次支出);
                                                            command.Parameters.AddWithValue("@余额", 余额);
                                                            command.Parameters.AddWithValue("@出款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
                                                            command.Parameters.AddWithValue("@出款银行卡名称", DropDownList_选择银行卡.SelectedItem.Text);
                                                            command.Parameters.AddWithValue("@类型", 类型);
                                                            command.Parameters.AddWithValue("@状态", 状态);
                                                            command.Parameters.AddWithValue("@时间创建", 时间创建1);

                                                            command.Connection = scon;
                                                            command.CommandText = str;
                                                            int obj = command.ExecuteNonQuery();

                                                            scon.Close();
                                                        }

                                                    }
                                                }
                                            }
                                        }


                                        Response.Redirect("商户提款记录.aspx");
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
                string 从URL传来值 = 从URL获取值();
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

                                                    using (MySqlCommand cmdmy2 = new MySqlCommand("UPDATE table_商户账号 SET 提款余额=提款余额+"+ 交易金额 + " , 手续费余额= 手续费余额+"+ 手续费 + " where 商户ID=@商户ID ", conmy2))
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
                                                    string 提款手续费_生成编号标头 = "TKSXF";
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
                                                    string 账户余额_订单号 = "TKYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
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
                                                    using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户明细提款 SET 备注管理写=@备注管理写 , 状态=@状态,时间完成=@时间完成 , 操作员=@操作员 WHERE 订单号=@订单号 ", con))
                                                    {
                                                        cmd.Parameters.AddWithValue("@备注管理写", TextBox_备注.Text);
                                                        cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                                                        cmd.Parameters.AddWithValue("@时间完成", 时间完成);
                                                        cmd.Parameters.AddWithValue("@操作员", ClassLibrary1.ClassAccount.检查管理L2端cookie2());
                                                        cmd.Parameters.AddWithValue("@订单号", 从URL获取值());

                                                        con.Open();
                                                        cmd.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                }


                                                Response.Redirect("商户提款记录.aspx");

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


        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 订单号,商户ID,交易方姓名,交易方卡号,交易方银行,交易金额,状态 FROM table_商户明细提款 WHERE 订单号=@订单号", con))
                {
                    cmd.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.Label_订单号.Text = dr["订单号"].ToString();
                            this.Label_交易方.Text = dr["交易方姓名"].ToString();
                            this.Label_交易方卡号.Text = dr["交易方卡号"].ToString();
                            this.Label_交易方银行.Text = dr["交易方银行"].ToString();
                            this.Label_金额.Text = dr["交易金额"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();
                            //this.imgCustomer.ImageUrl = "~/images/" + dr["CustomerId"].ToString() + ".jpg";
                        }
                    }
                }
            }
        }

        protected void Button_启动冲正_Click(object sender, EventArgs e)
        {
            string 从URL传来值 = 从URL获取值();
            Response.Redirect("商户提款记录状态更新冲正.aspx?Bianhao=" + 从URL传来值 + "");
        }

    }
}