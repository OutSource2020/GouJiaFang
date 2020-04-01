using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sugar.Enties
{
    public class table_snapshot
    {
        public long? Id { get; set; }
        public string MerchantID { get; set; }
        public double? Balance { get; set; }
        public double? Reverse { get; set; }
        public double? Deposit { get; set; }
        public double? Withdraw { get; set; }
        public double? Diff { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}