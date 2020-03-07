using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace web1
{
    public class DBClient
    {
        public void AddUpdateLog(string logInfo)
        {
            try
            {
                string rootPath = Path.Combine(HttpRuntime.AppDomainAppPath.ToString(), "Log\\");
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                File.AppendAllText(rootPath + "LOG_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "[" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "]  " + logInfo + "\r\n",
                        Encoding.Default);
            }
            catch
            {
            }
        }
        public SqlSugarClient GetClient()
        {

            SqlSugarClient db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = ClassLibrary1.ClassDataControl.conStr1,
                    DbType = DbType.MySql,//设置数据库类型
              IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
              InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                #if DEBUG
                Debug.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Debug.WriteLine("");
                #else
                AddUpdateLog(db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                #endif
            };
            return db;

        }

    }
}