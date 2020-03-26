using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
////using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Web;

namespace ClassLibrary1
{
    public class ClassAccount
    {
        //====================================================================================================
        public static bool 检查管理L1端cookie()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string 检查管理L1端cookie2()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"]["username"]);
                return Cookie_UserName;
            }
            else
            {
                弹回登入管理L1页();
                return null;

            }
        }

        public static void 弹回登入管理L1页()
        {
            ClassLibrary1.ClassMessage.tizun("/WebsiteBackstage/L1/Login/登入.aspx");
        }

        //驗證帳號的方法  只需要在頁面調用這個方法就可以驗證cookie內帳號密碼 如果非法訪問和錯誤的密碼將會被遣送回登錄頁面
        public static void 验证账号管理L1端()
        {
            //獲取cookie
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"];
            //檢測cookie
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"]["username"]);
                //string Cookie_Password = System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"]["password"];
                string Cookie_Password = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL1"]["password"]);

                MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from table_后台账号 where 后台ID='" + Cookie_UserName + "' and 后台密码='" + Cookie_Password + "'", conn);
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄成功");

                    //if (DateTime.Parse(Cookie_TimeSave) > DateTime.Parse(DateTime.Now.AddHours(-1)))
                    //{

                    //}
                    //else
                    //{
                    //    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                    //    ClassLibrary1.ClassMessage.tizun("/管理/登入/登入.aspx");
                    //}

                }
                else
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                    弹回登入管理L1页();
                }
                conn.Close();
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                弹回登入管理L1页();
            }
        }

        public static bool 验证管理L1白名单IP(string 后台ID, string 获取要验证目标IP)
        {
#if DEBUG
            return true;
#else
            MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select 后台ID,后台白名单IP from table_后台白名单管理 where 后台ID=@后台ID and 后台白名单IP=@后台白名单IP", conn);

            cmd.Parameters.AddWithValue("@后台ID", 后台ID);
            cmd.Parameters.AddWithValue("@后台白名单IP", 获取要验证目标IP);

            MySqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "IP白名单验证成功!");
                conn.Close();
                return true;
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "IP白名单验证失败!");
                conn.Close();
                return false;
            }
#endif

            
        }

        //====================================================================================================

        //====================================================================================================
        public static bool 检查管理L2端cookie()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string 检查管理L2端cookie2()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"]["username"]);
                return Cookie_UserName;
            }
            else
            {
                弹回登入管理L2页();
                return null;

            }
        }

        public static void 弹回登入管理L2页()
        {
            ClassLibrary1.ClassMessage.tizun("/WebsiteBackstage/L2/Login/登入.aspx");
        }

        //驗證帳號的方法  只需要在頁面調用這個方法就可以驗證cookie內帳號密碼 如果非法訪問和錯誤的密碼將會被遣送回登錄頁面
        public static void 验证账号管理L2端()
        {
            //獲取cookie
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"];
            //檢測cookie
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"]["username"]);
                //string Cookie_Password = System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"]["password"];
                string Cookie_Password = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameBackstageL2"]["password"]);

                MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from table_后台账号 where 后台ID='" + Cookie_UserName + "' and 后台密码='" + Cookie_Password + "' and 后台账号分级='L2' ", conn);
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄成功");

                    //if (DateTime.Parse(Cookie_TimeSave) > DateTime.Parse(DateTime.Now.AddHours(-1)))
                    //{

                    //}
                    //else
                    //{
                    //    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                    //    ClassLibrary1.ClassMessage.tizun("/管理/登入/登入.aspx");
                    //}

                }
                else
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                    弹回登入管理L2页();
                }
                conn.Close();
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                弹回登入管理L2页();
            }
        }

        public static bool 验证管理L2白名单IP(string 后台ID, string 获取要验证目标IP)
        {
            MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select 后台ID,后台白名单IP from table_后台白名单管理 where 后台ID=@后台ID and 后台白名单IP=@后台白名单IP", conn);

            cmd.Parameters.AddWithValue("@后台ID", 后台ID);
            cmd.Parameters.AddWithValue("@后台白名单IP", 获取要验证目标IP);

            MySqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "IP白名单验证成功!");
                conn.Close();
                return true;
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "IP白名单验证失败!");
                conn.Close();
                return false;
            }


        }

        //====================================================================================================

        //====================================================================================================
        public static bool 检查商户端cookie()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string 检查商户端cookie2()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["username"]);
                return Cookie_UserName;
            }
            else
            {
                弹回登入商户页();
                return null;

            }
        }

        public static void 弹回登入商户页()
        {
            ClassLibrary1.ClassMessage.tizun("/WebsiteMerchant/Login/登入.aspx");
        }

        //驗證帳號的方法  只需要在頁面調用這個方法就可以驗證cookie內帳號密碼 如果非法訪問和錯誤的密碼將會被遣送回登錄頁面
        public static void 验证账号商户端()
        {
            //獲取cookie
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"];
            //檢測cookie
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["username"]);
                //string Cookie_Password = System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["password"];
                string Cookie_Password = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["password"]);
                string 时间最后登录 = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["ginx"]);

                MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("select * from table_商户账号 where 商户ID='" + Cookie_UserName + "' and 商户密码='" + Cookie_Password + "' and 时间最后登入='" + 时间最后登录 + "' and 状态='启用' and 登入错误累计<6 ", conn);
                MySqlCommand cmd = new MySqlCommand("select * from table_商户账号 where 商户ID='" + Cookie_UserName + "' and 商户密码='" + Cookie_Password + "' and 状态='启用' and 登入错误累计<6 ", conn);
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄成功");

                }
                else
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                    弹回登入商户页();
                }
                conn.Close();
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                弹回登入商户页();
            }
        }
        
        public static bool 验证商户白名单IP(string 商户ID, string 获取要验证目标IP)
        {
#if DEBUG
            return true;
#else
            MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select 商户ID,商户白名单IP from table_商户白名单管理 where 商户ID=@商户ID and 商户白名单IP=@商户白名单IP", conn);

            cmd.Parameters.AddWithValue("@商户ID", 商户ID);
            cmd.Parameters.AddWithValue("@商户白名单IP", 获取要验证目标IP);

            MySqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "IP白名单验证成功!");
                conn.Close();
                return true;
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "IP白名单验证失败!");
                conn.Close();
                return false;
            }
