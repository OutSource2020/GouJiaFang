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

                foreach (string id in accounts)
                {
                    dbClient.Ado.UseTran(() =>
                    {
                        var detail = dbClient.Queryable<table_商户明细余额>()
                            .Where(it => DateTime.Now >= it.时间创建.Value.AddDays(1) && it.商户ID == int.Parse(id))
                            .First();
                        if (detail == null)
                            return;
                        var reverse = dbClient.Queryable<table_商户明细提款>()
                            .Where(it => it.类型 == "冲正" && SqlFunc.DateIsSame(it.时间创建, DateTime.Now) && it.商户ID == id)
                            .Sum(it => it.交易金额);
                        var deposit = dbClient.Queryable<table_商户明细充值>()
                            .Where(it => it.充值类型 == "充值提款余额" && it.状态 == "成功" && it.商户ID == int.Parse(id))
                            .Sum(it => it.充值金额);
                        var withdraw = dbClient.Queryable<table_商户明细提款>()
                            .Where(it => it.类型 == "提款" && it.状态 == "成功" && SqlFunc.DateIsSame(it.时间创建, DateTime.Now) && it.商户ID == id)
                            .Sum(it => it.交易金额);

                        table_snapshot snapshot = new table_snapshot()
                        {
                            MerchantID = detail.商户ID.Value.ToString(),
                            Balance = Double.Parse(detail.交易后账户余额),
                            Reverse = reverse,
                            Deposit = deposit,
                            Withdraw = withdraw,
                            CreateTime = DateTime.Now
                        };
                        snapshot.Diff = snapshot.Balance + snapshot.Reverse + snapshot.Deposit - snapshot.Withdraw;
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
            while(true)
            {
                DateTime now = DateTime.Now;
                if (now.Hour == 23 && now.Minute == 58)
                    TakeSnapshot();
                else
                    Console.WriteLine("Hello World");
                Thread.Sleep(1000);
            }
#endif
        }
    }
}
