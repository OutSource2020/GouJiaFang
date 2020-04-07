using SqlSugar;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using web1;

namespace Snapshot
{
    class Program
    {
        static void TakeSnapshot()
        {
            using (SqlSugarClient dbClient = new DBClient().GetClient())
            {
                dbClient.Ado.UseTran(() => { });
                List<string> accounts = null;
                dbClient.Ado.UseTran(() =>
                {
                    accounts = dbClient.Queryable<table_商户账号>().Select(it => it.商户ID).ToList();
                });

                if (accounts == null)
                    return;

                DateTime current = DateTime.Now;
                DateTime dateTime = new DateTime(current.Year, current.Month, current.Day, 23, 59, 0);

                foreach (string id in accounts)
                {
                    dbClient.Ado.UseTran(() =>
                    {
                        var balance = dbClient.Queryable<table_商户账号>().Where(it => it.商户ID == id).Select(it => it.提款余额).First();
                        var reverse = dbClient.Queryable<table_商户明细提款>()
                            .Where(it => it.类型 == "冲正" && SqlFunc.DateIsSame(it.时间创建, current) && it.商户ID == id)
                            .Sum(it => it.交易金额);
                        var deposit = dbClient.Queryable<table_商户明细充值>()
                            .Where(it => it.充值类型 == "充值提款余额" && it.状态 == "成功" && SqlFunc.DateIsSame(it.时间创建, current)  && it.商户ID == int.Parse(id))
                            .Sum(it => it.充值金额);
                        var withdraw = dbClient.Queryable<table_商户明细提款>()
                            .Where(it => it.类型 == "提款" && it.状态 == "成功" && SqlFunc.DateIsSame(it.时间创建, current) && it.商户ID == id)
                            .Sum(it => it.交易金额);

                        var tableId = dbClient.Queryable<table_snapshot>()
                        .Where(it => it.MerchantID == id)
                        .OrderBy(it => it.Id, OrderByType.Desc)
                        .Select(it => it.Id).First();

                        if (tableId.HasValue)
                        {
                            dbClient.Updateable<table_snapshot>()
                            .SetColumns(it => it.Balance == balance.Value)
                            .SetColumns(it => it.Diff == balance.Value + it.Reverse + it.Deposit - it.Withdraw)
                            .Where(it => it.Id == tableId.Value).ExecuteCommand();
                        }

                        table_snapshot snapshot = new table_snapshot()
                        {
                            MerchantID = id,
                            Reverse = reverse,
                            Deposit = deposit,
                            Withdraw = withdraw,
                            CreateTime = DateTime.Now
                        };
                        dbClient.Insertable(snapshot).ExecuteCommand();
                    });
                }
            }
        }

        static void Main(string[] args)
        {
#if DEBUG
            TakeSnapshot();
#else
            bool finished = true;
            while(true)
            {
                DateTime now = DateTime.Now;
                if (now.Hour == 23 && now.Minute == 58 && finished)
                {
                    TakeSnapshot();
                    finished = false;
                }
                else if (now.Hour == 0)
                    finished = true;
                Thread.Sleep(1000);
            }
#endif
        }
    }
}
