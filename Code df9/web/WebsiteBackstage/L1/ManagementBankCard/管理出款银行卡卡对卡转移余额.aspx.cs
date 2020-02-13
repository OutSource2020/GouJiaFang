using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace web1.WebsiteBackstage.L1.ManagementBankCard
{
    public partial class 管理出款银行卡卡对卡转移余额 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)/*如果省略这句 , 下面的更新操作将无法完成 , 因为获得的值是不变的*/
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                选择A卡();
                选择B卡();
            }
            
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("管理出款银行卡.aspx");
        }


        //选择 A 卡 ====================================================================================================
        private void 选择A卡()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 出款银行卡名称,出款银行卡卡号 from table_后台出款银行卡管理 where 状态='启用' ";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_后台出款银行卡管理");
            myconn.Close();
            DropDownList_银行卡转移余额.DataSource = ds.Tables[0].DefaultView;
            DropDownList_银行卡转移余额.DataTextField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡名称"].ToString();
            DropDownList_银行卡转移余额.DataValueField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡卡号"].ToString();
            DropDownList_银行卡转移余额.DataBind();
            DropDownList_银行卡转移余额.Items.Insert(0, new ListItem("请选择", "0"));

            myconn.Close();
        }

        //选择 B卡 ====================================================================================================
        private void 选择B卡()
        {
            string connstring = ClassLibrary1.ClassDataControl.conStr1;
            string querystring = "select distinct 出款银行卡名称,出款银行卡卡号 from table_后台出款银行卡管理 where 状态='启用' ";
            MySqlConnection myconn = new MySqlConnection(connstring);
            myconn.Open();
            MySqlDataAdapter myadapter = new MySqlDataAdapter(querystring, myconn);
            DataSet ds = new DataSet();
            myadapter.Fill(ds, "table_后台出款银行卡管理");
            myconn.Close();
            DropDownList_银行卡转移目标.DataSource = ds.Tables[0].DefaultView;
            DropDownList_银行卡转移目标.DataTextField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡名称"].ToString();
            DropDownList_银行卡转移目标.DataValueField = ds.Tables["table_后台出款银行卡管理"].Columns["出款银行卡卡号"].ToString();
            DropDownList_银行卡转移目标.DataBind();
            DropDownList_银行卡转移目标.Items.Insert(0, new ListItem("请选择", "0"));

            myconn.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("./管理出款银行卡.aspx");
        }

        protected void Button_操作转移开始_Click(object sender, EventArgs e)
        {
            if (TextBox_金额.Text.Length > 0 )
            {
                if(DropDownList_银行卡转移余额.SelectedItem.Value!= DropDownList_银行卡转移目标.SelectedItem.Value)
                {
                    操作转移();
                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "转移卡相同");
                }
                
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }


        }

        private void 操作转移()
        {
            Button_操作转移开始.Enabled = false;

            string 从哪转移 = DropDownList_银行卡转移余额.SelectedItem.Value;
            string 转移到哪 = DropDownList_银行卡转移目标.SelectedItem.Value;
            string 金额 = TextBox_金额.Text;

            MySqlConnection conn0 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            conn0.Open();
            MySqlCommand scmd0 = new MySqlCommand("select * from table_后台出款银行卡管理 where 出款银行卡卡号='" + 从哪转移 + "' and 出款银行卡余额>='" + 金额 + "' ", conn0);
            MySqlDataReader sdr0 = scmd0.ExecuteReader();
            if (sdr0.Read())
            {
                Button_操作转移开始.Enabled = false;

                //更新卡A余额
                MySqlConnection conn1 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                conn1.Open();
                MySqlCommand scmd1 = new MySqlCommand("update table_后台出款银行卡管理 set 出款银行卡余额 = 出款银行卡余额-'" + 金额 + "'  where 出款银行卡卡号 = '" + 从哪转移 + "'", conn1);
                scmd1.ExecuteNonQuery();
                scmd1.Dispose();
                conn1.Close();

                //更新卡B余额
                MySqlConnection conn2 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                conn2.Open();
                MySqlCommand scmd2 = new MySqlCommand("update table_后台出款银行卡管理 set 出款银行卡余额 = 出款银行卡余额+'" + 金额 + "'  where 出款银行卡卡号 = '" + 转移到哪 + "'", conn2);
                scmd2.ExecuteNonQuery();
                scmd2.Dispose();
                conn2.Close();

                //流水记录插入-卡A的支出
                string sql = ClassLibrary1.ClassDataControl.conStr1;

                using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT 出款银行卡卡号,出款银行卡余额 FROM table_后台出款银行卡管理 WHERE 出款银行卡卡号=@出款银行卡卡号", con))
                    {
                        cmd.Parameters.AddWithValue("@出款银行卡卡号", 从哪转移);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable images = new DataTable();
                            da.Fill(images);
                            foreach (DataRow dr in images.Rows)
                            {
                                string 出款银行卡余额 = dr["出款银行卡余额"].ToString();

                                string 生成编号1 = "BCTCO" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                string 类型 = "卡对卡转移";
                                double 支出 = double.Parse(TextBox_金额.Text);
                                double 余额 = double.Parse(出款银行卡余额);
                                string 出款银行卡卡号 = DropDownList_银行卡转移余额.SelectedItem.Value;
                                string 出款银行卡名称 = DropDownList_银行卡转移余额.SelectedItem.Text;
                                string 状态 = "成功";
                                string 现在时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


                                string 写表 = "table_后台出款银行卡流水";
                                string 写头 = "订单号,类型,支出,余额,出款银行卡卡号,出款银行卡名称,状态,时间创建";
                                string 写值 = "@订单号,@类型,@支出,@余额,@出款银行卡卡号,@出款银行卡名称,@状态,@时间创建";

                                string _query = "INSERT INTO "+ 写表 + "("+ 写头 + ") values ("+ 写值 + ")";
                                using (MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand comm = new MySqlCommand())
                                    {
                                        comm.Connection = conn;
                                        comm.CommandType = CommandType.Text;
                                        comm.CommandText = _query;
                                        comm.Parameters.AddWithValue("@订单号", 生成编号1);
                                        comm.Parameters.AddWithValue("@类型", 类型);
                                        comm.Parameters.AddWithValue("@支出", 支出);
                                        comm.Parameters.AddWithValue("@余额", 余额);
                                        comm.Parameters.AddWithValue("@出款银行卡卡号", 出款银行卡卡号);
                                        comm.Parameters.AddWithValue("@出款银行卡名称", 出款银行卡名称);
                                        comm.Parameters.AddWithValue("@状态", 状态);
                                        comm.Parameters.AddWithValue("@时间创建", 现在时间);
                                        try
                                        {
                                            conn.Open();
                                            comm.ExecuteNonQuery();
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
                        }
                    }
                }

                //流水记录插入-卡B的收入

                using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT 出款银行卡卡号,出款银行卡余额 FROM table_后台出款银行卡管理 WHERE 出款银行卡卡号=@出款银行卡卡号", con))
                    {
                        cmd.Parameters.AddWithValue("@出款银行卡卡号", 转移到哪);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable images = new DataTable();
                            da.Fill(images);
                            foreach (DataRow dr in images.Rows)
                            {
                                string 出款银行卡余额 = dr["出款银行卡余额"].ToString();

                                string 生成编号1 = "BCTCE" + DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));
                                string 类型 = "卡对卡转移";
                                double 收入 = double.Parse(TextBox_金额.Text);
                                double 余额 = double.Parse(出款银行卡余额);
                                string 出款银行卡卡号 = DropDownList_银行卡转移目标.SelectedItem.Value;
                                string 出款银行卡名称 = DropDownList_银行卡转移目标.SelectedItem.Text;
                                string 状态 = "成功";
                                string 现在时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


                                string 写表 = "table_后台出款银行卡流水";
                                string 写头 = "订单号,类型,收入,余额,出款银行卡卡号,出款银行卡名称,状态,时间创建";
                                string 写值 = "@订单号,@类型,@支出,@余额,@出款银行卡卡号,@出款银行卡名称,@状态,@时间创建";

                                string _query = "INSERT INTO " + 写表 + "(" + 写头 + ") values (" + 写值 + ")";
                                using (MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
                                {
                                    using (MySqlCommand comm = new MySqlCommand())
                                    {
                                        comm.Connection = conn;
                                        comm.CommandType = CommandType.Text;
                                        comm.CommandText = _query;
                                        comm.Parameters.AddWithValue("@订单号", 生成编号1);
                                        comm.Parameters.AddWithValue("@类型", 类型);
                                        comm.Parameters.AddWithValue("@支出", 收入);
                                        comm.Parameters.AddWithValue("@余额", 余额);
                                        comm.Parameters.AddWithValue("@出款银行卡卡号", 出款银行卡卡号);
                                        comm.Parameters.AddWithValue("@出款银行卡名称", 出款银行卡名称);
                                        comm.Parameters.AddWithValue("@状态", 状态);
                                        comm.Parameters.AddWithValue("@时间创建", 现在时间);
                                        try
                                        {
                                            conn.Open();
                                            comm.ExecuteNonQuery();
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
                        }
                    }
                }

                Response.Redirect("./管理出款银行卡.aspx");
                //Response.Redirect("./管理出款银行卡.aspx");
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "转移停止.银行卡A余额不足");
            }

            conn0.Close();
        }
    }
}