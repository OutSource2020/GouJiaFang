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
    public class ClassLog2X
    {
        public static void 数据库插入数据(string 哪个表,string 写哪些,string 写这些)
        {
            string sql = ClassLibrary1.ClassDataControl.conStr1;
            using (MySqlConnection scon = new MySqlConnection(sql))
            {
                string str = "insert into "+哪个表+"("+写哪些+") values("+写这些+")";

                scon.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = scon;
                command.CommandText = str;
                int obj = command.ExecuteNonQuery();

                scon.Close();
            }
        }


        //public string void 数据库更新数据(string 哪个表,string 写内容,string 哪一条 )
        //{
        //    using (MySqlConnection con = new MySqlConnection(ClassLibrary1.ClassDataControl.conStr1))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand("UPDATE "+ 哪个表 + " SET "+写内容+" WHERE "+ 哪一条 + " ", con))
        //        {
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //}
    }
}
