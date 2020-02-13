using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClassMessage
    {
        /// <summary>
        /// 传入信息弹出来(服務器接收定義信息 然後客戶端彈出這個信息)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TEAWebMessage"></param>
        public static void HinXi(System.Web.UI.Page page, string TEAWebMessage)
        {

            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", "<script>alert('" + TEAWebMessage + "');</script>");
        }

        /// <summary>
        /// 自定义脚本信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void AlertLocation(System.Web.UI.Page page, string msg)
        {

            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", "<script>" + msg + "</script>");
        }

        /// <summary>
        /// 跳轉頁面(轉到頁面)
        /// </summary>
        /// <param name="tizundo"></param>
        public static void tizun(string tizundo)
        {
            System.Web.HttpContext.Current.Response.Redirect(tizundo);  //刷新頁面
        }
    }


}
