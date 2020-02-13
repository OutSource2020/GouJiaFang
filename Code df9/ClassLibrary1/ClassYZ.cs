using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ClassLibrary1
{
    public class ClassYZ
    {
        //验证是否为数字或者小数
        public static bool IsNumber(string input)
        {
            double d = 0;
            if (double.TryParse(input, out d)) // TryParse返回true说明是数值
            {
                return true;
            }

            else // 不是数值
            {
                return false;
            }

        }

        #region 过滤特殊字符
        public static string SpecialCode(string s)
        {
            s = s.Replace(@"\", "");
            s = Regex.Replace(s, "[ \\[ \\] \\^ \\-_*×――(^)$%~!/@#$…&%￥—+=<>《》|!！??？:：•`·、。，；,.;\"‘’“”-]", "").ToUpper();
            return s;
        }
        #endregion


        public static string 过滤数字和小数(string 传来值)
        {
            string str = 传来值;
            /**  \\d+\\.?\\d*
            * \d 表示数字
            * + 表示前面的数字有一个或多个（至少出现一次）
            * \. 此处需要注意，. 表示任何原子，此处进行转义，表示单纯的 小数点
            * ? 表示0个或1个
            * * 表示0次或者多次
            */
            Regex r = new Regex("\\d+\\.?\\d*");
            bool ismatch = r.IsMatch(str);
            MatchCollection mc = r.Matches(str);

            string result = string.Empty;
            for (int i = 0; i < mc.Count; i++)
            {
                result += mc[i];//匹配结果是完整的数字，此处可以不做拼接的
            }
            //Console.WriteLine(result);

            return result;
        }

        private static string FilterChar(string input)
        {
            Regex r = new Regex("^[0-9]{1,}$"); //正则表达式 表示数字的范围 ^符号是开始，$是关闭
            StringBuilder sb = new StringBuilder();
            foreach (var item in input)
            {
                if (item >= 0x4e00 && item <= 0x9fbb)//汉字范围
                {
                    sb.Append(item);
                }

                if (Regex.IsMatch(item.ToString(), @"[A-Za-z0-9]"))
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

        public static bool 检查日期格式yyyyMMdd(string 传来待检查的参数)
        {
            string 待检查的参数 = 传来待检查的参数;//yyyy-MM-dd
            DateTime dt;
            if (DateTime.TryParseExact(待检查的参数, "yyyy-MM-dd", null, DateTimeStyles.None, out dt))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}





//using System.Text.RegularExpressions;

//匹配中文:[\u4e00-\u9fa5]

//英文字母:[a-zA-Z]

//数字:[0-9]

//匹配中文，英文字母和数字及_:
//^[\u4e00-\u9fa5_a-zA-Z0-9]+$

//同时判断输入长度:
//[\u4e00-\u9fa5_a-zA-Z0-9_]{4,10}

//^[\w\u4E00-\u9FA5\uF900-\uFA2D]*$1、一个正则表达式，只含有汉字、数字、字母、下划线不能以下划线开头和结尾:
//^(?!_)(?!.*? _$)[a-zA-Z0-9_\u4e00-\u9fa5]+$ 其中:
//^ 与字符串开始的地方匹配
//(?!_)不能以_开头
//(?!.*?_$)不能以_结尾
//[a - zA - Z0 - 9_\u4e00 - \u9fa5]+　　至少一个汉字、数字、字母、下划线
//$　　与字符串结束的地方匹配

//放在程序里前面加@，否则需要\\进行转义@"^(?!_)(?!.*?_$)[a-zA-Z0-9_\u4e00-\u9fa5]+$"
//（或者:@"^(?!_)\w*(?
//2、只含有汉字、数字、字母、下划线，下划线位置不限:
//^[a-zA-Z0-9_\u4e00-\u9fa5]+$

//3、由数字、26个英文字母或者下划线组成的字符串
//^\w+$

//4、2~4个汉字
//@"^[\u4E00-\u9FA5]{2,4}$";

//5、
//^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$

//用:(Abc)+ 来分析: XYZAbcAbcAbcXYZAbcAb

//XYZAbcAbcAbcXYZAbcAb6、
//[^\u4E00-\u9FA50-9a-zA-Z_]
//34555#5' -->34555#5'

//[\u4E00-\u9FA50-9a-zA-Z_] eiieng_89_ --->eiieng_89_
//_';'eiieng_88&*9_ -->_';'eiieng_88&*9_
//_';'eiieng_88_&*9_ -->_';'eiieng_88_&*9_

//public bool RegexName(string str)
//{
//    bool flag = Regex.IsMatch(str, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
//    return flag;
//}

//Regex reg = new Regex("^[a-zA-Z_0-9]+$");
//if(reg.IsMatch(s))
//{
//\\符合规则
//}
//else
//{
//\\存在非法字符
//}

//最长不得超过7个汉字，或14个字节(数字，字母和下划线)正则表达式
//^[\u4e00-\u9fa5]{1,7}$|^[\dA-Za-z_]{14}$