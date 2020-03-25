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
    public partial class 商户提款记录状态更新冲正 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
            }
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
                            string redkey1 = dr1["状态"].ToString();
                            if (redkey1 == "待处理")
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "待处理订单不可以冲正");
                                Response.Redirect("商户提款记录.aspx");
                            }
                            if (redkey1 == "成功")
                            {
                                //如果订单类型为成功 才允许当前页面
                                string redkey2 = dr1["类型"].ToString();
                                if (redkey2 == "提款")
                                {

                                }
                                if (redkey2 == "冲正")
                                {
                                    ClassLibrary1.ClassMessage.HinXi(Page, "已经是冲正");
                                    Response.Redirect("商户提款记录.aspx");
                                }

                            }
                            if (redkey1 == "失败")
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "订单状态已经失败或者类型为冲正");
                                Response.Redirect("商户提款记录.aspx");
                            }


                            

                        }
                    }
                }
            }
        }


        protected void Button_冲正_Click(object sender, EventArgs e)
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
                            if (drCHA["状态"].ToString() == "成功")
                            {
                                //开始更新

                                发出冲正();

                            }
                            else
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "订单已完成,不可操作");
                                //Response.Redirect("代理列表L1.aspx");
                            }

                        }
                    }
                }
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

        private void 发出冲正()//更新出去
        {
            Button_冲正.Enabled = false;
            string 从URL传来值 = 从URL获取值();

      using (SqlSugarClient dbClient = new DBClient().GetClient()){
       
        //1.订单设置为冲正
        //2.修改账户内余额和手续费 退回
        //3.插入余额流水 退回
        //4.插入出款银行卡流水 退款
        //5.插入手续费流水 退回
        //6.管理出款银行卡 退款
        string 生成编号标头 = "DKTC";
        string 生成编号 = 生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
        string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
           
        dbClient.Ado.UseTran(() => { });

        string 提款手续费_生成编号标头 = "DKCZ";
        string 提款手续费_订单号 = 提款手续费_生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
     
        string 账户余额_生成编号标头 = "DKCZ";
        string 账户余额_订单号 = 账户余额_生成编号标头 + DateTime. Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
        
        DateTime now = DateTime.Now;

        string 生成编号_出款流水 = "CZHYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
        string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        dbClient.Ado.UseTran(() => { });
        var result =  dbClient.Ado.UseTran(() =>
           {
             var orderNumInfo = dbClient.Queryable<table_商户明细提款>().Where(it => it.订单号 == 从URL传来值).First();

             var userInfo = dbClient.Queryable<table_商户账号>().Where(it => it.商户ID == orderNumInfo.商户ID).First();

             var outcardInfo = dbClient.Queryable<table_后台出款银行卡管理>().Where(it => it.出款银行卡卡号 == orderNumInfo.出款银行卡卡号).First();
             // 修改提款状态
             dbClient.Updateable<table_商户明细提款>()
             .SetColumns(it => new table_商户明细提款() { 类型 = "冲正",状态 = "失败"})
             .Where(it=>it.订单号== 从URL传来值).ExecuteCommand();

            

             string 冲正_类型 = "订单提款冲正";
            
             table_商户明细手续费 fee = new table_商户明细手续费
             {
               订单号 = 提款手续费_订单号,
               商户ID = Int32.Parse(userInfo.商户ID),
               手续费支出 =  Convert.ToDouble(orderNumInfo.手续费),
               交易金额    =   Convert.ToDouble(orderNumInfo.交易金额),
               交易前手续费余额 = userInfo.手续费余额.Value,
               交易后手续费余额 = userInfo.手续费余额.Value + orderNumInfo.手续费.Value,
               类型 = 冲正_类型,
               时间创建 = now,
             };

             table_商户明细余额 money = new table_商户明细余额
             {
               订单号 = 账户余额_订单号,
               商户ID = Int32.Parse(userInfo.商户ID),
               类型 = 冲正_类型,
               交易金额 = orderNumInfo.交易金额.ToString(),
               交易前账户余额 = userInfo.提款余额.Value.ToString(),
               交易后账户余额 = (userInfo.提款余额.Value + orderNumInfo.交易金额.Value).ToString(),
               状态 = "",
               时间创建 = now,
             };



             table_后台出款银行卡流水 outCardHistory = new table_后台出款银行卡流水
             {
               订单号 = 生成编号_出款流水,
               商户ID = Int32.Parse(userInfo.商户ID),
               余额 = (outcardInfo.出款银行卡余额+orderNumInfo.交易金额),
               类型 = 冲正_类型,
               状态 = "成功",
               时间创建 = now,
               时间交易 = now,
               支出 = Convert.ToDouble(orderNumInfo.交易金额),
               出款银行卡名称 = outcardInfo.出款银行卡主姓名,
               出款银行卡卡号 = outcardInfo.出款银行卡卡号
             };


             //插入退款手续费明细
             dbClient.Insertable<table_商户明细手续费>(fee).ExecuteCommand();
             //插入退款余额明细
             dbClient.Insertable<table_商户明细余额>(money).ExecuteCommand();
              // 插入出款卡记录
             dbClient.Insertable<table_后台出款银行卡流水>(outCardHistory).ExecuteCommand();

             // 退余额
             dbClient.Updateable<table_商户账号>().SetColumns(it => it.提款余额 == it.提款余额.Value + orderNumInfo.交易金额.Value).Where(it => it.商户ID == orderNumInfo.商户ID).ExecuteCommand();
             // 退手续费
             dbClient.Updateable<table_商户账号>().SetColumns(it => it.手续费余额 == it.手续费余额.Value + orderNumInfo.手续费.Value).Where(it => it.商户ID == orderNumInfo.商户ID).ExecuteCommand();
             // 出款银行卡加回money
             dbClient.Updateable<table_后台出款银行卡管理>().SetColumns(it => it.出款银行卡余额 == it.出款银行卡余额.Value + orderNumInfo.交易金额.Value).Where(it=>it.出款银行卡卡号==orderNumInfo.出款银行卡卡号).ExecuteCommand();

           
           });

        

          dbClient.Ado.UseTran(() => { });

        if(!result.IsSuccess){
          ClassLibrary1.ClassMessage.HinXi(Page, "冲正失败，请重试");
        }

      }

      
        }


        private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 订单号,交易方姓名,交易方卡号,交易方银行,交易金额,状态,出款银行卡名称,备注管理写 FROM table_商户明细提款 WHERE 订单号=@订单号", con))
                {
                    cmd.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_订单号.Text = dr["订单号"].ToString();
                            this.Label_交易方.Text = dr["交易方姓名"].ToString();
                            this.Label_交易方卡号.Text = dr["交易方卡号"].ToString();
                            this.Label_交易方银行.Text = dr["交易方银行"].ToString();
                            this.Label_金额.Text = dr["交易金额"].ToString();
                            this.Label_状态.Text = dr["状态"].ToString();
                            this.Label_银行卡.Text = dr["出款银行卡名称"].ToString();
                            this.Label_备注.Text = dr["备注管理写"].ToString();
                        }
                    }
                }
            }
        }


    }
}