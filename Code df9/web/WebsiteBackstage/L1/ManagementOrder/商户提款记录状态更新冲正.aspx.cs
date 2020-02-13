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

            using (MySqlConnection con1 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd1 = new MySqlCommand("SELECT 订单号,商户ID,出款银行卡名称,出款银行卡卡号,类型,状态,交易金额,手续费 FROM table_商户明细提款 WHERE 订单号=@订单号", con1))
                {
                    string 从URL传来值 = 从URL获取值();
                    cmd1.Parameters.AddWithValue("@订单号", 从URL传来值);
                    using (MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1))
                    {
                        DataTable images = new DataTable();
                        da1.Fill(images);
                        foreach (DataRow dr1 in images.Rows)
                        {
                            string 读订单商户ID = dr1["商户ID"].ToString();
                            double 读订单提款金额 = double.Parse(dr1["交易金额"].ToString());
                            double 读订单手续费 = double.Parse(dr1["手续费"].ToString());
                            string 读订单内出款银行卡名称 = dr1["出款银行卡名称"].ToString();
                            string 读订单内出款银行卡卡号 = dr1["出款银行卡卡号"].ToString();



                            //string redkey1 = dr1["状态"].ToString();
                            //if (redkey1 == "待处理")
                            //{
                            //    ClassLibrary1.ClassMessage.HinXi(Page, "待处理订单不可以冲正");
                            //    Response.Redirect("商户提款记录.aspx");
                            //}
                            //if (redkey1 == "成功")
                            //{
                            //    //如果订单类型为成功 才允许当前页面
                            //    string redkey2 = dr1["类型"].ToString();
                            //    if (redkey2 == "提款")
                            //    {

                            //    }
                            //    if (redkey2 == "冲正")
                            //    {
                            //        ClassLibrary1.ClassMessage.HinXi(Page, "已经是冲正");
                            //        Response.Redirect("商户提款记录.aspx");
                            //    }

                            //}
                            //if (redkey1 == "失败")
                            //{
                            //    ClassLibrary1.ClassMessage.HinXi(Page, "订单状态已经失败或者类型为冲正");
                            //    Response.Redirect("商户提款记录.aspx");
                            //}


                            


                            //1.订单设置为冲正
                            //2.修改账户内余额和手续费 退回
                            //3.插入余额流水 退回
                            //4.插入出款银行卡流水 退款
                            //5.插入手续费流水 退回
                            //6.管理出款银行卡 退款

                            //1. 将这个订单状态改为冲正
                            string 时间修改 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("UPDATE table_商户明细提款 SET 类型='冲正',状态='失败', 时间完成=@时间完成 WHERE 订单号=@订单号 ", con))
                                {
                                    cmd.Parameters.AddWithValue("@时间完成", 时间修改);
                                    cmd.Parameters.AddWithValue("@订单号", 从URL获取值());

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    //this.SaveImage(filePath);
                                    //Response.Redirect(Request.Url.AbsoluteUri, false);
                                    //Response.Redirect("商户提款记录.aspx");

                                }
                            }

                            //2. 查询这个订单的商户ID信息
                            using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd12 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额 FROM table_商户账号 WHERE 商户ID=@商户ID", con12))
                                {
                                    cmd12.Parameters.AddWithValue("@商户ID", "" + dr1["商户ID"].ToString() + "");
                                    using (MySqlDataAdapter da12 = new MySqlDataAdapter(cmd12))
                                    {
                                        DataTable images12 = new DataTable();
                                        da12.Fill(images12);
                                        foreach (DataRow dr12 in images12.Rows)
                                        {
                                            string 生成编号标头 = "DKTC";
                                            string 生成编号 = 生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

                                            string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


                                            //3. 商户手续费交易明细 提款
                                            using (MySqlConnection scon13 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                            {
                                                string 提款手续费_生成编号标头 = "DKCZ";
                                                string 提款手续费_订单号 = 提款手续费_生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

                                                string 提款手续费_商户ID = dr1["商户ID"].ToString();

                                                double 提款手续费_手续费 = 读订单手续费;
                                                double 提款手续费_交易金额 = 读订单提款金额;
                                                double 提款手续费_交易前手续费余额 = double.Parse(dr12["手续费余额"].ToString());
                                                double 提款手续费_交易后手续费余额 = double.Parse(dr12["手续费余额"].ToString()) + 读订单手续费;
                                                string 提款手续费_类型 = "订单提款冲正";
                                                string 提款手续费_时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                string 手续费交易明细哪个表 = " table_商户明细手续费 ";
                                                string 手续费交易明细写哪些 = "订单号,商户ID,手续费收入,交易金额,交易前手续费余额,交易后手续费余额,类型,时间创建";
                                                string 手续费交易明细写这些 = "@订单号,@商户ID,@手续费收入,@交易金额,@交易前手续费余额,@交易后手续费余额,@类型,@时间创建 ";


                                                string str = "insert into " + 手续费交易明细哪个表 + "(" + 手续费交易明细写哪些 + ") values(" + 手续费交易明细写这些 + ")";

                                                scon13.Open();
                                                MySqlCommand command = new MySqlCommand();

                                                command.Parameters.AddWithValue("@订单号", 提款手续费_订单号);
                                                command.Parameters.AddWithValue("@商户ID", 提款手续费_商户ID);
                                                command.Parameters.AddWithValue("@手续费收入", 提款手续费_手续费);
                                                command.Parameters.AddWithValue("@交易金额", 提款手续费_交易金额);
                                                command.Parameters.AddWithValue("@交易前手续费余额", 提款手续费_交易前手续费余额);
                                                command.Parameters.AddWithValue("@交易后手续费余额", 提款手续费_交易后手续费余额);
                                                command.Parameters.AddWithValue("@类型", 提款手续费_类型);
                                                command.Parameters.AddWithValue("@时间创建", 提款手续费_时间创建);

                                                command.Connection = scon13;
                                                command.CommandText = str;
                                                int obj = command.ExecuteNonQuery();

                                                scon13.Close();
                                            }


                                            //4. 商户账户余额交易明细
                                            using (MySqlConnection scon14 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                            {
                                                string 账户余额_生成编号标头 = "DKCZ";
                                                string 账户余额_订单号 = 账户余额_生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

                                                string 账户余额_商户ID = dr1["商户ID"].ToString();

                                                double 账户余额_手续费 = 读订单手续费;

                                                double 账户余额_交易金额 = 读订单提款金额;
                                                double 账户余额_交易前余额 = double.Parse(dr12["提款余额"].ToString());
                                                double 账户余额_交易后余额 = double.Parse(dr12["提款余额"].ToString()) + 读订单提款金额;
                                                string 账户余额_类型 = "订单提款冲正";
                                                string 账户余额_状态 = "成功";
                                                string 账户余额_时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                                string 余额交易明细哪个表 = " table_商户明细余额 ";
                                                string 余额交易明细写哪些 = "订单号,商户ID,手续费,交易金额,交易前账户余额,交易后账户余额,类型,状态,时间创建";
                                                string 余额交易明细写这些 = "@订单号,@商户ID,@手续费,@交易金额,@交易前账户余额,@交易后账户余额,@类型,@状态,@时间创建 ";

                                                string str = "insert into " + 余额交易明细哪个表 + "(" + 余额交易明细写哪些 + ") values(" + 余额交易明细写这些 + ")";

                                                scon14.Open();
                                                MySqlCommand command = new MySqlCommand();

                                                command.Parameters.AddWithValue("@订单号", 账户余额_订单号);
                                                command.Parameters.AddWithValue("@商户ID", 账户余额_商户ID);
                                                command.Parameters.AddWithValue("@手续费", 账户余额_手续费);
                                                command.Parameters.AddWithValue("@交易金额", 账户余额_交易金额);
                                                command.Parameters.AddWithValue("@交易前账户余额", 账户余额_交易前余额);
                                                command.Parameters.AddWithValue("@交易后账户余额", 账户余额_交易后余额);
                                                command.Parameters.AddWithValue("@类型", 账户余额_类型);
                                                command.Parameters.AddWithValue("@状态", 账户余额_状态);
                                                command.Parameters.AddWithValue("@时间创建", 账户余额_时间创建);

                                                command.Connection = scon14;
                                                command.CommandText = str;
                                                int obj = command.ExecuteNonQuery();

                                                scon14.Close();
                                            }
                                        }
                                    }
                                }
                            }

                            //5. 修改账户内余额 还回去
                            using (MySqlConnection con12 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd12 = new MySqlCommand("UPDATE table_商户账号 SET 提款余额=提款余额+" + 读订单提款金额 + ", 手续费余额= 手续费余额+" + 读订单手续费 + " where 商户ID=@商户ID ", con12))
                                {
                                    cmd12.Parameters.AddWithValue("@商户ID", dr1["商户ID"].ToString());

                                    con12.Open();
                                    cmd12.ExecuteNonQuery();
                                    con12.Close();
                                    //this.SaveImage(filePath);
                                    //Response.Redirect(Request.Url.AbsoluteUri, false);

                                }
                            }

                            //6. 插入出款银行卡流水 退款
                            using (MySqlConnection con14a = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd14a = new MySqlCommand("SELECT 出款银行卡卡号,出款银行卡余额 FROM table_后台出款银行卡管理 WHERE 出款银行卡卡号=@出款银行卡卡号", con14a))
                                {
                                    cmd14a.Parameters.AddWithValue("@出款银行卡卡号", 读订单内出款银行卡卡号);
                                    using (MySqlDataAdapter da14a = new MySqlDataAdapter(cmd14a))
                                    {
                                        DataTable images14a = new DataTable();
                                        da14a.Fill(images14a);
                                        foreach (DataRow dr14a in images14a.Rows)
                                        {
                                            double 余额 = double.Parse(dr14a["出款银行卡余额"].ToString()) + System.Convert.ToDouble(读订单提款金额);

                                            //5.1插入 出款银行卡流水明细
                                            using (MySqlConnection scon14 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                            {
                                                string 生成编号 = "CZHYE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                string 时间创建 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                string 类型 = "订单提款冲正";
                                                string 状态 = "成功";

                                                string 有哪些 = "订单号,商户ID,收入,余额,出款银行卡卡号,出款银行卡名称,类型,状态,时间创建";
                                                string 收哪些 = "@订单号,@商户ID,@收入,@余额,@出款银行卡卡号,@出款银行卡名称,@类型,@状态,@时间创建 ";

                                                string str14 = "insert into table_后台出款银行卡流水(" + 有哪些 + ") values(" + 收哪些 + ")";
                                                scon14.Open();
                                                MySqlCommand command = new MySqlCommand();

                                                command.Parameters.AddWithValue("@订单号", 生成编号);
                                                command.Parameters.AddWithValue("@商户ID", 读订单商户ID);
                                                command.Parameters.AddWithValue("@收入", 读订单提款金额);
                                                command.Parameters.AddWithValue("@余额", 余额);
                                                command.Parameters.AddWithValue("@出款银行卡卡号", 读订单内出款银行卡卡号);
                                                command.Parameters.AddWithValue("@出款银行卡名称", 读订单内出款银行卡名称);
                                                command.Parameters.AddWithValue("@类型", 类型);
                                                command.Parameters.AddWithValue("@状态", 状态);
                                                command.Parameters.AddWithValue("@时间创建", RegisterTime);

                                                command.Connection = scon14;
                                                command.CommandText = str14;
                                                int obj = command.ExecuteNonQuery();

                                                scon14.Close();
                                            }

                                        }
                                    }
                                }
                            }

                            //6. 管理出款银行卡 退款
                            using (MySqlConnection con16 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                            {
                                using (MySqlCommand cmd16 = new MySqlCommand("UPDATE table_后台出款银行卡管理 SET 出款银行卡余额=出款银行卡余额+"+ 读订单提款金额 + " WHERE 出款银行卡卡号=@出款银行卡卡号  ", con16))
                                {
                                    cmd16.Parameters.AddWithValue("@出款银行卡卡号", 读订单内出款银行卡卡号);

                                    con16.Open();
                                    cmd16.ExecuteNonQuery();
                                    con16.Close();
                                }
                            }



                            Response.Redirect("商户提款记录.aspx");

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