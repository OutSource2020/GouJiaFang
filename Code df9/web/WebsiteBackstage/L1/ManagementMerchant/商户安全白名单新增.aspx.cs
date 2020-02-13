using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{
    public partial class 商户安全白名单新增 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();

                下拉获取商户ID();
            }

            
        }

        private void 下拉获取商户ID()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 商户ID,商户名称 from table_商户账号 where 状态 = '启用'";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_商户账号");
            myconn.Close();
            DropDownList_商户ID.DataSource = ds.Tables[0].DefaultView;
            DropDownList_商户ID.DataTextField = ds.Tables["table_商户账号"].Columns["商户名称"].ToString();
            DropDownList_商户ID.DataValueField = ds.Tables["table_商户账号"].Columns["商户ID"].ToString();
            DropDownList_商户ID.DataBind();

            myconn.Close();
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户安全白名单.aspx");
        }

        protected void Button_创建提交_Click(object sender, EventArgs e)
        {
            if (TextBox_商户白名单IP.Text.Length > 1 || TextBox_商户白名单IP.Text.Length > 1)
            {
                新增();
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "未填写完整");
            }

        }


        private bool 检查是否有相同商户白名单IP()
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("select 商户白名单IP from table_商户白名单管理 where 商户白名单IP=@商户白名单IP and 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户白名单IP", TextBox_商户白名单IP.Text);
                    cmd.Parameters.AddWithValue("@商户ID", DropDownList_商户ID.SelectedItem.Value);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    object o = cmd.ExecuteScalar();
                    if (o != null)
                    {
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        return false;
                    }
                    //con.Close();
                }
            }

        }

        private void 新增()
        {
            if (检查是否有相同商户白名单IP() == false)
            {
                Button_创建提交.Enabled = false;

                string 编号 = "MWIPA" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100, 999));
                string 商户ID = ClassLibrary1.ClassSecurityZF.FilteSQLStr(DropDownList_商户ID.SelectedItem.Value);
                string 商户白名单IP = ClassLibrary1.ClassSecurityZF.FilteSQLStr(TextBox_商户白名单IP.Text);
                string 商户白名单备注 = ClassLibrary1.ClassSecurityZF.FilteSQLStr(TextBox_商户白名单备注.Text);
                string 状态 = DropDownList_下拉框1.SelectedItem.Value;
                string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


                string 写表 = "table_商户白名单管理";
                string 写头 = "编号,商户ID,商户白名单IP,商户白名单备注,状态,时间创建";
                string 写值 = "@编号,@商户ID,@商户白名单IP,@商户白名单备注,@状态,@时间创建";

                string _query = "INSERT INTO " + 写表 + "(" + 写头 + ") values (" + 写值 + ")";
                using (MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                {
                    using (MySqlCommand comm = new MySqlCommand())
                    {
                        comm.Connection = conn;
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = _query;
                        comm.Parameters.AddWithValue("@编号", 编号);
                        comm.Parameters.AddWithValue("@商户ID", 商户ID);
                        comm.Parameters.AddWithValue("@商户白名单IP", 商户白名单IP);
                        comm.Parameters.AddWithValue("@商户白名单备注", 商户白名单备注);
                        comm.Parameters.AddWithValue("@状态", 状态);
                        comm.Parameters.AddWithValue("@时间创建", RegisterTime);
                        try
                        {
                            conn.Open();
                            comm.ExecuteNonQuery();

                            conn.Close();

                            Response.Redirect("商户安全白名单.aspx");
                        }
                        catch (MySqlException ex)
                        {
                            // other codes here
                            // do something with the exception
                            // don't swallow it.

                            ClassLibrary1.ClassMessage.HinXi(Page, "错误 " + ex + " ");
                        }
                    }
                }

            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "目标商户ID的白名单列中已存在此IP");
            }


        }


        // 搜索 asp.net using Rows Tables Count >

        private bool 检查同商户号中是否有相同IP2()
        {
            //========== 限制白名单IP只能一个 ==========

            //连接数据库
            ClassLibrary1.ClassDataControl.OpenConnection1();
            string UserName = DropDownList_商户ID.SelectedItem.Value;
            string 目标白名单IP = TextBox_商户白名单IP.Text;
            //创建SQL语句
            string selStr = "select 商户白名单IP from table_商户白名单管理 where 商户白名单IP='" + 目标白名单IP + "' and 商户ID='" + UserName + "'";
            //创建数据适配器
            MySqlDataAdapter da = new MySqlDataAdapter(selStr, ClassLibrary1.ClassDataControl.con1);
            //创建满足条件的数据集
            DataSet ds = new DataSet();
            da.Fill(ds);
            //如果数据集不为空 , 则用户名已经存在
            if (ds.Tables[0].Rows.Count != 0)
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "这个 商户的白名单列中 已存在IP");             
                return true;

            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "这个 商户的白名单列中 没有这个IP");
                return false;
            }
        }


    }
}