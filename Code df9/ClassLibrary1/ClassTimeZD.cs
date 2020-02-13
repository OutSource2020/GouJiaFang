using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClassTimeZD
    {
        public static string 时间数据库mysql今天 = "WHERE TO_DAYS(时间创建) = TO_DAYS(NOW())";
        public static string 时间数据库mysql昨天 = "WHERE TO_DAYS(NOW()) - TO_DAYS(时间创建) = 1";
        public static string 时间数据库mysql7天 = "WHERE DATE_SUB(CURDATE(), INTERVAL 7 DAY) <= date(时间创建)";
        public static string 时间数据库mysql本周 = "WHERE YEARWEEK( date_format(时间创建,'%Y-%m-%d' ) ) = YEARWEEK( now() )";
        public static string 时间数据库mysql本月 = "WHERE DATE_FORMAT(时间创建, '%Y%m' ) = DATE_FORMAT( CURDATE( ) ,'%Y%m' )";
        //public static string 时间数据库mysql区间 = "where 时间创建 between '" + TextBox1.Text + "'  and '" + TextBox2.Text + "'";

        public static string 时分秒 = "HH:mm:ss:fffffff";//fffffff表示秒部分的七個最高有效數字; 也就是說，它代表日期和時間值中的百萬分之一秒。

        public static string 时分秒更多 = " 23:59:59:9999";

        //string 时间今天 = "WHERE TO_DAYS(时间创建) = TO_DAYS(NOW())";
        //string 时间昨天 = "WHERE TO_DAYS(NOW()) - TO_DAYS(时间创建) = 1";
        //string 时间7天 = "WHERE DATE_SUB(CURDATE(), INTERVAL 7 DAY) <= date(时间创建)";
        //string 时间本周 = "WHERE YEARWEEK( date_format(时间创建,'%Y-%m-%d' ) ) = YEARWEEK( now() )";
        //string 时间本月 = "WHERE DATE_FORMAT(时间创建, '%Y%m' ) = DATE_FORMAT( CURDATE( ) ,'%Y%m' )";
        ////string 时间区间 = "where 时间创建 between '" + TextBox1.Text + "'  and '" + TextBox2.Text + "'";

        ////string 时分秒 = "HH:mm:ss:fffffff";//fffffff表示秒部分的七個最高有效數字; 也就是說，它代表日期和時間值中的百萬分之一秒。
        //string 时分秒更多 = " 23:59:59:9999";


        public void SHIJIAN()
        {
            DateTime dt = DateTime.Now;
            string dtStr = dt.ToString();
            dt.ToString("yyyy-MM-dd HH:mm:ss.fff");//2005-11-5 11:22:33.444
            //dt.ToString("yyyy-MM-dd");//2005-11-5
            //string.Format("{0:d}", dt);//2005-11-5
            //string.Format("{0:D}", dt);//2005年11月5日
            //string RegisterTime = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
