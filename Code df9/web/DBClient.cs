using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace web1
{
    public class DBClient
    {

        // string conStr = "data source = 127.0.0.1; port=3306;charset= utf8; Initial Catalog = web; user id = root;  password = ldqZ070C8CxTZXXf; SslMode = none; Connect Timeout = 60; max pool size=10000";
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
            #if DEBUG
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Debug.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Debug.WriteLine("");
            };
            #endif
            return db;

        }

    }
}