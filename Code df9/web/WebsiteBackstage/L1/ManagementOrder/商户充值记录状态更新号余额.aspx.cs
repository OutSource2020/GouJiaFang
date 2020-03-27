using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using Sugar.Enties;

namespace web1.WebsiteBackstage.L1.ManagementOrder
{
    public partial class 商户充值记录状态更新号余额 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
                下拉获取银行卡();

                if (DropDownList_选择银行卡.SelectedIndex == -1)//用于判断下拉框是否有值存在1
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "你可能还没有设定出款银行卡 不可操作");

                }
                //if (DropDownList_选择银行卡.SelectedItem.Value == "")//用于判断下拉框是否有值存在2
                //{
                //    ClassLibrary1.ClassMessage.HinXi(Page, "你可能还没有设定出款银行卡 不可操作");
                //}
            }

            检查状态是否();

        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户充值记录.aspx");
        }

        private void 检查状态是否()
        {
            using (MySqlConnection con1 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd1 = new MySqlCommand("SELECT 订单号,商户ID,商户银行卡卡号,商户充值目标卡号,充值类型,充值金额,备注后台,收款银行卡卡号,状态 FROM table_商户明细充值 WHERE 订单号=@订单号", con1))
                {
                    string 从URL传来值 = 从URL获取值();

                    cmd1.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1))
                    {
                        DataTable images = new DataTable();
                        da1.Fill(images);
                        foreach (DataRow dr1 in images.Rows)
                        {
                            this.Label_订单号.Text = dr1["订单号"].ToString();
                            this.Label_商户ID.Text = dr1["商户ID"].ToString();
                            this.Label_商户银行卡卡号.Text = dr1["商户银行卡卡号"].ToString();
                            this.Label_商户充值目标卡号.Text = dr1["商户充值目标卡号"].ToString();
                            this.Label_充值类型.Text = dr1["充值类型"].ToString();
                            this.Label_充值金额.Text = dr1["充值金额"].ToString();
                            //this.DropDownList_银行卡.SelectedItem.Value = dr["出款银行卡卡号"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr1["状态"].ToString();


                            string 查询状态是否符合 = dr1["状态"].ToString();
                            if (查询状态是否符合 == "待处理")
                            {

                            }
                            else
                            {
                                TextBox_备注后台.Enabled = false;
                                DropDownList_选择银行卡.Enabled = false;
                                DropDownList_下拉框1.Enabled = false;
                                Button_变更状态.Enabled = false;
                                //Response.Redirect("商户充值记录手续费.aspx");
                            }

                            if (dr1["充值类型"].ToString() == "充值提款手续费")
                            {

                                Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");

                                string redkey = DropDownList_下拉框1.SelectedItem.Value;
                                if (redkey == "成功")
                                {
                                    Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");
                                }
                                if (redkey == "失败")
                                {
                                    Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");
                                }


                            }
                        }
                    }
                }
            }
        }


        private void 下拉获取银行卡()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 出款银行卡卡号,出款银行卡名称 from table_后台出款银行卡管理 where 状态 = '启用'";
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
            if (TextBox_备注后台.Text.Length > 0)
            {
                //判定 DropDownList_选择银行卡 是否空
                if (String.IsNullOrEmpty(DropDownList_选择银行卡.SelectedValue))
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "银行卡还未设置或者未启用");
                }
                else
                {

                    //先检查状态是否待处理
                    using (MySqlConnection conCHA = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                    {
                        using (MySqlCommand cmdCHA = new MySqlCommand("SELECT 状态 FROM table_商户明细充值 WHERE 订单号=@订单号", conCHA))
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
            Button_变更状态.Enabled = false;//发出操作后禁止按钮

      DBClient db = new DBClient();
      var dbCilent = db.GetClient();
      DateTime nowTime = DateTime.Now;

      dbCilent.Ado.ExecuteCommand("set session transaction isolation level serializable;");
      // 获取商户充值
      var rechargeRecord =dbCilent.Queryable<table_商户明细充值>().Where(it=>it.订单号== 从URL获取值()).First();
        // 获取出款卡余额
        //充值提款手续费
        if (rechargeRecord.充值类型 == "充值提款手续费")
        {
          Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");

          string redkey = DropDownList_下拉框1.SelectedItem.Value;
          if (redkey == "成功")
          {
            Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");
          }
          if (redkey == "失败")
          {
            Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");
          }
        }
     
      if (rechargeRecord.充值类型 == "充值提款余额")
      {
        string 生成编号2 = "CZHYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
        string 生成编号 = "CZHYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));


   
          string redkey = DropDownList_下拉框1.SelectedItem.Value;
          if (redkey == "成功")
        {
       
          dbCilent.UseTran(() => { });
            if (rechargeRecord.状态!="成功")
            if(  dbCilent.UseTran(() => {
            var busInfo = dbCilent.Queryable<table_商户账号>().Where(it => it.商户ID == rechargeRecord.商户ID.ToString()).First();

            var outCard = dbCilent.Queryable<table_后台出款银行卡管理>().Where(it => it.出款银行卡卡号 == DropDownList_选择银行卡.SelectedItem.Value ).First();

            table_商户明细余额 bus = new table_商户明细余额
            {
              订单号 = 生成编号,
              商户ID = rechargeRecord.商户ID,
              类型 = "充值提款余额",
              交易金额 = rechargeRecord.充值金额.ToString(),
              交易前账户余额 = busInfo.提款余额.Value.ToString(),
              交易后账户余额 = (busInfo.提款余额.Value + rechargeRecord.充值金额).ToString(),
              状态 = "",
              时间创建 = nowTime,
            };


            table_后台出款银行卡流水 outCardHistory = new table_后台出款银行卡流水
            {
              订单号 = 生成编号2,
              商户ID = Convert.ToInt32(rechargeRecord.商户ID),
              余额 = outCard.出款银行卡余额 + rechargeRecord.充值金额,
              类型 = "充值提款余额",
              状态 = "成功",
              时间创建 = nowTime,
              时间交易 = nowTime,
              收入 = Convert.ToDouble(rechargeRecord.充值金额.ToString()),
              出款银行卡名称 = DropDownList_选择银行卡.SelectedItem.Text,
              出款银行卡卡号 = DropDownList_选择银行卡.SelectedItem.Value
            };

            // 插入明细余额
            dbCilent.Insertable<table_商户明细余额>(bus).ExecuteCommand();
            // 插入出款卡流水
            dbCilent.Insertable<table_后台出款银行卡流水>(outCardHistory).ExecuteCommand();
          
            var sql1 = "UPDATE table_商户明细充值 SET  备注后台='" + TextBox_备注后台.Text.ToString() + "',收款银行卡卡号='" + DropDownList_选择银行卡.SelectedItem.Value .ToString()+ "',状态='" + DropDownList_下拉框1.SelectedItem.Value.ToString() + "' WHERE 订单号= '" + rechargeRecord.订单号 + "'  ;";
            
            // 修改订单状态
            dbCilent.Ado.ExecuteCommand(sql1);
            // 更新商户余额

            dbCilent.Updateable<table_商户账号>().UpdateColumns(it => new { it.提款余额 }).Where(it => it.商户ID == rechargeRecord.商户ID.ToString()).ReSetValue(it => it.提款余额 == (it.提款余额 + rechargeRecord.充值金额)).ExecuteCommand();

           
            // 加出款卡
            dbCilent.Updateable<table_后台出款银行卡管理>().UpdateColumns(it => new { it.出款银行卡余额 }).Where(it=>it.出款银行卡卡号 == DropDownList_选择银行卡.SelectedItem.Value).ReSetValue(it => it.出款银行卡余额 == (it.出款银行卡余额 + rechargeRecord.充值金额)).ExecuteCommand();
       

          }).IsSuccess) { ClassLibrary1.ClassMessage.HinXi(Page, "充值成功"); }
           else{ ClassLibrary1.ClassMessage.HinXi(Page, "充值失败"); }
          dbCilent.UseTran(() => { });


        }
        if (redkey == "失败")
        {
          dbCilent.UseTran(() => { });
          if (rechargeRecord.状态 != "失败")
           if( dbCilent.UseTran(() => {

            string 生成编号3 = "THSXF" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

            var busInfo = dbCilent.Queryable<table_商户账号>().Where(it => it.商户ID == rechargeRecord.商户ID.ToString()).First();

              table_商户明细手续费 fee = new table_商户明细手续费
              {
                订单号 = 生成编号3,
                商户ID = Int32.Parse(busInfo.商户ID),
                手续费支出 = Convert.ToDouble(rechargeRecord.产生手续费),
                交易金额 = Convert.ToDouble(rechargeRecord.充值金额),
                交易前手续费余额 = busInfo.手续费余额.Value,
                交易后手续费余额 = busInfo.手续费余额.Value + rechargeRecord.产生手续费,
                类型 = "充值失败",
                时间创建 = nowTime,
              };
              dbCilent.Insertable<table_商户明细手续费>(fee).ExecuteCommand();
          
              dbCilent.Updateable<table_商户账号>().UpdateColumns(it => new { it.手续费余额 }).Where(it => it.商户ID == rechargeRecord.商户ID.ToString()).ReSetValue(it => it.手续费余额 == (it.手续费余额 + rechargeRecord.产生手续费)).ExecuteCommand();

              
            dbCilent.Ado.ExecuteCommand("UPDATE table_商户明细充值 SET  备注后台='" + TextBox_备注后台.Text + "',收款银行卡卡号='" + DropDownList_选择银行卡.SelectedItem.Value + "',状态='" + DropDownList_下拉框1.SelectedItem.Value + "' WHERE 订单号='" + rechargeRecord.订单号 + " '");

        }).IsSuccess) { ClassLibrary1.ClassMessage.HinXi(Page, "充值失败"); } ;
          dbCilent.UseTran(() => { });


        }


    }
      #region
  //    using (MySqlConnection con01 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //{
            //    using (MySqlCommand cmd01 = new MySqlCommand("SELECT 订单号,商户ID,商户银行卡卡号,充值类型,充值金额,产生手续费,备注后台,状态 FROM table_商户明细充值 WHERE 订单号=@订单号", con01))
            //    {
            //        cmd01.Parameters.AddWithValue("@订单号", 从URL获取值());
            //        using (MySqlDataAdapter da01 = new MySqlDataAdapter(cmd01))
            //        {
            //            DataTable images01 = new DataTable();
            //            da01.Fill(images01);
            //            foreach (DataRow dr01 in images01.Rows)
            //            {
            //                string 订单内商户ID = dr01["商户ID"].ToString();
            //                string 订单内类型 = dr01["充值类型"].ToString();
            //                string 订单内交易金额 = dr01["充值金额"].ToString();
            //                string 订单内产生手续费= dr01["产生手续费"].ToString();
            //                string 订单内状态 = dr01["状态"].ToString();

            //                //充值提款手续费
            //                if (dr01["充值类型"].ToString() == "充值提款手续费")
            //                {
            //                    Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");

            //                    string redkey = DropDownList_下拉框1.SelectedItem.Value;
            //                    if (redkey == "成功")
            //                    {
            //                        Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");
            //                    }
            //                    if (redkey == "失败")
            //                    {
            //                        Response.Redirect("商户充值记录状态更新手续费.aspx?Bianhao=" + 从URL获取值() + "");
            //                    }
            //                }
            //                //充值提款余额
            //                if (dr01["充值类型"].ToString() == "充值提款余额")
            //                {
            //                    string redkey = DropDownList_下拉框1.SelectedItem.Value;
            //                    if (redkey == "成功")
            //                    {
            //                        //ClassLibrary1.ClassMessage.HinXi(Page, "充值号余额成功");
            //                        //如果订单确定成功的话就充入号余额
            //                        //1.写入余额变更明细
            //                        //2.修改账户内号余额
            //                        //3.修改本单状态成功完成

            //                        //4.出款银行卡流水 增加记录
            //                        //5.出款终端银行卡 增加订单的余额

            //                        string 从URL传来值 = 从URL获取值();

            //                        //1.插入 余额明细 到 table_商户明细余额
            //                        using (MySqlConnection con查询商户账号 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                        {
            //                            using (MySqlCommand cmd查询商户账号 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额 FROM table_商户账号 WHERE 商户ID=@商户ID", con查询商户账号))
            //                            {
            //                                cmd查询商户账号.Parameters.AddWithValue("@商户ID", 订单内商户ID);
            //                                using (MySqlDataAdapter da查询商户账号 = new MySqlDataAdapter(cmd查询商户账号))
            //                                {
            //                                    DataTable images查询商户账号 = new DataTable();
            //                                    da查询商户账号.Fill(images查询商户账号);
            //                                    foreach (DataRow dr查询商户账号 in images查询商户账号.Rows)
            //                                    {
            //                                        string 提款余额 = dr查询商户账号["提款余额"].ToString();
            //                                        string 手续费余额 = dr查询商户账号["手续费余额"].ToString();

            //                                        Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1,100, 300));
            //                                        string 生成编号 = "CZHYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
            //                                        string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //                                        double 交易前账户余额 = double.Parse(提款余额);
            //                                        double 交易后账户余额 = double.Parse(提款余额) + double.Parse(订单内交易金额);
            //                                        string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            //                                        using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                                        {
            //                                            string 有哪些 = "订单号,商户ID,类型,交易金额,交易前账户余额,交易后账户余额,时间创建";
            //                                            string 收哪些 = "@订单号,@商户ID,@类型,@交易金额,@交易前账户余额,@交易后账户余额,@时间创建 ";

            //                                            string str = "insert into table_商户明细余额(" + 有哪些 + ") values(" + 收哪些 + ")";
            //                                            scon.Open();
            //                                            MySqlCommand command = new MySqlCommand();
            //                                            command.Parameters.AddWithValue("@订单号", 生成编号);
            //                                            command.Parameters.AddWithValue("@商户ID", 订单内商户ID);
            //                                            command.Parameters.AddWithValue("@类型", 订单内类型);
            //                                            command.Parameters.AddWithValue("@交易金额", 订单内交易金额);
            //                                            command.Parameters.AddWithValue("@交易前账户余额", 交易前账户余额);
            //                                            command.Parameters.AddWithValue("@交易后账户余额", 交易后账户余额);
            //                                            command.Parameters.AddWithValue("@时间创建", RegisterTime);

            //                                            command.Connection = scon;
            //                                            command.CommandText = str;
            //                                            int obj = command.ExecuteNonQuery();

            //                                            scon.Close();
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }

            //                        //2. 修改账户内号余额
            //                        using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                        {
            //                            using (MySqlCommand cmd12 = new MySqlCommand("UPDATE table_商户账号 SET 提款余额=提款余额+"+ 订单内交易金额 + "  WHERE 商户ID=@商户ID ", con12))
            //                            {
            //                                cmd12.Parameters.AddWithValue("@商户ID", 订单内商户ID);

            //                                con12.Open();
            //                                cmd12.ExecuteNonQuery();
            //                                con12.Close();
            //                                //this.SaveImage(filePath);
            //                            }
            //                        }

            //                        //3. 修改本单状态成功完成
            //                        using (MySqlConnection con13 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                        {
            //                            using (MySqlCommand cmd13 = new MySqlCommand("UPDATE table_商户明细充值 SET  备注后台=@备注后台,收款银行卡卡号=@收款银行卡卡号,状态=@状态 WHERE 订单号=@订单号 ", con13))
            //                            {
            //                                cmd13.Parameters.AddWithValue("@备注后台", TextBox_备注后台.Text);
            //                                cmd13.Parameters.AddWithValue("@收款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
            //                                cmd13.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
            //                                cmd13.Parameters.AddWithValue("@订单号", 从URL传来值);

            //                                con13.Open();
            //                                cmd13.ExecuteNonQuery();
            //                                con13.Close();
            //                                //this.SaveImage(filePath);
            //                            }
            //                        }

            //                        //4.插入 出款银行卡流水明细 本单交易
            //                        //4.1查询出款终端管理的 原余额
            //                        using (MySqlConnection con14a = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                        {
            //                            using (MySqlCommand cmd14a = new MySqlCommand("SELECT 出款银行卡卡号,出款银行卡余额 FROM table_后台出款银行卡管理 WHERE 出款银行卡卡号=@出款银行卡卡号", con14a))
            //                            {
            //                                cmd14a.Parameters.AddWithValue("@出款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
            //                                using (MySqlDataAdapter da14a = new MySqlDataAdapter(cmd14a))
            //                                {
            //                                    DataTable images14a = new DataTable();
            //                                    da14a.Fill(images14a);
            //                                    foreach (DataRow dr14a in images14a.Rows)
            //                                    {
            //                                        double 余额 = double.Parse(dr14a["出款银行卡余额"].ToString())+ double.Parse(订单内交易金额);

            //                                        //4.2插入 出款银行卡流水明细
            //                                        using (MySqlConnection scon14 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                                        {
            //                                            Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1,100, 300));
            //                                            string 生成编号 = "CZHYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
            //                                            string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //                                            string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //                                            string 类型 = "充值";
            //                                            string 状态 = "成功";
                                                        

            //                                            string 有哪些 = "订单号,商户ID,收入,余额,出款银行卡卡号,出款银行卡名称,类型,状态,时间创建";
            //                                            string 收哪些 = "@订单号,@商户ID,@收入,@余额,@出款银行卡卡号,@出款银行卡名称,@类型,@状态,@时间创建 ";

            //                                            string str14 = "insert into table_后台出款银行卡流水(" + 有哪些 + ") values(" + 收哪些 + ")";
            //                                            scon14.Open();
            //                                            MySqlCommand command = new MySqlCommand();
            //                                            command.Parameters.AddWithValue("@订单号", 生成编号);
            //                                            command.Parameters.AddWithValue("@商户ID", 订单内商户ID);
            //                                            command.Parameters.AddWithValue("@收入", 订单内交易金额);
            //                                            command.Parameters.AddWithValue("@余额", 余额);
            //                                            command.Parameters.AddWithValue("@出款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
            //                                            command.Parameters.AddWithValue("@出款银行卡名称", DropDownList_选择银行卡.SelectedItem.Text);
            //                                            command.Parameters.AddWithValue("@类型", 类型);
            //                                            command.Parameters.AddWithValue("@状态", 状态);
            //                                            command.Parameters.AddWithValue("@时间创建", RegisterTime);

            //                                            command.Connection = scon14;
            //                                            command.CommandText = str14;
            //                                            int obj = command.ExecuteNonQuery();

            //                                            scon14.Close();
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }



                                    

            //                        //5. 修改出款银行卡余额 增加本单的额度
            //                        using (MySqlConnection con15 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                        {
            //                            using (MySqlCommand cmd15 = new MySqlCommand("UPDATE table_后台出款银行卡管理 SET 出款银行卡余额=出款银行卡余额+"+ 订单内交易金额 + " WHERE 出款银行卡卡号=@出款银行卡卡号 ", con15))
            //                            {
            //                                cmd15.Parameters.AddWithValue("@出款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);

            //                                con15.Open();
            //                                cmd15.ExecuteNonQuery();
            //                                con15.Close();
            //                                //this.SaveImage(filePath);
            //                            }
            //                        }


            //                        Response.Redirect("商户充值记录.aspx");
            //                    }
            //                    if (redkey == "失败")
            //                    {
            //                        //ClassLibrary1.ClassMessage.HinXi(Page, "充值号余额失败");
            //                        //充值失败-充值余额
            //                        //1. 插入 退回手续费明细
            //                        //2. 退回手续费
            //                        //3. 设置本单失败

            //                        Button_变更状态.Enabled = false;//发出操作后禁止按钮

            //                        //获取订单信息
            //                        string 从URL传来值 = 从URL获取值();
            //                        using (MySqlConnection con25 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                        {
            //                            using (MySqlCommand cmd25 = new MySqlCommand("SELECT 订单号,商户ID,充值类型,充值金额,产生手续费 FROM table_商户明细充值 WHERE 订单号=@订单号", con25))
            //                            {
            //                                cmd25.Parameters.AddWithValue("@订单号", 从URL传来值);
            //                                using (MySqlDataAdapter da25 = new MySqlDataAdapter(cmd25))
            //                                {
            //                                    DataTable images25 = new DataTable();
            //                                    da25.Fill(images25);
            //                                    foreach (DataRow dr25 in images25.Rows)
            //                                    {
            //                                        string 获取商户ID = dr25["商户ID"].ToString();
            //                                        double 获取产生手续费 = double.Parse(dr25["产生手续费"].ToString());


            //                                        //1. 插入订单-手续费明细 退回
            //                                        using (MySqlConnection con查询商户账号 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                                        {
            //                                            using (MySqlCommand cmd查询商户账号 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额 FROM table_商户账号 WHERE 商户ID=@商户ID", con查询商户账号))
            //                                            {
            //                                                cmd查询商户账号.Parameters.AddWithValue("@商户ID", 订单内商户ID);
            //                                                using (MySqlDataAdapter da查询商户账号 = new MySqlDataAdapter(cmd查询商户账号))
            //                                                {
            //                                                    DataTable images查询商户账号 = new DataTable();
            //                                                    da查询商户账号.Fill(images查询商户账号);
            //                                                    foreach (DataRow dr查询商户账号 in images查询商户账号.Rows)
            //                                                    {
            //                                                        string 手续费余额 = dr查询商户账号["手续费余额"].ToString();
            //                                                        string 产生手续费 = 订单内产生手续费;

            //                                                        string 生成编号 = "THSXF" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
            //                                                        string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //                                                        string 类型 = "退"+订单内类型;
            //                                                        double 交易前手续费余额 = double.Parse(手续费余额);
            //                                                        double 交易后手续费余额 = double.Parse(手续费余额) + double.Parse(产生手续费);
            //                                                        string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            //                                                        using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                                                        {
            //                                                            string 有哪些 = "订单号,商户ID,类型,手续费收入,交易前手续费余额,交易后手续费余额,时间创建";
            //                                                            string 收哪些 = "@订单号,@商户ID,@类型,@手续费收入,@交易前手续费余额,@交易后手续费余额,@时间创建 ";

            //                                                            string str = "insert into table_商户明细手续费(" + 有哪些 + ") values(" + 收哪些 + ")";
            //                                                            scon.Open();
            //                                                            MySqlCommand command = new MySqlCommand();
            //                                                            command.Parameters.AddWithValue("@订单号", 生成编号);
            //                                                            command.Parameters.AddWithValue("@商户ID", 订单内商户ID);
            //                                                            command.Parameters.AddWithValue("@类型", 类型);
            //                                                            command.Parameters.AddWithValue("@手续费收入", 产生手续费);
            //                                                            command.Parameters.AddWithValue("@交易前手续费余额", 交易前手续费余额);
            //                                                            command.Parameters.AddWithValue("@交易后手续费余额", 交易后手续费余额);
            //                                                            command.Parameters.AddWithValue("@时间创建", RegisterTime);

            //                                                            command.Connection = scon;
            //                                                            command.CommandText = str;
            //                                                            int obj = command.ExecuteNonQuery();

            //                                                            scon.Close();
            //                                                        }
            //                                                    }
            //                                                }
            //                                            }
            //                                        }


            //                                        //2.退回商户的手续费
            //                                        using (MySqlConnection con26 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                                        {
            //                                            using (MySqlCommand cmd26 = new MySqlCommand("UPDATE table_商户账号 SET 手续费余额 = 手续费余额+"+ 获取产生手续费 + " WHERE 商户ID=@商户ID ", con26))
            //                                            {
            //                                                cmd26.Parameters.AddWithValue("@商户ID", 获取商户ID);

            //                                                con26.Open();
            //                                                cmd26.ExecuteNonQuery();
            //                                                con26.Close();
            //                                                //this.SaveImage(filePath);
            //                                            }
            //                                        }

            //                                        //3.修改本单状态失败完成
            //                                        using (MySqlConnection con27 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            //                                        {
            //                                            using (MySqlCommand cmd27 = new MySqlCommand("UPDATE table_商户明细充值 SET  备注后台=@备注后台 , 收款银行卡卡号=@收款银行卡卡号 , 状态=@状态 WHERE 订单号=@订单号 ", con27))
            //                                            {
            //                                                cmd27.Parameters.AddWithValue("@备注后台", TextBox_备注后台.Text);
            //                                                cmd27.Parameters.AddWithValue("@收款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
            //                                                cmd27.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
            //                                                cmd27.Parameters.AddWithValue("@订单号", 从URL传来值);

            //                                                con27.Open();
            //                                                cmd27.ExecuteNonQuery();
            //                                                con27.Close();
            //                                                //this.SaveImage(filePath);
            //                                                //Response.Redirect("商户充值记录.aspx");

            //                                            }
            //                                        }

            //                                        Response.Redirect("商户充值记录.aspx");

            //                                    }

            //                                }
            //                            }
            //                        }
            //                    }
            //                }



            //            }
            //        }
            //    }
            //}
      #endregion
    }


    private void GetCustomer()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 订单号,商户ID,商户银行卡卡号,充值类型,充值金额,备注后台,收款银行卡卡号,状态 FROM table_商户明细充值 WHERE 订单号=@订单号", con))
                {
                    cmd.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_订单号.Text = dr["订单号"].ToString();
                            this.Label_商户ID.Text = dr["商户ID"].ToString();
                            this.Label_商户银行卡卡号.Text = dr["商户银行卡卡号"].ToString();
                            this.Label_充值类型.Text = dr["充值类型"].ToString();
                            this.Label_充值金额.Text = dr["充值金额"].ToString();
                            this.TextBox_备注后台.Text = dr["备注后台"].ToString();
                            //this.DropDownList_银行卡.SelectedItem.Value = dr["出款银行卡卡号"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();


                            string 查询状态是否符合 = dr["状态"].ToString();
                            if (查询状态是否符合 == "待处理")
                            {

                            }
                            else
                            {
                                TextBox_备注后台.Enabled = false;
                                DropDownList_选择银行卡.Enabled = false;
                                DropDownList_下拉框1.Enabled = false;
                                Button_变更状态.Enabled = false;
                                //Response.Redirect("商户充值记录手续费.aspx");
                            }
                        }
                    }
                }
            }
        }

    }
}