using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("table_后台账号")]
    public partial class table_后台账号
    {
           public table_后台账号(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string 后台ID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string 后台密码 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string keyga {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string 后台账号名称 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string 后台账号分级 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string 状态 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? 时间最后登入 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:current_timestamp()
           /// Nullable:True
           /// </summary>           
           public DateTime? 时间注册 {get;set;}

    }
}
