using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class ClassSaveCode
    {

        string 时间商户充值今天 = " TO_DAYS(时间创建) = TO_DAYS(NOW())";
        string 时间商户充值昨天 = " TO_DAYS(NOW()) - TO_DAYS(时间创建) = 1";
        string 时间商户充值7天 = " DATE_SUB(CURDATE(), INTERVAL 7 DAY) <= date(时间创建)";
        string 时间商户充值本周 = " YEARWEEK( date_format(时间创建,'%Y-%m-%d' ) ) = YEARWEEK( now() )";
        string 时间商户充值本月 = " DATE_FORMAT(时间创建, '%Y%m' ) = DATE_FORMAT( CURDATE( ) ,'%Y%m' )";


        string 时间商户提款今天 = " TO_DAYS(时间创建) = TO_DAYS(NOW())";
        string 时间商户提款昨天 = " TO_DAYS(NOW()) - TO_DAYS(时间创建) = 1";
        string 时间商户提款7天 = " DATE_SUB(CURDATE(), INTERVAL 7 DAY) <= date(时间创建)";
        string 时间商户提款本周 = " YEARWEEK( date_format(时间创建,'%Y-%m-%d' ) ) = YEARWEEK( now() )";
        string 时间商户提款本月 = " DATE_FORMAT(时间创建, '%Y%m' ) = DATE_FORMAT( CURDATE( ) ,'%Y%m' )";


    }
}
