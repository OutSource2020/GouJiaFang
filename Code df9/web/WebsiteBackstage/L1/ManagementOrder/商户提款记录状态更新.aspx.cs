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
using SqlSugar;
using Sugar.Enties;

namespace web1.WebsiteBackstage.L1.ManagementOrder
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
                                        //Response.Redirect("代理列表L1.aspx");
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
                    //Label1.Text = "是符合要求字符";

                    //获得传值
                    string URL传来值 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(Request.QueryString["Bianhao"]);
                    return URL传来值;
                }
                else
                {
                    //Label1.Text = "是no符合要求字符";

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
            string 从URL传来值 = 从URL获取值();
            string 出款银行卡卡号 = DropDownList_选择银行卡.SelectedItem.Value;
            double 本单交易金额 = double.Parse(Label_金额.Text);
            DateTime now = DateTime.Now;

            using (SqlSugarClient dbClient = new DBClient().GetClient())
            {
             var result=   dbClient.Ado.UseTran(() =>
                {
                    if (设置订单的状态 == "成功")
                    {
                        var data = dbClient.Queryable<table_后台出款银行卡管理>()
                        .Where(it => it.出款银行卡卡号 == 出款银行卡卡号);
                        if (data.Count() == 0)
                            return;

                        var record = data.First();
                        if (record.出款银行卡最小交易金额 - 本单交易金额 > 0)
                        {
                            ClassLibrary1.ClassMessage.HinXi(Page, "本单金额小于出款银行卡限制的 最小交易金额");
                            return;
                        }

                        if ((record.出款银行卡余额 - 本单交易金额) < 0)
                        {
                            ClassLibrary1.ClassMessage.HinXi(Page, "出款银行卡余额不足");
                            return;
                        }

                        dbClient.Ado.ExecuteCommand("UPDATE `table_商户明细提款` SET `备注管理写`=@备注管理写, `状态`=@状态, `时间完成`=@时间完成, `出款银行卡名称`=@出款银行卡名称, `出款银行卡卡号`=@出款银行卡卡号, `操作员`=@操作员 WHERE `订单号`=@订单号 ", 
                            new {
                                备注管理写 =  TextBox_备注.Text,
                                状态 =  DropDownList_下拉框1.SelectedItem.Value,
                                时间完成 =  now,
                                出款银行卡名称 =  DropDownList_选择银行卡.SelectedItem.Text,
                                出款银行卡卡号 =  DropDownList_选择银行卡.SelectedItem.Value,
                                操作员 =  ClassLibrary1.ClassAccount.检查管理L1端cookie2(),
                                订单号 =  从URL获取值()
                            });

                        var record1 = dbClient.Queryable<table_商户明细提款>().Where(it => it.订单号 == 从URL传来值).First();
                        dbClient.Ado.ExecuteCommand("UPDATE `table_后台出款银行卡管理` SET `出款银行卡余额`=`出款银行卡余额`-" + record1.交易金额 + " WHERE `出款银行卡卡号`=@出款银行卡卡号", new { 出款银行卡卡号 = DropDownList_选择银行卡.SelectedItem.Value });

                        string 生成编号1 = "" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                        string 时间创建1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        string 类型 = "订单提款出款";
                        string 状态 = "成功";
                        double 余额 = System.Convert.ToDouble(record.出款银行卡余额) - System.Convert.ToDouble(record1.交易金额);
                        table_后台出款银行卡流水 t = new table_后台出款银行卡流水()
                        {
                            订单号 = 生成编号1,
                            商户ID = Int32.Parse(record1.商户ID),
                            支出 = record1.交易金额,
                            余额 = 余额,
                            出款银行卡卡号 = DropDownList_选择银行卡.SelectedItem.Value,
                            出款银行卡名称 = DropDownList_选择银行卡.SelectedItem.Text,
                            类型 = 类型,
                            状态 = 状态,
                            时间创建 = now
                        };
                        dbClient.Insertable(t).ExecuteCommand();
                    }
                    else if (设置订单的状态 == "失败")
                    {
                        var record = dbClient.Queryable<table_商户明细提款>()
                        .Where(it => it.订单号 == 从URL传来值).First();

                        var record1 = dbClient.Queryable<table_商户账号>()
                        .Where(it => it.商户ID == record.商户ID).First();

                        dbClient.Ado.ExecuteCommand("UPDATE `table_商户账号` SET `提款余额`=`提款余额`+" + record.交易金额 + " , 手续费余额= 手续费余额+" + record.手续费 + " where 商户ID=@商户ID", new { 商户ID = record.商户ID });

                        table_商户明细手续费 t = new table_商户明细手续费()
                        {
                            订单号 = "TKSXF" + now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)),
                            商户ID = Int32.Parse(record.商户ID),
                            手续费支出 = record.手续费,
                            交易金额 = record.交易金额,
                            交易前手续费余额 = record1.手续费余额,
                            交易后手续费余额 = record1.手续费余额.Value + record.手续费.Value,
                            类型 = "订单出款退款",
                            时间创建 =now
                        };
                        dbClient.Insertable(t).ExecuteCommand();

                        table_商户明细余额 t1 = new table_商户明细余额()
                        {
                            订单号 = "TKYE" + now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)),
                            商户ID = Int32.Parse(record.商户ID),
                            类型 = "订单出款退款",
                            交易金额 = record.交易金额.Value.ToString(),
                            交易前账户余额 = record1.提款余额.Value.ToString(),
                            交易后账户余额 = (record1.提款余额.Value + record.交易金额.Value).ToString(),
                            状态 = "",
                            时间创建 = now
                        };
                        dbClient.Insertable(t1).ExecuteCommand();

                        dbClient.Ado.ExecuteCommand("UPDATE `table_商户明细提款` SET `备注管理写`=@备注管理写, `状态`=@状态, `时间完成`=@时间完成, `操作员`=@操作员 WHERE `订单号`=@订单号", new
                        {
                            备注管理写 = TextBox_备注.Text,
                            状态 = DropDownList_下拉框1.SelectedItem.Value,
                            时间完成 = now,
                            操作员 = ClassLibrary1.ClassAccount.检查管理L1端cookie2(),
                            订单号 = 从URL获取值()
                        });
                    }
                });

        if (!result.IsSuccess){
          ClassLibrary1.ClassMessage.HinXi(Page, "操作失败");
        }
            }
           
            Response.Redirect("商户提款记录.aspx");
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