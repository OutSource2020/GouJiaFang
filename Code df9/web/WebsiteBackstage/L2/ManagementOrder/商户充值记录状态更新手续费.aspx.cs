using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L2.ManagementOrder
{
    public partial class 商户充值记录状态更新手续费 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Buffer = true;
                Response.CacheControl = "no-cache";


                ClassLibrary1.ClassAccount.验证账号管理L1端();
                this.GetCustomer();
                下拉获取银行卡();
                检查状态是否();
            }

        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户充值记录.aspx");
        }

        private void 下拉获取银行卡()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 收款银行卡卡号,收款银行卡名称 from table_后台收款银行卡管理 where 状态 = '启用'";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_后台收款银行卡管理");
            myconn.Close();
            DropDownList_选择银行卡.DataSource = ds.Tables[0].DefaultView;
            DropDownList_选择银行卡.DataTextField = ds.Tables["table_后台收款银行卡管理"].Columns["收款银行卡名称"].ToString();
            DropDownList_选择银行卡.DataValueField = ds.Tables["table_后台收款银行卡管理"].Columns["收款银行卡卡号"].ToString();
            DropDownList_选择银行卡.DataBind();

            myconn.Close();
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
                            this.TextBox_备注后台.Text = dr1["备注后台"].ToString();
                            //this.DropDownList_银行卡.SelectedItem.Value = dr["收款银行卡卡号"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr1["状态"].ToString();


                            string 查询状态是否符合 = dr1["状态"].ToString();
                            if (查询状态是否符合 == "待处理")
                            {

                            }
                            else
                            {
                                TextBox_备注后台.Enabled = false;
                                DropDownList_下拉框1.Enabled = false;
                                Button_变更状态.Enabled = false;
                                //Response.Redirect("商户充值记录手续费.aspx");
                            }


                            if (dr1["充值类型"].ToString() == "充值提款余额")
                            {
                                Response.Redirect("商户充值记录状态更新号余额.aspx?Bianhao=" + 从URL获取值() + "");

                                string redkey = DropDownList_下拉框1.SelectedItem.Value;
                                if (redkey == "成功")
                                {
                                    Response.Redirect("商户充值记录状态更新号余额.aspx?Bianhao=" + 从URL获取值() + "");
                                }
                                if (redkey == "失败")
                                {
                                    Response.Redirect("商户充值记录状态更新号余额.aspx?Bianhao=" + 从URL获取值() + "");
                                }
                            }
                        }
                    }
                }
            }
        }


        protected void Button_变更状态_Click(object sender, EventArgs e)
        {
            if (TextBox_备注后台.Text.Length > 0)
            {

                //判定 DropDownList_选择银行卡 是否空
                if (String.IsNullOrEmpty(DropDownList_选择银行卡.SelectedValue))
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "收款银行卡还未设置或者未启用");
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
            Button_变更状态.Enabled = false;

            using (MySqlConnection con01 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd01 = new MySqlCommand("SELECT 订单号,商户ID,商户银行卡卡号,充值类型,充值金额,备注后台,状态 FROM table_商户明细充值 WHERE 订单号=@订单号", con01))
                {
                    cmd01.Parameters.AddWithValue("@订单号", 从URL获取值());
                    using (MySqlDataAdapter da01 = new MySqlDataAdapter(cmd01))
                    {
                        DataTable images01 = new DataTable();
                        da01.Fill(images01);
                        foreach (DataRow dr01 in images01.Rows)
                        {
                            string 订单内商户ID = dr01["商户ID"].ToString();
                            string 订单内类型 = dr01["充值类型"].ToString();
                            string 订单内交易金额 = dr01["充值金额"].ToString();
                            string 订单内状态 = dr01["状态"].ToString();

                            //充值提款手续费
                            if (dr01["充值类型"].ToString() == "充值提款手续费")
                            {
                                string redkey = DropDownList_下拉框1.SelectedItem.Value;
                                if (redkey == "成功")
                                {
                                    //ClassLibrary1.ClassMessage.HinXi(Page, "充值手续费成功");
                                    //如果订单确定成功的话就充入手续费
                                    //1. 插入 商户明细手续费 增加
                                    //2.修改账户内手续费余额
                                    //3.修改本单状态成功完成

                                    //4.收款银行卡流水 增加记录
                                    //5.收款终端银行卡 增加订单的余额

                                    //查询订单信息
                                    using (MySqlConnection con31 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                    {
                                        using (MySqlCommand cmd31 = new MySqlCommand("SELECT 订单号,商户ID,商户银行卡卡号,充值类型,充值金额,备注后台,状态 FROM table_商户明细充值 WHERE 订单号=@订单号", con31))
                                        {
                                            cmd31.Parameters.AddWithValue("@订单号", 从URL获取值());
                                            using (MySqlDataAdapter da31 = new MySqlDataAdapter(cmd31))
                                            {
                                                DataTable images31 = new DataTable();
                                                da31.Fill(images31);
                                                foreach (DataRow dr31 in images31.Rows)
                                                {
                                                    string 获取商户ID = dr31["商户ID"].ToString();
                                                    string 获取充值余额 = dr31["充值金额"].ToString();

                                                    //1.插入 商户明细手续费 增加
                                                    using (MySqlConnection con查询账户内手续费余额 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        using (MySqlCommand cmd查询账户内手续费余额 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额 FROM table_商户账号 WHERE 商户ID=@商户ID", con查询账户内手续费余额))
                                                        {
                                                            cmd查询账户内手续费余额.Parameters.AddWithValue("@商户ID", 订单内商户ID);
                                                            using (MySqlDataAdapter da查询账户内手续费余额 = new MySqlDataAdapter(cmd查询账户内手续费余额))
                                                            {
                                                                DataTable images查询账户内手续费余额 = new DataTable();
                                                                da查询账户内手续费余额.Fill(images查询账户内手续费余额);
                                                                foreach (DataRow dr查询账户内手续费余额 in images查询账户内手续费余额.Rows)
                                                                {
                                                                    //double 提款余额 = double.Parse(dr查询账户内手续费余额["提款余额"].ToString());
                                                                    //double 手续费余额 = double.Parse(dr查询账户内手续费余额["手续费余额"].ToString());

                                                                    string 生成编号 = "CZSXF" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                                    string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                                    double 交易前手续费余额 = double.Parse(dr查询账户内手续费余额["手续费余额"].ToString());
                                                                    double 交易后手续费余额 = double.Parse(dr查询账户内手续费余额["手续费余额"].ToString()) + double.Parse(订单内交易金额);
                                                                    string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                                    using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                                    {
                                                                        string 有哪些 = "订单号,商户ID,类型,手续费收入,交易前手续费余额,交易后手续费余额,时间创建";
                                                                        string 收哪些 = "@订单号,@商户ID,@类型,@手续费收入,@交易前手续费余额,@交易后手续费余额,@时间创建 ";

                                                                        string str = "insert into table_商户明细手续费(" + 有哪些 + ") values(" + 收哪些 + ")";
                                                                        scon.Open();
                                                                        MySqlCommand command = new MySqlCommand();

                                                                        command.Parameters.AddWithValue("@订单号", 生成编号);
                                                                        command.Parameters.AddWithValue("@商户ID", 订单内商户ID);
                                                                        command.Parameters.AddWithValue("@类型", 订单内类型);
                                                                        command.Parameters.AddWithValue("@手续费收入", 订单内交易金额);
                                                                        command.Parameters.AddWithValue("@交易前手续费余额", 交易前手续费余额);
                                                                        command.Parameters.AddWithValue("@交易后手续费余额", 交易后手续费余额);
                                                                        command.Parameters.AddWithValue("@时间创建", RegisterTime);

                                                                        command.Connection = scon;
                                                                        command.CommandText = str;
                                                                        int obj = command.ExecuteNonQuery();

                                                                        scon.Close();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }


                                                    //2.修改账户内手续费余额
                                                    using (MySqlConnection conmy12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        using (MySqlCommand cmdmy12 = new MySqlCommand("UPDATE table_商户账号 SET 手续费余额= 手续费余额+"+ 获取充值余额 + " where 商户ID=@商户ID ", conmy12))
                                                        {
                                                            cmdmy12.Parameters.AddWithValue("@商户ID", 获取商户ID);

                                                            conmy12.Open();
                                                            cmdmy12.ExecuteNonQuery();
                                                            conmy12.Close();
                                                            //this.SaveImage(filePath);


                                                            //Response.Redirect("商户充值记录.aspx");
                                                        }
                                                    }

                                                    //3.修改本单信息为成功
                                                    string 从URL传来值 = 从URL获取值();
                                                    using (MySqlConnection con13 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        using (MySqlCommand cmd13 = new MySqlCommand("UPDATE table_商户明细充值 SET 备注后台=@备注后台,收款银行卡卡号=@收款银行卡卡号,状态=@状态 WHERE 订单号=@订单号", con13))
                                                        {
                                                            cmd13.Parameters.AddWithValue("@收款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
                                                            cmd13.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                                                            cmd13.Parameters.AddWithValue("@备注后台", TextBox_备注后台.Text);
                                                            cmd13.Parameters.AddWithValue("@订单号", 从URL传来值);

                                                            con13.Open();
                                                            cmd13.ExecuteNonQuery();
                                                            con13.Close();
                                                            //this.SaveImage(filePath);
                                                        }
                                                    }

                                                    //4.插入 收款银行卡流水明细 本单交易
                                                    //4.1查询收款终端管理的 原余额
                                                    using (MySqlConnection con14a = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        using (MySqlCommand cmd14a = new MySqlCommand("SELECT 收款银行卡卡号,收款银行卡余额 FROM table_后台收款银行卡管理 WHERE 收款银行卡卡号=@收款银行卡卡号", con14a))
                                                        {
                                                            cmd14a.Parameters.AddWithValue("@收款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
                                                            using (MySqlDataAdapter da14a = new MySqlDataAdapter(cmd14a))
                                                            {
                                                                DataTable images14a = new DataTable();
                                                                da14a.Fill(images14a);
                                                                foreach (DataRow dr14a in images14a.Rows)
                                                                {
                                                                    double 余额 = double.Parse(dr14a["收款银行卡余额"].ToString()) + double.Parse(订单内交易金额);

                                                                    //4.2插入 收款银行卡流水明细
                                                                    using (MySqlConnection scon14 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                                    {
                                                                        string 生成编号 = "CZHYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                                        string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                                        string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                                        string 类型 = "充值";
                                                                        string 状态 = "成功";

                                                                        string 有哪些 = "订单号,商户ID,收入,余额,收款银行卡卡号,收款银行卡名称,类型,状态,时间创建";
                                                                        string 收哪些 = "@订单号,@商户ID,@收入,@余额,@收款银行卡卡号,@收款银行卡名称,@类型,@状态,@时间创建 ";

                                                                        string str14 = "insert into table_后台收款银行卡流水(" + 有哪些 + ") values(" + 收哪些 + ")";
                                                                        scon14.Open();
                                                                        MySqlCommand command = new MySqlCommand();
                                                                        command.Parameters.AddWithValue("@订单号", 生成编号);
                                                                        command.Parameters.AddWithValue("@商户ID", 订单内商户ID);
                                                                        command.Parameters.AddWithValue("@收入", 订单内交易金额);
                                                                        command.Parameters.AddWithValue("@余额", 余额);
                                                                        command.Parameters.AddWithValue("@收款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);
                                                                        command.Parameters.AddWithValue("@收款银行卡名称", DropDownList_选择银行卡.SelectedItem.Text);
                                                                        command.Parameters.AddWithValue("@类型", 类型);
                                                                        command.Parameters.AddWithValue("@状态", 状态);
                                                                        command.Parameters.AddWithValue("@时间创建", RegisterTime);

                                                                        command.Connection = scon14;
                                                                        command.CommandText = str14;
                                                                        int obj = command.ExecuteNonQuery();
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }

                                                    //5. 修改收款银行卡余额 增加本单的额度
                                                    using (MySqlConnection con15 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        using (MySqlCommand cmd15 = new MySqlCommand("UPDATE table_后台收款银行卡管理 SET 收款银行卡余额 = 收款银行卡余额+"+ 订单内交易金额 + " WHERE 收款银行卡卡号=@收款银行卡卡号 ", con15))
                                                        {
                                                            cmd15.Parameters.AddWithValue("@收款银行卡卡号", DropDownList_选择银行卡.SelectedItem.Value);


                                                            con15.Open();
                                                            cmd15.ExecuteNonQuery();
                                                            con15.Close();
                                                            //this.SaveImage(filePath);
                                                        }
                                                    }


                                                    Response.Redirect("商户充值记录.aspx");
                                                }
                                            }
                                        }
                                    }
                                }
                                if (redkey == "失败")
                                {
                                    //ClassLibrary1.ClassMessage.HinXi(Page, "充值手续费失败");

                                    Button_变更状态.Enabled = false;

                                    //手续费订单失败的话直接设置订单
                                    string 从URL传来值 = 从URL获取值();
                                    using (MySqlConnection con32 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                    {
                                        using (MySqlCommand cmd32 = new MySqlCommand("UPDATE table_商户明细充值 SET 状态=@状态 ,备注后台=@备注后台 WHERE 订单号=@订单号 ", con32))
                                        {
                                            cmd32.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                                            cmd32.Parameters.AddWithValue("@备注后台", TextBox_备注后台.Text);
                                            cmd32.Parameters.AddWithValue("@订单号", 从URL传来值);

                                            con32.Open();
                                            cmd32.ExecuteNonQuery();
                                            con32.Close();
                                            //this.SaveImage(filePath);


                                            Response.Redirect("商户充值记录.aspx");
                                        }
                                    }
                                }


                            }
                            //充值提款余额
                            if (dr01["充值类型"].ToString() == "充值提款余额")
                            {
                                Response.Redirect("商户充值记录状态更新号余额.aspx?Bianhao=" + 从URL获取值() + "");

                                string redkey = DropDownList_下拉框1.SelectedItem.Value;
                                if (redkey == "成功")
                                {
                                    Response.Redirect("商户充值记录状态更新号余额.aspx?Bianhao=" + 从URL获取值() + "");
                                }
                                if (redkey == "失败")
                                {
                                    Response.Redirect("商户充值记录状态更新号余额.aspx?Bianhao=" + 从URL获取值() + "");
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
                            //this.DropDownList_银行卡.SelectedItem.Value = dr["收款银行卡卡号"].ToString();
                            this.DropDownList_下拉框1.SelectedValue = dr["状态"].ToString();


                            string 查询状态是否符合 = dr["状态"].ToString();
                            if (查询状态是否符合 == "待处理")
                            {

                            }
                            else
                            {
                                TextBox_备注后台.Enabled = false;
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