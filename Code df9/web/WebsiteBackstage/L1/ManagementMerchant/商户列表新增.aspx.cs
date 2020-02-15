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
    public partial class 商户列表新增 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ClassLibrary1.ClassAccount.验证账号管理L1端();
            }
        }

        protected void Button_返回_Click(object sender, EventArgs e)
        {
            Response.Redirect("商户列表.aspx");
        }

        protected void Button_新增商户_Click(object sender, EventArgs e)
        {
            if (TextBox_账号信息_商户ID.Text.Length > 0 && 

                TextBox_联系人信息_姓名.Text.Length > 0 &&
                TextBox_联系人信息_联系电话.Text.Length > 0 &&
                TextBox_联系人信息_邮箱.Text.Length > 0 &&

                TextBox_充值最低手续费.Text.Length > 0 &&
                TextBox_充值最低余额.Text.Length > 0 &&
                TextBox_提款最低单笔金额.Text.Length > 0 &&
                TextBox_提款最高单笔金额.Text.Length > 0 &&
                TextBox_手续费比率.Text.Length > 0 &&
                TextBox_单笔手续费.Text.Length > 0 &&
                TextBox_第一阶梯起.Text.Length > 0 &&
                TextBox_第一阶梯止.Text.Length > 0 &&
                TextBox_第一阶梯百分比.Text.Length > 0 &&
                TextBox_第二阶梯起.Text.Length > 0 &&
                TextBox_第二阶梯止.Text.Length > 0 &&
                TextBox_第二阶梯百分比.Text.Length > 0 &&
                TextBox_第三阶梯起.Text.Length > 0 &&
                TextBox_第三阶梯止.Text.Length > 0 &&
                TextBox_第三阶梯百分比.Text.Length > 0 &&
                TextBox_第四阶梯起.Text.Length > 0 &&
                TextBox_第四阶梯止.Text.Length > 0 &&
                TextBox_第四阶梯百分比.Text.Length > 0
                )
            {
                

                if (TextBox_账号信息_商户ID.Text.Length > 1)
                {
                    if(判断是否存在相同ID()==false)
                    {
                        //ClassLibrary1.ClassMessage.HinXi(Page, "ID不存在");
                        操作新增();
                    }
                    else
                    {
                        ClassLibrary1.ClassMessage.HinXi(Page, "ID已存在");
                    }

                }
                else
                {
                    ClassLibrary1.ClassMessage.HinXi(Page, "商户ID是否符合格式? (空或者使用非数字)");
                }
            }
            else
            {
                ClassLibrary1.ClassMessage.HinXi(Page, "检查所有栏位是否都已填写");
            }
        }

        private bool 判断是否存在相同ID()
        {
            using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand cmd = new MySqlCommand("select 商户ID from table_商户账号 where 商户ID=@商户ID", con))
                {
                    cmd.Parameters.AddWithValue("@商户ID", TextBox_账号信息_商户ID.Text);

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

        private void 操作新增()
        {
      //Button_新增商户.Enabled = false;
      using (var db = (new DBClient()).GetClient())
      {
        var data = db.Queryable<Sugar.Enties.table_商户账号>().Where(it => it.商户ID == TextBox_账号信息_商户ID.Text).ToList();
        if(data.Count()>1l)
        {
          ClassLibrary1.ClassMessage.HinXi(Page, "商户账号重名请更改");
           return;
        }
      }

      

      string 生成编号 = DateTime.Now.ToString("yyyyMMddHHmmss") + Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

            string RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string 获得选择手续费收款方式 = "";
            if (RadioButton_手续费收款方式_预充值.Checked)
            {
                获得选择手续费收款方式 = "预充值";
            }
            if (RadioButton_手续费收款方式_后充值.Checked)
            {
                获得选择手续费收款方式 = "后充值";
            }

            string 商户密码第一次 = Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100000, 999999));
            string 商户密码API第一次 = Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100000, 999999));
            string 支付密码第一次 = Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 100000, 999999));
            string 验证器google = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            string 账号信息列名 = "商户ID,商户密码,商户密码API,支付密码,手续费收款方式,状态,时间注册";
            string 账号信息数据 = "@商户ID,@商户密码,@商户密码API,@支付密码,@手续费收款方式,@状态,@时间注册";

            string 联系人信息列名 = "," + "商户名称,绑定手机,keyga,绑定邮箱";
            string 联系人信息数据 = "," + "@商户名称,@绑定手机,@keyga,@绑定邮箱";

            string 登入错误累计 = "0";
            string 支付错误累计 = "0";
            string 账户安全列名 = "," + "登入错误累计,支付错误累计";
            string 账户安全数据 = "," + "@登入错误累计,@支付错误累计";

            string 提款余额 = "0";
            string 手续费余额 = "0";
            string 账户金额列名 = "," + "提款余额,手续费余额";
            string 账户金额数据 = "," + "@提款余额,@手续费余额";

            string 费率方面列名 = "," + "充值最低手续费,充值最低余额,提款最低单笔金额,提款最高单笔金额,手续费比率,单笔手续费";
            string 费率方面数据 = "," + "@充值最低手续费,@充值最低余额,@提款最低单笔金额,@提款最高单笔金额,@手续费比率,@单笔手续费";

            string 费率阶段之第一阶梯字段 = "," + "第一阶梯起,第一阶梯止,第一阶梯百分比 ";
            string 费率阶段之第一阶梯内容 = "," + "@第一阶梯起,@第一阶梯止,@第一阶梯百分比 ";
            string 费率阶段之第二阶梯字段 = "," + "第二阶梯起,第二阶梯止,第二阶梯百分比 ";
            string 费率阶段之第二阶梯内容 = "," + "@第二阶梯起,@第二阶梯止,@第二阶梯百分比 ";
            string 费率阶段之第三阶梯字段 = "," + "第三阶梯起,第三阶梯止,第三阶梯百分比 ";
            string 费率阶段之第三阶梯内容 = "," + "@第三阶梯起,@第三阶梯止,@第三阶梯百分比 ";
            string 费率阶段之第四阶梯字段 = "," + "第四阶梯起,第四阶梯止,第四阶梯百分比 ";
            string 费率阶段之第四阶梯内容 = "," + "@第四阶梯起,@第四阶梯止,@第四阶梯百分比 ";

            string 费率阶段列名 = " " + 费率阶段之第一阶梯字段 + "" + 费率阶段之第二阶梯字段 + "" + 费率阶段之第三阶梯字段 + "" + 费率阶段之第四阶梯字段 + " ";
            string 费率阶段数据 = " " + 费率阶段之第一阶梯内容 + "" + 费率阶段之第二阶梯内容 + "" + 费率阶段之第三阶梯内容 + "" + 费率阶段之第四阶梯内容 + " ";

            string 有哪些 = " " + 账号信息列名 + "" + 联系人信息列名 + "" + 账户安全列名 + "" + 账户金额列名 + "" + 费率方面列名 + "" + 费率阶段列名 + " ";
            string 收哪些 = " " + 账号信息数据 + "" + 联系人信息数据 + "" + 账户安全数据 + "" + 账户金额数据 + "" + 费率方面数据 + "" + 费率阶段数据 + " ";


            string 写表 = "table_商户账号";
            string 写头 = 有哪些;
            string 写值 = 收哪些;

            string _query = "INSERT INTO " + 写表 + "(" + 写头 + ") values (" + 写值 + ")";
            using (MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@商户ID", TextBox_账号信息_商户ID.Text);
                    comm.Parameters.AddWithValue("@商户密码", 商户密码第一次);
                    comm.Parameters.AddWithValue("@商户密码API", 商户密码API第一次);
                    comm.Parameters.AddWithValue("@支付密码", 支付密码第一次);
                    comm.Parameters.AddWithValue("@手续费收款方式", 获得选择手续费收款方式);
                    comm.Parameters.AddWithValue("@状态", DropDownList_下拉框1.SelectedItem.Value);
                    comm.Parameters.AddWithValue("@时间注册", RegisterTime);

                    comm.Parameters.AddWithValue("@商户名称", TextBox_联系人信息_姓名.Text);
                    comm.Parameters.AddWithValue("@绑定手机", TextBox_联系人信息_联系电话.Text);
                    comm.Parameters.AddWithValue("@keyga", 验证器google);
                    comm.Parameters.AddWithValue("@绑定邮箱", TextBox_联系人信息_邮箱.Text);

                    comm.Parameters.AddWithValue("@登入错误累计", 登入错误累计);
                    comm.Parameters.AddWithValue("@支付错误累计", 支付错误累计);

                    comm.Parameters.AddWithValue("@提款余额", 提款余额);
                    comm.Parameters.AddWithValue("@手续费余额", 手续费余额);

                    comm.Parameters.AddWithValue("@充值最低手续费", TextBox_充值最低手续费.Text);
                    comm.Parameters.AddWithValue("@充值最低余额", TextBox_充值最低余额.Text);
                    comm.Parameters.AddWithValue("@提款最低单笔金额", TextBox_提款最低单笔金额.Text);
                    comm.Parameters.AddWithValue("@提款最高单笔金额", TextBox_提款最高单笔金额.Text);
                    comm.Parameters.AddWithValue("@手续费比率", TextBox_手续费比率.Text);
                    comm.Parameters.AddWithValue("@单笔手续费", TextBox_单笔手续费.Text);

                    comm.Parameters.AddWithValue("@第一阶梯起", TextBox_第一阶梯起.Text);
                    comm.Parameters.AddWithValue("@第一阶梯止", TextBox_第一阶梯止.Text);
                    comm.Parameters.AddWithValue("@第一阶梯百分比", TextBox_第一阶梯百分比.Text);
                    comm.Parameters.AddWithValue("@第二阶梯起", TextBox_第二阶梯起.Text);
                    comm.Parameters.AddWithValue("@第二阶梯止", TextBox_第二阶梯止.Text);
                    comm.Parameters.AddWithValue("@第二阶梯百分比", TextBox_第二阶梯百分比.Text);
                    comm.Parameters.AddWithValue("@第三阶梯起", TextBox_第三阶梯起.Text);
                    comm.Parameters.AddWithValue("@第三阶梯止", TextBox_第三阶梯止.Text);
                    comm.Parameters.AddWithValue("@第三阶梯百分比", TextBox_第三阶梯百分比.Text);
                    comm.Parameters.AddWithValue("@第四阶梯起", TextBox_第四阶梯起.Text);
                    comm.Parameters.AddWithValue("@第四阶梯止", TextBox_第四阶梯止.Text);
                    comm.Parameters.AddWithValue("@第四阶梯百分比", TextBox_第四阶梯百分比.Text);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();

                        conn.Close();

                        Response.Redirect("商户列表.aspx");
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

            
            using( var db = (new DBClient()).GetClient())
            {
                var data = db.Queryable<Sugar.Enties.table_商户账号>().Where(it => it.商户ID == TextBox_账号信息_商户ID.Text).First();
               data.二步验证状态 = (DropDownList_二步验证.SelectedValue)== "启用" ? true:false;
               data.api提款状态 = (DropDownList_API提款启用状态.SelectedItem.Value== "启用") ? true : false;
               data.手动提款状态 = (DropDownList_手动提款启用状态.SelectedItem.Value== "启用")? true : false;
          }
     

    }

        protected void Button_随机生成商户ID_Click(object sender, EventArgs e)
        {
            Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1,100, 300));
            string 生成编号 = "163"+ Convert.ToString(ClassLibrary1.ClassHelpMe.GenerateRandomCode(1, 1000, 9999));

            TextBox_账号信息_商户ID.Text = 生成编号;
        }

        protected void CheckBox_手动设置商户ID_CheckedChanged(object sender, EventArgs e)
        {
            if(CheckBox_手动设置商户ID.Checked==true)
            {
                TextBox_账号信息_商户ID.Enabled = true;
            }
            else
            {
                TextBox_账号信息_商户ID.Enabled = false;
            }
        }
    }
}