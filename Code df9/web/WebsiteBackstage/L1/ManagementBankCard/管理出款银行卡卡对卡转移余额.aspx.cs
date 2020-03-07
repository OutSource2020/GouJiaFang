using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SqlSugar;
using Sugar.Enties;

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
            if (TextBox_金额.Text.Length > 0)
            {
                if (DropDownList_银行卡转移余额.SelectedItem.Value != DropDownList_银行卡转移目标.SelectedItem.Value)
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

            using (SqlSugarClient dbClient = new DBClient().GetClient())
            {
                dbClient.Ado.UseTran(() =>
                {
                    var data = dbClient.Queryable<table_后台出款银行卡管理>()
                    .Where(it => it.出款银行卡卡号 == 从哪转移 && it.出款银行卡余额.Value - Double.Parse(金额) >= 0);
                    if (data.Count() == 0)
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "转移停止.银行卡A余额不足");
                        return;
                    }

                    var record = data.First();

                    dbClient.Ado.ExecuteCommand("UPDATE `table_后台出款银行卡管理` SET `出款银行卡余额` = `出款银行卡余额` - '" + 金额 + "' WHERE `出款银行卡卡号` = '" + 从哪转移 + "';");
                    dbClient.Ado.ExecuteCommand("UPDATE `table_后台出款银行卡管理` SET `出款银行卡余额` = `出款银行卡余额` + '" + 金额 + "' WHERE `出款银行卡卡号` = '" + 转移到哪 + "';");

                    record = dbClient.Queryable<table_后台出款银行卡管理>()
                    .Where(it => it.出款银行卡卡号 == 从哪转移).First();

                    DateTime createTime = DateTime.Now;

                    table_后台出款银行卡流水 runningA = new table_后台出款银行卡流水()
                    {
                        订单号 = "BCTCO" + createTime.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)),
                        类型 = "卡对卡转移",
                        支出 = Double.Parse(金额),
                        余额 = record.出款银行卡余额,
                        出款银行卡卡号 = record.出款银行卡卡号,
                        出款银行卡名称 = record.出款银行名称,
                        状态 = "成功",
                        时间创建 = createTime
                    };
                    dbClient.Insertable(runningA).ExecuteCommand();

                    record = dbClient.Queryable<table_后台出款银行卡管理>()
                    .Where(it => it.出款银行卡卡号 == 转移到哪).First();
                    table_后台出款银行卡流水 runningB = new table_后台出款银行卡流水()
                    {
                        订单号 = "BCTCE" + createTime.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999)),
                        类型 = "卡对卡转移",
                        支出 = Double.Parse(金额),
                        余额 = record.出款银行卡余额,
                        出款银行卡卡号 = record.出款银行卡卡号,
                        出款银行卡名称 = record.出款银行名称,
                        状态 = "成功",
                        时间创建 = createTime
                    };
                    dbClient.Insertable(runningB).ExecuteCommand();
                });
            }
            Response.Redirect("./管理出款银行卡.aspx");
        }
    }
}