#endif
        }

        //====================================================================================================

        //====================================================================================================
        public static bool 检查代理L1端cookie()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string 检查代理L1端cookie2()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"]["username"]);
                return Cookie_UserName;
            }
            else
            {
                弹回登入代理L1页();
                return null;

            }
        }

        public static void 弹回登入代理L1页()
        {
            ClassLibrary1.ClassMessage.tizun("/WebsiteAgent/L1/Login/登入.aspx");
        }

        //驗證帳號的方法  只需要在頁面調用這個方法就可以驗證cookie內帳號密碼 如果非法訪問和錯誤的密碼將會被遣送回登錄頁面
        public static void 验证账号代理L1端()
        {
            //獲取cookie
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"];
            //檢測cookie
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"]["username"]);
                //string Cookie_Password = System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"]["password"];
                string Cookie_Password = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"]["password"]);
                string 时间最后登录 = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL1"]["ginx"]);

                MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("select * from table_代理账号等级1 where 代理ID='" + Cookie_UserName + "' and 代理密码='" + Cookie_Password + "' and 时间最后登入='" + 时间最后登录 + "' and 状态='启用' and 登入错误累计<6 ", conn);
                MySqlCommand cmd = new MySqlCommand("select * from table_代理账号等级1 where 代理ID='" + Cookie_UserName + "' and 代理密码='" + Cookie_Password + "' and 状态='启用' and 登入错误累计<6 ", conn);
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄成功");

                }
                else
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                    弹回登入代理L1页();
                }
                conn.Close();
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                弹回登入代理L1页();
            }
        }
        //====================================================================================================

        //====================================================================================================
        public static bool 检查代理L2端cookie()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string 检查代理L2端cookie2()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.获得USERNAME(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"]["username"]);
                return Cookie_UserName;
            }
            else
            {
                弹回登入代理L2页();
                return null;
                
            }
        }

        public static void 弹回登入代理L2页()
        {
            ClassLibrary1.ClassMessage.tizun("/WebsiteAgent/L2/Login/登入.aspx");
        }

        //驗證帳號的方法  只需要在頁面調用這個方法就可以驗證cookie內帳號密碼 如果非法訪問和錯誤的密碼將會被遣送回登錄頁面
        public static void 验证账号代理L2端()
        {
            //獲取cookie
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"];
            //檢測cookie
            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"] != null)
            {
                string Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"]["username"]);
                //string Cookie_Password = System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"]["password"];
                string Cookie_Password = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"]["password"]);
                string 时间最后登录 = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameAgentL2"]["ginx"]);

                MySqlConnection conn = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1);
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("select * from table_代理账号等级2 where 代理ID='" + Cookie_UserName + "' and 代理密码='" + Cookie_Password + "' and 时间最后登入='" + 时间最后登录 + "' and 状态='启用' and 登入错误累计<6 ", conn);
                MySqlCommand cmd = new MySqlCommand("select * from table_代理账号等级2 where 代理ID='" + Cookie_UserName + "' and 代理密码='" + Cookie_Password + "' and 状态='启用' and 登入错误累计<6 ", conn);
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄成功");

                }
                else
                {
                    //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                    弹回登入代理L2页();
                }
                conn.Close();
            }
            else
            {
                //ClassLibrary1.ClassMessage.HinXi(Page, "登錄失效,重新登錄");
                弹回登入代理L2页();
            }
        }
        //====================================================================================================

        public static string cookie加密(string 要加密的)
        {
            var cookieText = Encoding.UTF8.GetBytes(要加密的);
            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "shnsINC20XXDEV"));

            return encryptedValue;
        }

        public static string cookie解密(string 要解密的)
        {
            var bytes = Convert.FromBase64String(要解密的);
            var output = MachineKey.Unprotect(bytes, "shnsINC20XXDEV");
            string result = Encoding.UTF8.GetString(output);

            return result;
        }

        public static string 获得USERNAME(string 用户名解密)
        {
            string 解密的用户名 = ClassLibrary1.ClassAccount.cookie解密(用户名解密);

            return 解密的用户名;
        }
        
        public static string 来源IP()
        {
            string ipaddress;
            ipaddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //Response.Write("IP Address : " + ipaddress);
            return ipaddress;
        }

        


    }
}
