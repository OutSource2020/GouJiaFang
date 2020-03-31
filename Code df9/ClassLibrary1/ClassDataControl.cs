using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
////using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ClassLibrary1
{
    /// <summary>
    ///DB数据库操作
    /// </summary>
    public class ClassDataControl
    {
        public static string substr(string str, int num)
        {
            string strs;
            if (str.Length > num)
            {
                strs = str.Substring(0, num) + "...";


            }
            else
            {
                strs = str;

            }
            return strs;
        }

        public static string substr1(string str, int num)
        {
            string strs;
            if (str.Length > num)
            {
                strs = str.Substring(0, num);


            }
            else
            {
                strs = str;

            }
            return strs;
        }

        #if DEBUG
        public static string conStr1 = ("data source = 127.0.0.1; port=3306;charset= utf8; Initial Catalog = web; user id = root;  password = root; SslMode = none; Connect Timeout = 60; max pool size=10000");
#else
        public static string conStr1 = ("data source = 127.0.0.1; port=3306;charset= utf8; Initial Catalog = web; user id = huiju;  password = huiju666; SslMode = none; Connect Timeout = 60; max pool size=10000");
#endif

        public static MySqlConnection con1 = new MySqlConnection();
        public static MySqlCommand scmd1 = new MySqlCommand();


        public static void OpenConnection1()//打开数据库
        {
            if (con1.State == ConnectionState.Closed)
            {
                try
                {
                    con1.ConnectionString = conStr1;
                    scmd1.Connection = con1;
                    con1.Open();
                }
                catch (Exception err)
                {

                    throw new Exception(err.Message);
                }

            }
        }
        public static void CloseConnection1()//关闭数据库
        {
            if (con1.State == ConnectionState.Open)
            {
                try
                {

                    con1.Close();
                    con1.Dispose();
                    scmd1.Dispose();
                }
                catch (Exception err)
                {

                    throw new Exception(err.Message);
                }

            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlstr">写上你们的sql</param>
        public static int ExecuteSql(string sqlstr)
        {
            int result = 0;
            try
            {
                OpenConnection1();

                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                result = scmd1.ExecuteNonQuery();

                CloseConnection1();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
        }


#region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="coll">SqlParameters 集合</param>
        public static void ExecutePorcedure(string procName, MySqlParameter[] coll)
        {
            try
            {
                OpenConnection1();
                for (int i = 0; i < coll.Length; i++)
                {
                    scmd1.Parameters.Add(coll[i]);
                }
                scmd1.CommandType = CommandType.StoredProcedure;
                scmd1.CommandText = procName;
                scmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                scmd1.Parameters.Clear();
                CloseConnection1();
            }
        }
#endregion
#region 执行存储过程并返回数据集
        /// <summary>
        /// 执行存储过程并返回数据集
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="coll">SqlParameter集合</param>
        /// <param name="ds">DataSet </param>
        public static void ExecutePorcedure(string procName, MySqlParameter[] coll, ref DataSet ds)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                OpenConnection1();
                for (int i = 0; i < coll.Length; i++)
                {
                    scmd1.Parameters.Add(coll[i]);
                }
                scmd1.CommandType = CommandType.StoredProcedure;
                scmd1.CommandText = procName;
                da.SelectCommand = scmd1;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                scmd1.Parameters.Clear();
                CloseConnection1();
            }
        }
#endregion

#region 执行Sql查询语句并返回第一行的第一条记录,返回值为object 使用时需要拆箱操作 -> Unbox
        /// <summary>
        /// 执行Sql查询语句并返回第一行的第一条记录,返回值为object 使用时需要拆箱操作 -> Unbox
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>object 返回值 </returns>
        public static object ExecuteScalar(string sqlstr)
        {
            object obj = new object();
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                obj = scmd1.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
            return obj;
        }
#endregion

#region 执行Sql查询语句,同时进行事务处理
        /// <summary>
        /// 执行Sql查询语句,同时进行事务处理
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        public static void ExecuteSqlWithTransaction(string sqlstr)
        {
            MySqlTransaction trans;
            trans = con1.BeginTransaction();
            scmd1.Transaction = trans;
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                scmd1.ExecuteNonQuery();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                CloseConnection1();
            }
        }
#endregion

#region 返回指定Sql语句的SqlDataReader , 请注意 , 在使用后请关闭本对象 , 同时将自动调用closeConnection()来关闭数据库连接方法关闭数据库连接
        /// <summary>
        /// 返回指定Sql语句的SqlDataReader , 请注意 , 在使用后请关闭本对象 , 同时将自动调用closeConnection()来关闭数据库连接
        /// 方法关闭数据库连接
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>SqlDataReader对象</returns>
        public static MySqlDataReader dataReader(string sqlstr)
        {
            MySqlDataReader dr = null;
            try
            {
                OpenConnection1();
                scmd1.CommandText = sqlstr;
                scmd1.CommandType = CommandType.Text;
                dr = scmd1.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    dr.Close();
                    CloseConnection1();
                }
                catch
                {
                }
            }
            return dr;
        }
#endregion

#region (ref)返回指定Sql语句的SqlDataReader , 请注意 , 在使用后请关闭本对象 , 同时将自动调用closeConnection()来关闭数据库连接方法关闭数据库连接
        /// <summary>
        /// 返回指定Sql语句的SqlDataReader , 请注意 , 在使用后请关闭本对象 , 同时将自动调用closeConnection()来关闭数据库连接
        /// 方法关闭数据库连接
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <param name="dr">传入的ref DataReader 对象</param>
        public static void dataReader(string sqlstr, ref MySqlDataReader dr)
        {
            try
            {
                OpenConnection1();
                scmd1.CommandText = sqlstr;
                scmd1.CommandType = CommandType.Text;
                dr = scmd1.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    if (dr != null && !dr.IsClosed)
                        dr.Close();
                }
                catch
                {
                }
                finally
                {
                    CloseConnection1();
                }
            }
        }
