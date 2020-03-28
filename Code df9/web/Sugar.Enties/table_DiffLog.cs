using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sugar.Enties
{
    public class table_difflog
    {
        public long? Id { get; set; }
        public string OrderId { get; set; }
        public string MerchantID { get; set; }
        public double? Amount { get; set; }
        public double? OutTotal { get; set; }
        public double? EnableOutTotal { get; set; }
        public double? MerchantTotal { get; set; }
        public double? Pending { get; set; }
        public double? Diff { get; set; }
        public long? 后台处理批次ID组 { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
    }
}