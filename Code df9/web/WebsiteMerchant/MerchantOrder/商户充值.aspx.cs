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
    public partial class 商户充值 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号商户端();
            }

            收手续费卡();
            收金额卡();
            获得下拉();

        }



    protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户充值记录.aspx");
        }

        private void 收手续费卡()
        {
            string strQuery = "select 收款银行名称,收款银行卡主姓名,收款银行卡卡号 from table_后台收款银行卡管理 where 状态 = '启用' and 显示标记='启用'   ";
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
                GridView_收手续费卡.DataSource = dt;
                GridView_收手续费卡.DataBind();
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

        private void 收金额卡()
        {
            string strQuery = "select 出款银行名称,出款银行卡主姓名,出款银行卡卡号 from table_后台出款银行卡管理 where 状态 = '启用' and 显示标记='启用'";
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
                GridView_收金额卡.DataSource = dt;
                GridView_收金额卡.DataBind();
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


        private void 获得下拉()
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 商户银行卡卡号,商户银行卡主姓名 from table_商户银行卡 where 商户ID='" + Cookie_UserName + "' and 状态 = '启用' and 商户银行卡卡标记='显示' ";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_商户银行卡");
            myconn.Close();
            DropDownList_发起卡号.DataSource = ds.Tables[0].DefaultView;
            DropDownList_发起卡号.DataTextField = ds.Tables["table_商户银行卡"].Columns["商户银行卡主姓名"].ToString();
            DropDownList_发起卡号.DataValueField = ds.Tables["table_商户银行卡"].Columns["商户银行卡卡号"].ToString();
            DropDownList_发起卡号.DataBind();
            //DropDownList_发起卡号.Items.Insert(0, new ListItem("请选择", "0"));

        }


        protected void Button_充值_Click(object sender, EventArgs e)
        {
            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            if (TextBox_金额.Text.Length > 0 && TextBox_备注.Text.Length > 0 && TextBox_备注.Text.Length < 30)
            {
                if (TextBox_备注.Text.Length < 30)
                {
                    if (ClassLibrary1.ClassYZ.IsNumber(TextBox_金额.Text) == true)
                    {
                        //ClassLibrary1.ClassMessage.HinXi(Page, "是数字或者小数");


                        //判定 DropDownList_选择银行卡 是否空
                        if (String.IsNullOrEmpty(DropDownList_发起卡号.SelectedValue))
                        {
                            ClassLibrary1.ClassMessage.HinXi(Page, "出款银行卡还未设置或者未启用");
                        }
                        else
                        {
                            //如果选择充值提款手续费
                            if (RadioButton_充值提款手续费.Checked == true)
                            {
                                using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand cmd = new MySqlCommand("SELECT 充值最低手续费 FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                                    {
                                        cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                                        {
                                            DataTable images = new DataTable();
                                            da.Fill(images);
                                            foreach (DataRow dr in images.Rows)
                                            {
                                                double 充值最低手续费 = double.Parse(dr["充值最低手续费"].ToString());

                                                
                                                if (double.Parse(TextBox_金额.Text) >= 充值最低手续费)
                                                {
                                                    选了充商户手续费余额();
                                                }
                                                else
                                                {
                                                    ClassLibrary1.ClassMessage.HinXi(Page, "充值提款手续费最低" + 充值最低手续费 + "元起");
                                                }

                                            }
                                        }
                                    }
                                }
                                
                            }

                            //如果选择充值提款余额
                            if (RadioButton_充值提款余额.Checked == true)
                            {

                                using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand cmd = new MySqlCommand("SELECT 充值最低余额 FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                                    {
                                        cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                                        {
                                            DataTable images = new DataTable();
                                            da.Fill(images);
                                            foreach (DataRow dr in images.Rows)
                                            {
                                                double 充值最低余额 = double.Parse(dr["充值最低余额"].ToString());


                                                if (double.Parse(TextBox_金额.Text) >= 充值最低余额 )
                                                {
                                                    选了充商户提款余额();
                                                }
                                                else
                                                {
                                                    ClassLibrary1.ClassMessage.HinXi(Page, "充值提款余额最低" + 充值最低余额 + "起");
                                                }

                                            }
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
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "备注太长");
                }
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }

        private void 选了充商户手续费余额()
        {
            //因为是充值手续费 充手续费不扣 手续费比率和单笔手续费 

            Button_充值.Enabled = false;//防止重复点击按钮

            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,商户银行卡卡号 FROM table_商户银行卡 WHERE 商户ID=@商户ID and 商户银行卡卡号=@商户银行卡卡号 and 状态='启用'", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    cmd.Parameters.AddWithValue("@商户银行卡卡号", DropDownList_发起卡号.SelectedItem.Value);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {

                            if (dr["商户银行卡卡号"].ToString() != null)
                            {
                                //查询账户手续费有多少
                                using (MySqlConnection con13 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand cmd13 = new MySqlCommand("SELECT 商户ID,手续费余额 FROM table_商户账号 WHERE 商户ID=@商户ID", con13))
                                    {
                                        cmd13.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                                        using (MySqlDataAdapter da13 = new MySqlDataAdapter(cmd13))
                                        {
                                            DataTable images13 = new DataTable();
                                            da13.Fill(images13);
                                            foreach (DataRow dr13 in images13.Rows)
                                            {
                                                string 账户内手续费余额 = dr13["手续费余额"].ToString();

                                                //先定义
                                                string 生成编号标头 = "MRONHF";
                                                string 生成编号 = 生成编号标头 + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)); ;
                                                string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                string 充值类型 = "充值提款手续费";

                                                //插入 充值信息 到 table_商户明细充值
                                                using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                {
                                                    string 有哪些 = "订单号,商户ID,商户银行卡卡号,充值类型,充值金额,状态,时间创建,商户充值目标姓名,商户充值目标卡号,商户充值目标银行 ";
                                                    string 收哪些 = "'" + 生成编号 + "','" + Cookie_UserName + "','" + DropDownList_发起卡号.SelectedItem.Value + "','" + 充值类型 + "','" + TextBox_金额.Text + "','待处理','" + RegisterTime + "','" + TextBox_目标姓名.Text + "','" + TextBox_目标卡号.Text + "','"+TextBox_目标银行名称.Text+"' ";

                                                    string str = "insert into table_商户明细充值(" + 有哪些 + ") values(" + 收哪些 + ")";
                                                    scon.Open();
                                                    MySqlCommand command = new MySqlCommand();
                                                    command.Connection = scon;
                                                    command.CommandText = str;
                                                    int obj = command.ExecuteNonQuery();

                                                    scon.Close();
                                                }

                                                Response.Redirect("商户充值记录.aspx");
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "卡号为空");
                                //Response.Redirect("./商户充值记录.aspx");
                            }
                        }
                    }
                }
            }
        }


        private void 选了充商户提款余额()
        {
            //充值提款余额  按账户设置的手续费比率扣除手续费

            Button_充值.Enabled = false;//防止重复点击按钮

            string Cookie_UserName = ClassLibrary1.ClassAccount.检查商户端cookie2();


            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID,商户银行卡卡号 FROM table_商户银行卡 WHERE 商户ID=@商户ID and 商户银行卡卡号=@商户银行卡卡号 and 状态='启用'", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                    cmd.Parameters.AddWithValue("@商户银行卡卡号", DropDownList_发起卡号.SelectedItem.Value);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {

                            if (dr["商户银行卡卡号"].ToString() != null)
                            {
                                string 卡号 = dr["商户银行卡卡号"].ToString();


                                //查询账户余额多少
                                using (MySqlConnection con13 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand cmd13 = new MySqlCommand("SELECT 商户ID,提款余额,手续费余额,手续费比率,单笔手续费 FROM table_商户账号 WHERE 商户ID=@商户ID", con13))
                                    {
                                        cmd13.Parameters.AddWithValue("@商户ID", Cookie_UserName);
                                        using (MySqlDataAdapter da13 = new MySqlDataAdapter(cmd13))
                                        {
                                            DataTable images13 = new DataTable();
                                            da13.Fill(images13);
                                            foreach (DataRow dr13 in images13.Rows)
                                            {
                                                double 账户内提款余额 = double.Parse(dr13["提款余额"].ToString());
                                                double 账户内手续费余额 = double.Parse(dr13["手续费余额"].ToString());

                                                double a = double.Parse(TextBox_金额.Text);
                                                double b = 100;
                                                double c = double.Parse(dr13["手续费比率"].ToString());
                                                //double d = double.Parse(dr13["单笔手续费"].ToString());
                                                //double 手续费计算 = (((a / b) * c) + d);
                                                double 手续费计算 = ((a / b) * c);

                                                //double 手续费多少 = Math.Round(手续费计算, 2);
                                                double 手续费多少 = 手续费计算;

                                                //判定本次充值所需支付的手续费是否足够支付
                                                if ((账户内手续费余额 - 手续费多少) >= 0)
                                                {
                                                    //判定条件都通过就开始执行任务
                                                    //1.插入订单充值订单
                                                    //2.插入手续费明细
                                                    //3.扣除账户内的手续费

                                                    string 生成编号 = "MRONB" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                                    string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                                    string 充值类型 = "充值提款余额";



                                                    //插入订单-充值订单
                                                    using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        string 有哪些 = "订单号,商户ID,商户银行卡卡号,充值类型,充值金额,产生手续费,状态,时间创建,商户充值目标姓名,商户充值目标卡号,商户充值目标银行 ";
                                                        string 收哪些 = "'" + 生成编号 + "','" + Cookie_UserName + "','" + DropDownList_发起卡号.SelectedItem.Value + "','" + 充值类型 + "','" + TextBox_金额.Text + "','" + 手续费多少 + "','待处理','" + RegisterTime + "','" + TextBox_目标姓名.Text + "','" + TextBox_目标卡号.Text + "','" + TextBox_目标银行名称.Text + "' ";

                                                        string str = "insert into table_商户明细充值(" + 有哪些 + ") values(" + 收哪些 + ")";
                                                        scon.Open();
                                                        MySqlCommand command = new MySqlCommand();
                                                        command.Connection = scon;
                                                        command.CommandText = str;
                                                        int obj = command.ExecuteNonQuery();

                                                        scon.Close();
                                                    }

                                                    //2. 插入订单-手续费明细
                                                    double 查询交易前手续费余额 = double.Parse(dr13["手续费余额"].ToString());
                                                    double 定义交易后手续费余额 = double.Parse(dr13["手续费余额"].ToString()) - System.Convert.ToDouble(手续费多少);
                                                    using (MySqlConnection scon = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        string 有哪些 = "订单号,商户ID,商户银行卡卡号,类型,交易金额,手续费支出,交易前手续费余额,交易后手续费余额,时间创建 ";
                                                        string 收哪些 = "'" + 生成编号 + "','" + Cookie_UserName + "','" + DropDownList_发起卡号.SelectedItem.Value + "','" + 充值类型 + "','" + TextBox_金额.Text + "','" + 手续费多少 + "','" + 查询交易前手续费余额 + "','" + 定义交易后手续费余额 + "','" + RegisterTime + "'";

                                                        string str = "insert into table_商户明细手续费(" + 有哪些 + ") values(" + 收哪些 + ")";
                                                        scon.Open();
                                                        MySqlCommand command = new MySqlCommand();
                                                        command.Connection = scon;
                                                        command.CommandText = str;
                                                        int obj = command.ExecuteNonQuery();

                                                        scon.Close();
                                                    }

                                                    //3.扣商户账号内的手续费
                                                    using (MySqlConnection conGX = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                                    {
                                                        using (MySqlCommand cmdGX = new MySqlCommand("UPDATE table_商户账号 SET 手续费余额=手续费余额-'" + 手续费多少 + "' WHERE 商户ID='" + Cookie_UserName + "'", conGX))
                                                        {

                                                            //cmd.Parameters.AddWithValue("@订单号", id);
                                                            //cmd.Parameters.AddWithValue("@备注", txtName.Text);
                                                            //cmd.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                                                            conGX.Open();
                                                            cmdGX.ExecuteNonQuery();
                                                            conGX.Close();

                                                            //Response.Redirect(Request.Url.AbsoluteUri, false);
                                                        }
                                                    }

                                                    Response.Redirect("商户充值记录.aspx");
                                                }
                                                else
                                                {
                                                    ClassLibrary1.ClassMessage.HinXi(Page, "目标充值金额,所需的手续费不足");
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            else
                            {
                                ClassLibrary1.ClassMessage.HinXi(Page, "卡号为空");
                                //Response.Redirect("./商户充值记录.aspx");
                            }
                        }
                    }
                }
            }
        }

    protected void GridView_收金额卡_SelectedIndexChanged(object sender, EventArgs e)
    {
      Button button = (Button)sender;
      GridViewRow gvr = (GridViewRow)button.Parent.Parent;
      string pk = GridView_收金额卡.DataKeys[gvr.RowIndex].Value.ToString();

    }


  

    protected void GridView_收手续费卡_SelectedIndexChanged(object sender, EventArgs e)
    {
      Button button = (Button)sender;
      GridViewRow gvr = (GridViewRow)button.Parent.Parent;
      string pk = GridView_收金额卡.DataKeys[gvr.RowIndex].Value.ToString();

    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow) return;

      if (e.Row.FindControl("ButtonS_收手续卡") != null)
      {
        Button CtlButton = (Button)e.Row.FindControl("ButtonS_收手续卡");
        CtlButton.Click += new EventHandler(Select_收手续费卡);
      }
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow) return;

      if (e.Row.FindControl("ButtonS_收金额卡") != null)
      {
        Button CtlButton = (Button)e.Row.FindControl("ButtonS_收金额卡");
        CtlButton.Click += new EventHandler(Select_收金额卡);
      }

    }

    private void Select_收手续费卡(object sender, EventArgs e)
    {
      Button button = (Button)sender;
      GridViewRow gvr = (GridViewRow)button.Parent.Parent;
      int index = gvr.RowIndex;
      var bank= GridView_收手续费卡.Rows[index].Cells[0].Text;
      var userNmae = GridView_收手续费卡.Rows[index].Cells[1].Text;
      var cardNumber = GridView_收手续费卡.Rows[index].Cells[2].Text;
      TextBox_目标姓名.Text = userNmae;
      TextBox_目标银行名称.Text = bank;
      TextBox_目标卡号.Text = cardNumber;

    }

    private void Select_收金额卡(object sender, EventArgs e)
    {
      Button button = (Button)sender;
      GridViewRow gvr = (GridViewRow)button.Parent.Parent;
      int index = gvr.RowIndex;

      var bank = GridView_收金额卡.Rows[index].Cells[0].Text;
      var userNmae = GridView_收金额卡.Rows[index].Cells[1].Text;
      var cardNumber = GridView_收金额卡.Rows[index].Cells[2].Text;
      TextBox_目标姓名.Text = userNmae;
      TextBox_目标银行名称.Text = bank;
      TextBox_目标卡号.Text = cardNumber;
    }


  }










}