#endregion
#region 返回指定Sql语句的DataSet
        /// <summary>
        /// 返回指定Sql语句的DataSet
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>DataSet</returns>
        public static DataSet dataSet(string sqlstr)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter();
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                da.SelectCommand = scmd1;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
            return ds;
        }




        //string table 为虚拟表 可以用 搜索的数据库表"name"
        //DataSet ds1 = DB.PagedataSet(sqlstr1, AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1),
        //    AspNetPager1.PageSize,"newlist");
        //    Repeater1.DataSource = ds1.Tables[0];
        public static DataSet PagedataSet(string sqlstr, int pageindex, int pagesize, string table)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter();
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                da.SelectCommand = scmd1;
                da.Fill(ds, pageindex, pagesize, table);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
            return ds;
        }

#endregion
#region (ref)返回指定Sql语句的DataSet
        /// <summary>
        /// 返回指定Sql语句的DataSet
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <param name="ds">传入的引用DataSet对象</param>
        public static void dataSet(string sqlstr, ref DataSet ds)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                da.SelectCommand = scmd1;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
        }
#endregion
#region 返回指定Sql语句的DataTable
        /// <summary>
        /// 返回指定Sql语句的DataTable
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>DataTable</returns>
        public static DataTable dataTable(string sqlstr)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable datatable = new DataTable();
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                da.SelectCommand = scmd1;
                da.Fill(datatable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
            return datatable;
        }
#endregion
#region 执行指定Sql语句,同时给传入DataTable进行赋值
        /// <summary>
        /// 执行指定Sql语句,同时给传入DataTable进行赋值
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <param name="dt">ref DataTable dt </param>
        public static void dataTable(string sqlstr, ref DataTable dt)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                da.SelectCommand = scmd1;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
        }
#endregion
#region 执行带参数存储过程并返回数据集合
        /// <summary>
        /// 执行带参数存储过程并返回数据集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">SqlParameterCollection 输入参数</param>
        /// <returns></returns>
        public static DataTable dataTable(string procName, MySqlParameterCollection parameters)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable datatable = new DataTable();
            try
            {
                OpenConnection1();
                scmd1.Parameters.Clear();
                scmd1.CommandType = CommandType.StoredProcedure;
                scmd1.CommandText = procName;
                foreach (MySqlParameter para in parameters)
                {
                    MySqlParameter p = (MySqlParameter)para;
                    scmd1.Parameters.Add(p);
                }
                da.SelectCommand = scmd1;
                da.Fill(datatable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
            return datatable;
        }
#endregion
#region 返回指定sql语句的 DataView
        public static DataView dataView(string sqlstr)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            try
            {
                OpenConnection1();
                scmd1.CommandType = CommandType.Text;
                scmd1.CommandText = sqlstr;
                da.SelectCommand = scmd1;
                da.Fill(ds);
                dv = ds.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection1();
            }
            return dv;
        }
#endregion





        /// 过滤html,js,css代码
        /// <summary>
        /// 过滤html,js,css代码
        /// </summary>
        /// <param name="html">参数传入</param>
        /// <returns></returns>
        public static string CheckStr(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script. *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script. *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe. *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=java script. (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = html.Replace("'", "＇");
            return html;
        }
        /// <summary>
        /// 传入用户输入判断是不是非法关键字
        /// </summary>
        /// <param name="sql_str">传入用户输入字符判断</param>
        /// <returns></returns>
        public static bool sql_immit(String sql_str)
        {//用#分割关键字
            string model_str = "'#and#exec#insert#select#delete#update#count#*#%#chr#mid#master#truncate#char#declare#;#or#-#+#,";

            string[] model_split_str = model_str.Split('#');

            for (int i = 0; i < model_split_str.Length; i++)
            {
                if (sql_str.IndexOf(model_split_str[i]) >= 0)
                {
                    //>=0说明有关键字 , 否则说明没有关键字
                    return true;

                }
            }
            return false;
        }
    }
}
