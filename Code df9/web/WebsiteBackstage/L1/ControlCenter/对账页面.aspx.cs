using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SqlSugar;
using Sugar.Enties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web1.API.Enties;

namespace web1.WebsiteBackstage.L1.ControlCenter
{
    public partial class 对账页面 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlSugarClient dbClient = new DBClient().GetClient())
            {
                DataTable dt = dbClient.Queryable<table_snapshot>().Where(it => DateTime.Now <= it.CreateTime.Value.AddDays(7)).OrderBy(it => it.Id, OrderByType.Desc).ToDataTable();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }
}
