﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SqlSugar;
using System.IO;
using System.Text;

namespace web1.WebsiteBackstage.L1.ManagementMerchant
{
    public partial class 商户删除 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)/*如果省略这句 , 下面的更新操作将无法完成 , 因为获得的值是不变的*/
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
                获得目标账户();
            }
        }


        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户列表.aspx");
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


        private void 获得目标账户()//获得数据
        {
            string 从URL传来值 = 从URL获取值();

            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT 商户ID FROM table_商户账号 WHERE 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", 从URL传来值);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable images = new DataTable();
                        da.Fill(images);
                        foreach (DataRow dr in images.Rows)
                        {
                            this.Label_目标商户账号.Text = dr["商户ID"].ToString();

                        }
                    }
                }
            }
        }

        private void AddUpdateLog(string logInfo)
        {
            try
            {
                string rootPath = Path.Combine(HttpRuntime.AppDomainAppPath.ToString(), "Log\\");
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                File.AppendAllText(rootPath + "LOG_DELETE_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "[" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "]  " + logInfo + "\r\n",
                        Encoding.UTF8);
            }
            catch
            {
            }
        }

        protected void Button_操作删除_Click(object sender, EventArgs e)
        {
            Button_操作删除.Enabled = false;

            MySqlConnection conn2 = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            conn2.Open();
            MySqlCommand scmd2 = new MySqlCommand("delete from table_商户账号 where 商户ID = '" + 从URL获取值() + "' ", conn2);
            AddUpdateLog("delete from table_商户账号 where 商户ID = '" + 从URL获取值() + "' ");
            scmd2.ExecuteNonQuery();
            scmd2.Dispose();
            conn2.Close();

            Response.Redirect("商户列表.aspx");
        }
    }
}